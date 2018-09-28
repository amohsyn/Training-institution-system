//modelType = Details_Schedule_Model

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
	public partial class BaseDetails_Schedule_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDetails_Schedule_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Schedule ConverTo_Schedule(Details_Schedule_Model Details_Schedule_Model)
        {
			Schedule Schedule = null;
            if (Details_Schedule_Model.Id != 0)
            {
                Schedule = new ScheduleBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Details_Schedule_Model.Id);
            }
            else
            {
                Schedule = new Schedule();
            } 
			Schedule.TrainingYear = Details_Schedule_Model.TrainingYear;
			Schedule.StartDate = DefaultDateTime_If_Empty(Details_Schedule_Model.StartDate);
			Schedule.EndtDate = DefaultDateTime_If_Empty(Details_Schedule_Model.EndtDate);
			Schedule.Reference = Details_Schedule_Model.Reference;
			Schedule.Id = Details_Schedule_Model.Id;
            return Schedule;
        }
        public virtual Details_Schedule_Model ConverTo_Details_Schedule_Model(Schedule Schedule)
        {  
			Details_Schedule_Model Details_Schedule_Model = new Details_Schedule_Model();
			Details_Schedule_Model.toStringValue = Schedule.ToString();
			Details_Schedule_Model.TrainingYear = Schedule.TrainingYear;
			Details_Schedule_Model.StartDate = DefaultDateTime_If_Empty(Schedule.StartDate);
			Details_Schedule_Model.EndtDate = DefaultDateTime_If_Empty(Schedule.EndtDate);
			Details_Schedule_Model.Id = Schedule.Id;
			Details_Schedule_Model.Reference = Schedule.Reference;
            return Details_Schedule_Model;            
        }

		public virtual Details_Schedule_Model CreateNew()
        {
            Schedule Schedule = new ScheduleBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Details_Schedule_Model Details_Schedule_Model = this.ConverTo_Details_Schedule_Model(Schedule);
            return Details_Schedule_Model;
        } 

		public List<Details_Schedule_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            ScheduleBLO entityBLO = new ScheduleBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Schedule> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Details_Schedule_Model> ls_models = new List<Details_Schedule_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Details_Schedule_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Details_Schedule_ModelBLM : BaseDetails_Schedule_ModelBLM
	{
		public Details_Schedule_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
