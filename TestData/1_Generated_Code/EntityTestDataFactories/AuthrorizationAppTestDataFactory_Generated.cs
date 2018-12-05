using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using AutoFixture;
using TrainingIS.BLL;
using TrainingIS.DAL;
using System.ComponentModel.DataAnnotations;
using GApp.WebApp.Manager.Views;
using GApp.DAL;
using GApp.Entities;
using GApp.Core.Context;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;
using GApp.UnitTest.TestData;
using GApp.UnitTest.TestData.Enums;
using System.IO;
using System.Data;
using GApp.DAL.ReadExcel;
using ClosedXML.Excel;
using GApp.Exceptions;
namespace TestData
{
    public class BaseAuthrorizationAppTestDataFactory : EntityTestData<AuthrorizationApp>
    {
		public string Entity_CRUD_Test_Reference { set; get; } 
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new AuthrorizationAppBLO(UnitOfWork, GAppContext);
			Entity_CRUD_Test_Reference  = "AuthrorizationApp_CRUD_Test";
        }

        public BaseAuthrorizationAppTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

 
		protected override List<AuthrorizationApp> Load_Data_From_ExcelFile()
        {
            List<AuthrorizationApp> Data = null;

            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/AuthrorizationApp.xlsx";

            if (File.Exists(FileName))
            {
                Data = new List<AuthrorizationApp>();

                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                Data = (this.BLO as AuthrorizationAppBLO).Convert_DataTable_to_List(firstTable);
            }
            return Data;
        }

        protected override List<AuthrorizationApp> Insert_Or_Update_ExcelFile_TestData(out bool is_Insert_Or_Update)
        {
            is_Insert_Or_Update = false;
            List<AuthrorizationApp> Data = new List<AuthrorizationApp>();

            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/AuthrorizationApp.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/AuthrorizationApp.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported

				// Delete Repport_File if exist
                if (!File.Exists(Repport_File))
                {
					ImportReport importReport = (this.BLO as AuthrorizationAppBLO).Import(firstTable, FileName);
					// Save ExcelRepport file to Server
					DataSet DataSet_report = importReport.get_DataSet_Report();
					using (XLWorkbook wb = new XLWorkbook())
					{
						wb.Worksheets.Add(DataSet_report);
						wb.SaveAs(Repport_File);
					}

						// Throw Exceltion if there is error in Import
					if( importReport.Number_of_inserted_erros_rows > 0 || importReport.Number_of_updated_erros_rows > 0)
					{
						string msg_ex = string.Format(" {0} : There are {1} error of Inserts and {2} of Update",
								"AuthrorizationApp",
							importReport.Number_of_inserted_erros_rows, 
							importReport.Number_of_updated_erros_rows
                           
							);
						throw new GAppException(msg_ex);
					}

					// Convert Data Table to Data
					Data = importReport.ImportedObjects.Cast<AuthrorizationApp>().ToList();
					is_Insert_Or_Update = true;
                }

				

            }
            return Data;
        }


		/// <summary>
        /// Find the first AuthrorizationApp instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual AuthrorizationApp CreateOrLouadFirstAuthrorizationApp()
        {
            AuthrorizationAppBLO authrorizationappBLO = new AuthrorizationAppBLO(UnitOfWork,GAppContext);
           
			AuthrorizationApp entity = null;
            if (authrorizationappBLO.FindAll()?.Count > 0)
                entity = authrorizationappBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp AuthrorizationApp for Test
                entity = this.CreateValideAuthrorizationAppInstance();
                authrorizationappBLO.Save(entity);
            }
            return entity;
        }

		public virtual AuthrorizationApp Create_CRUD_AuthrorizationApp_Test_Instance()
        {
			AuthrorizationApp AuthrorizationApp = this.CreateValideAuthrorizationAppInstance();
            AuthrorizationApp.Reference = this.Entity_CRUD_Test_Reference;
            return AuthrorizationApp;
        }

        public virtual AuthrorizationApp CreateValideAuthrorizationAppInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            AuthrorizationApp  Valide_AuthrorizationApp = this._Fixture.Create<AuthrorizationApp>();
            Valide_AuthrorizationApp.Id = 0;
            // Many to One 
            //   
			// RoleApp
			var RoleApp = new RoleAppTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstRoleApp();
            Valide_AuthrorizationApp.RoleApp = RoleApp;
						 Valide_AuthrorizationApp.RoleAppId = RoleApp.Id;
			           
			// ControllerApp
			var ControllerApp = new ControllerAppTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstControllerApp();
            Valide_AuthrorizationApp.ControllerApp = ControllerApp;
						 Valide_AuthrorizationApp.ControllerAppId = ControllerApp.Id;
			           
            // One to Many
            //
			Valide_AuthrorizationApp.ActionControllerApps = null;
            return Valide_AuthrorizationApp;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide AuthrorizationApp can't exist</returns>
        public virtual AuthrorizationApp CreateInValideAuthrorizationAppInstance()
        {
            AuthrorizationApp authrorizationapp = this.CreateValideAuthrorizationAppInstance();
             
			// Required   
 
			authrorizationapp.RoleAppId = 0;
 
			authrorizationapp.ControllerAppId = 0;
 
			authrorizationapp.isAllAction = false;
            //Unique
			var existant_AuthrorizationApp = this.CreateOrLouadFirstAuthrorizationApp();
			authrorizationapp.Reference = existant_AuthrorizationApp.Reference;
 
            return authrorizationapp;
        }


		public virtual AuthrorizationApp CreateInValideAuthrorizationAppInstance_ForEdit()
        {
            AuthrorizationApp authrorizationapp = this.CreateOrLouadFirstAuthrorizationApp();
			// Required   
 
			authrorizationapp.RoleAppId = 0;
 
			authrorizationapp.ControllerAppId = 0;
 
			authrorizationapp.isAllAction = false;
            //Unique
			var existant_AuthrorizationApp = this.CreateOrLouadFirstAuthrorizationApp();
			authrorizationapp.Reference = existant_AuthrorizationApp.Reference;
            return authrorizationapp;
        }

		public override void Generate_Excel_File(List<AuthrorizationApp> generated_Data)
        {
            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/AuthrorizationApp.xlsx";

            var DataTeble = (this.BLO as AuthrorizationAppBLO).Convert_to_DataTable(generated_Data);
            (this.BLO as AuthrorizationAppBLO).ExcelDAO.Save(DataTeble, FileName);
        }
    }

	public partial class AuthrorizationAppTestDataFactory : BaseAuthrorizationAppTestDataFactory{
	
		public AuthrorizationAppTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
