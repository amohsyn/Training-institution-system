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
    public class BaseStateOfAbseceTestDataFactory : EntityTestData<StateOfAbsece>
    {
		public string Entity_CRUD_Test_Reference { set; get; } 
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new StateOfAbseceBLO(UnitOfWork, GAppContext);
			Entity_CRUD_Test_Reference  = "StateOfAbsece_CRUD_Test";
        }

        public BaseStateOfAbseceTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

 
		protected override List<StateOfAbsece> Load_Data_From_ExcelFile()
        {
            List<StateOfAbsece> Data = null;

            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/StateOfAbsece.xlsx";

            if (File.Exists(FileName))
            {
                Data = new List<StateOfAbsece>();

                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                Data = (this.BLO as StateOfAbseceBLO).Convert_DataTable_to_List(firstTable);
            }
            return Data;
        }

        protected override List<StateOfAbsece> Insert_Or_Update_ExcelFile_TestData(out bool is_Insert_Or_Update)
        {
            is_Insert_Or_Update = false;
            List<StateOfAbsece> Data = new List<StateOfAbsece>();

            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/StateOfAbsece.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/StateOfAbsece.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported

				// Delete Repport_File if exist
                if (!File.Exists(Repport_File))
                {
					ImportReport importReport = (this.BLO as StateOfAbseceBLO).Import(firstTable, FileName);
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
								"StateOfAbsece",
							importReport.Number_of_inserted_erros_rows, 
							importReport.Number_of_updated_erros_rows
                           
							);
						throw new GAppException(msg_ex);
					}

					// Convert Data Table to Data
					Data = importReport.ImportedObjects.Cast<StateOfAbsece>().ToList();
					is_Insert_Or_Update = true;
                }

				

            }
            return Data;
        }


		/// <summary>
        /// Find the first StateOfAbsece instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual StateOfAbsece CreateOrLouadFirstStateOfAbsece()
        {
            StateOfAbseceBLO stateofabseceBLO = new StateOfAbseceBLO(UnitOfWork,GAppContext);
           
			StateOfAbsece entity = null;
            if (stateofabseceBLO.FindAll()?.Count > 0)
                entity = stateofabseceBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp StateOfAbsece for Test
                entity = this.CreateValideStateOfAbseceInstance();
                stateofabseceBLO.Save(entity);
            }
            return entity;
        }

		public virtual StateOfAbsece Create_CRUD_StateOfAbsece_Test_Instance()
        {
			StateOfAbsece StateOfAbsece = this.CreateValideStateOfAbseceInstance();
            StateOfAbsece.Reference = this.Entity_CRUD_Test_Reference;
            return StateOfAbsece;
        }

        public virtual StateOfAbsece CreateValideStateOfAbseceInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            StateOfAbsece  Valide_StateOfAbsece = this._Fixture.Create<StateOfAbsece>();
            Valide_StateOfAbsece.Id = 0;
            // Many to One 
            //   
			// Trainee
			var Trainee = new TraineeTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstTrainee();
            Valide_StateOfAbsece.Trainee = Trainee;
						 Valide_StateOfAbsece.TraineeId = Trainee.Id;
			           
            // One to Many
            //
            return Valide_StateOfAbsece;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide StateOfAbsece can't exist</returns>
        public virtual StateOfAbsece CreateInValideStateOfAbseceInstance()
        {
            StateOfAbsece stateofabsece = this.CreateValideStateOfAbseceInstance();
             
			// Required   
 
			stateofabsece.Name = null;
 
			stateofabsece.Category = StateOfAbseceCategories.TrainingYear;
 
			stateofabsece.Value = 0;
 
			stateofabsece.TraineeId = 0;
            //Unique
			var existant_StateOfAbsece = this.CreateOrLouadFirstStateOfAbsece();
			stateofabsece.Reference = existant_StateOfAbsece.Reference;
 
            return stateofabsece;
        }


		public virtual StateOfAbsece CreateInValideStateOfAbseceInstance_ForEdit()
        {
            StateOfAbsece stateofabsece = this.CreateOrLouadFirstStateOfAbsece();
			// Required   
 
			stateofabsece.Name = null;
 
			stateofabsece.Category = StateOfAbseceCategories.TrainingYear;
 
			stateofabsece.Value = 0;
 
			stateofabsece.TraineeId = 0;
            //Unique
			var existant_StateOfAbsece = this.CreateOrLouadFirstStateOfAbsece();
			stateofabsece.Reference = existant_StateOfAbsece.Reference;
            return stateofabsece;
        }

		public override void Generate_Excel_File(List<StateOfAbsece> generated_Data)
        {
            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/StateOfAbsece.xlsx";

            var DataTeble = (this.BLO as StateOfAbseceBLO).Convert_to_DataTable(generated_Data);
            (this.BLO as StateOfAbseceBLO).ExcelDAO.Save(DataTeble, FileName);
        }
    }

	public partial class StateOfAbseceTestDataFactory : BaseStateOfAbseceTestDataFactory{
	
		public StateOfAbseceTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
