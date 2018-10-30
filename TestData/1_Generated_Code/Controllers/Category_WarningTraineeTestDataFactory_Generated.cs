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

		protected override List<Category_WarningTrainee> Generate_TestData()
        {
            List<Category_WarningTrainee> Data = new List<Category_WarningTrainee>();
 
            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Category_WarningTrainee_TestData.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/Category_WarningTrainee_TestData.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported
                if (File.Exists(Repport_File))
                {
                    ImportReport importReport = (this.BLO as Category_WarningTraineeBLO).Import(firstTable, FileName);
                    // Save ExcelRepport file to Server
                    DataSet DataSet_report = importReport.get_DataSet_Report();
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(DataSet_report);
                        wb.SaveAs(Repport_File);
                    }
                    // Convert Data Table to Data
                    Data = importReport.ImportedObjects.Cast<Category_WarningTrainee>().ToList();
                }
                else
                {
                    Data = (this.BLO as Category_WarningTraineeBLO).Convert_DataTable_to_List(firstTable);
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
    }

	public partial class Category_WarningTraineeTestDataFactory : BaseCategory_WarningTraineeTestDataFactory{
	
		public Category_WarningTraineeTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
