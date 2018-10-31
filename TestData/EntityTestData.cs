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
    /// <summary>
    /// Base Class of TestData Management
    /// it Generate TestData and Load TestData from Excel Files from Data Directory in current Projet
    /// the Directory "Data" it Loaded only from Developpement and Test machine.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class EntityTestData<T> : IEntityTestData<T> where T : BaseEntity
    {
        protected Fixture _Fixture = null;

        protected UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        protected GAppContext GAppContext { set; get; }
        public BaseBLO<T> BLO { set; get; }

        protected List<T> Data;
        protected Dictionary<T, DataErrorsTypes> Data_with_errors;

        #region Constructor
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
        #endregion



        #region Get All Data

        /// <summary>
        /// Generate Test Data
        /// </summary>
        /// <returns></returns>
        protected virtual List<T> Generate_TestData()
        {
            return null;
        }

        /// <summary>
        /// Load Data From Excel File
        /// </summary>
        /// <returns></returns>
        protected virtual List<T> Load_Data_From_ExcelFile()
        {
            return null;
        }

        /// <summary>
        /// Get All Test Data
        /// From Generated Test Data and Data in Excel Files
        /// </summary>
        /// <returns></returns>
        public virtual List<T> Get_TestData()
        {
            if (this.Data != null)
                return this.Data;
            else
            {
                this.Data = new List<T>();

                // Generated Data
                var Generated_Data = this.Generate_TestData();
                if(Generated_Data != null) Data.AddRange(Generated_Data);

                // Exce Data
                Boolean is_Insert_Or_Update;
                var Load_FromExcelFile_Data = this.Load_Data_From_ExcelFile();
                if(Load_FromExcelFile_Data != null) Data.AddRange(Load_FromExcelFile_Data);

                return this.Data;
            }
             
        }

       
        #endregion

        #region Insert or Update Generated and Excel Data
        /// <summary>
        /// Insert or Update Test Data from Excel File and Generated TestData
        /// </summary>
        public void Insert_Or_Update_Test_Data()
        {
            Boolean is_Insert_Or_Update;
            // The Generated Test Data is inserted when the loaded Test id is Inserted
            this.Insert_Or_Update_ExcelFile_TestData(out is_Insert_Or_Update);

            if (is_Insert_Or_Update)
                this.Insert_Or_Update_Generated_Test_Data();
        }

        /// <summary>
        /// Insert or Update the Generated TestData
        /// </summary>
        private void Insert_Or_Update_Generated_Test_Data()
        {
            var Data = this.Generate_TestData();
            if (Data == null) return;

            foreach (var item in Data)
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
                    long Id = entity.Id;
                    item.CopyProperties(entity);
                    entity.Id = Id;
                    this.BLO.Save(entity);
                }

            }
        }
      

        /// <summary>
        /// Insert or Update the TestData from Excel File
        /// </summary>
        /// <param name="is_Insert_Or_Update">Out params : Indicate is the TestData is Inserted or Updated in DataBase</param>
        /// <returns>The TestData from Excel File</returns>
        protected virtual List<T> Insert_Or_Update_ExcelFile_TestData(out Boolean is_Insert_Or_Update)
        {
            is_Insert_Or_Update = false;
            return null;
        }
        #endregion

      

     

        #region File Management
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
        #endregion

        //protected virtual bool is_TestData_Exist()
        //{
        //    foreach (var item in this.Get_TestData())
        //    {
        //        var item_db = this.BLO.FindBaseEntityByReference(item.Reference);
        //        if (item_db == null) return false;
        //    }
        //    return true;
        //}

        //public virtual void Insert_Test_Data_If_Not_Exist()
        //{
        //    if (!this.is_TestData_Exist())
        //    {
        //        foreach (var item in this.Get_TestData())
        //        {
        //            var entity = this.BLO.FindBaseEntityByReference(item.Reference);
        //            if (entity == null)
        //            {
        //                // Insert
        //                this.BLO.Save(item);
        //            }

        //        }
        //    }
        //}
    }
}
