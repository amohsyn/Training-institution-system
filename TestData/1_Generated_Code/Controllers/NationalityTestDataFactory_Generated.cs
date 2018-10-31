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
    public class BaseNationalityTestDataFactory : EntityTestData<Nationality>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new NationalityBLO(UnitOfWork, GAppContext);
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
                    // Convert Data Table to Data
                    Data = importReport.ImportedObjects.Cast<Nationality>().ToList();
                    is_Insert_Or_Update = true;
                }
                else
                {
                    Data = (this.BLO as NationalityBLO).Convert_DataTable_to_List(firstTable);
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
    }

	public partial class NationalityTestDataFactory : BaseNationalityTestDataFactory{
	
		public NationalityTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
