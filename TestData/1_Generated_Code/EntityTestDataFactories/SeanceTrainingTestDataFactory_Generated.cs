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
using TrainingIS.Models.SeanceTrainings;
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
    public class BaseSeanceTrainingTestDataFactory : EntityTestData<SeanceTraining>
    {
		public string Entity_CRUD_Test_Reference { set; get; } 
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new SeanceTrainingBLO(UnitOfWork, GAppContext);
			Entity_CRUD_Test_Reference  = "SeanceTraining_CRUD_Test";
        }

        public BaseSeanceTrainingTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

 
		protected override List<SeanceTraining> Load_Data_From_ExcelFile()
        {
            List<SeanceTraining> Data = null;

            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/SeanceTraining.xlsx";

            if (File.Exists(FileName))
            {
                Data = new List<SeanceTraining>();

                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                Data = (this.BLO as SeanceTrainingBLO).Convert_DataTable_to_List(firstTable);
            }
            return Data;
        }

        protected override List<SeanceTraining> Insert_Or_Update_ExcelFile_TestData(out bool is_Insert_Or_Update)
        {
            is_Insert_Or_Update = false;
            List<SeanceTraining> Data = new List<SeanceTraining>();

            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/SeanceTraining.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/SeanceTraining.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported

				// Delete Repport_File if exist
                if (!File.Exists(Repport_File))
                {
					ImportReport importReport = (this.BLO as SeanceTrainingBLO).Import(firstTable, FileName);
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
								"SeanceTraining",
							importReport.Number_of_inserted_erros_rows, 
							importReport.Number_of_updated_erros_rows
                           
							);
						throw new GAppException(msg_ex);
					}

					// Convert Data Table to Data
					Data = importReport.ImportedObjects.Cast<SeanceTraining>().ToList();
					is_Insert_Or_Update = true;
                }

				

            }
            return Data;
        }


		/// <summary>
        /// Find the first SeanceTraining instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual SeanceTraining CreateOrLouadFirstSeanceTraining()
        {
            SeanceTrainingBLO seancetrainingBLO = new SeanceTrainingBLO(UnitOfWork,GAppContext);
           
			SeanceTraining entity = null;
            if (seancetrainingBLO.FindAll()?.Count > 0)
                entity = seancetrainingBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp SeanceTraining for Test
                entity = this.CreateValideSeanceTrainingInstance();
                seancetrainingBLO.Save(entity);
            }
            return entity;
        }

		public virtual SeanceTraining Create_CRUD_SeanceTraining_Test_Instance()
        {
			SeanceTraining SeanceTraining = this.CreateValideSeanceTrainingInstance();
            SeanceTraining.Reference = this.Entity_CRUD_Test_Reference;
            return SeanceTraining;
        }

        public virtual SeanceTraining CreateValideSeanceTrainingInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            SeanceTraining  Valide_SeanceTraining = this._Fixture.Create<SeanceTraining>();
            Valide_SeanceTraining.Id = 0;
            // Many to One 
            //   
			// SeancePlanning
			var SeancePlanning = new SeancePlanningTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstSeancePlanning();
            Valide_SeanceTraining.SeancePlanning = SeancePlanning;
						 Valide_SeanceTraining.SeancePlanningId = SeancePlanning.Id;
			           
            // One to Many
            //
			Valide_SeanceTraining.Absences = null;
            return Valide_SeanceTraining;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide SeanceTraining can't exist</returns>
        public virtual SeanceTraining CreateInValideSeanceTrainingInstance()
        {
            SeanceTraining seancetraining = this.CreateValideSeanceTrainingInstance();
             
			// Required   
 
			seancetraining.SeanceDate = null;
 
			seancetraining.SeancePlanningId = 0;
            //Unique
			var existant_SeanceTraining = this.CreateOrLouadFirstSeanceTraining();
			seancetraining.Reference = existant_SeanceTraining.Reference;
 
            return seancetraining;
        }


		public virtual SeanceTraining CreateInValideSeanceTrainingInstance_ForEdit()
        {
            SeanceTraining seancetraining = this.CreateOrLouadFirstSeanceTraining();
			// Required   
 
			seancetraining.SeanceDate = null;
 
			seancetraining.SeancePlanningId = 0;
            //Unique
			var existant_SeanceTraining = this.CreateOrLouadFirstSeanceTraining();
			seancetraining.Reference = existant_SeanceTraining.Reference;
            return seancetraining;
        }

		public override void Generate_Excel_File(List<SeanceTraining> generated_Data)
        {
            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/SeanceTraining.xlsx";

            var DataTeble = (this.BLO as SeanceTrainingBLO).Convert_to_DataTable(generated_Data);
            (this.BLO as SeanceTrainingBLO).ExcelDAO.Save(DataTeble, FileName);
        }
    }

	public partial class SeanceTrainingTestDataFactory : BaseSeanceTrainingTestDataFactory{
	
		public SeanceTrainingTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
