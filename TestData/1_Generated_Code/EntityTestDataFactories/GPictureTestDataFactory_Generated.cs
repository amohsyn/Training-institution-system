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
    public class BaseGPictureTestDataFactory : EntityTestData<GPicture>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new GPictureBLO(UnitOfWork, GAppContext);
        }

        public BaseGPictureTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<GPicture> Load_Data_From_ExcelFile()
        {
            List<GPicture> Data = null;

            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/GPicture.xlsx";

            if (File.Exists(FileName))
            {
                Data = new List<GPicture>();

                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                Data = (this.BLO as GPictureBLO).Convert_DataTable_to_List(firstTable);
            }
            return Data;
        }

        protected override List<GPicture> Insert_Or_Update_ExcelFile_TestData(out bool is_Insert_Or_Update)
        {
            is_Insert_Or_Update = false;
            List<GPicture> Data = new List<GPicture>();

            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/GPicture.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/GPicture.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported

				// Delete Repport_File if exist
                if (!File.Exists(Repport_File))
                {
					ImportReport importReport = (this.BLO as GPictureBLO).Import(firstTable, FileName);
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
								"GPicture",
							importReport.Number_of_inserted_erros_rows, 
							importReport.Number_of_updated_erros_rows
                           
							);
						throw new GAppException(msg_ex);
					}

					// Convert Data Table to Data
					Data = importReport.ImportedObjects.Cast<GPicture>().ToList();
					is_Insert_Or_Update = true;
                }

				

            }
            return Data;
        }


		/// <summary>
        /// Find the first GPicture instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual GPicture CreateOrLouadFirstGPicture()
        {
            GPictureBLO gpictureBLO = new GPictureBLO(UnitOfWork,GAppContext);
           
			GPicture entity = null;
            if (gpictureBLO.FindAll()?.Count > 0)
                entity = gpictureBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp GPicture for Test
                entity = this.CreateValideGPictureInstance();
                gpictureBLO.Save(entity);
            }
            return entity;
        }

        public virtual GPicture CreateValideGPictureInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            GPicture  Valide_GPicture = this._Fixture.Create<GPicture>();
            Valide_GPicture.Id = 0;
            // Many to One 
            //   
            // One to Many
            //
            return Valide_GPicture;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide GPicture can't exist</returns>
        public virtual GPicture CreateInValideGPictureInstance()
        {
            GPicture gpicture = this.CreateValideGPictureInstance();
             
			// Required   
            //Unique
			var existant_GPicture = this.CreateOrLouadFirstGPicture();
			gpicture.Reference = existant_GPicture.Reference;
 
            return gpicture;
        }


		public virtual GPicture CreateInValideGPictureInstance_ForEdit()
        {
            GPicture gpicture = this.CreateOrLouadFirstGPicture();
			// Required   
            //Unique
			var existant_GPicture = this.CreateOrLouadFirstGPicture();
			gpicture.Reference = existant_GPicture.Reference;
            return gpicture;
        }

		public override void Generate_Excel_File(List<GPicture> generated_Data)
        {
            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/GPicture.xlsx";

            var DataTeble = (this.BLO as GPictureBLO).Convert_to_DataTable(generated_Data);
            (this.BLO as GPictureBLO).ExcelDAO.Save(DataTeble, FileName);
        }
    }

	public partial class GPictureTestDataFactory : BaseGPictureTestDataFactory{
	
		public GPictureTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
