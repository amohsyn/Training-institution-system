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
using TrainingIS.Models.WorkGroups;
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
    public class BaseWorkGroupTestDataFactory : EntityTestData<WorkGroup>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new WorkGroupBLO(UnitOfWork, GAppContext);
        }

        public BaseWorkGroupTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<WorkGroup> Generate_TestData()
        {
            List<WorkGroup> Data = new List<WorkGroup>();
 
            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/WorkGroup.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/WorkGroup.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported
                if (File.Exists(Repport_File))
                {
                    ImportReport importReport = (this.BLO as WorkGroupBLO).Import(firstTable, FileName);
                    // Save ExcelRepport file to Server
                    DataSet DataSet_report = importReport.get_DataSet_Report();
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(DataSet_report);
                        wb.SaveAs(Repport_File);
                    }
                    // Convert Data Table to Data
                    Data = importReport.ImportedObjects.Cast<WorkGroup>().ToList();
                }
                else
                {
                    Data = (this.BLO as WorkGroupBLO).Convert_DataTable_to_List(firstTable);
                }
            }
            return Data;
        }
	
		/// <summary>
        /// Find the first WorkGroup instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual WorkGroup CreateOrLouadFirstWorkGroup()
        {
            WorkGroupBLO workgroupBLO = new WorkGroupBLO(UnitOfWork,GAppContext);
           
			WorkGroup entity = null;
            if (workgroupBLO.FindAll()?.Count > 0)
                entity = workgroupBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp WorkGroup for Test
                entity = this.CreateValideWorkGroupInstance();
                workgroupBLO.Save(entity);
            }
            return entity;
        }

        public virtual WorkGroup CreateValideWorkGroupInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            WorkGroup  Valide_WorkGroup = this._Fixture.Create<WorkGroup>();
            Valide_WorkGroup.Id = 0;
            // Many to One 
            //   
			// President_Former
			var President_Former = new FormerTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstFormer();
            Valide_WorkGroup.President_Former = President_Former;
			           
			// President_Trainee
			var President_Trainee = new TraineeTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstTrainee();
            Valide_WorkGroup.President_Trainee = President_Trainee;
			           
			// President_Administrator
			var President_Administrator = new AdministratorTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstAdministrator();
            Valide_WorkGroup.President_Administrator = President_Administrator;
			           
			// VicePresident_Former
			var VicePresident_Former = new FormerTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstFormer();
            Valide_WorkGroup.VicePresident_Former = VicePresident_Former;
			           
			// VicePresident_Trainee
			var VicePresident_Trainee = new TraineeTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstTrainee();
            Valide_WorkGroup.VicePresident_Trainee = VicePresident_Trainee;
			           
			// VicePresident_Administrator
			var VicePresident_Administrator = new AdministratorTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstAdministrator();
            Valide_WorkGroup.VicePresident_Administrator = VicePresident_Administrator;
			           
			// Protractor_Former
			var Protractor_Former = new FormerTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstFormer();
            Valide_WorkGroup.Protractor_Former = Protractor_Former;
			           
			// Protractor_Administrator
			var Protractor_Administrator = new AdministratorTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstAdministrator();
            Valide_WorkGroup.Protractor_Administrator = Protractor_Administrator;
			           
			// Protractor_Trainee
			var Protractor_Trainee = new TraineeTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstTrainee();
            Valide_WorkGroup.Protractor_Trainee = Protractor_Trainee;
			           
            // One to Many
            //
			Valide_WorkGroup.MemebersAdministrators = null;
			Valide_WorkGroup.MemebersFormers = null;
			Valide_WorkGroup.MemebersTrainees = null;
			Valide_WorkGroup.Mission_Working_Groups = null;
            return Valide_WorkGroup;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide WorkGroup can't exist</returns>
        public virtual WorkGroup CreateInValideWorkGroupInstance()
        {
            WorkGroup workgroup = this.CreateValideWorkGroupInstance();
             
			// Required   
 
			workgroup.Name = null;
 
			workgroup.Code = null;
            //Unique
			var existant_WorkGroup = this.CreateOrLouadFirstWorkGroup();
			workgroup.Reference = existant_WorkGroup.Reference;
 
            return workgroup;
        }


		public virtual WorkGroup CreateInValideWorkGroupInstance_ForEdit()
        {
            WorkGroup workgroup = this.CreateOrLouadFirstWorkGroup();
			// Required   
 
			workgroup.Name = null;
 
			workgroup.Code = null;
            //Unique
			var existant_WorkGroup = this.CreateOrLouadFirstWorkGroup();
			workgroup.Reference = existant_WorkGroup.Reference;
            return workgroup;
        }
    }

	public partial class WorkGroupTestDataFactory : BaseWorkGroupTestDataFactory{
	
		public WorkGroupTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
