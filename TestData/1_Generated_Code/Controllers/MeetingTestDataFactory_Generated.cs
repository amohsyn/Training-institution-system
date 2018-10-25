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

namespace TestData
{
    public class BaseMeetingTestDataFactory : EntityTestData<Meeting>
    {
        public BaseMeetingTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<Meeting> Generate_TestData()
        {
            List<Meeting> Data = base.Generate_TestData();
            if(Data == null) Data = new List<Meeting>();
            Data.Add(this.CreateValideMeetingInstance());
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
