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
    public class BaseTrainingLevelTestDataFactory : EntityTestData<TrainingLevel>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new TrainingLevelBLO(UnitOfWork, GAppContext);
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
                    // Convert Data Table to Data
                    Data = importReport.ImportedObjects.Cast<TrainingLevel>().ToList();
                    is_Insert_Or_Update = true;
                }
                else
                {
                    Data = (this.BLO as TrainingLevelBLO).Convert_DataTable_to_List(firstTable);
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
    }

	public partial class TrainingLevelTestDataFactory : BaseTrainingLevelTestDataFactory{
	
		public TrainingLevelTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
