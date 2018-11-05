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
    public class BaseSanctionTestDataFactory : EntityTestData<Sanction>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new SanctionBLO(UnitOfWork, GAppContext);
        }

        public BaseSanctionTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<Sanction> Load_Data_From_ExcelFile()
        {
            List<Sanction> Data = null;

            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Sanction.xlsx";

            if (File.Exists(FileName))
            {
                Data = new List<Sanction>();

                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                Data = (this.BLO as SanctionBLO).Convert_DataTable_to_List(firstTable);
            }
            return Data;
        }

        protected override List<Sanction> Insert_Or_Update_ExcelFile_TestData(out bool is_Insert_Or_Update)
        {
            is_Insert_Or_Update = false;
            List<Sanction> Data = new List<Sanction>();

            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Sanction.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/Sanction.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported

				// Delete Repport_File if exist
                if (!File.Exists(Repport_File))
                {
					ImportReport importReport = (this.BLO as SanctionBLO).Import(firstTable, FileName);
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
								"Sanction",
							importReport.Number_of_inserted_erros_rows, 
							importReport.Number_of_updated_erros_rows
                           
							);
						throw new GAppException(msg_ex);
					}

					// Convert Data Table to Data
					Data = importReport.ImportedObjects.Cast<Sanction>().ToList();
					is_Insert_Or_Update = true;
                }

				

            }
            return Data;
        }


		/// <summary>
        /// Find the first Sanction instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Sanction CreateOrLouadFirstSanction()
        {
            SanctionBLO sanctionBLO = new SanctionBLO(UnitOfWork,GAppContext);
           
			Sanction entity = null;
            if (sanctionBLO.FindAll()?.Count > 0)
                entity = sanctionBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Sanction for Test
                entity = this.CreateValideSanctionInstance();
                sanctionBLO.Save(entity);
            }
            return entity;
        }

        public virtual Sanction CreateValideSanctionInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            Sanction  Valide_Sanction = this._Fixture.Create<Sanction>();
            Valide_Sanction.Id = 0;
            // Many to One 
            //   
			// Trainee
			var Trainee = new TraineeTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstTrainee();
            Valide_Sanction.Trainee = Trainee;
						 Valide_Sanction.TraineeId = Trainee.Id;
			           
			// SanctionCategory
			var SanctionCategory = new SanctionCategoryTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstSanctionCategory();
            Valide_Sanction.SanctionCategory = SanctionCategory;
						 Valide_Sanction.SanctionCategoryId = SanctionCategory.Id;
			           
			// Meeting
			var Meeting = new MeetingTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstMeeting();
            Valide_Sanction.Meeting = Meeting;
						 Valide_Sanction.MeetingId = Meeting.Id;
			           
            // One to Many
            //
			Valide_Sanction.Absences = null;
            return Valide_Sanction;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Sanction can't exist</returns>
        public virtual Sanction CreateInValideSanctionInstance()
        {
            Sanction sanction = this.CreateValideSanctionInstance();
             
			// Required   
 
			sanction.TraineeId = 0;
 
			sanction.SanctionCategoryId = 0;
            //Unique
			var existant_Sanction = this.CreateOrLouadFirstSanction();
			sanction.Reference = existant_Sanction.Reference;
 
            return sanction;
        }


		public virtual Sanction CreateInValideSanctionInstance_ForEdit()
        {
            Sanction sanction = this.CreateOrLouadFirstSanction();
			// Required   
 
			sanction.TraineeId = 0;
 
			sanction.SanctionCategoryId = 0;
            //Unique
			var existant_Sanction = this.CreateOrLouadFirstSanction();
			sanction.Reference = existant_Sanction.Reference;
            return sanction;
        }

		public override void Generate_Excel_File(List<Sanction> generated_Data)
        {
            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Sanction.xlsx";

            var DataTeble = (this.BLO as SanctionBLO).Convert_to_DataTable(generated_Data);
            (this.BLO as SanctionBLO).ExcelDAO.Save(DataTeble, FileName);
        }
    }

	public partial class SanctionTestDataFactory : BaseSanctionTestDataFactory{
	
		public SanctionTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
