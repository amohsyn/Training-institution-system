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
    public class BaseFormerSpecialtyTestDataFactory : EntityTestData<FormerSpecialty>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new FormerSpecialtyBLO(UnitOfWork, GAppContext);
        }

        public BaseFormerSpecialtyTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<FormerSpecialty> Generate_TestData()
        {
            List<FormerSpecialty> Data = new List<FormerSpecialty>();
 
            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/FormerSpecialty_TestData.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/FormerSpecialty_TestData.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported
                if (File.Exists(Repport_File))
                {
                    ImportReport importReport = (this.BLO as FormerSpecialtyBLO).Import(firstTable, FileName);
                    // Save ExcelRepport file to Server
                    DataSet DataSet_report = importReport.get_DataSet_Report();
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(DataSet_report);
                        wb.SaveAs(Repport_File);
                    }
                    // Convert Data Table to Data
                    Data = importReport.ImportedObjects.Cast<FormerSpecialty>().ToList();
                }
                else
                {
                    Data = (this.BLO as FormerSpecialtyBLO).Convert_DataTable_to_List(firstTable);
                }
            }
            return Data;
        }
	
		/// <summary>
        /// Find the first FormerSpecialty instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual FormerSpecialty CreateOrLouadFirstFormerSpecialty()
        {
            FormerSpecialtyBLO formerspecialtyBLO = new FormerSpecialtyBLO(UnitOfWork,GAppContext);
           
			FormerSpecialty entity = null;
            if (formerspecialtyBLO.FindAll()?.Count > 0)
                entity = formerspecialtyBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp FormerSpecialty for Test
                entity = this.CreateValideFormerSpecialtyInstance();
                formerspecialtyBLO.Save(entity);
            }
            return entity;
        }

        public virtual FormerSpecialty CreateValideFormerSpecialtyInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            FormerSpecialty  Valide_FormerSpecialty = this._Fixture.Create<FormerSpecialty>();
            Valide_FormerSpecialty.Id = 0;
            // Many to One 
            //   
            // One to Many
            //
            return Valide_FormerSpecialty;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide FormerSpecialty can't exist</returns>
        public virtual FormerSpecialty CreateInValideFormerSpecialtyInstance()
        {
            FormerSpecialty formerspecialty = this.CreateValideFormerSpecialtyInstance();
             
			// Required   
 
			formerspecialty.Code = null;
 
			formerspecialty.Name = null;
            //Unique
			var existant_FormerSpecialty = this.CreateOrLouadFirstFormerSpecialty();
			formerspecialty.Reference = existant_FormerSpecialty.Reference;
 
            return formerspecialty;
        }


		public virtual FormerSpecialty CreateInValideFormerSpecialtyInstance_ForEdit()
        {
            FormerSpecialty formerspecialty = this.CreateOrLouadFirstFormerSpecialty();
			// Required   
 
			formerspecialty.Code = null;
 
			formerspecialty.Name = null;
            //Unique
			var existant_FormerSpecialty = this.CreateOrLouadFirstFormerSpecialty();
			formerspecialty.Reference = existant_FormerSpecialty.Reference;
            return formerspecialty;
        }
    }

	public partial class FormerSpecialtyTestDataFactory : BaseFormerSpecialtyTestDataFactory{
	
		public FormerSpecialtyTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
