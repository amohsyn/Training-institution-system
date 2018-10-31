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
    public class BaseYearStudyTestDataFactory : EntityTestData<YearStudy>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new YearStudyBLO(UnitOfWork, GAppContext);
        }

        public BaseYearStudyTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<YearStudy> Load_Data_From_ExcelFile()
        {
            List<YearStudy> Data = null;

            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/YearStudy.xlsx";

            if (File.Exists(FileName))
            {
                Data = new List<YearStudy>();

                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                Data = (this.BLO as YearStudyBLO).Convert_DataTable_to_List(firstTable);
            }
            return Data;
        }

        protected override List<YearStudy> Insert_Or_Update_ExcelFile_TestData(out bool is_Insert_Or_Update)
        {
            is_Insert_Or_Update = false;
            List<YearStudy> Data = new List<YearStudy>();

            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/YearStudy.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/YearStudy.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported
                if (!File.Exists(Repport_File))
                {
                   
                    ImportReport importReport = (this.BLO as YearStudyBLO).Import(firstTable, FileName);
                    // Save ExcelRepport file to Server
                    DataSet DataSet_report = importReport.get_DataSet_Report();
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(DataSet_report);
                        wb.SaveAs(Repport_File);
                    }
                    // Convert Data Table to Data
                    Data = importReport.ImportedObjects.Cast<YearStudy>().ToList();
                    is_Insert_Or_Update = true;
                }
                else
                {
                    Data = (this.BLO as YearStudyBLO).Convert_DataTable_to_List(firstTable);
                }
            }
            return Data;
        }


		/// <summary>
        /// Find the first YearStudy instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual YearStudy CreateOrLouadFirstYearStudy()
        {
            YearStudyBLO yearstudyBLO = new YearStudyBLO(UnitOfWork,GAppContext);
           
			YearStudy entity = null;
            if (yearstudyBLO.FindAll()?.Count > 0)
                entity = yearstudyBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp YearStudy for Test
                entity = this.CreateValideYearStudyInstance();
                yearstudyBLO.Save(entity);
            }
            return entity;
        }

        public virtual YearStudy CreateValideYearStudyInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            YearStudy  Valide_YearStudy = this._Fixture.Create<YearStudy>();
            Valide_YearStudy.Id = 0;
            // Many to One 
            //   
            // One to Many
            //
            return Valide_YearStudy;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide YearStudy can't exist</returns>
        public virtual YearStudy CreateInValideYearStudyInstance()
        {
            YearStudy yearstudy = this.CreateValideYearStudyInstance();
             
			// Required   
 
			yearstudy.Code = null;
 
			yearstudy.Name = null;
            //Unique
			var existant_YearStudy = this.CreateOrLouadFirstYearStudy();
			yearstudy.Code = existant_YearStudy.Code;
			yearstudy.Reference = existant_YearStudy.Reference;
 
            return yearstudy;
        }


		public virtual YearStudy CreateInValideYearStudyInstance_ForEdit()
        {
            YearStudy yearstudy = this.CreateOrLouadFirstYearStudy();
			// Required   
 
			yearstudy.Code = null;
 
			yearstudy.Name = null;
            //Unique
			var existant_YearStudy = this.CreateOrLouadFirstYearStudy();
			yearstudy.Code = existant_YearStudy.Code;
			yearstudy.Reference = existant_YearStudy.Reference;
            return yearstudy;
        }
    }

	public partial class YearStudyTestDataFactory : BaseYearStudyTestDataFactory{
	
		public YearStudyTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
