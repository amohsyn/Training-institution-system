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
        public Dictionary<string,object> GetDecisionInfo(long id)
        {
            Meeting meeting = this.FindBaseEntityByID(id);
            Dictionary<string, object> DecisionInfo = new Dictionary<string, object>();

            switch (meeting.Mission_Working_Group.DecisionAuthority)
            {
                case Entities.enums.DecisionsAuthorities.GeneralSupervisor_Absence:
                    throw new NotImplementedException();
                    break;
                case Entities.enums.DecisionsAuthorities.Administration_Absence:
                    throw new NotImplementedException();
                    break;
                case Entities.enums.DecisionsAuthorities.Disciplinary_Council_Trainee:
                    throw new NotImplementedException();
                    break;
                case Entities.enums.DecisionsAuthorities.Disciplinary_Council_Trainees:
                    throw new NotImplementedException();
                    break;
                case Entities.enums.DecisionsAuthorities.Disciplinary_Council_Absences_Of_Trainee:
                    {
                        DecisionInfo.Add(MeetingBLO.CONTROLLER, "Sanctions");
                        DecisionInfo.Add(MeetingBLO.CREAT_FORM_ACTION, "Create_Sanction_Form");
                        DecisionInfo.Add(MeetingBLO.CREATE_PARAMS, new { MeetingId = meeting.Id });

                        // Edit
                        BaseEntity Decision = this.GetDecision(meeting.Id);
                        if(Decision == null)
                        {
                            DecisionInfo.Add(MeetingBLO.EDIT_FORM_ACTION, "Create_Sanction_Form");
                            DecisionInfo.Add(MeetingBLO.EDIT_PARAMS, new { id = meeting.Id });
                        }
                        else
                        {
                            DecisionInfo.Add(MeetingBLO.EDIT_FORM_ACTION, "Edit_Sanction_Form");
                            DecisionInfo.Add(MeetingBLO.EDIT_PARAMS, new { id = Decision.Id });
                        }
                        return DecisionInfo;
                    } 
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
                case Entities.enums.DecisionsAuthorities.GeneralSupervisor_Absence:
                    throw new NotImplementedException();
                    break;
                case Entities.enums.DecisionsAuthorities.Administration_Absence:
                    throw new NotImplementedException();
                    break;
                case Entities.enums.DecisionsAuthorities.Disciplinary_Council_Trainee:
                    throw new NotImplementedException();
                    break;
                case Entities.enums.DecisionsAuthorities.Disciplinary_Council_Trainees:
                    throw new NotImplementedException();
                    break;
                case Entities.enums.DecisionsAuthorities.Disciplinary_Council_Absences_Of_Trainee:
                    {
                        SanctionBLO sanctionBLO = new SanctionBLO(this._UnitOfWork, this.GAppContext);
                        var sanction = sanctionBLO.Find_By_Meeting_Id(meeting.Id);
                        return sanction;

                    }
            }
            return null;
        }
    }
}
