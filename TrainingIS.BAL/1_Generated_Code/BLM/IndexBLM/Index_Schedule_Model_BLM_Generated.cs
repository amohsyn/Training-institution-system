//modelType = Index_Schedule_Model

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
	public partial class BaseIndex_Schedule_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseIndex_Schedule_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Schedule ConverTo_Schedule(Index_Schedule_Model Index_Schedule_Model)
        {
			Schedule Schedule = null;
            if (Index_Schedule_Model.Id != 0)
            {
                Schedule = new ScheduleBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Index_Schedule_Model.Id);
            }
            else
            {
                Schedule = new Schedule();
            } 
			Schedule.TrainingYear = Index_Schedule_Model.TrainingYear;
			Schedule.StartDate = DefaultDateTime_If_Empty(Index_Schedule_Model.StartDate);
			Schedule.EndtDate = DefaultDateTime_If_Empty(Index_Schedule_Model.EndtDate);
			Schedule.Reference = Index_Schedule_Model.Reference;
			Schedule.Id = Index_Schedule_Model.Id;
            return Schedule;
        }
        public virtual Index_Schedule_Model ConverTo_Index_Schedule_Model(Schedule Schedule)
        {  
			Index_Schedule_Model Index_Schedule_Model = new Index_Schedule_Model();
			Index_Schedule_Model.toStringValue = Schedule.ToString();
			Index_Schedule_Model.TrainingYear = Schedule.TrainingYear;
			Index_Schedule_Model.StartDate = DefaultDateTime_If_Empty(Schedule.StartDate);
			Index_Schedule_Model.EndtDate = DefaultDateTime_If_Empty(Schedule.EndtDate);
			Index_Schedule_Model.Id = Schedule.Id;
			Index_Schedule_Model.Reference = Schedule.Reference;
            return Index_Schedule_Model;            
        }

		public virtual Index_Schedule_Model CreateNew()
        {
            Schedule Schedule = new ScheduleBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Index_Schedule_Model Index_Schedule_Model = this.ConverTo_Index_Schedule_Model(Schedule);
            return Index_Schedule_Model;
        } 

		public virtual List<Index_Schedule_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            ScheduleBLO entityBLO = new ScheduleBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Schedule> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Index_Schedule_Model> ls_models = new List<Index_Schedule_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Index_Schedule_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Index_Schedule_ModelBLM : BaseIndex_Schedule_Model_BLM
	{
		public Index_Schedule_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
