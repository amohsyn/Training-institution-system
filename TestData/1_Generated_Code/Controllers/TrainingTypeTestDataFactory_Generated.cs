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
    public class BaseTrainingTypeTestDataFactory : EntityTestData<TrainingType>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new TrainingTypeBLO(UnitOfWork, GAppContext);
        }

        public BaseTrainingTypeTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<TrainingType> Load_Data_From_ExcelFile()
        {
            List<TrainingType> Data = null;

            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/TrainingType.xlsx";

            if (File.Exists(FileName))
            {
                Data = new List<TrainingType>();

                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                Data = (this.BLO as TrainingTypeBLO).Convert_DataTable_to_List(firstTable);
            }
            return Data;
        }

        protected override List<TrainingType> Insert_Or_Update_ExcelFile_TestData(out bool is_Insert_Or_Update)
        {
            is_Insert_Or_Update = false;
            List<TrainingType> Data = new List<TrainingType>();

            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/TrainingType.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/TrainingType.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported
                if (!File.Exists(Repport_File))
                {
                   
                    ImportReport importReport = (this.BLO as TrainingTypeBLO).Import(firstTable, FileName);
                    // Save ExcelRepport file to Server
                    DataSet DataSet_report = importReport.get_DataSet_Report();
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(DataSet_report);
                        wb.SaveAs(Repport_File);
                    }
                    // Convert Data Table to Data
                    Data = importReport.ImportedObjects.Cast<TrainingType>().ToList();
                    is_Insert_Or_Update = true;
                }
                else
                {
                    Data = (this.BLO as TrainingTypeBLO).Convert_DataTable_to_List(firstTable);
                }
            }
            return Data;
        }


		/// <summary>
        /// Find the first TrainingType instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual TrainingType CreateOrLouadFirstTrainingType()
        {
            TrainingTypeBLO trainingtypeBLO = new TrainingTypeBLO(UnitOfWork,GAppContext);
           
			TrainingType entity = null;
            if (trainingtypeBLO.FindAll()?.Count > 0)
                entity = trainingtypeBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp TrainingType for Test
                entity = this.CreateValideTrainingTypeInstance();
                trainingtypeBLO.Save(entity);
            }
            return entity;
        }

        public virtual TrainingType CreateValideTrainingTypeInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            TrainingType  Valide_TrainingType = this._Fixture.Create<TrainingType>();
            Valide_TrainingType.Id = 0;
            // Many to One 
            //   
            // One to Many
            //
            return Valide_TrainingType;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide TrainingType can't exist</returns>
        public virtual TrainingType CreateInValideTrainingTypeInstance()
        {
            TrainingType trainingtype = this.CreateValideTrainingTypeInstance();
             
			// Required   
 
			trainingtype.Code = null;
 
			trainingtype.Name = null;
            //Unique
			var existant_TrainingType = this.CreateOrLouadFirstTrainingType();
			trainingtype.Code = existant_TrainingType.Code;
			trainingtype.Reference = existant_TrainingType.Reference;
 
            return trainingtype;
        }


		public virtual TrainingType CreateInValideTrainingTypeInstance_ForEdit()
        {
            TrainingType trainingtype = this.CreateOrLouadFirstTrainingType();
			// Required   
 
			trainingtype.Code = null;
 
			trainingtype.Name = null;
            //Unique
			var existant_TrainingType = this.CreateOrLouadFirstTrainingType();
			trainingtype.Code = existant_TrainingType.Code;
			trainingtype.Reference = existant_TrainingType.Reference;
            return trainingtype;
        }
    }

	public partial class TrainingTypeTestDataFactory : BaseTrainingTypeTestDataFactory{
	
		public TrainingTypeTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
