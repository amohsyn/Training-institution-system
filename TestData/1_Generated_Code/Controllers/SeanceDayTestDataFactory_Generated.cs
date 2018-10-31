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
    public class BaseSeanceDayTestDataFactory : EntityTestData<SeanceDay>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new SeanceDayBLO(UnitOfWork, GAppContext);
        }

        public BaseSeanceDayTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<SeanceDay> Load_Data_From_ExcelFile()
        {
            List<SeanceDay> Data = null;

            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/SeanceDay.xlsx";

            if (File.Exists(FileName))
            {
                Data = new List<SeanceDay>();

                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                Data = (this.BLO as SeanceDayBLO).Convert_DataTable_to_List(firstTable);
            }
            return Data;
        }

        protected override List<SeanceDay> Insert_Or_Update_ExcelFile_TestData(out bool is_Insert_Or_Update)
        {
            is_Insert_Or_Update = false;
            List<SeanceDay> Data = new List<SeanceDay>();

            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/SeanceDay.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/SeanceDay.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported
                if (!File.Exists(Repport_File))
                {
                   
                    ImportReport importReport = (this.BLO as SeanceDayBLO).Import(firstTable, FileName);
                    // Save ExcelRepport file to Server
                    DataSet DataSet_report = importReport.get_DataSet_Report();
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(DataSet_report);
                        wb.SaveAs(Repport_File);
                    }
                    // Convert Data Table to Data
                    Data = importReport.ImportedObjects.Cast<SeanceDay>().ToList();
                    is_Insert_Or_Update = true;
                }
                else
                {
                    Data = (this.BLO as SeanceDayBLO).Convert_DataTable_to_List(firstTable);
                }
            }
            return Data;
        }


		/// <summary>
        /// Find the first SeanceDay instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual SeanceDay CreateOrLouadFirstSeanceDay()
        {
            SeanceDayBLO seancedayBLO = new SeanceDayBLO(UnitOfWork,GAppContext);
           
			SeanceDay entity = null;
            if (seancedayBLO.FindAll()?.Count > 0)
                entity = seancedayBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp SeanceDay for Test
                entity = this.CreateValideSeanceDayInstance();
                seancedayBLO.Save(entity);
            }
            return entity;
        }

        public virtual SeanceDay CreateValideSeanceDayInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            SeanceDay  Valide_SeanceDay = this._Fixture.Create<SeanceDay>();
            Valide_SeanceDay.Id = 0;
            // Many to One 
            //   
            // One to Many
            //
            return Valide_SeanceDay;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide SeanceDay can't exist</returns>
        public virtual SeanceDay CreateInValideSeanceDayInstance()
        {
            SeanceDay seanceday = this.CreateValideSeanceDayInstance();
             
			// Required   
 
			seanceday.Name = null;
 
			seanceday.Code = null;
            //Unique
			var existant_SeanceDay = this.CreateOrLouadFirstSeanceDay();
			seanceday.Code = existant_SeanceDay.Code;
			seanceday.Day = existant_SeanceDay.Day;
			seanceday.Reference = existant_SeanceDay.Reference;
 
            return seanceday;
        }


		public virtual SeanceDay CreateInValideSeanceDayInstance_ForEdit()
        {
            SeanceDay seanceday = this.CreateOrLouadFirstSeanceDay();
			// Required   
 
			seanceday.Name = null;
 
			seanceday.Code = null;
            //Unique
			var existant_SeanceDay = this.CreateOrLouadFirstSeanceDay();
			seanceday.Code = existant_SeanceDay.Code;
			seanceday.Day = existant_SeanceDay.Day;
			seanceday.Reference = existant_SeanceDay.Reference;
            return seanceday;
        }
    }

	public partial class SeanceDayTestDataFactory : BaseSeanceDayTestDataFactory{
	
		public SeanceDayTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
