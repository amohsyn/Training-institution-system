﻿using System;
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
    public class BaseSpecialtyTestDataFactory : EntityTestData<Specialty>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new SpecialtyBLO(UnitOfWork, GAppContext);
        }

        public BaseSpecialtyTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<Specialty> Generate_TestData()
        {
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
                if (File.Exists(Repport_File))
                {
                    ImportReport importReport = (this.BLO as SpecialtyBLO).Import(firstTable, FileName);
                    // Save ExcelRepport file to Server
                    DataSet DataSet_report = importReport.get_DataSet_Report();
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(DataSet_report);
                        wb.SaveAs(Repport_File);
                    }
                    // Convert Data Table to Data
                    Data = importReport.ImportedObjects.Cast<Specialty>().ToList();
                }
                else
                {
                    Data = (this.BLO as SpecialtyBLO).Convert_DataTable_to_List(firstTable);
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
    }

	public partial class SpecialtyTestDataFactory : BaseSpecialtyTestDataFactory{
	
		public SpecialtyTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
