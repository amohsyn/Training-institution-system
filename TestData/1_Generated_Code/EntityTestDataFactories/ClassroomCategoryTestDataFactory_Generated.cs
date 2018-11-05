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
    public class BaseClassroomCategoryTestDataFactory : EntityTestData<ClassroomCategory>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new ClassroomCategoryBLO(UnitOfWork, GAppContext);
        }

        public BaseClassroomCategoryTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<ClassroomCategory> Load_Data_From_ExcelFile()
        {
            List<ClassroomCategory> Data = null;

            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/ClassroomCategory.xlsx";

            if (File.Exists(FileName))
            {
                Data = new List<ClassroomCategory>();

                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                Data = (this.BLO as ClassroomCategoryBLO).Convert_DataTable_to_List(firstTable);
            }
            return Data;
        }

        protected override List<ClassroomCategory> Insert_Or_Update_ExcelFile_TestData(out bool is_Insert_Or_Update)
        {
            is_Insert_Or_Update = false;
            List<ClassroomCategory> Data = new List<ClassroomCategory>();

            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/ClassroomCategory.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/ClassroomCategory.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported

				// Delete Repport_File if exist
                if (!File.Exists(Repport_File))
                {
					ImportReport importReport = (this.BLO as ClassroomCategoryBLO).Import(firstTable, FileName);
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
								"ClassroomCategory",
							importReport.Number_of_inserted_erros_rows, 
							importReport.Number_of_updated_erros_rows
                           
							);
						throw new GAppException(msg_ex);
					}

					// Convert Data Table to Data
					Data = importReport.ImportedObjects.Cast<ClassroomCategory>().ToList();
					is_Insert_Or_Update = true;
                }

				

            }
            return Data;
        }


		/// <summary>
        /// Find the first ClassroomCategory instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual ClassroomCategory CreateOrLouadFirstClassroomCategory()
        {
            ClassroomCategoryBLO classroomcategoryBLO = new ClassroomCategoryBLO(UnitOfWork,GAppContext);
           
			ClassroomCategory entity = null;
            if (classroomcategoryBLO.FindAll()?.Count > 0)
                entity = classroomcategoryBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp ClassroomCategory for Test
                entity = this.CreateValideClassroomCategoryInstance();
                classroomcategoryBLO.Save(entity);
            }
            return entity;
        }

        public virtual ClassroomCategory CreateValideClassroomCategoryInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            ClassroomCategory  Valide_ClassroomCategory = this._Fixture.Create<ClassroomCategory>();
            Valide_ClassroomCategory.Id = 0;
            // Many to One 
            //   
            // One to Many
            //
            return Valide_ClassroomCategory;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide ClassroomCategory can't exist</returns>
        public virtual ClassroomCategory CreateInValideClassroomCategoryInstance()
        {
            ClassroomCategory classroomcategory = this.CreateValideClassroomCategoryInstance();
             
			// Required   
 
			classroomcategory.Code = null;
            //Unique
			var existant_ClassroomCategory = this.CreateOrLouadFirstClassroomCategory();
			classroomcategory.Code = existant_ClassroomCategory.Code;
			classroomcategory.Reference = existant_ClassroomCategory.Reference;
 
            return classroomcategory;
        }


		public virtual ClassroomCategory CreateInValideClassroomCategoryInstance_ForEdit()
        {
            ClassroomCategory classroomcategory = this.CreateOrLouadFirstClassroomCategory();
			// Required   
 
			classroomcategory.Code = null;
            //Unique
			var existant_ClassroomCategory = this.CreateOrLouadFirstClassroomCategory();
			classroomcategory.Code = existant_ClassroomCategory.Code;
			classroomcategory.Reference = existant_ClassroomCategory.Reference;
            return classroomcategory;
        }

		public override void Generate_Excel_File(List<ClassroomCategory> generated_Data)
        {
            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/ClassroomCategory.xlsx";

            var DataTeble = (this.BLO as ClassroomCategoryBLO).Convert_to_DataTable(generated_Data);
            (this.BLO as ClassroomCategoryBLO).ExcelDAO.Save(DataTeble, FileName);
        }
    }

	public partial class ClassroomCategoryTestDataFactory : BaseClassroomCategoryTestDataFactory{
	
		public ClassroomCategoryTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
