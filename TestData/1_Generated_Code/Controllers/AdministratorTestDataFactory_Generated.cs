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
    public class BaseAdministratorTestDataFactory : EntityTestData<Administrator>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new AdministratorBLO(UnitOfWork, GAppContext);
        }

        public BaseAdministratorTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<Administrator> Generate_TestData()
        {
            List<Administrator> Data = new List<Administrator>();
 
            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Administrator_TestData.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/Administrator_TestData.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported
                if (File.Exists(Repport_File))
                {
                    ImportReport importReport = (this.BLO as AdministratorBLO).Import(firstTable, FileName);
                    // Save ExcelRepport file to Server
                    DataSet DataSet_report = importReport.get_DataSet_Report();
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(DataSet_report);
                        wb.SaveAs(Repport_File);
                    }
                    // Convert Data Table to Data
                    Data = importReport.ImportedObjects.Cast<Administrator>().ToList();
                }
                else
                {
                    Data = (this.BLO as AdministratorBLO).Convert_DataTable_to_List(firstTable);
                }
            }
            return Data;
        }
	
		/// <summary>
        /// Find the first Administrator instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Administrator CreateOrLouadFirstAdministrator()
        {
            AdministratorBLO administratorBLO = new AdministratorBLO(UnitOfWork,GAppContext);
           
			Administrator entity = null;
            if (administratorBLO.FindAll()?.Count > 0)
                entity = administratorBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Administrator for Test
                entity = this.CreateValideAdministratorInstance();
                administratorBLO.Save(entity);
            }
            return entity;
        }

        public virtual Administrator CreateValideAdministratorInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            Administrator  Valide_Administrator = this._Fixture.Create<Administrator>();
            Valide_Administrator.Id = 0;
            // Many to One 
            //   
			// Photo
			var Photo = new GPictureTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstGPicture();
            Valide_Administrator.Photo = Photo;
			           
			// Nationality
			var Nationality = new NationalityTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstNationality();
            Valide_Administrator.Nationality = Nationality;
						 Valide_Administrator.NationalityId = Nationality.Id;
			           
            // One to Many
            //
			Valide_Administrator.Member_To_WorkGroups = null;
            return Valide_Administrator;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Administrator can't exist</returns>
        public virtual Administrator CreateInValideAdministratorInstance()
        {
            Administrator administrator = this.CreateValideAdministratorInstance();
             
			// Required   
 
			administrator.RegistrationNumber = null;
 
			administrator.Login = null;
 
			administrator.Password = null;
 
			administrator.FirstName = null;
 
			administrator.LastName = null;
 
			administrator.Sex = SexEnum.man;
            //Unique
			var existant_Administrator = this.CreateOrLouadFirstAdministrator();
			administrator.RegistrationNumber = existant_Administrator.RegistrationNumber;
			administrator.CIN = existant_Administrator.CIN;
			administrator.Email = existant_Administrator.Email;
			administrator.Reference = existant_Administrator.Reference;
 
            return administrator;
        }


		public virtual Administrator CreateInValideAdministratorInstance_ForEdit()
        {
            Administrator administrator = this.CreateOrLouadFirstAdministrator();
			// Required   
 
			administrator.RegistrationNumber = null;
 
			administrator.Login = null;
 
			administrator.Password = null;
 
			administrator.FirstName = null;
 
			administrator.LastName = null;
 
			administrator.Sex = SexEnum.man;
            //Unique
			var existant_Administrator = this.CreateOrLouadFirstAdministrator();
			administrator.RegistrationNumber = existant_Administrator.RegistrationNumber;
			administrator.CIN = existant_Administrator.CIN;
			administrator.Email = existant_Administrator.Email;
			administrator.Reference = existant_Administrator.Reference;
            return administrator;
        }
    }

	public partial class AdministratorTestDataFactory : BaseAdministratorTestDataFactory{
	
		public AdministratorTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
