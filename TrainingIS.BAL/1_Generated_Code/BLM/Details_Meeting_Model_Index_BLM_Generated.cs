//modelType = Details_Meeting_Model

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
	public partial class BaseDetails_Meeting_Model_Index_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDetails_Meeting_Model_Index_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Meeting ConverTo_Meeting(Details_Meeting_Model Details_Meeting_Model)
        {
			Meeting Meeting = null;
            if (Details_Meeting_Model.Id != 0)
            {
                Meeting = new MeetingBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Details_Meeting_Model.Id);
            }
            else
            {
                Meeting = new Meeting();
            } 
			Meeting.MeetingDate = DefaultDateTime_If_Empty(Details_Meeting_Model.MeetingDate);
			Meeting.WorkGroup = Details_Meeting_Model.WorkGroup;
			Meeting.Mission_Working_Group = Details_Meeting_Model.Mission_Working_Group;
			Meeting.Id = Details_Meeting_Model.Id;
            return Meeting;
        }
        public virtual Details_Meeting_Model ConverTo_Details_Meeting_Model(Meeting Meeting)
        {  
			Details_Meeting_Model Details_Meeting_Model = new Details_Meeting_Model();
			Details_Meeting_Model.toStringValue = Meeting.ToString();
			Details_Meeting_Model.MeetingDate = DefaultDateTime_If_Empty(Meeting.MeetingDate);
			Details_Meeting_Model.WorkGroup = Meeting.WorkGroup;
			Details_Meeting_Model.Mission_Working_Group = Meeting.Mission_Working_Group;
			Details_Meeting_Model.Id = Meeting.Id;
            return Details_Meeting_Model;            
        }

		public virtual Details_Meeting_Model CreateNew()
        {
            Meeting Meeting = new MeetingBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Details_Meeting_Model Details_Meeting_Model = this.ConverTo_Details_Meeting_Model(Meeting);
            return Details_Meeting_Model;
        } 

		public virtual List<Details_Meeting_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            MeetingBLO entityBLO = new MeetingBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Meeting> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Details_Meeting_Model> ls_models = new List<Details_Meeting_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Details_Meeting_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Details_Meeting_ModelBLM : BaseDetails_Meeting_Model_Index_BLM
	{
		public Details_Meeting_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
