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
    public class BaseWarningTraineeTestDataFactory : EntityTestData<WarningTrainee>
    {
		public string Entity_CRUD_Test_Reference { set; get; } 
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new WarningTraineeBLO(UnitOfWork, GAppContext);
			Entity_CRUD_Test_Reference  = "WarningTrainee_CRUD_Test";
        }

        public BaseWarningTraineeTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

 
		protected override List<WarningTrainee> Load_Data_From_ExcelFile()
        {
            List<WarningTrainee> Data = null;

            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/WarningTrainee.xlsx";

            if (File.Exists(FileName))
            {
                Data = new List<WarningTrainee>();

                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                Data = (this.BLO as WarningTraineeBLO).Convert_DataTable_to_List(firstTable);
            }
            return Data;
        }

        protected override List<WarningTrainee> Insert_Or_Update_ExcelFile_TestData(out bool is_Insert_Or_Update)
        {
            is_Insert_Or_Update = false;
            List<WarningTrainee> Data = new List<WarningTrainee>();

            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/WarningTrainee.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/WarningTrainee.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported

				// Delete Repport_File if exist
                if (!File.Exists(Repport_File))
                {
					ImportReport importReport = (this.BLO as WarningTraineeBLO).Import(firstTable, FileName);
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
								"WarningTrainee",
							importReport.Number_of_inserted_erros_rows, 
							importReport.Number_of_updated_erros_rows
                           
							);
						throw new GAppException(msg_ex);
					}

					// Convert Data Table to Data
					Data = importReport.ImportedObjects.Cast<WarningTrainee>().ToList();
					is_Insert_Or_Update = true;
                }

				

            }
            return Data;
        }


		/// <summary>
        /// Find the first WarningTrainee instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual WarningTrainee CreateOrLouadFirstWarningTrainee()
        {
            WarningTraineeBLO warningtraineeBLO = new WarningTraineeBLO(UnitOfWork,GAppContext);
           
			WarningTrainee entity = null;
            if (warningtraineeBLO.FindAll()?.Count > 0)
                entity = warningtraineeBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp WarningTrainee for Test
                entity = this.CreateValideWarningTraineeInstance();
                warningtraineeBLO.Save(entity);
            }
            return entity;
        }

		public virtual WarningTrainee Create_CRUD_WarningTrainee_Test_Instance()
        {
			WarningTrainee WarningTrainee = this.CreateValideWarningTraineeInstance();
            WarningTrainee.Reference = this.Entity_CRUD_Test_Reference;
            return WarningTrainee;
        }

        public virtual WarningTrainee CreateValideWarningTraineeInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            WarningTrainee  Valide_WarningTrainee = this._Fixture.Create<WarningTrainee>();
            Valide_WarningTrainee.Id = 0;
            // Many to One 
            //   
			// Trainee
			var Trainee = new TraineeTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstTrainee();
            Valide_WarningTrainee.Trainee = Trainee;
						 Valide_WarningTrainee.TraineeId = Trainee.Id;
			           
			// Category_WarningTrainee
			var Category_WarningTrainee = new Category_WarningTraineeTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstCategory_WarningTrainee();
            Valide_WarningTrainee.Category_WarningTrainee = Category_WarningTrainee;
						 Valide_WarningTrainee.Category_WarningTraineeId = Category_WarningTrainee.Id;
			           
            // One to Many
            //
            return Valide_WarningTrainee;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide WarningTrainee can't exist</returns>
        public virtual WarningTrainee CreateInValideWarningTraineeInstance()
        {
            WarningTrainee warningtrainee = this.CreateValideWarningTraineeInstance();
             
			// Required   
 
			warningtrainee.TraineeId = 0;
 
			warningtrainee.WarningDate = DateTime.Now;
 
			warningtrainee.Category_WarningTraineeId = 0;
            //Unique
			var existant_WarningTrainee = this.CreateOrLouadFirstWarningTrainee();
			warningtrainee.Reference = existant_WarningTrainee.Reference;
 
            return warningtrainee;
        }


		public virtual WarningTrainee CreateInValideWarningTraineeInstance_ForEdit()
        {
            WarningTrainee warningtrainee = this.CreateOrLouadFirstWarningTrainee();
			// Required   
 
			warningtrainee.TraineeId = 0;
 
			warningtrainee.WarningDate = DateTime.Now;
 
			warningtrainee.Category_WarningTraineeId = 0;
            //Unique
			var existant_WarningTrainee = this.CreateOrLouadFirstWarningTrainee();
			warningtrainee.Reference = existant_WarningTrainee.Reference;
            return warningtrainee;
        }

		public override void Generate_Excel_File(List<WarningTrainee> generated_Data)
        {
            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/WarningTrainee.xlsx";

            var DataTeble = (this.BLO as WarningTraineeBLO).Convert_to_DataTable(generated_Data);
            (this.BLO as WarningTraineeBLO).ExcelDAO.Save(DataTeble, FileName);
        }
    }

	public partial class WarningTraineeTestDataFactory : BaseWarningTraineeTestDataFactory{
	
		public WarningTraineeTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
