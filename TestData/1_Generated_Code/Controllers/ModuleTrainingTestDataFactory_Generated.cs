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
using TrainingIS.Models.ModuleTrainings;
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
    public class BaseModuleTrainingTestDataFactory : EntityTestData<ModuleTraining>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new ModuleTrainingBLO(UnitOfWork, GAppContext);
        }

        public BaseModuleTrainingTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<ModuleTraining> Load_Data_From_ExcelFile()
        {
            List<ModuleTraining> Data = null;

            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/ModuleTraining.xlsx";

            if (File.Exists(FileName))
            {
                Data = new List<ModuleTraining>();

                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                Data = (this.BLO as ModuleTrainingBLO).Convert_DataTable_to_List(firstTable);
            }
            return Data;
        }

        protected override List<ModuleTraining> Insert_Or_Update_ExcelFile_TestData(out bool is_Insert_Or_Update)
        {
            is_Insert_Or_Update = false;
            List<ModuleTraining> Data = new List<ModuleTraining>();

            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/ModuleTraining.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/ModuleTraining.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported
                if (!File.Exists(Repport_File))
                {
                   
                    ImportReport importReport = (this.BLO as ModuleTrainingBLO).Import(firstTable, FileName);
                    // Save ExcelRepport file to Server
                    DataSet DataSet_report = importReport.get_DataSet_Report();
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(DataSet_report);
                        wb.SaveAs(Repport_File);
                    }
                    // Convert Data Table to Data
                    Data = importReport.ImportedObjects.Cast<ModuleTraining>().ToList();
                    is_Insert_Or_Update = true;
                }
                else
                {
                    Data = (this.BLO as ModuleTrainingBLO).Convert_DataTable_to_List(firstTable);
                }
            }
            return Data;
        }


		/// <summary>
        /// Find the first ModuleTraining instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual ModuleTraining CreateOrLouadFirstModuleTraining()
        {
            ModuleTrainingBLO moduletrainingBLO = new ModuleTrainingBLO(UnitOfWork,GAppContext);
           
			ModuleTraining entity = null;
            if (moduletrainingBLO.FindAll()?.Count > 0)
                entity = moduletrainingBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp ModuleTraining for Test
                entity = this.CreateValideModuleTrainingInstance();
                moduletrainingBLO.Save(entity);
            }
            return entity;
        }

        public virtual ModuleTraining CreateValideModuleTrainingInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            ModuleTraining  Valide_ModuleTraining = this._Fixture.Create<ModuleTraining>();
            Valide_ModuleTraining.Id = 0;
            // Many to One 
            //   
			// Specialty
			var Specialty = new SpecialtyTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstSpecialty();
            Valide_ModuleTraining.Specialty = Specialty;
						 Valide_ModuleTraining.SpecialtyId = Specialty.Id;
			           
			// Metier
			var Metier = new MetierTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstMetier();
            Valide_ModuleTraining.Metier = Metier;
						 Valide_ModuleTraining.MetierId = Metier.Id;
			           
			// YearStudy
			var YearStudy = new YearStudyTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstYearStudy();
            Valide_ModuleTraining.YearStudy = YearStudy;
						 Valide_ModuleTraining.YearStudyId = YearStudy.Id;
			           
            // One to Many
            //
            return Valide_ModuleTraining;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide ModuleTraining can't exist</returns>
        public virtual ModuleTraining CreateInValideModuleTrainingInstance()
        {
            ModuleTraining moduletraining = this.CreateValideModuleTrainingInstance();
             
			// Required   
 
			moduletraining.SpecialtyId = 0;
 
			moduletraining.MetierId = 0;
 
			moduletraining.YearStudyId = 0;
 
			moduletraining.Name = null;
            //Unique
			var existant_ModuleTraining = this.CreateOrLouadFirstModuleTraining();
			moduletraining.Reference = existant_ModuleTraining.Reference;
 
            return moduletraining;
        }


		public virtual ModuleTraining CreateInValideModuleTrainingInstance_ForEdit()
        {
            ModuleTraining moduletraining = this.CreateOrLouadFirstModuleTraining();
			// Required   
 
			moduletraining.SpecialtyId = 0;
 
			moduletraining.MetierId = 0;
 
			moduletraining.YearStudyId = 0;
 
			moduletraining.Name = null;
            //Unique
			var existant_ModuleTraining = this.CreateOrLouadFirstModuleTraining();
			moduletraining.Reference = existant_ModuleTraining.Reference;
            return moduletraining;
        }
    }

	public partial class ModuleTrainingTestDataFactory : BaseModuleTrainingTestDataFactory{
	
		public ModuleTrainingTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
