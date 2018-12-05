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
    public class BaseProjectTestDataFactory : EntityTestData<Project>
    {
		public string Entity_CRUD_Test_Reference { set; get; } 
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new ProjectBLO(UnitOfWork, GAppContext);
			Entity_CRUD_Test_Reference  = "Project_CRUD_Test";
        }

        public BaseProjectTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

 
		protected override List<Project> Load_Data_From_ExcelFile()
        {
            List<Project> Data = null;

            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Project.xlsx";

            if (File.Exists(FileName))
            {
                Data = new List<Project>();

                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                Data = (this.BLO as ProjectBLO).Convert_DataTable_to_List(firstTable);
            }
            return Data;
        }

        protected override List<Project> Insert_Or_Update_ExcelFile_TestData(out bool is_Insert_Or_Update)
        {
            is_Insert_Or_Update = false;
            List<Project> Data = new List<Project>();

            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Project.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/Project.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported

				// Delete Repport_File if exist
                if (!File.Exists(Repport_File))
                {
					ImportReport importReport = (this.BLO as ProjectBLO).Import(firstTable, FileName);
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
								"Project",
							importReport.Number_of_inserted_erros_rows, 
							importReport.Number_of_updated_erros_rows
                           
							);
						throw new GAppException(msg_ex);
					}

					// Convert Data Table to Data
					Data = importReport.ImportedObjects.Cast<Project>().ToList();
					is_Insert_Or_Update = true;
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

		public virtual Project Create_CRUD_Project_Test_Instance()
        {
			Project Project = this.CreateValideProjectInstance();
            Project.Reference = this.Entity_CRUD_Test_Reference;
            return Project;
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

		public override void Generate_Excel_File(List<Project> generated_Data)
        {
            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Project.xlsx";

            var DataTeble = (this.BLO as ProjectBLO).Convert_to_DataTable(generated_Data);
            (this.BLO as ProjectBLO).ExcelDAO.Save(DataTeble, FileName);
        }
    }

	public partial class ProjectTestDataFactory : BaseProjectTestDataFactory{
	
		public ProjectTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
