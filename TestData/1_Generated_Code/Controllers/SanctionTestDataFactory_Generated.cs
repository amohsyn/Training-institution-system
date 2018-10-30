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
    public class BaseSanctionTestDataFactory : EntityTestData<Sanction>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new SanctionBLO(UnitOfWork, GAppContext);
        }

        public BaseSanctionTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<Sanction> Generate_TestData()
        {
            List<Sanction> Data = new List<Sanction>();
 
            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Sanction_TestData.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/Sanction_TestData.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported
                if (File.Exists(Repport_File))
                {
                    ImportReport importReport = (this.BLO as SanctionBLO).Import(firstTable, FileName);
                    // Save ExcelRepport file to Server
                    DataSet DataSet_report = importReport.get_DataSet_Report();
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(DataSet_report);
                        wb.SaveAs(Repport_File);
                    }
                    // Convert Data Table to Data
                    Data = importReport.ImportedObjects.Cast<Sanction>().ToList();
                }
                else
                {
                    Data = (this.BLO as SanctionBLO).Convert_DataTable_to_List(firstTable);
                }
            }
            return Data;
        }
	
		/// <summary>
        /// Find the first Sanction instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Sanction CreateOrLouadFirstSanction()
        {
            SanctionBLO sanctionBLO = new SanctionBLO(UnitOfWork,GAppContext);
           
			Sanction entity = null;
            if (sanctionBLO.FindAll()?.Count > 0)
                entity = sanctionBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Sanction for Test
                entity = this.CreateValideSanctionInstance();
                sanctionBLO.Save(entity);
            }
            return entity;
        }

        public virtual Sanction CreateValideSanctionInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            Sanction  Valide_Sanction = this._Fixture.Create<Sanction>();
            Valide_Sanction.Id = 0;
            // Many to One 
            //   
			// Trainee
			var Trainee = new TraineeTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstTrainee();
            Valide_Sanction.Trainee = Trainee;
						 Valide_Sanction.TraineeId = Trainee.Id;
			           
			// SanctionCategory
			var SanctionCategory = new SanctionCategoryTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstSanctionCategory();
            Valide_Sanction.SanctionCategory = SanctionCategory;
						 Valide_Sanction.SanctionCategoryId = SanctionCategory.Id;
			           
			// Meeting
			var Meeting = new MeetingTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstMeeting();
            Valide_Sanction.Meeting = Meeting;
						 Valide_Sanction.MeetingId = Meeting.Id;
			           
            // One to Many
            //
			Valide_Sanction.Absences = null;
            return Valide_Sanction;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Sanction can't exist</returns>
        public virtual Sanction CreateInValideSanctionInstance()
        {
            Sanction sanction = this.CreateValideSanctionInstance();
             
			// Required   
 
			sanction.TraineeId = 0;
 
			sanction.SanctionCategoryId = 0;
 
			sanction.MeetingId = 0;
            //Unique
			var existant_Sanction = this.CreateOrLouadFirstSanction();
			sanction.Reference = existant_Sanction.Reference;
 
            return sanction;
        }


		public virtual Sanction CreateInValideSanctionInstance_ForEdit()
        {
            Sanction sanction = this.CreateOrLouadFirstSanction();
			// Required   
 
			sanction.TraineeId = 0;
 
			sanction.SanctionCategoryId = 0;
 
			sanction.MeetingId = 0;
            //Unique
			var existant_Sanction = this.CreateOrLouadFirstSanction();
			sanction.Reference = existant_Sanction.Reference;
            return sanction;
        }
    }

	public partial class SanctionTestDataFactory : BaseSanctionTestDataFactory{
	
		public SanctionTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
