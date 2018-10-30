using AutoFixture;
using GApp.BLL;
using GApp.Core.Context;
using GApp.DAL;
using GApp.Entities;
using GApp.UnitTest.TestData;
using GApp.UnitTest.TestData.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;

namespace TestData
{
    public abstract class  EntityTestData<T> : IEntityTestData<T> where T : BaseEntity
    {
        public BaseBLO<T> BLO { set; get; }

        protected Fixture _Fixture = null;
        protected List<T> Data;
        protected Dictionary<T, DataErrorsTypes> Data_with_errors;

        protected UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        protected GAppContext GAppContext { set; get; }
 

        public EntityTestData(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            this.Constructor(UnitOfWork, GAppContext);
        }

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="UnitOfWork"></param>
        /// <param name="GAppContext"></param>
        protected virtual void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            this.UnitOfWork = UnitOfWork;
            this.GAppContext = GAppContext;

            // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        /// <summary>
        /// Get All Test Data
        /// </summary>
        /// <returns></returns>
        public virtual List<T> Get_TestData()
        {
            return Data ?? (Data = Generate_TestData());
        }
        protected virtual List<T> Generate_TestData()
        {
            return null;
        }

        public virtual void Insert_Test_Data_If_Not_Exist()
        {
            if (!this.is_TestData_Exist())
            {
                foreach (var item in this.Get_TestData())
                {
                    var entity = this.BLO.FindBaseEntityByReference(item.Reference);
                    if (entity == null)
                    {
                        // Insert
                        this.BLO.Save(item);
                    }

                }
            }
        }

        public void Insert_Or_Update_Test_Data()
        {
            if (!this.is_TestData_Exist())
            {
                foreach (var item in this.Get_TestData())
                {
                    var entity = this.BLO.FindBaseEntityByReference(item.Reference);
                    if (entity == null)
                    {
                        // Insert
                        this.BLO.Save(item);
                    }
                    else
                    {
                        // Update
                        item.CopyProperties(entity);
                        this.BLO.Save(entity);
                    }

                }
            }
        }

        protected virtual bool is_TestData_Exist()
        {
            foreach (var item in this.Get_TestData())
            {
                var item_db = this.BLO.FindBaseEntityByReference(item.Reference);
                if (item_db == null) return false;
            }
            return true;
        }

        protected string Get_Solution_Path()
        {
            string root_path = System.AppDomain.CurrentDomain.BaseDirectory;
            root_path += "/../../../TestData/";
            return root_path;
        }
        /// <summary>
        ///  Create the Dicrectory ~/Content/Files if not exist
        ///  it is used to save all applications files
        /// </summary>
        protected virtual void Create_TestData_Files_Directory_If_Not_Exist()
        {

            string TestData_Directory = this.Get_Solution_Path() + "Data/";
            if (!Directory.Exists(TestData_Directory))
            {
                Directory.CreateDirectory(TestData_Directory);


            }
            string Repports_Directory = this.Get_Solution_Path() + "Data/Repports";
            if (!Directory.Exists(Repports_Directory))
            {
                Directory.CreateDirectory(Repports_Directory);
            }
        }
    }
}
