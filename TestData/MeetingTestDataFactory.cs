using GApp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;
using TrainingIS.Entities;

namespace TestData
{
    public partial class MeetingTestDataFactory
    {
        public override Meeting CreateValideMeetingInstance()
        {
            if (UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();

            Meeting Valide_Meeting = new Meeting();
            Valide_Meeting.Id = 0;
            Valide_Meeting.MeetingDate = DateTime.Now;
            Valide_Meeting.Presence_Of_President = true;
            Valide_Meeting.Presence_Of_VicePresident = true;
            Valide_Meeting.Presence_Of_Protractor = true;
          
            // Many to One 
            //   
            // WorkGroup
            var WorkGroup = new WorkGroupTestDataFactory(UnitOfWork, GAppContext).CreateOrLouadFirstWorkGroup();
            Valide_Meeting.WorkGroup = WorkGroup;
            Valide_Meeting.WorkGroupId = WorkGroup.Id;

            // Mission_Working_Group
            var Mission_Working_Group = new Mission_Working_GroupTestDataFactory(UnitOfWork, GAppContext).CreateOrLouadFirstMission_Working_Group();
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
    }
}
