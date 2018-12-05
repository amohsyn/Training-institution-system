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
    public class BaseNationalityTestDataFactory : EntityTestData<Nationality>
    {
		public string Entity_CRUD_Test_Reference { set; get; } 
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new NationalityBLO(UnitOfWork, GAppContext);
			Entity_CRUD_Test_Reference  = "Nationality_CRUD_Test";
        }

        public BaseNationalityTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

 
		protected override List<Nationality> Load_Data_From_ExcelFile()
        {
            List<Nationality> Data = null;

            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Nationality.xlsx";

            if (File.Exists(FileName))
            {
                Data = new List<Nationality>();

                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                Data = (this.BLO as NationalityBLO).Convert_DataTable_to_List(firstTable);
            }
            return Data;
        }

        protected override List<Nationality> Insert_Or_Update_ExcelFile_TestData(out bool is_Insert_Or_Update)
        {
            is_Insert_Or_Update = false;
            List<Nationality> Data = new List<Nationality>();

            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Nationality.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/Nationality.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported

				// Delete Repport_File if exist
                if (!File.Exists(Repport_File))
                {
					ImportReport importReport = (this.BLO as NationalityBLO).Import(firstTable, FileName);
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
								"Nationality",
							importReport.Number_of_inserted_erros_rows, 
							importReport.Number_of_updated_erros_rows
                           
							);
						throw new GAppException(msg_ex);
					}

					// Convert Data Table to Data
					Data = importReport.ImportedObjects.Cast<Nationality>().ToList();
					is_Insert_Or_Update = true;
                }

				

            }
            return Data;
        }


		/// <summary>
        /// Find the first Nationality instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Nationality CreateOrLouadFirstNationality()
        {
            NationalityBLO nationalityBLO = new NationalityBLO(UnitOfWork,GAppContext);
           
			Nationality entity = null;
            if (nationalityBLO.FindAll()?.Count > 0)
                entity = nationalityBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Nationality for Test
                entity = this.CreateValideNationalityInstance();
                nationalityBLO.Save(entity);
            }
            return entity;
        }

		public virtual Nationality Create_CRUD_Nationality_Test_Instance()
        {
			Nationality Nationality = this.CreateValideNationalityInstance();
            Nationality.Reference = this.Entity_CRUD_Test_Reference;
            return Nationality;
        }

        public virtual Nationality CreateValideNationalityInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            Nationality  Valide_Nationality = this._Fixture.Create<Nationality>();
            Valide_Nationality.Id = 0;
            // Many to One 
            //   
            // One to Many
            //
            return Valide_Nationality;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Nationality can't exist</returns>
        public virtual Nationality CreateInValideNationalityInstance()
        {
            Nationality nationality = this.CreateValideNationalityInstance();
             
			// Required   
 
			nationality.Code = null;
 
			nationality.Name = null;
            //Unique
			var existant_Nationality = this.CreateOrLouadFirstNationality();
			nationality.Code = existant_Nationality.Code;
			nationality.Reference = existant_Nationality.Reference;
 
            return nationality;
        }


		public virtual Nationality CreateInValideNationalityInstance_ForEdit()
        {
            Nationality nationality = this.CreateOrLouadFirstNationality();
			// Required   
 
			nationality.Code = null;
 
			nationality.Name = null;
            //Unique
			var existant_Nationality = this.CreateOrLouadFirstNationality();
			nationality.Code = existant_Nationality.Code;
			nationality.Reference = existant_Nationality.Reference;
            return nationality;
        }

		public override void Generate_Excel_File(List<Nationality> generated_Data)
        {
            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Nationality.xlsx";

            var DataTeble = (this.BLO as NationalityBLO).Convert_to_DataTable(generated_Data);
            (this.BLO as NationalityBLO).ExcelDAO.Save(DataTeble, FileName);
        }
    }

	public partial class NationalityTestDataFactory : BaseNationalityTestDataFactory{
	
		public NationalityTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
