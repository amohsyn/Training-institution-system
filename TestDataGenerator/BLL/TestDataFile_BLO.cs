using GApp.Core.Context;
using GApp.DAL;
using GApp.Exceptions;
using GApp.UnitTest.TestData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestDataGenerator.Model;
using TestDataGenerator.TestData;
using TrainingIS.DAL;

namespace TestDataGenerator.BLL
{
    public class TestDataFile_BLO
    {
        public List<TestData_File> Find_All(string Filter)
        {
            List<TestData_File> testData_Files = new List<TestData_File>();

            TrainingISModel trainingISModel = new TrainingISModel();
            DataGenerator dataGenerator = new DataGenerator(trainingISModel);
            Dictionary<Type, string> Data_Files = dataGenerator.Get_TestData_Files();
            var Query = Data_Files.Select(d => new TestData_File
            {
                EntityName = d.Key.Name,
                EntityType = d.Key,
                FilePath = d.Value
            })
            .OrderBy(o => o.EntityName)
            .ToList();
            

            if (!string.IsNullOrEmpty(Filter))
            {
                Query = Query.Where(e => e.EntityName.ToUpper().Contains(Filter.ToUpper())).ToList();
            }



            return Query;
        }

        public Type Find_TestDataFactory_By_EntityType(Type entityType)
        {
            TrainingISModel trainingISModel = new TrainingISModel();
            DataGenerator dataGenerator = new DataGenerator(trainingISModel);
            Dictionary<Type, Type> TestDataTypes = dataGenerator.Get_TestData_Types();
            return TestDataTypes.Where(d => d.Key.Name == entityType.Name).FirstOrDefault().Value;
        }

        /// <summary>
        /// Insert Test Data if not yet inserted
        /// </summary>
        /// <param name="entityType"></param>
        public void Insert_Entity_Data(Type entityType)
        {
            Type TestDataFactory_Type = this.Find_TestDataFactory_By_EntityType(entityType);
            var param_s = new List<Object>();
            param_s.Add(new UnitOfWork<TrainingISModel>());
            param_s.Add(new GAppContext("Root"));
            var param = param_s.ToArray();
            var TestDataInstance = Activator.CreateInstance(TestDataFactory_Type, param);
            TestDataFactory_Type.GetMethod("Insert_Test_Data").Invoke(TestDataInstance, null);

        }

        /// <summary>
        /// Update Test Data
        /// </summary>
        /// <param name="entityType"></param>
        public void Update_Entity_Data(Type entityType)
        {
            Type TestDataFactory_Type = this.Find_TestDataFactory_By_EntityType(entityType);
            var param_s = new List<Object>();
            param_s.Add(new UnitOfWork<TrainingISModel>());
            param_s.Add(new GAppContext("Root"));
            var param = param_s.ToArray();
            var TestDataInstance = Activator.CreateInstance(TestDataFactory_Type, param);
            try
            {
                TestDataFactory_Type.GetMethod("Update_Test_Data").Invoke(TestDataInstance, null);
            }
            catch (TargetInvocationException e)
            {
                
                throw new GAppException(e.Message);
            }
            

        }

        public List<Object> GetData(TestData_File testData_File)
        {

            Type TestDataFactory_Type = this.Find_TestDataFactory_By_EntityType(testData_File.EntityType);
            var param_s = new List<Object>();
            param_s.Add(new UnitOfWork<TrainingISModel>());
            param_s.Add(new GAppContext("Root"));
            var param = param_s.ToArray();
            var TestDataInstance = Activator.CreateInstance(TestDataFactory_Type, param);
            IList return_value = TestDataFactory_Type.GetMethod("Get_TestData").Invoke(TestDataInstance, null) as IList;
            var result_ls = return_value.Cast<object>().ToList();
            return result_ls;
        }

        public void PrepareData(Type entityType)
        {
            Type TestDataFactory_Type = this.Find_TestDataFactory_By_EntityType(entityType);
            var param_s = new List<Object>();
            param_s.Add(new UnitOfWork<TrainingISModel>());
            param_s.Add(new GAppContext("Root"));
            var param = param_s.ToArray();
            var TestDataInstance = Activator.CreateInstance(TestDataFactory_Type, param);
            try
            {
                TestDataFactory_Type.GetMethod("Prepare_Data_Aftter_Insert").Invoke(TestDataInstance, null);
            }
            catch (TargetInvocationException e)
            {

                throw new GAppException(e.Message);
            }
        }
    }
}
