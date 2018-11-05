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
    public class BaseFunctionTestDataFactory : EntityTestData<Function>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new FunctionBLO(UnitOfWork, GAppContext);
        }

        public BaseFunctionTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<Function> Load_Data_From_ExcelFile()
        {
            List<Function> Data = null;

            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Function.xlsx";

            if (File.Exists(FileName))
            {
                Data = new List<Function>();

                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                Data = (this.BLO as FunctionBLO).Convert_DataTable_to_List(firstTable);
            }
            return Data;
        }

        protected override List<Function> Insert_Or_Update_ExcelFile_TestData(out bool is_Insert_Or_Update)
        {
            is_Insert_Or_Update = false;
            List<Function> Data = new List<Function>();

            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Function.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/Function.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported

				// Delete Repport_File if exist
                if (!File.Exists(Repport_File))
                {
					ImportReport importReport = (this.BLO as FunctionBLO).Import(firstTable, FileName);
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
								"Function",
							importReport.Number_of_inserted_erros_rows, 
							importReport.Number_of_updated_erros_rows
                           
							);
						throw new GAppException(msg_ex);
					}

					// Convert Data Table to Data
					Data = importReport.ImportedObjects.Cast<Function>().ToList();
					is_Insert_Or_Update = true;
                }

				

            }
            return Data;
        }


		/// <summary>
        /// Find the first Function instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Function CreateOrLouadFirstFunction()
        {
            FunctionBLO functionBLO = new FunctionBLO(UnitOfWork,GAppContext);
           
			Function entity = null;
            if (functionBLO.FindAll()?.Count > 0)
                entity = functionBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Function for Test
                entity = this.CreateValideFunctionInstance();
                functionBLO.Save(entity);
            }
            return entity;
        }

        public virtual Function CreateValideFunctionInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            Function  Valide_Function = this._Fixture.Create<Function>();
            Valide_Function.Id = 0;
            // Many to One 
            //   
            // One to Many
            //
            return Valide_Function;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Function can't exist</returns>
        public virtual Function CreateInValideFunctionInstance()
        {
            Function function = this.CreateValideFunctionInstance();
             
			// Required   
 
			function.Code = null;
 
			function.Name = null;
            //Unique
			var existant_Function = this.CreateOrLouadFirstFunction();
			function.Reference = existant_Function.Reference;
 
            return function;
        }


		public virtual Function CreateInValideFunctionInstance_ForEdit()
        {
            Function function = this.CreateOrLouadFirstFunction();
			// Required   
 
			function.Code = null;
 
			function.Name = null;
            //Unique
			var existant_Function = this.CreateOrLouadFirstFunction();
			function.Reference = existant_Function.Reference;
            return function;
        }

		public override void Generate_Excel_File(List<Function> generated_Data)
        {
            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Function.xlsx";

            var DataTeble = (this.BLO as FunctionBLO).Convert_to_DataTable(generated_Data);
            (this.BLO as FunctionBLO).ExcelDAO.Save(DataTeble, FileName);
        }
    }

	public partial class FunctionTestDataFactory : BaseFunctionTestDataFactory{
	
		public FunctionTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
