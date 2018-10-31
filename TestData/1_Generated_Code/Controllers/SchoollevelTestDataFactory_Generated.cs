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
    public class BaseSchoollevelTestDataFactory : EntityTestData<Schoollevel>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new SchoollevelBLO(UnitOfWork, GAppContext);
        }

        public BaseSchoollevelTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<Schoollevel> Load_Data_From_ExcelFile()
        {
            List<Schoollevel> Data = null;

            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Schoollevel.xlsx";

            if (File.Exists(FileName))
            {
                Data = new List<Schoollevel>();

                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                Data = (this.BLO as SchoollevelBLO).Convert_DataTable_to_List(firstTable);
            }
            return Data;
        }

        protected override List<Schoollevel> Insert_Or_Update_ExcelFile_TestData(out bool is_Insert_Or_Update)
        {
            is_Insert_Or_Update = false;
            List<Schoollevel> Data = new List<Schoollevel>();

            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Schoollevel.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/Schoollevel.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported
                if (!File.Exists(Repport_File))
                {
                   
                    ImportReport importReport = (this.BLO as SchoollevelBLO).Import(firstTable, FileName);
                    // Save ExcelRepport file to Server
                    DataSet DataSet_report = importReport.get_DataSet_Report();
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(DataSet_report);
                        wb.SaveAs(Repport_File);
                    }
                    // Convert Data Table to Data
                    Data = importReport.ImportedObjects.Cast<Schoollevel>().ToList();
                    is_Insert_Or_Update = true;
                }
                else
                {
                    Data = (this.BLO as SchoollevelBLO).Convert_DataTable_to_List(firstTable);
                }
            }
            return Data;
        }


		/// <summary>
        /// Find the first Schoollevel instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Schoollevel CreateOrLouadFirstSchoollevel()
        {
            SchoollevelBLO schoollevelBLO = new SchoollevelBLO(UnitOfWork,GAppContext);
           
			Schoollevel entity = null;
            if (schoollevelBLO.FindAll()?.Count > 0)
                entity = schoollevelBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Schoollevel for Test
                entity = this.CreateValideSchoollevelInstance();
                schoollevelBLO.Save(entity);
            }
            return entity;
        }

        public virtual Schoollevel CreateValideSchoollevelInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            Schoollevel  Valide_Schoollevel = this._Fixture.Create<Schoollevel>();
            Valide_Schoollevel.Id = 0;
            // Many to One 
            //   
            // One to Many
            //
            return Valide_Schoollevel;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Schoollevel can't exist</returns>
        public virtual Schoollevel CreateInValideSchoollevelInstance()
        {
            Schoollevel schoollevel = this.CreateValideSchoollevelInstance();
             
			// Required   
 
			schoollevel.Code = null;
 
			schoollevel.Name = null;
            //Unique
			var existant_Schoollevel = this.CreateOrLouadFirstSchoollevel();
			schoollevel.Code = existant_Schoollevel.Code;
			schoollevel.Reference = existant_Schoollevel.Reference;
 
            return schoollevel;
        }


		public virtual Schoollevel CreateInValideSchoollevelInstance_ForEdit()
        {
            Schoollevel schoollevel = this.CreateOrLouadFirstSchoollevel();
			// Required   
 
			schoollevel.Code = null;
 
			schoollevel.Name = null;
            //Unique
			var existant_Schoollevel = this.CreateOrLouadFirstSchoollevel();
			schoollevel.Code = existant_Schoollevel.Code;
			schoollevel.Reference = existant_Schoollevel.Reference;
            return schoollevel;
        }
    }

	public partial class SchoollevelTestDataFactory : BaseSchoollevelTestDataFactory{
	
		public SchoollevelTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
