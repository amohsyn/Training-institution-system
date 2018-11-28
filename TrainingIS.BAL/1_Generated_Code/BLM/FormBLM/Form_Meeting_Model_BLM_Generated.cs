//modelType = Form_Meeting_Model

using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;
using GApp.Core.Utils;
using GApp.Entities;
using GApp.DAL;
using GApp.Core.Context;
using TrainingIS.Models.Meetings;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseForm_Meeting_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseForm_Meeting_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Meeting ConverTo_Meeting(Form_Meeting_Model Form_Meeting_Model)
        {
			Meeting Meeting = null;
            if (Form_Meeting_Model.Id != 0)
            {
                Meeting = new MeetingBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Form_Meeting_Model.Id);
            }
            else
            {
                Meeting = new Meeting();
            } 
			Meeting.Mission_Working_Group = Form_Meeting_Model.Mission_Working_Group;
			Meeting.WorkGroup = Form_Meeting_Model.WorkGroup;
			Meeting.MeetingDate = DefaultDateTime_If_Empty(Form_Meeting_Model.MeetingDate);
			Meeting.WorkGroupId = Form_Meeting_Model.WorkGroupId;
			Meeting.WorkGroup = new WorkGroupBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Form_Meeting_Model.WorkGroupId)) ;
			Meeting.Mission_Working_GroupId = Form_Meeting_Model.Mission_Working_GroupId;
			Meeting.Mission_Working_Group = new Mission_Working_GroupBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Form_Meeting_Model.Mission_Working_GroupId)) ;
			Meeting.Description = Form_Meeting_Model.Description;
			Meeting.Presence_Of_President = Form_Meeting_Model.Presence_Of_President;
			Meeting.Presence_Of_VicePresident = Form_Meeting_Model.Presence_Of_VicePresident;
			Meeting.Presence_Of_Protractor = Form_Meeting_Model.Presence_Of_Protractor;
			// Presences_Of_Formers
            FormerBLO Presences_Of_FormersBLO = new FormerBLO(this.UnitOfWork,this.GAppContext);
			if (Meeting.Presences_Of_Formers != null)
                Meeting.Presences_Of_Formers.Clear();
            else
                Meeting.Presences_Of_Formers = new List<Former>();
			if(Form_Meeting_Model.Selected_Presences_Of_Formers != null)
			{
				foreach (string Selected_Former_Id in Form_Meeting_Model.Selected_Presences_Of_Formers)
				{
					Int64 Selected_Former_Id_Int64 = Convert.ToInt64(Selected_Former_Id);
					Former Former =Presences_Of_FormersBLO.FindBaseEntityByID(Selected_Former_Id_Int64);
					Meeting.Presences_Of_Formers.Add(Former);
				}
			}
	
			// Presences_Of_Administrators
            AdministratorBLO Presences_Of_AdministratorsBLO = new AdministratorBLO(this.UnitOfWork,this.GAppContext);
			if (Meeting.Presences_Of_Administrators != null)
                Meeting.Presences_Of_Administrators.Clear();
            else
                Meeting.Presences_Of_Administrators = new List<Administrator>();
			if(Form_Meeting_Model.Selected_Presences_Of_Administrators != null)
			{
				foreach (string Selected_Administrator_Id in Form_Meeting_Model.Selected_Presences_Of_Administrators)
				{
					Int64 Selected_Administrator_Id_Int64 = Convert.ToInt64(Selected_Administrator_Id);
					Administrator Administrator =Presences_Of_AdministratorsBLO.FindBaseEntityByID(Selected_Administrator_Id_Int64);
					Meeting.Presences_Of_Administrators.Add(Administrator);
				}
			}
	
			// Presences_Of_Trainees
            TraineeBLO Presences_Of_TraineesBLO = new TraineeBLO(this.UnitOfWork,this.GAppContext);
			if (Meeting.Presences_Of_Trainees != null)
                Meeting.Presences_Of_Trainees.Clear();
            else
                Meeting.Presences_Of_Trainees = new List<Trainee>();
			if(Form_Meeting_Model.Selected_Presences_Of_Trainees != null)
			{
				foreach (string Selected_Trainee_Id in Form_Meeting_Model.Selected_Presences_Of_Trainees)
				{
					Int64 Selected_Trainee_Id_Int64 = Convert.ToInt64(Selected_Trainee_Id);
					Trainee Trainee =Presences_Of_TraineesBLO.FindBaseEntityByID(Selected_Trainee_Id_Int64);
					Meeting.Presences_Of_Trainees.Add(Trainee);
				}
			}
	
			// Presences_Of_Guests_Formers
            FormerBLO Presences_Of_Guests_FormersBLO = new FormerBLO(this.UnitOfWork,this.GAppContext);
			if (Meeting.Presences_Of_Guests_Formers != null)
                Meeting.Presences_Of_Guests_Formers.Clear();
            else
                Meeting.Presences_Of_Guests_Formers = new List<Former>();
			if(Form_Meeting_Model.Selected_Presences_Of_Guests_Formers != null)
			{
				foreach (string Selected_Former_Id in Form_Meeting_Model.Selected_Presences_Of_Guests_Formers)
				{
					Int64 Selected_Former_Id_Int64 = Convert.ToInt64(Selected_Former_Id);
					Former Former =Presences_Of_Guests_FormersBLO.FindBaseEntityByID(Selected_Former_Id_Int64);
					Meeting.Presences_Of_Guests_Formers.Add(Former);
				}
			}
	
			// Presences_Of_Guests_Administrators
            AdministratorBLO Presences_Of_Guests_AdministratorsBLO = new AdministratorBLO(this.UnitOfWork,this.GAppContext);
			if (Meeting.Presences_Of_Guests_Administrators != null)
                Meeting.Presences_Of_Guests_Administrators.Clear();
            else
                Meeting.Presences_Of_Guests_Administrators = new List<Administrator>();
			if(Form_Meeting_Model.Selected_Presences_Of_Guests_Administrators != null)
			{
				foreach (string Selected_Administrator_Id in Form_Meeting_Model.Selected_Presences_Of_Guests_Administrators)
				{
					Int64 Selected_Administrator_Id_Int64 = Convert.ToInt64(Selected_Administrator_Id);
					Administrator Administrator =Presences_Of_Guests_AdministratorsBLO.FindBaseEntityByID(Selected_Administrator_Id_Int64);
					Meeting.Presences_Of_Guests_Administrators.Add(Administrator);
				}
			}
	
			// Presences_Of_Guests_Trainees
            TraineeBLO Presences_Of_Guests_TraineesBLO = new TraineeBLO(this.UnitOfWork,this.GAppContext);
			if (Meeting.Presences_Of_Guests_Trainees != null)
                Meeting.Presences_Of_Guests_Trainees.Clear();
            else
                Meeting.Presences_Of_Guests_Trainees = new List<Trainee>();
			if(Form_Meeting_Model.Selected_Presences_Of_Guests_Trainees != null)
			{
				foreach (string Selected_Trainee_Id in Form_Meeting_Model.Selected_Presences_Of_Guests_Trainees)
				{
					Int64 Selected_Trainee_Id_Int64 = Convert.ToInt64(Selected_Trainee_Id);
					Trainee Trainee =Presences_Of_Guests_TraineesBLO.FindBaseEntityByID(Selected_Trainee_Id_Int64);
					Meeting.Presences_Of_Guests_Trainees.Add(Trainee);
				}
			}
	
			Meeting.Reference = Form_Meeting_Model.Reference;
			Meeting.Id = Form_Meeting_Model.Id;
            return Meeting;
        }
        public virtual void ConverTo_Form_Meeting_Model(Form_Meeting_Model Form_Meeting_Model, Meeting Meeting)
        {  
			 
			Form_Meeting_Model.toStringValue = Meeting.ToString();
			Form_Meeting_Model.MeetingDate = DefaultDateTime_If_Empty(Meeting.MeetingDate);
			Form_Meeting_Model.WorkGroup = Meeting.WorkGroup;
			Form_Meeting_Model.WorkGroupId = Meeting.WorkGroupId;
			Form_Meeting_Model.Mission_Working_Group = Meeting.Mission_Working_Group;
			Form_Meeting_Model.Mission_Working_GroupId = Meeting.Mission_Working_GroupId;
			Form_Meeting_Model.Description = Meeting.Description;
			Form_Meeting_Model.Presence_Of_President = Meeting.Presence_Of_President;
			Form_Meeting_Model.Presence_Of_VicePresident = Meeting.Presence_Of_VicePresident;
			Form_Meeting_Model.Presence_Of_Protractor = Meeting.Presence_Of_Protractor;

			// Presences_Of_Formers
            if (Meeting.Presences_Of_Formers != null && Meeting.Presences_Of_Formers.Count > 0)
            {
                Form_Meeting_Model.Selected_Presences_Of_Formers = Meeting
                                                        .Presences_Of_Formers
                                                        .Select(entity => entity.Id.ToString())
                                                        .ToList<string>();
            }  
            else
            {
                Form_Meeting_Model.Selected_Presences_Of_Formers = new List<string>();
            }			

			// Presences_Of_Administrators
            if (Meeting.Presences_Of_Administrators != null && Meeting.Presences_Of_Administrators.Count > 0)
            {
                Form_Meeting_Model.Selected_Presences_Of_Administrators = Meeting
                                                        .Presences_Of_Administrators
                                                        .Select(entity => entity.Id.ToString())
                                                        .ToList<string>();
            }  
            else
            {
                Form_Meeting_Model.Selected_Presences_Of_Administrators = new List<string>();
            }			

			// Presences_Of_Trainees
            if (Meeting.Presences_Of_Trainees != null && Meeting.Presences_Of_Trainees.Count > 0)
            {
                Form_Meeting_Model.Selected_Presences_Of_Trainees = Meeting
                                                        .Presences_Of_Trainees
                                                        .Select(entity => entity.Id.ToString())
                                                        .ToList<string>();
            }  
            else
            {
                Form_Meeting_Model.Selected_Presences_Of_Trainees = new List<string>();
            }			

			// Presences_Of_Guests_Formers
            if (Meeting.Presences_Of_Guests_Formers != null && Meeting.Presences_Of_Guests_Formers.Count > 0)
            {
                Form_Meeting_Model.Selected_Presences_Of_Guests_Formers = Meeting
                                                        .Presences_Of_Guests_Formers
                                                        .Select(entity => entity.Id.ToString())
                                                        .ToList<string>();
            }  
            else
            {
                Form_Meeting_Model.Selected_Presences_Of_Guests_Formers = new List<string>();
            }			

			// Presences_Of_Guests_Administrators
            if (Meeting.Presences_Of_Guests_Administrators != null && Meeting.Presences_Of_Guests_Administrators.Count > 0)
            {
                Form_Meeting_Model.Selected_Presences_Of_Guests_Administrators = Meeting
                                                        .Presences_Of_Guests_Administrators
                                                        .Select(entity => entity.Id.ToString())
                                                        .ToList<string>();
            }  
            else
            {
                Form_Meeting_Model.Selected_Presences_Of_Guests_Administrators = new List<string>();
            }			

			// Presences_Of_Guests_Trainees
            if (Meeting.Presences_Of_Guests_Trainees != null && Meeting.Presences_Of_Guests_Trainees.Count > 0)
            {
                Form_Meeting_Model.Selected_Presences_Of_Guests_Trainees = Meeting
                                                        .Presences_Of_Guests_Trainees
                                                        .Select(entity => entity.Id.ToString())
                                                        .ToList<string>();
            }  
            else
            {
                Form_Meeting_Model.Selected_Presences_Of_Guests_Trainees = new List<string>();
            }			
			Form_Meeting_Model.Id = Meeting.Id;
			Form_Meeting_Model.Reference = Meeting.Reference;
                     
        }

    }

	public partial class Form_Meeting_ModelBLM : BaseForm_Meeting_Model_BLM
	{
		public Form_Meeting_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
