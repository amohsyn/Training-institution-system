//modelType = Index_Meeting_Model

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
	public partial class BaseIndex_Meeting_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseIndex_Meeting_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Meeting ConverTo_Meeting(Index_Meeting_Model Index_Meeting_Model)
        {
			Meeting Meeting = null;
            if (Index_Meeting_Model.Id != 0)
            {
                Meeting = new MeetingBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Index_Meeting_Model.Id);
            }
            else
            {
                Meeting = new Meeting();
            } 
			Meeting.MeetingDate = DefaultDateTime_If_Empty(Index_Meeting_Model.MeetingDate);
			Meeting.WorkGroup = Index_Meeting_Model.WorkGroup;
			Meeting.Mission_Working_Group = Index_Meeting_Model.Mission_Working_Group;
			Meeting.Id = Index_Meeting_Model.Id;
            return Meeting;
        }
        public virtual Index_Meeting_Model ConverTo_Index_Meeting_Model(Meeting Meeting)
        {  
			Index_Meeting_Model Index_Meeting_Model = new Index_Meeting_Model();
			Index_Meeting_Model.toStringValue = Meeting.ToString();
			Index_Meeting_Model.MeetingDate = DefaultDateTime_If_Empty(Meeting.MeetingDate);
			Index_Meeting_Model.WorkGroup = Meeting.WorkGroup;
			Index_Meeting_Model.Mission_Working_Group = Meeting.Mission_Working_Group;
			Index_Meeting_Model.Id = Meeting.Id;
            return Index_Meeting_Model;            
        }

		public virtual Index_Meeting_Model CreateNew()
        {
            Meeting Meeting = new MeetingBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Index_Meeting_Model Index_Meeting_Model = this.ConverTo_Index_Meeting_Model(Meeting);
            return Index_Meeting_Model;
        } 

		public virtual List<Index_Meeting_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            MeetingBLO entityBLO = new MeetingBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Meeting> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Index_Meeting_Model> ls_models = new List<Index_Meeting_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Index_Meeting_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Index_Meeting_ModelBLM : BaseIndex_Meeting_Model_BLM
	{
		public Index_Meeting_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
