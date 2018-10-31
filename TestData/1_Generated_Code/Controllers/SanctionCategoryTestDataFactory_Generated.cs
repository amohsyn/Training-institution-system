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
    public class BaseSanctionCategoryTestDataFactory : EntityTestData<SanctionCategory>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new SanctionCategoryBLO(UnitOfWork, GAppContext);
        }

        public BaseSanctionCategoryTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<SanctionCategory> Load_Data_From_ExcelFile()
        {
            List<SanctionCategory> Data = null;

            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/SanctionCategory.xlsx";

            if (File.Exists(FileName))
            {
                Data = new List<SanctionCategory>();

                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                Data = (this.BLO as SanctionCategoryBLO).Convert_DataTable_to_List(firstTable);
            }
            return Data;
        }

        protected override List<SanctionCategory> Insert_Or_Update_ExcelFile_TestData(out bool is_Insert_Or_Update)
        {
            is_Insert_Or_Update = false;
            List<SanctionCategory> Data = new List<SanctionCategory>();

            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/SanctionCategory.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/SanctionCategory.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported
                if (!File.Exists(Repport_File))
                {
                   
                    ImportReport importReport = (this.BLO as SanctionCategoryBLO).Import(firstTable, FileName);
                    // Save ExcelRepport file to Server
                    DataSet DataSet_report = importReport.get_DataSet_Report();
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(DataSet_report);
                        wb.SaveAs(Repport_File);
                    }
                    // Convert Data Table to Data
                    Data = importReport.ImportedObjects.Cast<SanctionCategory>().ToList();
                    is_Insert_Or_Update = true;
                }
                else
                {
                    Data = (this.BLO as SanctionCategoryBLO).Convert_DataTable_to_List(firstTable);
                }
            }
            return Data;
        }


		/// <summary>
        /// Find the first SanctionCategory instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual SanctionCategory CreateOrLouadFirstSanctionCategory()
        {
            SanctionCategoryBLO sanctioncategoryBLO = new SanctionCategoryBLO(UnitOfWork,GAppContext);
           
			SanctionCategory entity = null;
            if (sanctioncategoryBLO.FindAll()?.Count > 0)
                entity = sanctioncategoryBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp SanctionCategory for Test
                entity = this.CreateValideSanctionCategoryInstance();
                sanctioncategoryBLO.Save(entity);
            }
            return entity;
        }

        public virtual SanctionCategory CreateValideSanctionCategoryInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            SanctionCategory  Valide_SanctionCategory = this._Fixture.Create<SanctionCategory>();
            Valide_SanctionCategory.Id = 0;
            // Many to One 
            //   
			// DisciplineCategory
			var DisciplineCategory = new DisciplineCategoryTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstDisciplineCategory();
            Valide_SanctionCategory.DisciplineCategory = DisciplineCategory;
						 Valide_SanctionCategory.DisciplineCategoryId = DisciplineCategory.Id;
			           
            // One to Many
            //
            return Valide_SanctionCategory;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide SanctionCategory can't exist</returns>
        public virtual SanctionCategory CreateInValideSanctionCategoryInstance()
        {
            SanctionCategory sanctioncategory = this.CreateValideSanctionCategoryInstance();
             
			// Required   
 
			sanctioncategory.DisciplineCategoryId = 0;
 
			sanctioncategory.Name = null;
 
			sanctioncategory.Code = null;
            //Unique
			var existant_SanctionCategory = this.CreateOrLouadFirstSanctionCategory();
			sanctioncategory.Reference = existant_SanctionCategory.Reference;
 
            return sanctioncategory;
        }


		public virtual SanctionCategory CreateInValideSanctionCategoryInstance_ForEdit()
        {
            SanctionCategory sanctioncategory = this.CreateOrLouadFirstSanctionCategory();
			// Required   
 
			sanctioncategory.DisciplineCategoryId = 0;
 
			sanctioncategory.Name = null;
 
			sanctioncategory.Code = null;
            //Unique
			var existant_SanctionCategory = this.CreateOrLouadFirstSanctionCategory();
			sanctioncategory.Reference = existant_SanctionCategory.Reference;
            return sanctioncategory;
        }
    }

	public partial class SanctionCategoryTestDataFactory : BaseSanctionCategoryTestDataFactory{
	
		public SanctionCategoryTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
