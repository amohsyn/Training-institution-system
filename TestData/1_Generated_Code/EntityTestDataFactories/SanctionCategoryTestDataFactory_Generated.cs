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
    public class BaseSanctionCategoryTestDataFactory : EntityTestData<SanctionCategory>
    {
		public string Entity_CRUD_Test_Reference { set; get; } 
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new SanctionCategoryBLO(UnitOfWork, GAppContext);
			Entity_CRUD_Test_Reference  = "SanctionCategory_CRUD_Test";
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

				// Delete Repport_File if exist
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

						// Throw Exceltion if there is error in Import
					if( importReport.Number_of_inserted_erros_rows > 0 || importReport.Number_of_updated_erros_rows > 0)
					{
						string msg_ex = string.Format(" {0} : There are {1} error of Inserts and {2} of Update",
								"SanctionCategory",
							importReport.Number_of_inserted_erros_rows, 
							importReport.Number_of_updated_erros_rows
                           
							);
						throw new GAppException(msg_ex);
					}

					// Convert Data Table to Data
					Data = importReport.ImportedObjects.Cast<SanctionCategory>().ToList();
					is_Insert_Or_Update = true;
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

		public virtual SanctionCategory Create_CRUD_SanctionCategory_Test_Instance()
        {
			SanctionCategory SanctionCategory = this.CreateValideSanctionCategoryInstance();
            SanctionCategory.Reference = this.Entity_CRUD_Test_Reference;
            return SanctionCategory;
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

		public override void Generate_Excel_File(List<SanctionCategory> generated_Data)
        {
            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/SanctionCategory.xlsx";

            var DataTeble = (this.BLO as SanctionCategoryBLO).Convert_to_DataTable(generated_Data);
            (this.BLO as SanctionCategoryBLO).ExcelDAO.Save(DataTeble, FileName);
        }
    }

	public partial class SanctionCategoryTestDataFactory : BaseSanctionCategoryTestDataFactory{
	
		public SanctionCategoryTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
