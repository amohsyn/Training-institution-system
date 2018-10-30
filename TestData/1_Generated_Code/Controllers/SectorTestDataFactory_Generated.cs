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
    public class BaseSectorTestDataFactory : EntityTestData<Sector>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new SectorBLO(UnitOfWork, GAppContext);
        }

        public BaseSectorTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<Sector> Generate_TestData()
        {
            List<Sector> Data = new List<Sector>();
 
            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Sector_TestData.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/Sector_TestData.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported
                if (File.Exists(Repport_File))
                {
                    ImportReport importReport = (this.BLO as SectorBLO).Import(firstTable, FileName);
                    // Save ExcelRepport file to Server
                    DataSet DataSet_report = importReport.get_DataSet_Report();
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(DataSet_report);
                        wb.SaveAs(Repport_File);
                    }
                    // Convert Data Table to Data
                    Data = importReport.ImportedObjects.Cast<Sector>().ToList();
                }
                else
                {
                    Data = (this.BLO as SectorBLO).Convert_DataTable_to_List(firstTable);
                }
            }
            return Data;
        }
	
		/// <summary>
        /// Find the first Sector instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Sector CreateOrLouadFirstSector()
        {
            SectorBLO sectorBLO = new SectorBLO(UnitOfWork,GAppContext);
           
			Sector entity = null;
            if (sectorBLO.FindAll()?.Count > 0)
                entity = sectorBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Sector for Test
                entity = this.CreateValideSectorInstance();
                sectorBLO.Save(entity);
            }
            return entity;
        }

        public virtual Sector CreateValideSectorInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            Sector  Valide_Sector = this._Fixture.Create<Sector>();
            Valide_Sector.Id = 0;
            // Many to One 
            //   
            // One to Many
            //
            return Valide_Sector;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Sector can't exist</returns>
        public virtual Sector CreateInValideSectorInstance()
        {
            Sector sector = this.CreateValideSectorInstance();
             
			// Required   
 
			sector.Code = null;
 
			sector.Name = null;
            //Unique
			var existant_Sector = this.CreateOrLouadFirstSector();
			sector.Reference = existant_Sector.Reference;
 
            return sector;
        }


		public virtual Sector CreateInValideSectorInstance_ForEdit()
        {
            Sector sector = this.CreateOrLouadFirstSector();
			// Required   
 
			sector.Code = null;
 
			sector.Name = null;
            //Unique
			var existant_Sector = this.CreateOrLouadFirstSector();
			sector.Reference = existant_Sector.Reference;
            return sector;
        }
    }

	public partial class SectorTestDataFactory : BaseSectorTestDataFactory{
	
		public SectorTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
