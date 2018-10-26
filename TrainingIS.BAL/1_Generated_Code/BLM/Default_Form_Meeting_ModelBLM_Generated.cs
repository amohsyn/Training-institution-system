//modelType = Default_Form_Meeting_Model

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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Form_Meeting_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_Meeting_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Meeting ConverTo_Meeting(Default_Form_Meeting_Model Default_Form_Meeting_Model)
        {
			Meeting Meeting = null;
            if (Default_Form_Meeting_Model.Id != 0)
            {
                Meeting = new MeetingBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_Meeting_Model.Id);
            }
            else
            {
                Meeting = new Meeting();
            } 
			Meeting.MeetingDate = DefaultDateTime_If_Empty(Default_Form_Meeting_Model.MeetingDate);
			Meeting.WorkGroupId = Default_Form_Meeting_Model.WorkGroupId;
			Meeting.WorkGroup = new WorkGroupBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Meeting_Model.WorkGroupId)) ;
			Meeting.Mission_Working_GroupId = Default_Form_Meeting_Model.Mission_Working_GroupId;
			Meeting.Mission_Working_Group = new Mission_Working_GroupBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Meeting_Model.Mission_Working_GroupId)) ;
			Meeting.Description = Default_Form_Meeting_Model.Description;
			Meeting.Presence_Of_President = Default_Form_Meeting_Model.Presence_Of_President;
			Meeting.Presence_Of_VicePresident = Default_Form_Meeting_Model.Presence_Of_VicePresident;
			Meeting.Presence_Of_Protractor = Default_Form_Meeting_Model.Presence_Of_Protractor;
			// Presences_Of_Formers
            FormerBLO Presences_Of_FormersBLO = new FormerBLO(this.UnitOfWork,this.GAppContext);
			if (Meeting.Presences_Of_Formers != null)
                Meeting.Presences_Of_Formers.Clear();
            else
                Meeting.Presences_Of_Formers = new List<Former>();
			if(Default_Form_Meeting_Model.Selected_Presences_Of_Formers != null)
			{
				foreach (string Selected_Former_Id in Default_Form_Meeting_Model.Selected_Presences_Of_Formers)
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
			if(Default_Form_Meeting_Model.Selected_Presences_Of_Administrators != null)
			{
				foreach (string Selected_Administrator_Id in Default_Form_Meeting_Model.Selected_Presences_Of_Administrators)
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
			if(Default_Form_Meeting_Model.Selected_Presences_Of_Trainees != null)
			{
				foreach (string Selected_Trainee_Id in Default_Form_Meeting_Model.Selected_Presences_Of_Trainees)
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
			if(Default_Form_Meeting_Model.Selected_Presences_Of_Guests_Formers != null)
			{
				foreach (string Selected_Former_Id in Default_Form_Meeting_Model.Selected_Presences_Of_Guests_Formers)
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
			if(Default_Form_Meeting_Model.Selected_Presences_Of_Guests_Administrators != null)
			{
				foreach (string Selected_Administrator_Id in Default_Form_Meeting_Model.Selected_Presences_Of_Guests_Administrators)
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
			if(Default_Form_Meeting_Model.Selected_Presences_Of_Guests_Trainees != null)
			{
				foreach (string Selected_Trainee_Id in Default_Form_Meeting_Model.Selected_Presences_Of_Guests_Trainees)
				{
					Int64 Selected_Trainee_Id_Int64 = Convert.ToInt64(Selected_Trainee_Id);
					Trainee Trainee =Presences_Of_Guests_TraineesBLO.FindBaseEntityByID(Selected_Trainee_Id_Int64);
					Meeting.Presences_Of_Guests_Trainees.Add(Trainee);
				}
			}
	
			Meeting.Reference = Default_Form_Meeting_Model.Reference;
			Meeting.Id = Default_Form_Meeting_Model.Id;
            return Meeting;
        }
        public virtual Default_Form_Meeting_Model ConverTo_Default_Form_Meeting_Model(Meeting Meeting)
        {  
			Default_Form_Meeting_Model Default_Form_Meeting_Model = new Default_Form_Meeting_Model();
			Default_Form_Meeting_Model.toStringValue = Meeting.ToString();
			Default_Form_Meeting_Model.MeetingDate = DefaultDateTime_If_Empty(Meeting.MeetingDate);
			Default_Form_Meeting_Model.WorkGroupId = Meeting.WorkGroupId;
			Default_Form_Meeting_Model.Mission_Working_GroupId = Meeting.Mission_Working_GroupId;
			Default_Form_Meeting_Model.Description = Meeting.Description;
			Default_Form_Meeting_Model.Presence_Of_President = Meeting.Presence_Of_President;
			Default_Form_Meeting_Model.Presence_Of_VicePresident = Meeting.Presence_Of_VicePresident;
			Default_Form_Meeting_Model.Presence_Of_Protractor = Meeting.Presence_Of_Protractor;

			// Presences_Of_Formers
            if (Meeting.Presences_Of_Formers != null && Meeting.Presences_Of_Formers.Count > 0)
            {
                Default_Form_Meeting_Model.Selected_Presences_Of_Formers = Meeting
                                                        .Presences_Of_Formers
                                                        .Select(entity => entity.Id.ToString())
                                                        .ToList<string>();
            }  
            else
            {
                Default_Form_Meeting_Model.Selected_Presences_Of_Formers = new List<string>();
            }			

			// Presences_Of_Administrators
            if (Meeting.Presences_Of_Administrators != null && Meeting.Presences_Of_Administrators.Count > 0)
            {
                Default_Form_Meeting_Model.Selected_Presences_Of_Administrators = Meeting
                                                        .Presences_Of_Administrators
                                                        .Select(entity => entity.Id.ToString())
                                                        .ToList<string>();
            }  
            else
            {
                Default_Form_Meeting_Model.Selected_Presences_Of_Administrators = new List<string>();
            }			

			// Presences_Of_Trainees
            if (Meeting.Presences_Of_Trainees != null && Meeting.Presences_Of_Trainees.Count > 0)
            {
                Default_Form_Meeting_Model.Selected_Presences_Of_Trainees = Meeting
                                                        .Presences_Of_Trainees
                                                        .Select(entity => entity.Id.ToString())
                                                        .ToList<string>();
            }  
            else
            {
                Default_Form_Meeting_Model.Selected_Presences_Of_Trainees = new List<string>();
            }			

			// Presences_Of_Guests_Formers
            if (Meeting.Presences_Of_Guests_Formers != null && Meeting.Presences_Of_Guests_Formers.Count > 0)
            {
                Default_Form_Meeting_Model.Selected_Presences_Of_Guests_Formers = Meeting
                                                        .Presences_Of_Guests_Formers
                                                        .Select(entity => entity.Id.ToString())
                                                        .ToList<string>();
            }  
            else
            {
                Default_Form_Meeting_Model.Selected_Presences_Of_Guests_Formers = new List<string>();
            }			

			// Presences_Of_Guests_Administrators
            if (Meeting.Presences_Of_Guests_Administrators != null && Meeting.Presences_Of_Guests_Administrators.Count > 0)
            {
                Default_Form_Meeting_Model.Selected_Presences_Of_Guests_Administrators = Meeting
                                                        .Presences_Of_Guests_Administrators
                                                        .Select(entity => entity.Id.ToString())
                                                        .ToList<string>();
            }  
            else
            {
                Default_Form_Meeting_Model.Selected_Presences_Of_Guests_Administrators = new List<string>();
            }			

			// Presences_Of_Guests_Trainees
            if (Meeting.Presences_Of_Guests_Trainees != null && Meeting.Presences_Of_Guests_Trainees.Count > 0)
            {
                Default_Form_Meeting_Model.Selected_Presences_Of_Guests_Trainees = Meeting
                                                        .Presences_Of_Guests_Trainees
                                                        .Select(entity => entity.Id.ToString())
                                                        .ToList<string>();
            }  
            else
            {
                Default_Form_Meeting_Model.Selected_Presences_Of_Guests_Trainees = new List<string>();
            }			
			Default_Form_Meeting_Model.Id = Meeting.Id;
			Default_Form_Meeting_Model.Reference = Meeting.Reference;
            return Default_Form_Meeting_Model;            
        }

		public virtual Default_Form_Meeting_Model CreateNew()
        {
            Meeting Meeting = new MeetingBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_Meeting_Model Default_Form_Meeting_Model = this.ConverTo_Default_Form_Meeting_Model(Meeting);
            return Default_Form_Meeting_Model;
        } 

		public virtual List<Default_Form_Meeting_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            MeetingBLO entityBLO = new MeetingBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Meeting> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Form_Meeting_Model> ls_models = new List<Default_Form_Meeting_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Form_Meeting_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Form_Meeting_ModelBLM : BaseDefault_Form_Meeting_ModelBLM
	{
		public Default_Form_Meeting_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
