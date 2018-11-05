using GApp.Core.Context;
using GApp.DAL;
using GApp.UnitTest.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDataGenerator.Model;
using TestDataGenerator.TestData;
using TrainingIS.DAL;

namespace TestDataGenerator.BLL
{
    public class TestDataFile_BLO
    {
        public List<TestData_File> Find_All()
        {
            List<TestData_File> testData_Files = new List<TestData_File>();

            TrainingISModel trainingISModel = new TrainingISModel();
            DataGenerator dataGenerator = new DataGenerator(trainingISModel);
            Dictionary<Type, string> Data_Files = dataGenerator.Get_TestData_Files();
            return Data_Files.Select(d => new TestData_File {
                EntityName = d.Key.Name,
                EntityType = d.Key,
                FilePath = d.Value })

                .OrderBy(o => o.EntityName)
                .ToList();
        }

        public Type Find_TestDataFactory_By_EntityType(Type entityType)
        {
            TrainingISModel trainingISModel = new TrainingISModel();
            DataGenerator dataGenerator = new DataGenerator(trainingISModel);
            Dictionary<Type, Type> TestDataTypes = dataGenerator.Get_TestData_Types();
            return TestDataTypes.Where(d => d.Key.Name == entityType.Name).FirstOrDefault().Value;
        }

        public void Update_Entity_Date(Type entityType)
        {
            Type TestDataFactory_Type = this.Find_TestDataFactory_By_EntityType(entityType);
            var param_s = new List<Object>();
            param_s.Add(new UnitOfWork<TrainingISModel>());
            param_s.Add(new GAppContext("Root"));
            var param = param_s.ToArray();
            var TestDataInstance = Activator.CreateInstance(TestDataFactory_Type, param);
            TestDataFactory_Type.GetMethod("Insert_Or_Update_Test_Data").Invoke(TestDataInstance, null);

        }
    }
}
