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

		protected override List<Function> Generate_TestData()
        {
            List<Function> Data = new List<Function>();
 
            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Function_TestData.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/Function_TestData.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported
                if (File.Exists(Repport_File))
                {
                    ImportReport importReport = (this.BLO as FunctionBLO).Import(firstTable, FileName);
                    // Save ExcelRepport file to Server
                    DataSet DataSet_report = importReport.get_DataSet_Report();
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(DataSet_report);
                        wb.SaveAs(Repport_File);
                    }
                    // Convert Data Table to Data
                    Data = importReport.ImportedObjects.Cast<Function>().ToList();
                }
                else
                {
                    Data = (this.BLO as FunctionBLO).Convert_DataTable_to_List(firstTable);
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
    }

	public partial class FunctionTestDataFactory : BaseFunctionTestDataFactory{
	
		public FunctionTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
