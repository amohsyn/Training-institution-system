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
    public class BaseTrainingTestDataFactory : EntityTestData<Training>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new TrainingBLO(UnitOfWork, GAppContext);
        }

        public BaseTrainingTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<Training> Load_Data_From_ExcelFile()
        {
            List<Training> Data = null;

            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Training.xlsx";

            if (File.Exists(FileName))
            {
                Data = new List<Training>();

                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                Data = (this.BLO as TrainingBLO).Convert_DataTable_to_List(firstTable);
            }
            return Data;
        }

        protected override List<Training> Insert_Or_Update_ExcelFile_TestData(out bool is_Insert_Or_Update)
        {
            is_Insert_Or_Update = false;
            List<Training> Data = new List<Training>();

            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Training.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/Training.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported

				// Delete Repport_File if exist
                if (!File.Exists(Repport_File))
                {
					ImportReport importReport = (this.BLO as TrainingBLO).Import(firstTable, FileName);
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
								"Training",
							importReport.Number_of_inserted_erros_rows, 
							importReport.Number_of_updated_erros_rows
                           
							);
						throw new GAppException(msg_ex);
					}

					// Convert Data Table to Data
					Data = importReport.ImportedObjects.Cast<Training>().ToList();
					is_Insert_Or_Update = true;
                }

				

            }
            return Data;
        }


		/// <summary>
        /// Find the first Training instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Training CreateOrLouadFirstTraining()
        {
            TrainingBLO trainingBLO = new TrainingBLO(UnitOfWork,GAppContext);
           
			Training entity = null;
            if (trainingBLO.FindAll()?.Count > 0)
                entity = trainingBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Training for Test
                entity = this.CreateValideTrainingInstance();
                trainingBLO.Save(entity);
            }
            return entity;
        }

        public virtual Training CreateValideTrainingInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            Training  Valide_Training = this._Fixture.Create<Training>();
            Valide_Training.Id = 0;
            // Many to One 
            //   
			// TrainingYear
			var TrainingYear = new TrainingYearTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstTrainingYear();
            Valide_Training.TrainingYear = TrainingYear;
						 Valide_Training.TrainingYearId = TrainingYear.Id;
			           
			// ModuleTraining
			var ModuleTraining = new ModuleTrainingTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstModuleTraining();
            Valide_Training.ModuleTraining = ModuleTraining;
						 Valide_Training.ModuleTrainingId = ModuleTraining.Id;
			           
			// Former
			var Former = new FormerTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstFormer();
            Valide_Training.Former = Former;
						 Valide_Training.FormerId = Former.Id;
			           
			// Group
			var Group = new GroupTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstGroup();
            Valide_Training.Group = Group;
						 Valide_Training.GroupId = Group.Id;
			           
            // One to Many
            //
            return Valide_Training;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Training can't exist</returns>
        public virtual Training CreateInValideTrainingInstance()
        {
            Training training = this.CreateValideTrainingInstance();
             
			// Required   
 
			training.TrainingYearId = 0;
 
			training.ModuleTrainingId = 0;
 
			training.FormerId = 0;
 
			training.GroupId = 0;
            //Unique
			var existant_Training = this.CreateOrLouadFirstTraining();
			training.Reference = existant_Training.Reference;
 
            return training;
        }


		public virtual Training CreateInValideTrainingInstance_ForEdit()
        {
            Training training = this.CreateOrLouadFirstTraining();
			// Required   
 
			training.TrainingYearId = 0;
 
			training.ModuleTrainingId = 0;
 
			training.FormerId = 0;
 
			training.GroupId = 0;
            //Unique
			var existant_Training = this.CreateOrLouadFirstTraining();
			training.Reference = existant_Training.Reference;
            return training;
        }

		public override void Generate_Excel_File(List<Training> generated_Data)
        {
            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Training.xlsx";

            var DataTeble = (this.BLO as TrainingBLO).Convert_to_DataTable(generated_Data);
            (this.BLO as TrainingBLO).ExcelDAO.Save(DataTeble, FileName);
        }
    }

	public partial class TrainingTestDataFactory : BaseTrainingTestDataFactory{
	
		public TrainingTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
