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
    public class BaseMetierTestDataFactory : EntityTestData<Metier>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new MetierBLO(UnitOfWork, GAppContext);
        }

        public BaseMetierTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<Metier> Load_Data_From_ExcelFile()
        {
            List<Metier> Data = null;

            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Metier.xlsx";

            if (File.Exists(FileName))
            {
                Data = new List<Metier>();

                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                Data = (this.BLO as MetierBLO).Convert_DataTable_to_List(firstTable);
            }
            return Data;
        }

        protected override List<Metier> Insert_Or_Update_ExcelFile_TestData(out bool is_Insert_Or_Update)
        {
            is_Insert_Or_Update = false;
            List<Metier> Data = new List<Metier>();

            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Metier.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/Metier.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported
                if (!File.Exists(Repport_File))
                {
                   
                    ImportReport importReport = (this.BLO as MetierBLO).Import(firstTable, FileName);
                    // Save ExcelRepport file to Server
                    DataSet DataSet_report = importReport.get_DataSet_Report();
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(DataSet_report);
                        wb.SaveAs(Repport_File);
                    }
                    // Convert Data Table to Data
                    Data = importReport.ImportedObjects.Cast<Metier>().ToList();
                    is_Insert_Or_Update = true;
                }
                else
                {
                    Data = (this.BLO as MetierBLO).Convert_DataTable_to_List(firstTable);
                }
            }
            return Data;
        }


		/// <summary>
        /// Find the first Metier instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Metier CreateOrLouadFirstMetier()
        {
            MetierBLO metierBLO = new MetierBLO(UnitOfWork,GAppContext);
           
			Metier entity = null;
            if (metierBLO.FindAll()?.Count > 0)
                entity = metierBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Metier for Test
                entity = this.CreateValideMetierInstance();
                metierBLO.Save(entity);
            }
            return entity;
        }

        public virtual Metier CreateValideMetierInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            Metier  Valide_Metier = this._Fixture.Create<Metier>();
            Valide_Metier.Id = 0;
            // Many to One 
            //   
            // One to Many
            //
            return Valide_Metier;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Metier can't exist</returns>
        public virtual Metier CreateInValideMetierInstance()
        {
            Metier metier = this.CreateValideMetierInstance();
             
			// Required   
 
			metier.Code = null;
 
			metier.Name = null;
            //Unique
			var existant_Metier = this.CreateOrLouadFirstMetier();
			metier.Reference = existant_Metier.Reference;
 
            return metier;
        }


		public virtual Metier CreateInValideMetierInstance_ForEdit()
        {
            Metier metier = this.CreateOrLouadFirstMetier();
			// Required   
 
			metier.Code = null;
 
			metier.Name = null;
            //Unique
			var existant_Metier = this.CreateOrLouadFirstMetier();
			metier.Reference = existant_Metier.Reference;
            return metier;
        }
    }

	public partial class MetierTestDataFactory : BaseMetierTestDataFactory{
	
		public MetierTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
