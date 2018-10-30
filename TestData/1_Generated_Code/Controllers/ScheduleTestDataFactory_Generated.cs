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
    public class BaseScheduleTestDataFactory : EntityTestData<Schedule>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new ScheduleBLO(UnitOfWork, GAppContext);
        }

        public BaseScheduleTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<Schedule> Generate_TestData()
        {
            List<Schedule> Data = new List<Schedule>();
 
            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Schedule.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/Schedule.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported
                if (File.Exists(Repport_File))
                {
                    ImportReport importReport = (this.BLO as ScheduleBLO).Import(firstTable, FileName);
                    // Save ExcelRepport file to Server
                    DataSet DataSet_report = importReport.get_DataSet_Report();
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(DataSet_report);
                        wb.SaveAs(Repport_File);
                    }
                    // Convert Data Table to Data
                    Data = importReport.ImportedObjects.Cast<Schedule>().ToList();
                }
                else
                {
                    Data = (this.BLO as ScheduleBLO).Convert_DataTable_to_List(firstTable);
                }
            }
            return Data;
        }
	
		/// <summary>
        /// Find the first Schedule instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Schedule CreateOrLouadFirstSchedule()
        {
            ScheduleBLO scheduleBLO = new ScheduleBLO(UnitOfWork,GAppContext);
           
			Schedule entity = null;
            if (scheduleBLO.FindAll()?.Count > 0)
                entity = scheduleBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Schedule for Test
                entity = this.CreateValideScheduleInstance();
                scheduleBLO.Save(entity);
            }
            return entity;
        }

        public virtual Schedule CreateValideScheduleInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            Schedule  Valide_Schedule = this._Fixture.Create<Schedule>();
            Valide_Schedule.Id = 0;
            // Many to One 
            //   
			// TrainingYear
			var TrainingYear = new TrainingYearTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstTrainingYear();
            Valide_Schedule.TrainingYear = TrainingYear;
						 Valide_Schedule.TrainingYearId = TrainingYear.Id;
			           
            // One to Many
            //
            return Valide_Schedule;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Schedule can't exist</returns>
        public virtual Schedule CreateInValideScheduleInstance()
        {
            Schedule schedule = this.CreateValideScheduleInstance();
             
			// Required   
 
			schedule.TrainingYearId = 0;
 
			schedule.StartDate = DateTime.Now;
 
			schedule.EndtDate = DateTime.Now;
            //Unique
			var existant_Schedule = this.CreateOrLouadFirstSchedule();
			schedule.Reference = existant_Schedule.Reference;
 
            return schedule;
        }


		public virtual Schedule CreateInValideScheduleInstance_ForEdit()
        {
            Schedule schedule = this.CreateOrLouadFirstSchedule();
			// Required   
 
			schedule.TrainingYearId = 0;
 
			schedule.StartDate = DateTime.Now;
 
			schedule.EndtDate = DateTime.Now;
            //Unique
			var existant_Schedule = this.CreateOrLouadFirstSchedule();
			schedule.Reference = existant_Schedule.Reference;
            return schedule;
        }
    }

	public partial class ScheduleTestDataFactory : BaseScheduleTestDataFactory{
	
		public ScheduleTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
