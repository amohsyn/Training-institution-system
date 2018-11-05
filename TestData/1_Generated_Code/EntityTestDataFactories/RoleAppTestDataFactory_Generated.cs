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
    public class BaseRoleAppTestDataFactory : EntityTestData<RoleApp>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new RoleAppBLO(UnitOfWork, GAppContext);
        }

        public BaseRoleAppTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<RoleApp> Load_Data_From_ExcelFile()
        {
            List<RoleApp> Data = null;

            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/RoleApp.xlsx";

            if (File.Exists(FileName))
            {
                Data = new List<RoleApp>();

                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                Data = (this.BLO as RoleAppBLO).Convert_DataTable_to_List(firstTable);
            }
            return Data;
        }

        protected override List<RoleApp> Insert_Or_Update_ExcelFile_TestData(out bool is_Insert_Or_Update)
        {
            is_Insert_Or_Update = false;
            List<RoleApp> Data = new List<RoleApp>();

            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/RoleApp.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/RoleApp.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported

				// Delete Repport_File if exist
                if (!File.Exists(Repport_File))
                {
					ImportReport importReport = (this.BLO as RoleAppBLO).Import(firstTable, FileName);
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
								"RoleApp",
							importReport.Number_of_inserted_erros_rows, 
							importReport.Number_of_updated_erros_rows
                           
							);
						throw new GAppException(msg_ex);
					}

					// Convert Data Table to Data
					Data = importReport.ImportedObjects.Cast<RoleApp>().ToList();
					is_Insert_Or_Update = true;
                }

				

            }
            return Data;
        }


		/// <summary>
        /// Find the first RoleApp instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual RoleApp CreateOrLouadFirstRoleApp()
        {
            RoleAppBLO roleappBLO = new RoleAppBLO(UnitOfWork,GAppContext);
           
			RoleApp entity = null;
            if (roleappBLO.FindAll()?.Count > 0)
                entity = roleappBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp RoleApp for Test
                entity = this.CreateValideRoleAppInstance();
                roleappBLO.Save(entity);
            }
            return entity;
        }

        public virtual RoleApp CreateValideRoleAppInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            RoleApp  Valide_RoleApp = this._Fixture.Create<RoleApp>();
            Valide_RoleApp.Id = 0;
            // Many to One 
            //   
            // One to Many
            //
            return Valide_RoleApp;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide RoleApp can't exist</returns>
        public virtual RoleApp CreateInValideRoleAppInstance()
        {
            RoleApp roleapp = this.CreateValideRoleAppInstance();
             
			// Required   
 
			roleapp.Code = null;
            //Unique
			var existant_RoleApp = this.CreateOrLouadFirstRoleApp();
			roleapp.Reference = existant_RoleApp.Reference;
 
            return roleapp;
        }


		public virtual RoleApp CreateInValideRoleAppInstance_ForEdit()
        {
            RoleApp roleapp = this.CreateOrLouadFirstRoleApp();
			// Required   
 
			roleapp.Code = null;
            //Unique
			var existant_RoleApp = this.CreateOrLouadFirstRoleApp();
			roleapp.Reference = existant_RoleApp.Reference;
            return roleapp;
        }

		public override void Generate_Excel_File(List<RoleApp> generated_Data)
        {
            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/RoleApp.xlsx";

            var DataTeble = (this.BLO as RoleAppBLO).Convert_to_DataTable(generated_Data);
            (this.BLO as RoleAppBLO).ExcelDAO.Save(DataTeble, FileName);
        }
    }

	public partial class RoleAppTestDataFactory : BaseRoleAppTestDataFactory{
	
		public RoleAppTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
