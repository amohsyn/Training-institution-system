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
    public class BaseClassroomTestDataFactory : EntityTestData<Classroom>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new ClassroomBLO(UnitOfWork, GAppContext);
        }

        public BaseClassroomTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<Classroom> Load_Data_From_ExcelFile()
        {
            List<Classroom> Data = null;

            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Classroom.xlsx";

            if (File.Exists(FileName))
            {
                Data = new List<Classroom>();

                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                Data = (this.BLO as ClassroomBLO).Convert_DataTable_to_List(firstTable);
            }
            return Data;
        }

        protected override List<Classroom> Insert_Or_Update_ExcelFile_TestData(out bool is_Insert_Or_Update)
        {
            is_Insert_Or_Update = false;
            List<Classroom> Data = new List<Classroom>();

            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Classroom.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/Classroom.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported

				// Delete Repport_File if exist
                if (!File.Exists(Repport_File))
                {
					ImportReport importReport = (this.BLO as ClassroomBLO).Import(firstTable, FileName);
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
								"Classroom",
							importReport.Number_of_inserted_erros_rows, 
							importReport.Number_of_updated_erros_rows
                           
							);
						throw new GAppException(msg_ex);
					}

					// Convert Data Table to Data
					Data = importReport.ImportedObjects.Cast<Classroom>().ToList();
					is_Insert_Or_Update = true;
                }

				

            }
            return Data;
        }


		/// <summary>
        /// Find the first Classroom instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Classroom CreateOrLouadFirstClassroom()
        {
            ClassroomBLO classroomBLO = new ClassroomBLO(UnitOfWork,GAppContext);
           
			Classroom entity = null;
            if (classroomBLO.FindAll()?.Count > 0)
                entity = classroomBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Classroom for Test
                entity = this.CreateValideClassroomInstance();
                classroomBLO.Save(entity);
            }
            return entity;
        }

        public virtual Classroom CreateValideClassroomInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            Classroom  Valide_Classroom = this._Fixture.Create<Classroom>();
            Valide_Classroom.Id = 0;
            // Many to One 
            //   
			// ClassroomCategory
			var ClassroomCategory = new ClassroomCategoryTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstClassroomCategory();
            Valide_Classroom.ClassroomCategory = ClassroomCategory;
						 Valide_Classroom.ClassroomCategoryId = ClassroomCategory.Id;
			           
            // One to Many
            //
            return Valide_Classroom;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Classroom can't exist</returns>
        public virtual Classroom CreateInValideClassroomInstance()
        {
            Classroom classroom = this.CreateValideClassroomInstance();
             
			// Required   
 
			classroom.Code = null;
 
			classroom.ClassroomCategoryId = 0;
            //Unique
			var existant_Classroom = this.CreateOrLouadFirstClassroom();
			classroom.Code = existant_Classroom.Code;
			classroom.Reference = existant_Classroom.Reference;
 
            return classroom;
        }


		public virtual Classroom CreateInValideClassroomInstance_ForEdit()
        {
            Classroom classroom = this.CreateOrLouadFirstClassroom();
			// Required   
 
			classroom.Code = null;
 
			classroom.ClassroomCategoryId = 0;
            //Unique
			var existant_Classroom = this.CreateOrLouadFirstClassroom();
			classroom.Code = existant_Classroom.Code;
			classroom.Reference = existant_Classroom.Reference;
            return classroom;
        }

		public override void Generate_Excel_File(List<Classroom> generated_Data)
        {
            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Classroom.xlsx";

            var DataTeble = (this.BLO as ClassroomBLO).Convert_to_DataTable(generated_Data);
            (this.BLO as ClassroomBLO).ExcelDAO.Save(DataTeble, FileName);
        }
    }

	public partial class ClassroomTestDataFactory : BaseClassroomTestDataFactory{
	
		public ClassroomTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
