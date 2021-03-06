﻿using System;
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
    public class BaseControllerAppTestDataFactory : EntityTestData<ControllerApp>
    {
		public string Entity_CRUD_Test_Reference { set; get; } 
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new ControllerAppBLO(UnitOfWork, GAppContext);
			Entity_CRUD_Test_Reference  = "ControllerApp_CRUD_Test";
        }

        public BaseControllerAppTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

 
		protected override List<ControllerApp> Load_Data_From_ExcelFile()
        {
            List<ControllerApp> Data = null;

            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/ControllerApp.xlsx";

            if (File.Exists(FileName))
            {
                Data = new List<ControllerApp>();

                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                Data = (this.BLO as ControllerAppBLO).Convert_DataTable_to_List(firstTable);
            }
            return Data;
        }

        protected override List<ControllerApp> Insert_Or_Update_ExcelFile_TestData(out bool is_Insert_Or_Update)
        {
            is_Insert_Or_Update = false;
            List<ControllerApp> Data = new List<ControllerApp>();

            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/ControllerApp.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/ControllerApp.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported

				// Delete Repport_File if exist
                if (!File.Exists(Repport_File))
                {
					ImportReport importReport = (this.BLO as ControllerAppBLO).Import(firstTable, FileName);
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
								"ControllerApp",
							importReport.Number_of_inserted_erros_rows, 
							importReport.Number_of_updated_erros_rows
                           
							);
						throw new GAppException(msg_ex);
					}

					// Convert Data Table to Data
					Data = importReport.ImportedObjects.Cast<ControllerApp>().ToList();
					is_Insert_Or_Update = true;
                }

				

            }
            return Data;
        }


		/// <summary>
        /// Find the first ControllerApp instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual ControllerApp CreateOrLouadFirstControllerApp()
        {
            ControllerAppBLO controllerappBLO = new ControllerAppBLO(UnitOfWork,GAppContext);
           
			ControllerApp entity = null;
            if (controllerappBLO.FindAll()?.Count > 0)
                entity = controllerappBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp ControllerApp for Test
                entity = this.CreateValideControllerAppInstance();
                controllerappBLO.Save(entity);
            }
            return entity;
        }

		public virtual ControllerApp Create_CRUD_ControllerApp_Test_Instance()
        {
			ControllerApp ControllerApp = this.CreateValideControllerAppInstance();
            ControllerApp.Reference = this.Entity_CRUD_Test_Reference;
            return ControllerApp;
        }

        public virtual ControllerApp CreateValideControllerAppInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            ControllerApp  Valide_ControllerApp = this._Fixture.Create<ControllerApp>();
            Valide_ControllerApp.Id = 0;
            // Many to One 
            //   
            // One to Many
            //
            return Valide_ControllerApp;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide ControllerApp can't exist</returns>
        public virtual ControllerApp CreateInValideControllerAppInstance()
        {
            ControllerApp controllerapp = this.CreateValideControllerAppInstance();
             
			// Required   
 
			controllerapp.Code = null;
 
			controllerapp.Name = null;
            //Unique
			var existant_ControllerApp = this.CreateOrLouadFirstControllerApp();
			controllerapp.Reference = existant_ControllerApp.Reference;
 
            return controllerapp;
        }


		public virtual ControllerApp CreateInValideControllerAppInstance_ForEdit()
        {
            ControllerApp controllerapp = this.CreateOrLouadFirstControllerApp();
			// Required   
 
			controllerapp.Code = null;
 
			controllerapp.Name = null;
            //Unique
			var existant_ControllerApp = this.CreateOrLouadFirstControllerApp();
			controllerapp.Reference = existant_ControllerApp.Reference;
            return controllerapp;
        }

		public override void Generate_Excel_File(List<ControllerApp> generated_Data)
        {
            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/ControllerApp.xlsx";

            var DataTeble = (this.BLO as ControllerAppBLO).Convert_to_DataTable(generated_Data);
            (this.BLO as ControllerAppBLO).ExcelDAO.Save(DataTeble, FileName);
        }
    }

	public partial class ControllerAppTestDataFactory : BaseControllerAppTestDataFactory{
	
		public ControllerAppTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
