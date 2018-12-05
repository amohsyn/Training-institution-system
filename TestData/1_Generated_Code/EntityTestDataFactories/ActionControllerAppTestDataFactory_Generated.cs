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
    public class BaseActionControllerAppTestDataFactory : EntityTestData<ActionControllerApp>
    {
		public string Entity_CRUD_Test_Reference { set; get; } 
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new ActionControllerAppBLO(UnitOfWork, GAppContext);
			Entity_CRUD_Test_Reference  = "ActionControllerApp_CRUD_Test";
        }

        public BaseActionControllerAppTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

 
		protected override List<ActionControllerApp> Load_Data_From_ExcelFile()
        {
            List<ActionControllerApp> Data = null;

            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/ActionControllerApp.xlsx";

            if (File.Exists(FileName))
            {
                Data = new List<ActionControllerApp>();

                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                Data = (this.BLO as ActionControllerAppBLO).Convert_DataTable_to_List(firstTable);
            }
            return Data;
        }

        protected override List<ActionControllerApp> Insert_Or_Update_ExcelFile_TestData(out bool is_Insert_Or_Update)
        {
            is_Insert_Or_Update = false;
            List<ActionControllerApp> Data = new List<ActionControllerApp>();

            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/ActionControllerApp.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/ActionControllerApp.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported

				// Delete Repport_File if exist
                if (!File.Exists(Repport_File))
                {
					ImportReport importReport = (this.BLO as ActionControllerAppBLO).Import(firstTable, FileName);
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
								"ActionControllerApp",
							importReport.Number_of_inserted_erros_rows, 
							importReport.Number_of_updated_erros_rows
                           
							);
						throw new GAppException(msg_ex);
					}

					// Convert Data Table to Data
					Data = importReport.ImportedObjects.Cast<ActionControllerApp>().ToList();
					is_Insert_Or_Update = true;
                }

				

            }
            return Data;
        }


		/// <summary>
        /// Find the first ActionControllerApp instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual ActionControllerApp CreateOrLouadFirstActionControllerApp()
        {
            ActionControllerAppBLO actioncontrollerappBLO = new ActionControllerAppBLO(UnitOfWork,GAppContext);
           
			ActionControllerApp entity = null;
            if (actioncontrollerappBLO.FindAll()?.Count > 0)
                entity = actioncontrollerappBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp ActionControllerApp for Test
                entity = this.CreateValideActionControllerAppInstance();
                actioncontrollerappBLO.Save(entity);
            }
            return entity;
        }

		public virtual ActionControllerApp Create_CRUD_ActionControllerApp_Test_Instance()
        {
			ActionControllerApp ActionControllerApp = this.CreateValideActionControllerAppInstance();
            ActionControllerApp.Reference = this.Entity_CRUD_Test_Reference;
            return ActionControllerApp;
        }

        public virtual ActionControllerApp CreateValideActionControllerAppInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            ActionControllerApp  Valide_ActionControllerApp = this._Fixture.Create<ActionControllerApp>();
            Valide_ActionControllerApp.Id = 0;
            // Many to One 
            //   
			// ControllerApp
			var ControllerApp = new ControllerAppTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstControllerApp();
            Valide_ActionControllerApp.ControllerApp = ControllerApp;
						 Valide_ActionControllerApp.ControllerAppId = ControllerApp.Id;
			           
            // One to Many
            //
			Valide_ActionControllerApp.AuthrorizationApps = null;
            return Valide_ActionControllerApp;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide ActionControllerApp can't exist</returns>
        public virtual ActionControllerApp CreateInValideActionControllerAppInstance()
        {
            ActionControllerApp actioncontrollerapp = this.CreateValideActionControllerAppInstance();
             
			// Required   
 
			actioncontrollerapp.Code = null;
 
			actioncontrollerapp.Name = null;
 
			actioncontrollerapp.ControllerAppId = 0;
            //Unique
			var existant_ActionControllerApp = this.CreateOrLouadFirstActionControllerApp();
			actioncontrollerapp.Reference = existant_ActionControllerApp.Reference;
 
            return actioncontrollerapp;
        }


		public virtual ActionControllerApp CreateInValideActionControllerAppInstance_ForEdit()
        {
            ActionControllerApp actioncontrollerapp = this.CreateOrLouadFirstActionControllerApp();
			// Required   
 
			actioncontrollerapp.Code = null;
 
			actioncontrollerapp.Name = null;
 
			actioncontrollerapp.ControllerAppId = 0;
            //Unique
			var existant_ActionControllerApp = this.CreateOrLouadFirstActionControllerApp();
			actioncontrollerapp.Reference = existant_ActionControllerApp.Reference;
            return actioncontrollerapp;
        }

		public override void Generate_Excel_File(List<ActionControllerApp> generated_Data)
        {
            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/ActionControllerApp.xlsx";

            var DataTeble = (this.BLO as ActionControllerAppBLO).Convert_to_DataTable(generated_Data);
            (this.BLO as ActionControllerAppBLO).ExcelDAO.Save(DataTeble, FileName);
        }
    }

	public partial class ActionControllerAppTestDataFactory : BaseActionControllerAppTestDataFactory{
	
		public ActionControllerAppTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
