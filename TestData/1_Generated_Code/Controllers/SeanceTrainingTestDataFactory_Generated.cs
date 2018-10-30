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

namespace TestData
{
    public class BaseSeanceTrainingTestDataFactory : EntityTestData<SeanceTraining>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new SeanceTrainingBLO(UnitOfWork, GAppContext);
        }

        public BaseSeanceTrainingTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<SeanceTraining> Generate_TestData()
        {
            List<SeanceTraining> Data = new List<SeanceTraining>();
 
            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/SeanceTraining_TestData.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/SeanceTraining_TestData.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported
                if (File.Exists(Repport_File))
                {
                    ImportReport importReport = (this.BLO as SeanceTrainingBLO).Import(firstTable, FileName);
                    // Save ExcelRepport file to Server
                    DataSet DataSet_report = importReport.get_DataSet_Report();
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(DataSet_report);
                        wb.SaveAs(Repport_File);
                    }
                    // Convert Data Table to Data
                    Data = importReport.ImportedObjects.Cast<SeanceTraining>().ToList();
                }
                else
                {
                    Data = (this.BLO as SeanceTrainingBLO).Convert_DataTable_to_List(firstTable);
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
    }

	public partial class SeanceTrainingTestDataFactory : BaseSeanceTrainingTestDataFactory{
	
		public SeanceTrainingTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
