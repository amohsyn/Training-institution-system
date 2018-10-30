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

namespace TestData
{
    public class BaseAuthrorizationAppTestDataFactory : EntityTestData<AuthrorizationApp>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new AuthrorizationAppBLO(UnitOfWork, GAppContext);
        }

        public BaseAuthrorizationAppTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<AuthrorizationApp> Generate_TestData()
        {
            List<AuthrorizationApp> Data = new List<AuthrorizationApp>();
 
            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/AuthrorizationApp_TestData.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/AuthrorizationApp_TestData.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported
                if (File.Exists(Repport_File))
                {
                    ImportReport importReport = (this.BLO as AuthrorizationAppBLO).Import(firstTable, FileName);
                    // Save ExcelRepport file to Server
                    DataSet DataSet_report = importReport.get_DataSet_Report();
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(DataSet_report);
                        wb.SaveAs(Repport_File);
                    }
                    // Convert Data Table to Data
                    Data = importReport.ImportedObjects.Cast<AuthrorizationApp>().ToList();
                }
                else
                {
                    Data = (this.BLO as AuthrorizationAppBLO).Convert_DataTable_to_List(firstTable);
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
    }

	public partial class AuthrorizationAppTestDataFactory : BaseAuthrorizationAppTestDataFactory{
	
		public AuthrorizationAppTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
