﻿using System;
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
    public class BaseJustificationAbsenceTestDataFactory : EntityTestData<JustificationAbsence>
    {
		public string Entity_CRUD_Test_Reference { set; get; } 
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new JustificationAbsenceBLO(UnitOfWork, GAppContext);
			Entity_CRUD_Test_Reference  = "JustificationAbsence_CRUD_Test";
        }

        public BaseJustificationAbsenceTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

 
		protected override List<JustificationAbsence> Load_Data_From_ExcelFile()
        {
            List<JustificationAbsence> Data = null;

            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/JustificationAbsence.xlsx";

            if (File.Exists(FileName))
            {
                Data = new List<JustificationAbsence>();

                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                Data = (this.BLO as JustificationAbsenceBLO).Convert_DataTable_to_List(firstTable);
            }
            return Data;
        }

        protected override List<JustificationAbsence> Insert_Or_Update_ExcelFile_TestData(out bool is_Insert_Or_Update)
        {
            is_Insert_Or_Update = false;
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

				// Delete Repport_File if exist
                if (!File.Exists(Repport_File))
                {
					ImportReport importReport = (this.BLO as JustificationAbsenceBLO).Import(firstTable, FileName);
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
								"JustificationAbsence",
							importReport.Number_of_inserted_erros_rows, 
							importReport.Number_of_updated_erros_rows
                           
							);
						throw new GAppException(msg_ex);
					}

					// Convert Data Table to Data
					Data = importReport.ImportedObjects.Cast<JustificationAbsence>().ToList();
					is_Insert_Or_Update = true;
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

		public virtual JustificationAbsence Create_CRUD_JustificationAbsence_Test_Instance()
        {
			JustificationAbsence JustificationAbsence = this.CreateValideJustificationAbsenceInstance();
            JustificationAbsence.Reference = this.Entity_CRUD_Test_Reference;
            return JustificationAbsence;
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

		public override void Generate_Excel_File(List<JustificationAbsence> generated_Data)
        {
            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/JustificationAbsence.xlsx";

            var DataTeble = (this.BLO as JustificationAbsenceBLO).Convert_to_DataTable(generated_Data);
            (this.BLO as JustificationAbsenceBLO).ExcelDAO.Save(DataTeble, FileName);
        }
    }

	public partial class JustificationAbsenceTestDataFactory : BaseJustificationAbsenceTestDataFactory{
	
		public JustificationAbsenceTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
