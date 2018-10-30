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
    public class BaseProjectTestDataFactory : EntityTestData<Project>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new ProjectBLO(UnitOfWork, GAppContext);
        }

        public BaseProjectTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<Project> Generate_TestData()
        {
            List<Project> Data = new List<Project>();
 
            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Project_TestData.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/Project_TestData.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported
                if (File.Exists(Repport_File))
                {
                    ImportReport importReport = (this.BLO as ProjectBLO).Import(firstTable, FileName);
                    // Save ExcelRepport file to Server
                    DataSet DataSet_report = importReport.get_DataSet_Report();
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(DataSet_report);
                        wb.SaveAs(Repport_File);
                    }
                    // Convert Data Table to Data
                    Data = importReport.ImportedObjects.Cast<Project>().ToList();
                }
                else
                {
                    Data = (this.BLO as ProjectBLO).Convert_DataTable_to_List(firstTable);
                }
            }
            return Data;
        }
	
		/// <summary>
        /// Find the first Project instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Project CreateOrLouadFirstProject()
        {
            ProjectBLO projectBLO = new ProjectBLO(UnitOfWork,GAppContext);
           
			Project entity = null;
            if (projectBLO.FindAll()?.Count > 0)
                entity = projectBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Project for Test
                entity = this.CreateValideProjectInstance();
                projectBLO.Save(entity);
            }
            return entity;
        }

        public virtual Project CreateValideProjectInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            Project  Valide_Project = this._Fixture.Create<Project>();
            Valide_Project.Id = 0;
            // Many to One 
            //   
			// Owner
			var Owner = new ApplicationUserTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstApplicationUser();
            Valide_Project.Owner = Owner;
						 Valide_Project.OwnerId = Owner.Id;
			           
            // One to Many
            //
            return Valide_Project;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Project can't exist</returns>
        public virtual Project CreateInValideProjectInstance()
        {
            Project project = this.CreateValideProjectInstance();
             
			// Required   
 
			project.Owner = null;
 
			project.Name = null;
            //Unique
			var existant_Project = this.CreateOrLouadFirstProject();
			project.Reference = existant_Project.Reference;
 
            return project;
        }


		public virtual Project CreateInValideProjectInstance_ForEdit()
        {
            Project project = this.CreateOrLouadFirstProject();
			// Required   
 
			project.Owner = null;
 
			project.Name = null;
            //Unique
			var existant_Project = this.CreateOrLouadFirstProject();
			project.Reference = existant_Project.Reference;
            return project;
        }
    }

	public partial class ProjectTestDataFactory : BaseProjectTestDataFactory{
	
		public ProjectTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
