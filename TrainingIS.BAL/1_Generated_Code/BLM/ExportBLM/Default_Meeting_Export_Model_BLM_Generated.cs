//modelType = Default_Meeting_Export_Model

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
	public partial class BaseDefault_Meeting_Export_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Meeting_Export_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Meeting ConverTo_Meeting(Default_Meeting_Export_Model Default_Meeting_Export_Model)
        {
			Meeting Meeting = null;
            if (Default_Meeting_Export_Model.Id != 0)
            {
                Meeting = new MeetingBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Meeting_Export_Model.Id);
            }
            else
            {
                Meeting = new Meeting();
            } 
			Meeting.MeetingDate = DefaultDateTime_If_Empty(Default_Meeting_Export_Model.MeetingDate);
			Meeting.WorkGroup = Default_Meeting_Export_Model.WorkGroup;
			Meeting.Mission_Working_Group = Default_Meeting_Export_Model.Mission_Working_Group;
			Meeting.Description = Default_Meeting_Export_Model.Description;
			Meeting.Presence_Of_President = Default_Meeting_Export_Model.Presence_Of_President;
			Meeting.Presence_Of_VicePresident = Default_Meeting_Export_Model.Presence_Of_VicePresident;
			Meeting.Presence_Of_Protractor = Default_Meeting_Export_Model.Presence_Of_Protractor;
			Meeting.Presences_Of_Formers = Default_Meeting_Export_Model.Presences_Of_Formers;
			Meeting.Presences_Of_Administrators = Default_Meeting_Export_Model.Presences_Of_Administrators;
			Meeting.Presences_Of_Trainees = Default_Meeting_Export_Model.Presences_Of_Trainees;
			Meeting.Presences_Of_Guests_Formers = Default_Meeting_Export_Model.Presences_Of_Guests_Formers;
			Meeting.Presences_Of_Guests_Administrators = Default_Meeting_Export_Model.Presences_Of_Guests_Administrators;
			Meeting.Presences_Of_Guests_Trainees = Default_Meeting_Export_Model.Presences_Of_Guests_Trainees;
			Meeting.Id = Default_Meeting_Export_Model.Id;
            return Meeting;
        }
        public virtual Default_Meeting_Export_Model ConverTo_Default_Meeting_Export_Model(Meeting Meeting)
        {  
			Default_Meeting_Export_Model Default_Meeting_Export_Model = new Default_Meeting_Export_Model();
			Default_Meeting_Export_Model.toStringValue = Meeting.ToString();
			Default_Meeting_Export_Model.MeetingDate = DefaultDateTime_If_Empty(Meeting.MeetingDate);
			Default_Meeting_Export_Model.WorkGroup = Meeting.WorkGroup;
			Default_Meeting_Export_Model.Mission_Working_Group = Meeting.Mission_Working_Group;
			Default_Meeting_Export_Model.Description = Meeting.Description;
			Default_Meeting_Export_Model.Presence_Of_President = Meeting.Presence_Of_President;
			Default_Meeting_Export_Model.Presence_Of_VicePresident = Meeting.Presence_Of_VicePresident;
			Default_Meeting_Export_Model.Presence_Of_Protractor = Meeting.Presence_Of_Protractor;
			Default_Meeting_Export_Model.Presences_Of_Formers = Meeting.Presences_Of_Formers;
			Default_Meeting_Export_Model.Presences_Of_Administrators = Meeting.Presences_Of_Administrators;
			Default_Meeting_Export_Model.Presences_Of_Trainees = Meeting.Presences_Of_Trainees;
			Default_Meeting_Export_Model.Presences_Of_Guests_Formers = Meeting.Presences_Of_Guests_Formers;
			Default_Meeting_Export_Model.Presences_Of_Guests_Administrators = Meeting.Presences_Of_Guests_Administrators;
			Default_Meeting_Export_Model.Presences_Of_Guests_Trainees = Meeting.Presences_Of_Guests_Trainees;
			Default_Meeting_Export_Model.Id = Meeting.Id;
            return Default_Meeting_Export_Model;            
        }

		public virtual Default_Meeting_Export_Model CreateNew()
        {
            Meeting Meeting = new MeetingBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Meeting_Export_Model Default_Meeting_Export_Model = this.ConverTo_Default_Meeting_Export_Model(Meeting);
            return Default_Meeting_Export_Model;
        } 

		public virtual List<Default_Meeting_Export_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            MeetingBLO entityBLO = new MeetingBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Meeting> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Meeting_Export_Model> ls_models = new List<Default_Meeting_Export_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Meeting_Export_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Meeting_Export_ModelBLM : BaseDefault_Meeting_Export_Model_BLM
	{
		public Default_Meeting_Export_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
