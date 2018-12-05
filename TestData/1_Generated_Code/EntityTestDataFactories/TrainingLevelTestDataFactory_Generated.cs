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
    public class BaseTrainingLevelTestDataFactory : EntityTestData<TrainingLevel>
    {
		public string Entity_CRUD_Test_Reference { set; get; } 
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new TrainingLevelBLO(UnitOfWork, GAppContext);
			Entity_CRUD_Test_Reference  = "TrainingLevel_CRUD_Test";
        }

        public BaseTrainingLevelTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

 
		protected override List<TrainingLevel> Load_Data_From_ExcelFile()
        {
            List<TrainingLevel> Data = null;

            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/TrainingLevel.xlsx";

            if (File.Exists(FileName))
            {
                Data = new List<TrainingLevel>();

                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                Data = (this.BLO as TrainingLevelBLO).Convert_DataTable_to_List(firstTable);
            }
            return Data;
        }

        protected override List<TrainingLevel> Insert_Or_Update_ExcelFile_TestData(out bool is_Insert_Or_Update)
        {
            is_Insert_Or_Update = false;
            List<TrainingLevel> Data = new List<TrainingLevel>();

            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/TrainingLevel.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/TrainingLevel.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported

				// Delete Repport_File if exist
                if (!File.Exists(Repport_File))
                {
					ImportReport importReport = (this.BLO as TrainingLevelBLO).Import(firstTable, FileName);
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
								"TrainingLevel",
							importReport.Number_of_inserted_erros_rows, 
							importReport.Number_of_updated_erros_rows
                           
							);
						throw new GAppException(msg_ex);
					}

					// Convert Data Table to Data
					Data = importReport.ImportedObjects.Cast<TrainingLevel>().ToList();
					is_Insert_Or_Update = true;
                }

				

            }
            return Data;
        }


		/// <summary>
        /// Find the first TrainingLevel instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual TrainingLevel CreateOrLouadFirstTrainingLevel()
        {
            TrainingLevelBLO traininglevelBLO = new TrainingLevelBLO(UnitOfWork,GAppContext);
           
			TrainingLevel entity = null;
            if (traininglevelBLO.FindAll()?.Count > 0)
                entity = traininglevelBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp TrainingLevel for Test
                entity = this.CreateValideTrainingLevelInstance();
                traininglevelBLO.Save(entity);
            }
            return entity;
        }

		public virtual TrainingLevel Create_CRUD_TrainingLevel_Test_Instance()
        {
			TrainingLevel TrainingLevel = this.CreateValideTrainingLevelInstance();
            TrainingLevel.Reference = this.Entity_CRUD_Test_Reference;
            return TrainingLevel;
        }

        public virtual TrainingLevel CreateValideTrainingLevelInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            TrainingLevel  Valide_TrainingLevel = this._Fixture.Create<TrainingLevel>();
            Valide_TrainingLevel.Id = 0;
            // Many to One 
            //   
            // One to Many
            //
            return Valide_TrainingLevel;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide TrainingLevel can't exist</returns>
        public virtual TrainingLevel CreateInValideTrainingLevelInstance()
        {
            TrainingLevel traininglevel = this.CreateValideTrainingLevelInstance();
             
			// Required   
 
			traininglevel.Code = null;
 
			traininglevel.Name = null;
            //Unique
			var existant_TrainingLevel = this.CreateOrLouadFirstTrainingLevel();
			traininglevel.Reference = existant_TrainingLevel.Reference;
 
            return traininglevel;
        }


		public virtual TrainingLevel CreateInValideTrainingLevelInstance_ForEdit()
        {
            TrainingLevel traininglevel = this.CreateOrLouadFirstTrainingLevel();
			// Required   
 
			traininglevel.Code = null;
 
			traininglevel.Name = null;
            //Unique
			var existant_TrainingLevel = this.CreateOrLouadFirstTrainingLevel();
			traininglevel.Reference = existant_TrainingLevel.Reference;
            return traininglevel;
        }

		public override void Generate_Excel_File(List<TrainingLevel> generated_Data)
        {
            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/TrainingLevel.xlsx";

            var DataTeble = (this.BLO as TrainingLevelBLO).Convert_to_DataTable(generated_Data);
            (this.BLO as TrainingLevelBLO).ExcelDAO.Save(DataTeble, FileName);
        }
    }

	public partial class TrainingLevelTestDataFactory : BaseTrainingLevelTestDataFactory{
	
		public TrainingLevelTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
