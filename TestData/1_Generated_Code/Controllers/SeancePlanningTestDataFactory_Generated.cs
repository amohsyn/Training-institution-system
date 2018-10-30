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
    public class BaseSeancePlanningTestDataFactory : EntityTestData<SeancePlanning>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new SeancePlanningBLO(UnitOfWork, GAppContext);
        }

        public BaseSeancePlanningTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<SeancePlanning> Generate_TestData()
        {
            List<SeancePlanning> Data = new List<SeancePlanning>();
 
            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/SeancePlanning_TestData.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/SeancePlanning_TestData.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported
                if (File.Exists(Repport_File))
                {
                    ImportReport importReport = (this.BLO as SeancePlanningBLO).Import(firstTable, FileName);
                    // Save ExcelRepport file to Server
                    DataSet DataSet_report = importReport.get_DataSet_Report();
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(DataSet_report);
                        wb.SaveAs(Repport_File);
                    }
                    // Convert Data Table to Data
                    Data = importReport.ImportedObjects.Cast<SeancePlanning>().ToList();
                }
                else
                {
                    Data = (this.BLO as SeancePlanningBLO).Convert_DataTable_to_List(firstTable);
                }
            }
            return Data;
        }
	
		/// <summary>
        /// Find the first SeancePlanning instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual SeancePlanning CreateOrLouadFirstSeancePlanning()
        {
            SeancePlanningBLO seanceplanningBLO = new SeancePlanningBLO(UnitOfWork,GAppContext);
           
			SeancePlanning entity = null;
            if (seanceplanningBLO.FindAll()?.Count > 0)
                entity = seanceplanningBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp SeancePlanning for Test
                entity = this.CreateValideSeancePlanningInstance();
                seanceplanningBLO.Save(entity);
            }
            return entity;
        }

        public virtual SeancePlanning CreateValideSeancePlanningInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            SeancePlanning  Valide_SeancePlanning = this._Fixture.Create<SeancePlanning>();
            Valide_SeancePlanning.Id = 0;
            // Many to One 
            //   
			// Schedule
			var Schedule = new ScheduleTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstSchedule();
            Valide_SeancePlanning.Schedule = Schedule;
						 Valide_SeancePlanning.ScheduleId = Schedule.Id;
			           
			// Training
			var Training = new TrainingTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstTraining();
            Valide_SeancePlanning.Training = Training;
						 Valide_SeancePlanning.TrainingId = Training.Id;
			           
			// SeanceDay
			var SeanceDay = new SeanceDayTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstSeanceDay();
            Valide_SeancePlanning.SeanceDay = SeanceDay;
						 Valide_SeancePlanning.SeanceDayId = SeanceDay.Id;
			           
			// SeanceNumber
			var SeanceNumber = new SeanceNumberTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstSeanceNumber();
            Valide_SeancePlanning.SeanceNumber = SeanceNumber;
						 Valide_SeancePlanning.SeanceNumberId = SeanceNumber.Id;
			           
			// Classroom
			var Classroom = new ClassroomTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstClassroom();
            Valide_SeancePlanning.Classroom = Classroom;
						 Valide_SeancePlanning.ClassroomId = Classroom.Id;
			           
            // One to Many
            //
			Valide_SeancePlanning.Absences = null;
            return Valide_SeancePlanning;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide SeancePlanning can't exist</returns>
        public virtual SeancePlanning CreateInValideSeancePlanningInstance()
        {
            SeancePlanning seanceplanning = this.CreateValideSeancePlanningInstance();
             
			// Required   
 
			seanceplanning.ScheduleId = 0;
 
			seanceplanning.TrainingId = 0;
 
			seanceplanning.SeanceDayId = 0;
 
			seanceplanning.SeanceNumberId = 0;
 
			seanceplanning.ClassroomId = 0;
            //Unique
			var existant_SeancePlanning = this.CreateOrLouadFirstSeancePlanning();
			seanceplanning.Reference = existant_SeancePlanning.Reference;
 
            return seanceplanning;
        }


		public virtual SeancePlanning CreateInValideSeancePlanningInstance_ForEdit()
        {
            SeancePlanning seanceplanning = this.CreateOrLouadFirstSeancePlanning();
			// Required   
 
			seanceplanning.ScheduleId = 0;
 
			seanceplanning.TrainingId = 0;
 
			seanceplanning.SeanceDayId = 0;
 
			seanceplanning.SeanceNumberId = 0;
 
			seanceplanning.ClassroomId = 0;
            //Unique
			var existant_SeancePlanning = this.CreateOrLouadFirstSeancePlanning();
			seanceplanning.Reference = existant_SeancePlanning.Reference;
            return seanceplanning;
        }
    }

	public partial class SeancePlanningTestDataFactory : BaseSeancePlanningTestDataFactory{
	
		public SeancePlanningTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
