using GApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;

namespace TrainingIS.BLL
{
    public partial class MeetingBLO
    {
        public static string CONTROLLER = "Controller";
        public static string CREAT_FORM_ACTION = "Create_Form_Action";
        public static string CREATE_PARAMS = "Create_Params";
        public static string EDIT_FORM_ACTION = "Edit_Form_Action";
        public static string EDIT_PARAMS = "Edit_Params";


        /// <summary>
        /// Get Informations about Decision used to include decision in reunion
        /// Create_Action, Edit_Action, Controller, Edit_Params, Create_Params
        /// </summary>
        /// <param name="meeting">meeting instance</param>
        /// <returns>Dictionary contraine (Create_Action, Edit_Action, Controller, Edit_Params, Create_Params)</returns>
        public Dictionary<string, object> GetDecisionInfo(long meeting_id)
        {
            Meeting meeting = this.FindBaseEntityByID(meeting_id);
            Dictionary<string, object> DecisionInfo = new Dictionary<string, object>();
 
            switch (meeting.Mission_Working_Group.DecisionAuthority)
            {
                case Entities.enums.DecisionsAuthorities.Sanction_Attendance_Per_GeneralSupervisor:
                    return Get_Sanction_DecisionInfo(meeting_id);

                case Entities.enums.DecisionsAuthorities.Sanction_Attendance_Per_Administration:
                    return Get_Sanction_DecisionInfo(meeting_id);

                case Entities.enums.DecisionsAuthorities.Sanction_Attendance_Per_Disciplinary_Council:
                    return Get_Sanction_DecisionInfo(meeting_id);

                case Entities.enums.DecisionsAuthorities.Sanction_Behavior_Per_GeneralSupervisor:
                    return Get_Sanction_DecisionInfo(meeting_id);
            
                case Entities.enums.DecisionsAuthorities.Sanction_Behavior_Per_Administration:
                    return Get_Sanction_DecisionInfo(meeting_id);
               
                case Entities.enums.DecisionsAuthorities.Sanction_Behavior_Per_Disciplinary_Council:
                    return Get_Sanction_DecisionInfo(meeting_id);
            }
 
            return null;
        }

        public bool isHaveDecision(long Id)
        {
            Meeting meeting = this.FindBaseEntityByID(Id);

            BaseEntity Devision = this.GetDecision(Id);
            if (Devision != null)
                return true;
            else
                return false;
        }

        public BaseEntity GetDecision(long meetingId)
        {
            Meeting meeting = this.FindBaseEntityByID(meetingId);

            switch (meeting.Mission_Working_Group.DecisionAuthority)
            {
                case Entities.enums.DecisionsAuthorities.Sanction_Attendance_Per_GeneralSupervisor:
                    return this.Get_Decision_Sanction_Object(meetingId);
                case Entities.enums.DecisionsAuthorities.Sanction_Attendance_Per_Administration:
                    return this.Get_Decision_Sanction_Object(meetingId);
                case Entities.enums.DecisionsAuthorities.Sanction_Attendance_Per_Disciplinary_Council:
                    return this.Get_Decision_Sanction_Object(meetingId);
                case Entities.enums.DecisionsAuthorities.Sanction_Behavior_Per_GeneralSupervisor:
                    return this.Get_Decision_Sanction_Object(meetingId);
                case Entities.enums.DecisionsAuthorities.Sanction_Behavior_Per_Administration:
                    return this.Get_Decision_Sanction_Object(meetingId);
                case Entities.enums.DecisionsAuthorities.Sanction_Behavior_Per_Disciplinary_Council:
                    return this.Get_Decision_Sanction_Object(meetingId);
            }
            return null;
        }

