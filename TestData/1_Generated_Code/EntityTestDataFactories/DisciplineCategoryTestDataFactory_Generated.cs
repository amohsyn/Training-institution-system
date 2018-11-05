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
    public class BaseDisciplineCategoryTestDataFactory : EntityTestData<DisciplineCategory>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new DisciplineCategoryBLO(UnitOfWork, GAppContext);
        }

        public BaseDisciplineCategoryTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<DisciplineCategory> Load_Data_From_ExcelFile()
        {
            List<DisciplineCategory> Data = null;

            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/DisciplineCategory.xlsx";

            if (File.Exists(FileName))
            {
                Data = new List<DisciplineCategory>();

                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                Data = (this.BLO as DisciplineCategoryBLO).Convert_DataTable_to_List(firstTable);
            }
            return Data;
        }

        protected override List<DisciplineCategory> Insert_Or_Update_ExcelFile_TestData(out bool is_Insert_Or_Update)
        {
            is_Insert_Or_Update = false;
            List<DisciplineCategory> Data = new List<DisciplineCategory>();

            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/DisciplineCategory.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/DisciplineCategory.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported

				// Delete Repport_File if exist
                if (!File.Exists(Repport_File))
                {
					ImportReport importReport = (this.BLO as DisciplineCategoryBLO).Import(firstTable, FileName);
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
								"DisciplineCategory",
							importReport.Number_of_inserted_erros_rows, 
							importReport.Number_of_updated_erros_rows
                           
							);
						throw new GAppException(msg_ex);
					}

					// Convert Data Table to Data
					Data = importReport.ImportedObjects.Cast<DisciplineCategory>().ToList();
					is_Insert_Or_Update = true;
                }

				

            }
            return Data;
        }


		/// <summary>
        /// Find the first DisciplineCategory instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual DisciplineCategory CreateOrLouadFirstDisciplineCategory()
        {
            DisciplineCategoryBLO disciplinecategoryBLO = new DisciplineCategoryBLO(UnitOfWork,GAppContext);
           
			DisciplineCategory entity = null;
            if (disciplinecategoryBLO.FindAll()?.Count > 0)
                entity = disciplinecategoryBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp DisciplineCategory for Test
                entity = this.CreateValideDisciplineCategoryInstance();
                disciplinecategoryBLO.Save(entity);
            }
            return entity;
        }

        public virtual DisciplineCategory CreateValideDisciplineCategoryInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            DisciplineCategory  Valide_DisciplineCategory = this._Fixture.Create<DisciplineCategory>();
            Valide_DisciplineCategory.Id = 0;
            // Many to One 
            //   
            // One to Many
            //
            return Valide_DisciplineCategory;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide DisciplineCategory can't exist</returns>
        public virtual DisciplineCategory CreateInValideDisciplineCategoryInstance()
        {
            DisciplineCategory disciplinecategory = this.CreateValideDisciplineCategoryInstance();
             
			// Required   
 
			disciplinecategory.Code = null;
 
			disciplinecategory.Name = null;
            //Unique
			var existant_DisciplineCategory = this.CreateOrLouadFirstDisciplineCategory();
			disciplinecategory.Reference = existant_DisciplineCategory.Reference;
 
            return disciplinecategory;
        }


		public virtual DisciplineCategory CreateInValideDisciplineCategoryInstance_ForEdit()
        {
            DisciplineCategory disciplinecategory = this.CreateOrLouadFirstDisciplineCategory();
			// Required   
 
			disciplinecategory.Code = null;
 
			disciplinecategory.Name = null;
            //Unique
			var existant_DisciplineCategory = this.CreateOrLouadFirstDisciplineCategory();
			disciplinecategory.Reference = existant_DisciplineCategory.Reference;
            return disciplinecategory;
        }

		public override void Generate_Excel_File(List<DisciplineCategory> generated_Data)
        {
            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/DisciplineCategory.xlsx";

            var DataTeble = (this.BLO as DisciplineCategoryBLO).Convert_to_DataTable(generated_Data);
            (this.BLO as DisciplineCategoryBLO).ExcelDAO.Save(DataTeble, FileName);
        }
    }

	public partial class DisciplineCategoryTestDataFactory : BaseDisciplineCategoryTestDataFactory{
	
		public DisciplineCategoryTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
