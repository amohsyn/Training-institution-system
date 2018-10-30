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
    public class BaseJustificationAbsenceTestDataFactory : EntityTestData<JustificationAbsence>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new JustificationAbsenceBLO(UnitOfWork, GAppContext);
        }

        public BaseJustificationAbsenceTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<JustificationAbsence> Generate_TestData()
        {
            List<JustificationAbsence> Data = new List<JustificationAbsence>();
 
            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/JustificationAbsence.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/JustificationAbsence.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported
                if (File.Exists(Repport_File))
                {
                    ImportReport importReport = (this.BLO as JustificationAbsenceBLO).Import(firstTable, FileName);
                    // Save ExcelRepport file to Server
                    DataSet DataSet_report = importReport.get_DataSet_Report();
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(DataSet_report);
                        wb.SaveAs(Repport_File);
                    }
                    // Convert Data Table to Data
                    Data = importReport.ImportedObjects.Cast<JustificationAbsence>().ToList();
                }
                else
                {
                    Data = (this.BLO as JustificationAbsenceBLO).Convert_DataTable_to_List(firstTable);
                }
            }
            return Data;
        }
	
		/// <summary>
        /// Find the first JustificationAbsence instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual JustificationAbsence CreateOrLouadFirstJustificationAbsence()
        {
            JustificationAbsenceBLO justificationabsenceBLO = new JustificationAbsenceBLO(UnitOfWork,GAppContext);
           
			JustificationAbsence entity = null;
            if (justificationabsenceBLO.FindAll()?.Count > 0)
                entity = justificationabsenceBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp JustificationAbsence for Test
                entity = this.CreateValideJustificationAbsenceInstance();
                justificationabsenceBLO.Save(entity);
            }
            return entity;
        }

        public virtual JustificationAbsence CreateValideJustificationAbsenceInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            JustificationAbsence  Valide_JustificationAbsence = this._Fixture.Create<JustificationAbsence>();
            Valide_JustificationAbsence.Id = 0;
            // Many to One 
            //   
			// Trainee
			var Trainee = new TraineeTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstTrainee();
            Valide_JustificationAbsence.Trainee = Trainee;
						 Valide_JustificationAbsence.TraineeId = Trainee.Id;
			           
			// Category_JustificationAbsence
			var Category_JustificationAbsence = new Category_JustificationAbsenceTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstCategory_JustificationAbsence();
            Valide_JustificationAbsence.Category_JustificationAbsence = Category_JustificationAbsence;
						 Valide_JustificationAbsence.Category_JustificationAbsenceId = Category_JustificationAbsence.Id;
			           
            // One to Many
            //
            return Valide_JustificationAbsence;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide JustificationAbsence can't exist</returns>
        public virtual JustificationAbsence CreateInValideJustificationAbsenceInstance()
        {
            JustificationAbsence justificationabsence = this.CreateValideJustificationAbsenceInstance();
             
			// Required   
 
			justificationabsence.TraineeId = 0;
 
			justificationabsence.Category_JustificationAbsenceId = 0;
 
			justificationabsence.StartDate = DateTime.Now;
 
			justificationabsence.EndtDate = DateTime.Now;
            //Unique
			var existant_JustificationAbsence = this.CreateOrLouadFirstJustificationAbsence();
			justificationabsence.Reference = existant_JustificationAbsence.Reference;
 
            return justificationabsence;
        }


		public virtual JustificationAbsence CreateInValideJustificationAbsenceInstance_ForEdit()
        {
            JustificationAbsence justificationabsence = this.CreateOrLouadFirstJustificationAbsence();
			// Required   
 
			justificationabsence.TraineeId = 0;
 
			justificationabsence.Category_JustificationAbsenceId = 0;
 
			justificationabsence.StartDate = DateTime.Now;
 
			justificationabsence.EndtDate = DateTime.Now;
            //Unique
			var existant_JustificationAbsence = this.CreateOrLouadFirstJustificationAbsence();
			justificationabsence.Reference = existant_JustificationAbsence.Reference;
            return justificationabsence;
        }
    }

	public partial class JustificationAbsenceTestDataFactory : BaseJustificationAbsenceTestDataFactory{
	
		public JustificationAbsenceTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
