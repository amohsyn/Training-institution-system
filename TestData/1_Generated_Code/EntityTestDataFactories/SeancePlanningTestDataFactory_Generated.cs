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
    public class BaseSeancePlanningTestDataFactory : EntityTestData<SeancePlanning>
    {
		public string Entity_CRUD_Test_Reference { set; get; } 
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new SeancePlanningBLO(UnitOfWork, GAppContext);
			Entity_CRUD_Test_Reference  = "SeancePlanning_CRUD_Test";
        }

        public BaseSeancePlanningTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

 
		protected override List<SeancePlanning> Load_Data_From_ExcelFile()
        {
            List<SeancePlanning> Data = null;

            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/SeancePlanning.xlsx";

            if (File.Exists(FileName))
            {
                Data = new List<SeancePlanning>();

                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                Data = (this.BLO as SeancePlanningBLO).Convert_DataTable_to_List(firstTable);
            }
            return Data;
        }

        protected override List<SeancePlanning> Insert_Or_Update_ExcelFile_TestData(out bool is_Insert_Or_Update)
        {
            is_Insert_Or_Update = false;
            List<SeancePlanning> Data = new List<SeancePlanning>();

            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/SeancePlanning.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/SeancePlanning.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported

				// Delete Repport_File if exist
                if (!File.Exists(Repport_File))
                {
					ImportReport importReport = (this.BLO as SeancePlanningBLO).Import(firstTable, FileName);
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
								"SeancePlanning",
							importReport.Number_of_inserted_erros_rows, 
							importReport.Number_of_updated_erros_rows
                           
							);
						throw new GAppException(msg_ex);
					}

					// Convert Data Table to Data
					Data = importReport.ImportedObjects.Cast<SeancePlanning>().ToList();
					is_Insert_Or_Update = true;
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

		public virtual SeancePlanning Create_CRUD_SeancePlanning_Test_Instance()
        {
			SeancePlanning SeancePlanning = this.CreateValideSeancePlanningInstance();
            SeancePlanning.Reference = this.Entity_CRUD_Test_Reference;
            return SeancePlanning;
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

		public override void Generate_Excel_File(List<SeancePlanning> generated_Data)
        {
            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/SeancePlanning.xlsx";

            var DataTeble = (this.BLO as SeancePlanningBLO).Convert_to_DataTable(generated_Data);
            (this.BLO as SeancePlanningBLO).ExcelDAO.Save(DataTeble, FileName);
        }
    }

	public partial class SeancePlanningTestDataFactory : BaseSeancePlanningTestDataFactory{
	
		public SeancePlanningTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
