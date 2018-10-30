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

		protected override List<ClassroomCategory> Generate_TestData()
        {
            List<ClassroomCategory> Data = new List<ClassroomCategory>();
 
            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/ClassroomCategory_TestData.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/ClassroomCategory_TestData.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported
                if (File.Exists(Repport_File))
                {
                    ImportReport importReport = (this.BLO as ClassroomCategoryBLO).Import(firstTable, FileName);
                    // Save ExcelRepport file to Server
                    DataSet DataSet_report = importReport.get_DataSet_Report();
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(DataSet_report);
                        wb.SaveAs(Repport_File);
                    }
                    // Convert Data Table to Data
                    Data = importReport.ImportedObjects.Cast<ClassroomCategory>().ToList();
                }
                else
                {
                    Data = (this.BLO as ClassroomCategoryBLO).Convert_DataTable_to_List(firstTable);
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
    }

	public partial class ClassroomCategoryTestDataFactory : BaseClassroomCategoryTestDataFactory{
	
		public ClassroomCategoryTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
