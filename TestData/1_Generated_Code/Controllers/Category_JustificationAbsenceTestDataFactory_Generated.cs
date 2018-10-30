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
    public class BaseCategory_JustificationAbsenceTestDataFactory : EntityTestData<Category_JustificationAbsence>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new Category_JustificationAbsenceBLO(UnitOfWork, GAppContext);
        }

        public BaseCategory_JustificationAbsenceTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<Category_JustificationAbsence> Generate_TestData()
        {
            List<Category_JustificationAbsence> Data = new List<Category_JustificationAbsence>();
 
            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Category_JustificationAbsence.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/Category_JustificationAbsence.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported
                if (File.Exists(Repport_File))
                {
                    ImportReport importReport = (this.BLO as Category_JustificationAbsenceBLO).Import(firstTable, FileName);
                    // Save ExcelRepport file to Server
                    DataSet DataSet_report = importReport.get_DataSet_Report();
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(DataSet_report);
                        wb.SaveAs(Repport_File);
                    }
                    // Convert Data Table to Data
                    Data = importReport.ImportedObjects.Cast<Category_JustificationAbsence>().ToList();
                }
                else
                {
                    Data = (this.BLO as Category_JustificationAbsenceBLO).Convert_DataTable_to_List(firstTable);
                }
            }
            return Data;
        }
	
		/// <summary>
        /// Find the first Category_JustificationAbsence instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Category_JustificationAbsence CreateOrLouadFirstCategory_JustificationAbsence()
        {
            Category_JustificationAbsenceBLO category_justificationabsenceBLO = new Category_JustificationAbsenceBLO(UnitOfWork,GAppContext);
           
			Category_JustificationAbsence entity = null;
            if (category_justificationabsenceBLO.FindAll()?.Count > 0)
                entity = category_justificationabsenceBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Category_JustificationAbsence for Test
                entity = this.CreateValideCategory_JustificationAbsenceInstance();
                category_justificationabsenceBLO.Save(entity);
            }
            return entity;
        }

        public virtual Category_JustificationAbsence CreateValideCategory_JustificationAbsenceInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            Category_JustificationAbsence  Valide_Category_JustificationAbsence = this._Fixture.Create<Category_JustificationAbsence>();
            Valide_Category_JustificationAbsence.Id = 0;
            // Many to One 
            //   
            // One to Many
            //
            return Valide_Category_JustificationAbsence;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Category_JustificationAbsence can't exist</returns>
        public virtual Category_JustificationAbsence CreateInValideCategory_JustificationAbsenceInstance()
        {
            Category_JustificationAbsence category_justificationabsence = this.CreateValideCategory_JustificationAbsenceInstance();
             
			// Required   
 
			category_justificationabsence.Name = null;
            //Unique
			var existant_Category_JustificationAbsence = this.CreateOrLouadFirstCategory_JustificationAbsence();
			category_justificationabsence.Reference = existant_Category_JustificationAbsence.Reference;
 
            return category_justificationabsence;
        }


		public virtual Category_JustificationAbsence CreateInValideCategory_JustificationAbsenceInstance_ForEdit()
        {
            Category_JustificationAbsence category_justificationabsence = this.CreateOrLouadFirstCategory_JustificationAbsence();
			// Required   
 
			category_justificationabsence.Name = null;
            //Unique
			var existant_Category_JustificationAbsence = this.CreateOrLouadFirstCategory_JustificationAbsence();
			category_justificationabsence.Reference = existant_Category_JustificationAbsence.Reference;
            return category_justificationabsence;
        }
    }

	public partial class Category_JustificationAbsenceTestDataFactory : BaseCategory_JustificationAbsenceTestDataFactory{
	
		public Category_JustificationAbsenceTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
