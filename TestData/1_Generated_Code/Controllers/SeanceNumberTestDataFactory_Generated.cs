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
    public class BaseSeanceNumberTestDataFactory : EntityTestData<SeanceNumber>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new SeanceNumberBLO(UnitOfWork, GAppContext);
        }

        public BaseSeanceNumberTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<SeanceNumber> Generate_TestData()
        {
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
                if (File.Exists(Repport_File))
                {
                    ImportReport importReport = (this.BLO as SeanceNumberBLO).Import(firstTable, FileName);
                    // Save ExcelRepport file to Server
                    DataSet DataSet_report = importReport.get_DataSet_Report();
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(DataSet_report);
                        wb.SaveAs(Repport_File);
                    }
                    // Convert Data Table to Data
                    Data = importReport.ImportedObjects.Cast<SeanceNumber>().ToList();
                }
                else
                {
                    Data = (this.BLO as SeanceNumberBLO).Convert_DataTable_to_List(firstTable);
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
    }

	public partial class SeanceNumberTestDataFactory : BaseSeanceNumberTestDataFactory{
	
		public SeanceNumberTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