        /// <summary>
        /// Get the Dictionnry DecisionInfo of Sanction Decision that contraine the 
        ///  - CONTROLLER
        ///  - CREAT_FORM_ACTION,
        ///  - EDIT_FORM_ACTION
        /// </summary>
        /// <param name="meetingId">Dictionary<string, object> Decision Info </param>
        /// <returns></returns>
        public Dictionary<string, object> Get_Sanction_DecisionInfo(long meetingId)
        {
            Dictionary<string, object> DecisionInfo = new Dictionary<string, object>();
            DecisionInfo.Add(MeetingBLO.CONTROLLER, "Sanctions");
            DecisionInfo.Add(MeetingBLO.CREAT_FORM_ACTION, "Create_Sanction_Form");
            DecisionInfo.Add(MeetingBLO.CREATE_PARAMS, new { MeetingId = meetingId });

            // Edit
            BaseEntity Decision = this.GetDecision(meetingId);
            if (Decision == null)
            {
                DecisionInfo.Add(MeetingBLO.EDIT_FORM_ACTION, "Create_Sanction_Form");
                DecisionInfo.Add(MeetingBLO.EDIT_PARAMS, new { id = meetingId });
            }
            else
            {
                DecisionInfo.Add(MeetingBLO.EDIT_FORM_ACTION, "Edit_Sanction_Form");
                DecisionInfo.Add(MeetingBLO.EDIT_PARAMS, new { id = Decision.Id });
            }
            return DecisionInfo;
        }

        public BaseEntity Get_Decision_Sanction_Object(long Meeting_Id)
        {
            SanctionBLO sanctionBLO = new SanctionBLO(this._UnitOfWork, this.GAppContext);
            var sanction = sanctionBLO.Find_By_Meeting_Id(Meeting_Id);

            return sanction;
        }


        /// <summary>
        /// Add Presence of All members 
        /// </summary>
        /// <param name="meeting"></param>
        public void Add_Presence_Of_All_Members(Meeting meeting)
        {
            meeting.Presences_Of_Administrators = meeting.WorkGroup.MemebersAdministrators;
            meeting.Presences_Of_Formers = meeting.WorkGroup.MemebersFormers;
            meeting.Presences_Of_Trainees = meeting.WorkGroup.MemebersTrainees;

            meeting.Presence_Of_President = true;
            meeting.Presence_Of_VicePresident = true;
            meeting.Presence_Of_Protractor = true;
 
        }

         
        public string Get_Presences(Meeting meeting)
        {
            // Présences
            List<string> Presences = new List<string>();
            if (meeting.Presence_Of_President && meeting.WorkGroup.President != null)
                Presences.Add(meeting.WorkGroup.President.ToString());
            if (meeting.Presence_Of_VicePresident && meeting.WorkGroup.VicePresident != null)
                Presences.Add(meeting.WorkGroup.VicePresident.ToString());
            if (meeting.Presence_Of_Protractor && meeting.WorkGroup.Protractor != null)
                Presences.Add(meeting.WorkGroup.Protractor.ToString());
            if (meeting.Presences_Of_Administrators != null  )
                foreach (var item in meeting.Presences_Of_Administrators)
                {
                    Presences.Add(item.ToString());
                }
            if (meeting.Presences_Of_Formers != null)
                foreach (var item in meeting.Presences_Of_Administrators)
                {
                    Presences.Add(item.ToString());
                }
            if (meeting.Presences_Of_Guests_Administrators != null)
                foreach (var item in meeting.Presences_Of_Administrators)
                {
                    Presences.Add(item.ToString());
                }
            if (meeting.Presences_Of_Guests_Formers != null)
                foreach (var item in meeting.Presences_Of_Administrators)
                {
                    Presences.Add(item.ToString());
                }
            if (meeting.Presences_Of_Guests_Trainees != null)
                foreach (var item in meeting.Presences_Of_Administrators)
                {
                    Presences.Add(item.ToString());
                }
            if (meeting.Presences_Of_Trainees != null)
                foreach (var item in meeting.Presences_Of_Administrators)
                {
                    Presences.Add(item.ToString());
                }
           string presences_value  = string.Join(" , ", Presences.ToArray());
            return presences_value;
        }

        /// <summary>
        /// Get Information of Decision : 
        /// - Name of Sanctioned Trainee
        /// </summary>
        /// <param name="meeting"></param>
        /// <returns></returns>
        public string Get_Decision_Info(Meeting meeting)
        {
            // BLO
            SanctionBLO sanctionBLO = new SanctionBLO(this._UnitOfWork, this.GAppContext);

            // Decision Info
            string Decision_Info = "";
            // Sanctioned Trainee 
            Sanction sanction = sanctionBLO.Find_By_Meeting_Id(meeting.Id);
            if(sanction != null)
            {
                Decision_Info = sanction.Trainee.ToString();
            }

            return Decision_Info;

        }
    }
}
