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
    public class BaseCalendarDayTestDataFactory : EntityTestData<CalendarDay>
    {
		public string Entity_CRUD_Test_Reference { set; get; } 
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new CalendarDayBLO(UnitOfWork, GAppContext);
			Entity_CRUD_Test_Reference  = "CalendarDay_CRUD_Test";
        }

        public BaseCalendarDayTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

 
		protected override List<CalendarDay> Load_Data_From_ExcelFile()
        {
            List<CalendarDay> Data = null;

            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/CalendarDay.xlsx";

            if (File.Exists(FileName))
            {
                Data = new List<CalendarDay>();

                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                Data = (this.BLO as CalendarDayBLO).Convert_DataTable_to_List(firstTable);
            }
            return Data;
        }

        protected override List<CalendarDay> Insert_Or_Update_ExcelFile_TestData(out bool is_Insert_Or_Update)
        {
            is_Insert_Or_Update = false;
            List<CalendarDay> Data = new List<CalendarDay>();

            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/CalendarDay.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/CalendarDay.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported

				// Delete Repport_File if exist
                if (!File.Exists(Repport_File))
                {
					ImportReport importReport = (this.BLO as CalendarDayBLO).Import(firstTable, FileName);
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
								"CalendarDay",
							importReport.Number_of_inserted_erros_rows, 
							importReport.Number_of_updated_erros_rows
                           
							);
						throw new GAppException(msg_ex);
					}

					// Convert Data Table to Data
					Data = importReport.ImportedObjects.Cast<CalendarDay>().ToList();
					is_Insert_Or_Update = true;
                }

				

            }
            return Data;
        }


		/// <summary>
        /// Find the first CalendarDay instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual CalendarDay CreateOrLouadFirstCalendarDay()
        {
            CalendarDayBLO calendardayBLO = new CalendarDayBLO(UnitOfWork,GAppContext);
           
			CalendarDay entity = null;
            if (calendardayBLO.FindAll()?.Count > 0)
                entity = calendardayBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp CalendarDay for Test
                entity = this.CreateValideCalendarDayInstance();
                calendardayBLO.Save(entity);
            }
            return entity;
        }

		public virtual CalendarDay Create_CRUD_CalendarDay_Test_Instance()
        {
			CalendarDay CalendarDay = this.CreateValideCalendarDayInstance();
            CalendarDay.Reference = this.Entity_CRUD_Test_Reference;
            return CalendarDay;
        }

        public virtual CalendarDay CreateValideCalendarDayInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            CalendarDay  Valide_CalendarDay = this._Fixture.Create<CalendarDay>();
            Valide_CalendarDay.Id = 0;
            // Many to One 
            //   
            // One to Many
            //
            return Valide_CalendarDay;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide CalendarDay can't exist</returns>
        public virtual CalendarDay CreateInValideCalendarDayInstance()
        {
            CalendarDay calendarday = this.CreateValideCalendarDayInstance();
             
			// Required   
            //Unique
			var existant_CalendarDay = this.CreateOrLouadFirstCalendarDay();
			calendarday.Reference = existant_CalendarDay.Reference;
 
            return calendarday;
        }


		public virtual CalendarDay CreateInValideCalendarDayInstance_ForEdit()
        {
            CalendarDay calendarday = this.CreateOrLouadFirstCalendarDay();
			// Required   
            //Unique
			var existant_CalendarDay = this.CreateOrLouadFirstCalendarDay();
			calendarday.Reference = existant_CalendarDay.Reference;
            return calendarday;
        }

		public override void Generate_Excel_File(List<CalendarDay> generated_Data)
        {
            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/CalendarDay.xlsx";

            var DataTeble = (this.BLO as CalendarDayBLO).Convert_to_DataTable(generated_Data);
            (this.BLO as CalendarDayBLO).ExcelDAO.Save(DataTeble, FileName);
        }
    }

	public partial class CalendarDayTestDataFactory : BaseCalendarDayTestDataFactory{
	
		public CalendarDayTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
