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
    public class BaseMeetingTestDataFactory : EntityTestData<Meeting>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new MeetingBLO(UnitOfWork, GAppContext);
        }

        public BaseMeetingTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<Meeting> Generate_TestData()
        {
            List<Meeting> Data = new List<Meeting>();
 
            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Meeting_TestData.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/Meeting_TestData.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported
                if (File.Exists(Repport_File))
                {
                    ImportReport importReport = (this.BLO as MeetingBLO).Import(firstTable, FileName);
                    // Save ExcelRepport file to Server
                    DataSet DataSet_report = importReport.get_DataSet_Report();
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(DataSet_report);
                        wb.SaveAs(Repport_File);
                    }
                    // Convert Data Table to Data
                    Data = importReport.ImportedObjects.Cast<Meeting>().ToList();
                }
                else
                {
                    Data = (this.BLO as MeetingBLO).Convert_DataTable_to_List(firstTable);
                }
            }
            return Data;
        }
	
		/// <summary>
        /// Find the first Meeting instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Meeting CreateOrLouadFirstMeeting()
        {
            MeetingBLO meetingBLO = new MeetingBLO(UnitOfWork,GAppContext);
           
			Meeting entity = null;
            if (meetingBLO.FindAll()?.Count > 0)
                entity = meetingBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Meeting for Test
                entity = this.CreateValideMeetingInstance();
                meetingBLO.Save(entity);
            }
            return entity;
        }

        public virtual Meeting CreateValideMeetingInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            Meeting  Valide_Meeting = this._Fixture.Create<Meeting>();
            Valide_Meeting.Id = 0;
            // Many to One 
            //   
			// WorkGroup
			var WorkGroup = new WorkGroupTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstWorkGroup();
            Valide_Meeting.WorkGroup = WorkGroup;
						 Valide_Meeting.WorkGroupId = WorkGroup.Id;
			           
			// Mission_Working_Group
			var Mission_Working_Group = new Mission_Working_GroupTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstMission_Working_Group();
            Valide_Meeting.Mission_Working_Group = Mission_Working_Group;
						 Valide_Meeting.Mission_Working_GroupId = Mission_Working_Group.Id;
			           
            // One to Many
            //
			Valide_Meeting.Presences_Of_Administrators = null;
			Valide_Meeting.Presences_Of_Formers = null;
			Valide_Meeting.Presences_Of_Guests_Administrators = null;
			Valide_Meeting.Presences_Of_Guests_Formers = null;
			Valide_Meeting.Presences_Of_Guests_Trainees = null;
			Valide_Meeting.Presences_Of_Trainees = null;
            return Valide_Meeting;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Meeting can't exist</returns>
        public virtual Meeting CreateInValideMeetingInstance()
        {
            Meeting meeting = this.CreateValideMeetingInstance();
             
			// Required   
 
			meeting.WorkGroupId = 0;
 
			meeting.Mission_Working_GroupId = 0;
            //Unique
			var existant_Meeting = this.CreateOrLouadFirstMeeting();
			meeting.Reference = existant_Meeting.Reference;
 
            return meeting;
        }


		public virtual Meeting CreateInValideMeetingInstance_ForEdit()
        {
            Meeting meeting = this.CreateOrLouadFirstMeeting();
			// Required   
 
			meeting.WorkGroupId = 0;
 
			meeting.Mission_Working_GroupId = 0;
            //Unique
			var existant_Meeting = this.CreateOrLouadFirstMeeting();
			meeting.Reference = existant_Meeting.Reference;
            return meeting;
        }
    }

	public partial class MeetingTestDataFactory : BaseMeetingTestDataFactory{
	
		public MeetingTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
