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
    public class BaseMission_Working_GroupTestDataFactory : EntityTestData<Mission_Working_Group>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new Mission_Working_GroupBLO(UnitOfWork, GAppContext);
        }

        public BaseMission_Working_GroupTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<Mission_Working_Group> Generate_TestData()
        {
            List<Mission_Working_Group> Data = new List<Mission_Working_Group>();
 
            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Mission_Working_Group_TestData.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/Mission_Working_Group_TestData.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported
                if (File.Exists(Repport_File))
                {
                    ImportReport importReport = (this.BLO as Mission_Working_GroupBLO).Import(firstTable, FileName);
                    // Save ExcelRepport file to Server
                    DataSet DataSet_report = importReport.get_DataSet_Report();
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(DataSet_report);
                        wb.SaveAs(Repport_File);
                    }
                    // Convert Data Table to Data
                    Data = importReport.ImportedObjects.Cast<Mission_Working_Group>().ToList();
                }
                else
                {
                    Data = (this.BLO as Mission_Working_GroupBLO).Convert_DataTable_to_List(firstTable);
                }
            }
            return Data;
        }
	
		/// <summary>
        /// Find the first Mission_Working_Group instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Mission_Working_Group CreateOrLouadFirstMission_Working_Group()
        {
            Mission_Working_GroupBLO mission_working_groupBLO = new Mission_Working_GroupBLO(UnitOfWork,GAppContext);
           
			Mission_Working_Group entity = null;
            if (mission_working_groupBLO.FindAll()?.Count > 0)
                entity = mission_working_groupBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Mission_Working_Group for Test
                entity = this.CreateValideMission_Working_GroupInstance();
                mission_working_groupBLO.Save(entity);
            }
            return entity;
        }

        public virtual Mission_Working_Group CreateValideMission_Working_GroupInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            Mission_Working_Group  Valide_Mission_Working_Group = this._Fixture.Create<Mission_Working_Group>();
            Valide_Mission_Working_Group.Id = 0;
            // Many to One 
            //   
            // One to Many
            //
			Valide_Mission_Working_Group.WorkGroups = null;
            return Valide_Mission_Working_Group;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Mission_Working_Group can't exist</returns>
        public virtual Mission_Working_Group CreateInValideMission_Working_GroupInstance()
        {
            Mission_Working_Group mission_working_group = this.CreateValideMission_Working_GroupInstance();
             
			// Required   
 
			mission_working_group.Code = null;
 
			mission_working_group.Name = null;
            //Unique
			var existant_Mission_Working_Group = this.CreateOrLouadFirstMission_Working_Group();
			mission_working_group.Reference = existant_Mission_Working_Group.Reference;
 
            return mission_working_group;
        }


		public virtual Mission_Working_Group CreateInValideMission_Working_GroupInstance_ForEdit()
        {
            Mission_Working_Group mission_working_group = this.CreateOrLouadFirstMission_Working_Group();
			// Required   
 
			mission_working_group.Code = null;
 
			mission_working_group.Name = null;
            //Unique
			var existant_Mission_Working_Group = this.CreateOrLouadFirstMission_Working_Group();
			mission_working_group.Reference = existant_Mission_Working_Group.Reference;
            return mission_working_group;
        }
    }

	public partial class Mission_Working_GroupTestDataFactory : BaseMission_Working_GroupTestDataFactory{
	
		public Mission_Working_GroupTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
