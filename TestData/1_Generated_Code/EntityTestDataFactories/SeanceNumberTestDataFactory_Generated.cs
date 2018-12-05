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
    public class BaseSeanceNumberTestDataFactory : EntityTestData<SeanceNumber>
    {
		public string Entity_CRUD_Test_Reference { set; get; } 
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new SeanceNumberBLO(UnitOfWork, GAppContext);
			Entity_CRUD_Test_Reference  = "SeanceNumber_CRUD_Test";
        }

        public BaseSeanceNumberTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

 
		protected override List<SeanceNumber> Load_Data_From_ExcelFile()
        {
            List<SeanceNumber> Data = null;

            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/SeanceNumber.xlsx";

            if (File.Exists(FileName))
            {
                Data = new List<SeanceNumber>();

                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                Data = (this.BLO as SeanceNumberBLO).Convert_DataTable_to_List(firstTable);
            }
            return Data;
        }

        protected override List<SeanceNumber> Insert_Or_Update_ExcelFile_TestData(out bool is_Insert_Or_Update)
        {
            is_Insert_Or_Update = false;
            List<SeanceNumber> Data = new List<SeanceNumber>();

            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/SeanceNumber.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/SeanceNumber.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported

				// Delete Repport_File if exist
                if (!File.Exists(Repport_File))
                {
					ImportReport importReport = (this.BLO as SeanceNumberBLO).Import(firstTable, FileName);
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
								"SeanceNumber",
							importReport.Number_of_inserted_erros_rows, 
							importReport.Number_of_updated_erros_rows
                           
							);
						throw new GAppException(msg_ex);
					}

					// Convert Data Table to Data
					Data = importReport.ImportedObjects.Cast<SeanceNumber>().ToList();
					is_Insert_Or_Update = true;
                }

				

            }
            return Data;
        }


		/// <summary>
        /// Find the first SeanceNumber instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual SeanceNumber CreateOrLouadFirstSeanceNumber()
        {
            SeanceNumberBLO seancenumberBLO = new SeanceNumberBLO(UnitOfWork,GAppContext);
           
			SeanceNumber entity = null;
            if (seancenumberBLO.FindAll()?.Count > 0)
                entity = seancenumberBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp SeanceNumber for Test
                entity = this.CreateValideSeanceNumberInstance();
                seancenumberBLO.Save(entity);
            }
            return entity;
        }

		public virtual SeanceNumber Create_CRUD_SeanceNumber_Test_Instance()
        {
			SeanceNumber SeanceNumber = this.CreateValideSeanceNumberInstance();
            SeanceNumber.Reference = this.Entity_CRUD_Test_Reference;
            return SeanceNumber;
        }

        public virtual SeanceNumber CreateValideSeanceNumberInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            SeanceNumber  Valide_SeanceNumber = this._Fixture.Create<SeanceNumber>();
            Valide_SeanceNumber.Id = 0;
            // Many to One 
            //   
            // One to Many
            //
            return Valide_SeanceNumber;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide SeanceNumber can't exist</returns>
        public virtual SeanceNumber CreateInValideSeanceNumberInstance()
        {
            SeanceNumber seancenumber = this.CreateValideSeanceNumberInstance();
             
			// Required   
 
			seancenumber.Code = null;
 
			seancenumber.StartTime = DateTime.Now;
 
			seancenumber.EndTime = DateTime.Now;
            //Unique
			var existant_SeanceNumber = this.CreateOrLouadFirstSeanceNumber();
			seancenumber.Code = existant_SeanceNumber.Code;
			seancenumber.Reference = existant_SeanceNumber.Reference;
 
            return seancenumber;
        }


		public virtual SeanceNumber CreateInValideSeanceNumberInstance_ForEdit()
        {
            SeanceNumber seancenumber = this.CreateOrLouadFirstSeanceNumber();
			// Required   
 
			seancenumber.Code = null;
 
			seancenumber.StartTime = DateTime.Now;
 
			seancenumber.EndTime = DateTime.Now;
            //Unique
			var existant_SeanceNumber = this.CreateOrLouadFirstSeanceNumber();
			seancenumber.Code = existant_SeanceNumber.Code;
			seancenumber.Reference = existant_SeanceNumber.Reference;
            return seancenumber;
        }

		public override void Generate_Excel_File(List<SeanceNumber> generated_Data)
        {
            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/SeanceNumber.xlsx";

            var DataTeble = (this.BLO as SeanceNumberBLO).Convert_to_DataTable(generated_Data);
            (this.BLO as SeanceNumberBLO).ExcelDAO.Save(DataTeble, FileName);
        }
    }

	public partial class SeanceNumberTestDataFactory : BaseSeanceNumberTestDataFactory{
	
		public SeanceNumberTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
