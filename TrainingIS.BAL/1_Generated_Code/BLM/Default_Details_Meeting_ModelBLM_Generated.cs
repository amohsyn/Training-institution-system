//modelType = Default_Details_Meeting_Model

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
	public partial class BaseDefault_Details_Meeting_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_Meeting_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Meeting ConverTo_Meeting(Default_Details_Meeting_Model Default_Details_Meeting_Model)
        {
			Meeting Meeting = null;
            if (Default_Details_Meeting_Model.Id != 0)
            {
                Meeting = new MeetingBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_Meeting_Model.Id);
            }
            else
            {
                Meeting = new Meeting();
            } 
			Meeting.MeetingDate = DefaultDateTime_If_Empty(Default_Details_Meeting_Model.MeetingDate);
			Meeting.WorkGroup = Default_Details_Meeting_Model.WorkGroup;
			Meeting.Mission_Working_Group = Default_Details_Meeting_Model.Mission_Working_Group;
			Meeting.Formers = Default_Details_Meeting_Model.Formers;
			Meeting.Administrators = Default_Details_Meeting_Model.Administrators;
			Meeting.Trainees = Default_Details_Meeting_Model.Trainees;
			Meeting.Description = Default_Details_Meeting_Model.Description;
			Meeting.Id = Default_Details_Meeting_Model.Id;
            return Meeting;
        }
        public virtual Default_Details_Meeting_Model ConverTo_Default_Details_Meeting_Model(Meeting Meeting)
        {  
			Default_Details_Meeting_Model Default_Details_Meeting_Model = new Default_Details_Meeting_Model();
			Default_Details_Meeting_Model.toStringValue = Meeting.ToString();
			Default_Details_Meeting_Model.MeetingDate = DefaultDateTime_If_Empty(Meeting.MeetingDate);
			Default_Details_Meeting_Model.WorkGroup = Meeting.WorkGroup;
			Default_Details_Meeting_Model.Mission_Working_Group = Meeting.Mission_Working_Group;
			Default_Details_Meeting_Model.Formers = Meeting.Formers;
			Default_Details_Meeting_Model.Administrators = Meeting.Administrators;
			Default_Details_Meeting_Model.Trainees = Meeting.Trainees;
			Default_Details_Meeting_Model.Description = Meeting.Description;
			Default_Details_Meeting_Model.Id = Meeting.Id;
            return Default_Details_Meeting_Model;            
        }

		public virtual Default_Details_Meeting_Model CreateNew()
        {
            Meeting Meeting = new MeetingBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_Meeting_Model Default_Details_Meeting_Model = this.ConverTo_Default_Details_Meeting_Model(Meeting);
            return Default_Details_Meeting_Model;
        } 

		public virtual List<Default_Details_Meeting_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            MeetingBLO entityBLO = new MeetingBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Meeting> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_Meeting_Model> ls_models = new List<Default_Details_Meeting_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_Meeting_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_Meeting_ModelBLM : BaseDefault_Details_Meeting_ModelBLM
	{
		public Default_Details_Meeting_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
