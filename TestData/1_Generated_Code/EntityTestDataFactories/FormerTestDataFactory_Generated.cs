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
using TrainingIS.Models.FormerModelsViews;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities.ModelsViews.FormerModelsViews;
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
    public class BaseFormerTestDataFactory : EntityTestData<Former>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new FormerBLO(UnitOfWork, GAppContext);
        }

        public BaseFormerTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<Former> Load_Data_From_ExcelFile()
        {
            List<Former> Data = null;

            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Former.xlsx";

            if (File.Exists(FileName))
            {
                Data = new List<Former>();

                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                Data = (this.BLO as FormerBLO).Convert_DataTable_to_List(firstTable);
            }
            return Data;
        }

        protected override List<Former> Insert_Or_Update_ExcelFile_TestData(out bool is_Insert_Or_Update)
        {
            is_Insert_Or_Update = false;
            List<Former> Data = new List<Former>();

            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Former.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/Former.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported

				// Delete Repport_File if exist
                if (!File.Exists(Repport_File))
                {
					ImportReport importReport = (this.BLO as FormerBLO).Import(firstTable, FileName);
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
								"Former",
							importReport.Number_of_inserted_erros_rows, 
							importReport.Number_of_updated_erros_rows
                           
							);
						throw new GAppException(msg_ex);
					}

					// Convert Data Table to Data
					Data = importReport.ImportedObjects.Cast<Former>().ToList();
					is_Insert_Or_Update = true;
                }

				

            }
            return Data;
        }


		/// <summary>
        /// Find the first Former instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Former CreateOrLouadFirstFormer()
        {
            FormerBLO formerBLO = new FormerBLO(UnitOfWork,GAppContext);
           
			Former entity = null;
            if (formerBLO.FindAll()?.Count > 0)
                entity = formerBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Former for Test
                entity = this.CreateValideFormerInstance();
                formerBLO.Save(entity);
            }
            return entity;
        }

        public virtual Former CreateValideFormerInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            Former  Valide_Former = this._Fixture.Create<Former>();
            Valide_Former.Id = 0;
            // Many to One 
            //   
			// Photo
			var Photo = new GPictureTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstGPicture();
            Valide_Former.Photo = Photo;
			           
			// FormerSpecialty
			var FormerSpecialty = new FormerSpecialtyTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstFormerSpecialty();
            Valide_Former.FormerSpecialty = FormerSpecialty;
						 Valide_Former.FormerSpecialtyId = FormerSpecialty.Id;
			           
			// Nationality
			var Nationality = new NationalityTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstNationality();
            Valide_Former.Nationality = Nationality;
						 Valide_Former.NationalityId = Nationality.Id;
			           
            // One to Many
            //
			Valide_Former.Member_To_WorkGroups = null;
            return Valide_Former;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Former can't exist</returns>
        public virtual Former CreateInValideFormerInstance()
        {
            Former former = this.CreateValideFormerInstance();
             
			// Required   
 
			former.FormerSpecialtyId = 0;
 
			former.RegistrationNumber = null;
 
			former.Login = null;
 
			former.Password = null;
 
			former.FirstName = null;
 
			former.LastName = null;
 
			former.Sex = SexEnum.man;
            //Unique
			var existant_Former = this.CreateOrLouadFirstFormer();
			former.RegistrationNumber = existant_Former.RegistrationNumber;
			former.CIN = existant_Former.CIN;
			former.Email = existant_Former.Email;
			former.Reference = existant_Former.Reference;
 
            return former;
        }


		public virtual Former CreateInValideFormerInstance_ForEdit()
        {
            Former former = this.CreateOrLouadFirstFormer();
			// Required   
 
			former.FormerSpecialtyId = 0;
 
			former.RegistrationNumber = null;
 
			former.Login = null;
 
			former.Password = null;
 
			former.FirstName = null;
 
			former.LastName = null;
 
			former.Sex = SexEnum.man;
            //Unique
			var existant_Former = this.CreateOrLouadFirstFormer();
			former.RegistrationNumber = existant_Former.RegistrationNumber;
			former.CIN = existant_Former.CIN;
			former.Email = existant_Former.Email;
			former.Reference = existant_Former.Reference;
            return former;
        }

		public override void Generate_Excel_File(List<Former> generated_Data)
        {
            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Former.xlsx";

            var DataTeble = (this.BLO as FormerBLO).Convert_to_DataTable(generated_Data);
            (this.BLO as FormerBLO).ExcelDAO.Save(DataTeble, FileName);
        }
    }

	public partial class FormerTestDataFactory : BaseFormerTestDataFactory{
	
		public FormerTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
