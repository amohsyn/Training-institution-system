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
    public class BaseTaskProjectTestDataFactory : EntityTestData<TaskProject>
    {
		public string Entity_CRUD_Test_Reference { set; get; } 
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new TaskProjectBLO(UnitOfWork, GAppContext);
			Entity_CRUD_Test_Reference  = "TaskProject_CRUD_Test";
        }

        public BaseTaskProjectTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

 
		protected override List<TaskProject> Load_Data_From_ExcelFile()
        {
            List<TaskProject> Data = null;

            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/TaskProject.xlsx";

            if (File.Exists(FileName))
            {
                Data = new List<TaskProject>();

                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                Data = (this.BLO as TaskProjectBLO).Convert_DataTable_to_List(firstTable);
            }
            return Data;
        }

        protected override List<TaskProject> Insert_Or_Update_ExcelFile_TestData(out bool is_Insert_Or_Update)
        {
            is_Insert_Or_Update = false;
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

				// Delete Repport_File if exist
                if (!File.Exists(Repport_File))
                {
					ImportReport importReport = (this.BLO as TaskProjectBLO).Import(firstTable, FileName);
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
								"TaskProject",
							importReport.Number_of_inserted_erros_rows, 
							importReport.Number_of_updated_erros_rows
                           
							);
						throw new GAppException(msg_ex);
					}

					// Convert Data Table to Data
					Data = importReport.ImportedObjects.Cast<TaskProject>().ToList();
					is_Insert_Or_Update = true;
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

		public virtual TaskProject Create_CRUD_TaskProject_Test_Instance()
        {
			TaskProject TaskProject = this.CreateValideTaskProjectInstance();
            TaskProject.Reference = this.Entity_CRUD_Test_Reference;
            return TaskProject;
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

		public override void Generate_Excel_File(List<TaskProject> generated_Data)
        {
            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/TaskProject.xlsx";

            var DataTeble = (this.BLO as TaskProjectBLO).Convert_to_DataTable(generated_Data);
            (this.BLO as TaskProjectBLO).ExcelDAO.Save(DataTeble, FileName);
        }
    }

	public partial class TaskProjectTestDataFactory : BaseTaskProjectTestDataFactory{
	
		public TaskProjectTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
