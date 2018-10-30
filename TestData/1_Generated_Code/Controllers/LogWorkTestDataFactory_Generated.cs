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
    public class BaseLogWorkTestDataFactory : EntityTestData<LogWork>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new LogWorkBLO(UnitOfWork, GAppContext);
        }

        public BaseLogWorkTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<LogWork> Generate_TestData()
        {
            List<LogWork> Data = new List<LogWork>();
 
            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/LogWork_TestData.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/LogWork_TestData.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported
                if (File.Exists(Repport_File))
                {
                    ImportReport importReport = (this.BLO as LogWorkBLO).Import(firstTable, FileName);
                    // Save ExcelRepport file to Server
                    DataSet DataSet_report = importReport.get_DataSet_Report();
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(DataSet_report);
                        wb.SaveAs(Repport_File);
                    }
                    // Convert Data Table to Data
                    Data = importReport.ImportedObjects.Cast<LogWork>().ToList();
                }
                else
                {
                    Data = (this.BLO as LogWorkBLO).Convert_DataTable_to_List(firstTable);
                }
            }
            return Data;
        }
	
		/// <summary>
        /// Find the first LogWork instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual LogWork CreateOrLouadFirstLogWork()
        {
            LogWorkBLO logworkBLO = new LogWorkBLO(UnitOfWork,GAppContext);
           
			LogWork entity = null;
            if (logworkBLO.FindAll()?.Count > 0)
                entity = logworkBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp LogWork for Test
                entity = this.CreateValideLogWorkInstance();
                logworkBLO.Save(entity);
            }
            return entity;
        }

        public virtual LogWork CreateValideLogWorkInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            LogWork  Valide_LogWork = this._Fixture.Create<LogWork>();
            Valide_LogWork.Id = 0;
            // Many to One 
            //   
            // One to Many
            //
            return Valide_LogWork;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide LogWork can't exist</returns>
        public virtual LogWork CreateInValideLogWorkInstance()
        {
            LogWork logwork = this.CreateValideLogWorkInstance();
             
			// Required   
 
			logwork.UserId = null;
 
			logwork.OperationWorkType = OperationWorkTypes.Import;
            //Unique
			var existant_LogWork = this.CreateOrLouadFirstLogWork();
			logwork.Reference = existant_LogWork.Reference;
 
            return logwork;
        }


		public virtual LogWork CreateInValideLogWorkInstance_ForEdit()
        {
            LogWork logwork = this.CreateOrLouadFirstLogWork();
			// Required   
 
			logwork.UserId = null;
 
			logwork.OperationWorkType = OperationWorkTypes.Import;
            //Unique
			var existant_LogWork = this.CreateOrLouadFirstLogWork();
			logwork.Reference = existant_LogWork.Reference;
            return logwork;
        }
    }

	public partial class LogWorkTestDataFactory : BaseLogWorkTestDataFactory{
	
		public LogWorkTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
