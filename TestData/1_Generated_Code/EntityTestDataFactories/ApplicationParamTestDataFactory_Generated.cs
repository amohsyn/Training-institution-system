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
    public class BaseApplicationParamTestDataFactory : EntityTestData<ApplicationParam>
    {
		public string Entity_CRUD_Test_Reference { set; get; } 
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new ApplicationParamBLO(UnitOfWork, GAppContext);
			Entity_CRUD_Test_Reference  = "ApplicationParam_CRUD_Test";
        }

        public BaseApplicationParamTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

 
		protected override List<ApplicationParam> Load_Data_From_ExcelFile()
        {
            List<ApplicationParam> Data = null;

            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/ApplicationParam.xlsx";

            if (File.Exists(FileName))
            {
                Data = new List<ApplicationParam>();

                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                Data = (this.BLO as ApplicationParamBLO).Convert_DataTable_to_List(firstTable);
            }
            return Data;
        }

        protected override List<ApplicationParam> Insert_Or_Update_ExcelFile_TestData(out bool is_Insert_Or_Update)
        {
            is_Insert_Or_Update = false;
            List<ApplicationParam> Data = new List<ApplicationParam>();

            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/ApplicationParam.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/ApplicationParam.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported

				// Delete Repport_File if exist
                if (!File.Exists(Repport_File))
                {
					ImportReport importReport = (this.BLO as ApplicationParamBLO).Import(firstTable, FileName);
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
								"ApplicationParam",
							importReport.Number_of_inserted_erros_rows, 
							importReport.Number_of_updated_erros_rows
                           
							);
						throw new GAppException(msg_ex);
					}

					// Convert Data Table to Data
					Data = importReport.ImportedObjects.Cast<ApplicationParam>().ToList();
					is_Insert_Or_Update = true;
                }

				

            }
            return Data;
        }


		/// <summary>
        /// Find the first ApplicationParam instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual ApplicationParam CreateOrLouadFirstApplicationParam()
        {
            ApplicationParamBLO applicationparamBLO = new ApplicationParamBLO(UnitOfWork,GAppContext);
           
			ApplicationParam entity = null;
            if (applicationparamBLO.FindAll()?.Count > 0)
                entity = applicationparamBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp ApplicationParam for Test
                entity = this.CreateValideApplicationParamInstance();
                applicationparamBLO.Save(entity);
            }
            return entity;
        }

		public virtual ApplicationParam Create_CRUD_ApplicationParam_Test_Instance()
        {
			ApplicationParam ApplicationParam = this.CreateValideApplicationParamInstance();
            ApplicationParam.Reference = this.Entity_CRUD_Test_Reference;
            return ApplicationParam;
        }

        public virtual ApplicationParam CreateValideApplicationParamInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            ApplicationParam  Valide_ApplicationParam = this._Fixture.Create<ApplicationParam>();
            Valide_ApplicationParam.Id = 0;
            // Many to One 
            //   
            // One to Many
            //
            return Valide_ApplicationParam;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide ApplicationParam can't exist</returns>
        public virtual ApplicationParam CreateInValideApplicationParamInstance()
        {
            ApplicationParam applicationparam = this.CreateValideApplicationParamInstance();
             
			// Required   
 
			applicationparam.Code = null;
            //Unique
			var existant_ApplicationParam = this.CreateOrLouadFirstApplicationParam();
			applicationparam.Reference = existant_ApplicationParam.Reference;
 
            return applicationparam;
        }


		public virtual ApplicationParam CreateInValideApplicationParamInstance_ForEdit()
        {
            ApplicationParam applicationparam = this.CreateOrLouadFirstApplicationParam();
			// Required   
 
			applicationparam.Code = null;
            //Unique
			var existant_ApplicationParam = this.CreateOrLouadFirstApplicationParam();
			applicationparam.Reference = existant_ApplicationParam.Reference;
            return applicationparam;
        }

		public override void Generate_Excel_File(List<ApplicationParam> generated_Data)
        {
            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/ApplicationParam.xlsx";

            var DataTeble = (this.BLO as ApplicationParamBLO).Convert_to_DataTable(generated_Data);
            (this.BLO as ApplicationParamBLO).ExcelDAO.Save(DataTeble, FileName);
        }
    }

	public partial class ApplicationParamTestDataFactory : BaseApplicationParamTestDataFactory{
	
		public ApplicationParamTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
