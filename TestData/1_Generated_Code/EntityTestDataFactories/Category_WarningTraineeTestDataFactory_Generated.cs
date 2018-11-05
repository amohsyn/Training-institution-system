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
    public class BaseCategory_WarningTraineeTestDataFactory : EntityTestData<Category_WarningTrainee>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new Category_WarningTraineeBLO(UnitOfWork, GAppContext);
        }

        public BaseCategory_WarningTraineeTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<Category_WarningTrainee> Load_Data_From_ExcelFile()
        {
            List<Category_WarningTrainee> Data = null;

            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Category_WarningTrainee.xlsx";

            if (File.Exists(FileName))
            {
                Data = new List<Category_WarningTrainee>();

                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                Data = (this.BLO as Category_WarningTraineeBLO).Convert_DataTable_to_List(firstTable);
            }
            return Data;
        }

        protected override List<Category_WarningTrainee> Insert_Or_Update_ExcelFile_TestData(out bool is_Insert_Or_Update)
        {
            is_Insert_Or_Update = false;
            List<Category_WarningTrainee> Data = new List<Category_WarningTrainee>();

            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Category_WarningTrainee.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/Category_WarningTrainee.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported

				// Delete Repport_File if exist
                if (!File.Exists(Repport_File))
                {
					ImportReport importReport = (this.BLO as Category_WarningTraineeBLO).Import(firstTable, FileName);
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
								"Category_WarningTrainee",
							importReport.Number_of_inserted_erros_rows, 
							importReport.Number_of_updated_erros_rows
                           
							);
						throw new GAppException(msg_ex);
					}

					// Convert Data Table to Data
					Data = importReport.ImportedObjects.Cast<Category_WarningTrainee>().ToList();
					is_Insert_Or_Update = true;
                }

				

            }
            return Data;
        }


		/// <summary>
        /// Find the first Category_WarningTrainee instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Category_WarningTrainee CreateOrLouadFirstCategory_WarningTrainee()
        {
            Category_WarningTraineeBLO category_warningtraineeBLO = new Category_WarningTraineeBLO(UnitOfWork,GAppContext);
           
			Category_WarningTrainee entity = null;
            if (category_warningtraineeBLO.FindAll()?.Count > 0)
                entity = category_warningtraineeBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Category_WarningTrainee for Test
                entity = this.CreateValideCategory_WarningTraineeInstance();
                category_warningtraineeBLO.Save(entity);
            }
            return entity;
        }

        public virtual Category_WarningTrainee CreateValideCategory_WarningTraineeInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            Category_WarningTrainee  Valide_Category_WarningTrainee = this._Fixture.Create<Category_WarningTrainee>();
            Valide_Category_WarningTrainee.Id = 0;
            // Many to One 
            //   
            // One to Many
            //
            return Valide_Category_WarningTrainee;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Category_WarningTrainee can't exist</returns>
        public virtual Category_WarningTrainee CreateInValideCategory_WarningTraineeInstance()
        {
            Category_WarningTrainee category_warningtrainee = this.CreateValideCategory_WarningTraineeInstance();
             
			// Required   
 
			category_warningtrainee.Name = null;
            //Unique
			var existant_Category_WarningTrainee = this.CreateOrLouadFirstCategory_WarningTrainee();
			category_warningtrainee.Reference = existant_Category_WarningTrainee.Reference;
 
            return category_warningtrainee;
        }


		public virtual Category_WarningTrainee CreateInValideCategory_WarningTraineeInstance_ForEdit()
        {
            Category_WarningTrainee category_warningtrainee = this.CreateOrLouadFirstCategory_WarningTrainee();
			// Required   
 
			category_warningtrainee.Name = null;
            //Unique
			var existant_Category_WarningTrainee = this.CreateOrLouadFirstCategory_WarningTrainee();
			category_warningtrainee.Reference = existant_Category_WarningTrainee.Reference;
            return category_warningtrainee;
        }

		public override void Generate_Excel_File(List<Category_WarningTrainee> generated_Data)
        {
            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Category_WarningTrainee.xlsx";

            var DataTeble = (this.BLO as Category_WarningTraineeBLO).Convert_to_DataTable(generated_Data);
            (this.BLO as Category_WarningTraineeBLO).ExcelDAO.Save(DataTeble, FileName);
        }
    }

	public partial class Category_WarningTraineeTestDataFactory : BaseCategory_WarningTraineeTestDataFactory{
	
		public Category_WarningTraineeTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
