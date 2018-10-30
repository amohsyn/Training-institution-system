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
    public class BaseTaskProjectTestDataFactory : EntityTestData<TaskProject>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new TaskProjectBLO(UnitOfWork, GAppContext);
        }

        public BaseTaskProjectTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<TaskProject> Generate_TestData()
        {
            List<TaskProject> Data = new List<TaskProject>();
 
            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/TaskProject.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/TaskProject.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported
                if (File.Exists(Repport_File))
                {
                    ImportReport importReport = (this.BLO as TaskProjectBLO).Import(firstTable, FileName);
                    // Save ExcelRepport file to Server
                    DataSet DataSet_report = importReport.get_DataSet_Report();
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(DataSet_report);
                        wb.SaveAs(Repport_File);
                    }
                    // Convert Data Table to Data
                    Data = importReport.ImportedObjects.Cast<TaskProject>().ToList();
                }
                else
                {
                    Data = (this.BLO as TaskProjectBLO).Convert_DataTable_to_List(firstTable);
                }
            }
            return Data;
        }
	
		/// <summary>
        /// Find the first TaskProject instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual TaskProject CreateOrLouadFirstTaskProject()
        {
            TaskProjectBLO taskprojectBLO = new TaskProjectBLO(UnitOfWork,GAppContext);
           
			TaskProject entity = null;
            if (taskprojectBLO.FindAll()?.Count > 0)
                entity = taskprojectBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp TaskProject for Test
                entity = this.CreateValideTaskProjectInstance();
                taskprojectBLO.Save(entity);
            }
            return entity;
        }

        public virtual TaskProject CreateValideTaskProjectInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            TaskProject  Valide_TaskProject = this._Fixture.Create<TaskProject>();
            Valide_TaskProject.Id = 0;
            // Many to One 
            //   
			// Project
			var Project = new ProjectTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstProject();
            Valide_TaskProject.Project = Project;
						 Valide_TaskProject.ProjectId = Project.Id;
			           
			// Owner
			var Owner = new ApplicationUserTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstApplicationUser();
            Valide_TaskProject.Owner = Owner;
						 Valide_TaskProject.OwnerId = Owner.Id;
			           
            // One to Many
            //
            return Valide_TaskProject;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide TaskProject can't exist</returns>
        public virtual TaskProject CreateInValideTaskProjectInstance()
        {
            TaskProject taskproject = this.CreateValideTaskProjectInstance();
             
			// Required   
 
			taskproject.Owner = null;
 
			taskproject.Name = null;
            //Unique
			var existant_TaskProject = this.CreateOrLouadFirstTaskProject();
			taskproject.Reference = existant_TaskProject.Reference;
 
            return taskproject;
        }


		public virtual TaskProject CreateInValideTaskProjectInstance_ForEdit()
        {
            TaskProject taskproject = this.CreateOrLouadFirstTaskProject();
			// Required   
 
			taskproject.Owner = null;
 
			taskproject.Name = null;
            //Unique
			var existant_TaskProject = this.CreateOrLouadFirstTaskProject();
			taskproject.Reference = existant_TaskProject.Reference;
            return taskproject;
        }
    }

	public partial class TaskProjectTestDataFactory : BaseTaskProjectTestDataFactory{
	
		public TaskProjectTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
