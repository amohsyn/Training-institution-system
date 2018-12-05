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
    public class BaseSpecialtyTestDataFactory : EntityTestData<Specialty>
    {
		public string Entity_CRUD_Test_Reference { set; get; } 
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new SpecialtyBLO(UnitOfWork, GAppContext);
			Entity_CRUD_Test_Reference  = "Specialty_CRUD_Test";
        }

        public BaseSpecialtyTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

 
		protected override List<Specialty> Load_Data_From_ExcelFile()
        {
            List<Specialty> Data = null;

            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Specialty.xlsx";

            if (File.Exists(FileName))
            {
                Data = new List<Specialty>();

                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                Data = (this.BLO as SpecialtyBLO).Convert_DataTable_to_List(firstTable);
            }
            return Data;
        }

        protected override List<Specialty> Insert_Or_Update_ExcelFile_TestData(out bool is_Insert_Or_Update)
        {
            is_Insert_Or_Update = false;
            List<Specialty> Data = new List<Specialty>();

            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Specialty.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/Specialty.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported

				// Delete Repport_File if exist
                if (!File.Exists(Repport_File))
                {
					ImportReport importReport = (this.BLO as SpecialtyBLO).Import(firstTable, FileName);
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
								"Specialty",
							importReport.Number_of_inserted_erros_rows, 
							importReport.Number_of_updated_erros_rows
                           
							);
						throw new GAppException(msg_ex);
					}

					// Convert Data Table to Data
					Data = importReport.ImportedObjects.Cast<Specialty>().ToList();
					is_Insert_Or_Update = true;
                }

				

            }
            return Data;
        }


		/// <summary>
        /// Find the first Specialty instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Specialty CreateOrLouadFirstSpecialty()
        {
            SpecialtyBLO specialtyBLO = new SpecialtyBLO(UnitOfWork,GAppContext);
           
			Specialty entity = null;
            if (specialtyBLO.FindAll()?.Count > 0)
                entity = specialtyBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Specialty for Test
                entity = this.CreateValideSpecialtyInstance();
                specialtyBLO.Save(entity);
            }
            return entity;
        }

		public virtual Specialty Create_CRUD_Specialty_Test_Instance()
        {
			Specialty Specialty = this.CreateValideSpecialtyInstance();
            Specialty.Reference = this.Entity_CRUD_Test_Reference;
            return Specialty;
        }

        public virtual Specialty CreateValideSpecialtyInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            Specialty  Valide_Specialty = this._Fixture.Create<Specialty>();
            Valide_Specialty.Id = 0;
            // Many to One 
            //   
			// Sector
			var Sector = new SectorTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstSector();
            Valide_Specialty.Sector = Sector;
						 Valide_Specialty.SectorId = Sector.Id;
			           
			// TrainingLevel
			var TrainingLevel = new TrainingLevelTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstTrainingLevel();
            Valide_Specialty.TrainingLevel = TrainingLevel;
						 Valide_Specialty.TrainingLevelId = TrainingLevel.Id;
			           
            // One to Many
            //
            return Valide_Specialty;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Specialty can't exist</returns>
        public virtual Specialty CreateInValideSpecialtyInstance()
        {
            Specialty specialty = this.CreateValideSpecialtyInstance();
             
			// Required   
 
			specialty.SectorId = 0;
 
			specialty.TrainingLevelId = 0;
 
			specialty.Code = null;
 
			specialty.Name = null;
            //Unique
			var existant_Specialty = this.CreateOrLouadFirstSpecialty();
			specialty.Code = existant_Specialty.Code;
			specialty.Reference = existant_Specialty.Reference;
 
            return specialty;
        }


		public virtual Specialty CreateInValideSpecialtyInstance_ForEdit()
        {
            Specialty specialty = this.CreateOrLouadFirstSpecialty();
			// Required   
 
			specialty.SectorId = 0;
 
			specialty.TrainingLevelId = 0;
 
			specialty.Code = null;
 
			specialty.Name = null;
            //Unique
			var existant_Specialty = this.CreateOrLouadFirstSpecialty();
			specialty.Code = existant_Specialty.Code;
			specialty.Reference = existant_Specialty.Reference;
            return specialty;
        }

		public override void Generate_Excel_File(List<Specialty> generated_Data)
        {
            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Specialty.xlsx";

            var DataTeble = (this.BLO as SpecialtyBLO).Convert_to_DataTable(generated_Data);
            (this.BLO as SpecialtyBLO).ExcelDAO.Save(DataTeble, FileName);
        }
    }

	public partial class SpecialtyTestDataFactory : BaseSpecialtyTestDataFactory{
	
		public SpecialtyTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
