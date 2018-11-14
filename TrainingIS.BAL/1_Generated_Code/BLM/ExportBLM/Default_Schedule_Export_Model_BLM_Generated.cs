//modelType = Default_Schedule_Export_Model

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
	public partial class BaseDefault_Schedule_Export_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Schedule_Export_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Schedule ConverTo_Schedule(Default_Schedule_Export_Model Default_Schedule_Export_Model)
        {
			Schedule Schedule = null;
            if (Default_Schedule_Export_Model.Id != 0)
            {
                Schedule = new ScheduleBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Schedule_Export_Model.Id);
            }
            else
            {
                Schedule = new Schedule();
            } 
			Schedule.TrainingYear = Default_Schedule_Export_Model.TrainingYear;
			Schedule.StartDate = DefaultDateTime_If_Empty(Default_Schedule_Export_Model.StartDate);
			Schedule.EndtDate = DefaultDateTime_If_Empty(Default_Schedule_Export_Model.EndtDate);
			Schedule.Description = Default_Schedule_Export_Model.Description;
			Schedule.Id = Default_Schedule_Export_Model.Id;
            return Schedule;
        }
        public virtual Default_Schedule_Export_Model ConverTo_Default_Schedule_Export_Model(Schedule Schedule)
        {  
			Default_Schedule_Export_Model Default_Schedule_Export_Model = new Default_Schedule_Export_Model();
			Default_Schedule_Export_Model.toStringValue = Schedule.ToString();
			Default_Schedule_Export_Model.TrainingYear = Schedule.TrainingYear;
			Default_Schedule_Export_Model.StartDate = DefaultDateTime_If_Empty(Schedule.StartDate);
			Default_Schedule_Export_Model.EndtDate = DefaultDateTime_If_Empty(Schedule.EndtDate);
			Default_Schedule_Export_Model.Description = Schedule.Description;
			Default_Schedule_Export_Model.Id = Schedule.Id;
            return Default_Schedule_Export_Model;            
        }

		public virtual Default_Schedule_Export_Model CreateNew()
        {
            Schedule Schedule = new ScheduleBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Schedule_Export_Model Default_Schedule_Export_Model = this.ConverTo_Default_Schedule_Export_Model(Schedule);
            return Default_Schedule_Export_Model;
        } 

		public virtual List<Default_Schedule_Export_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            ScheduleBLO entityBLO = new ScheduleBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Schedule> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Schedule_Export_Model> ls_models = new List<Default_Schedule_Export_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Schedule_Export_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Schedule_Export_ModelBLM : BaseDefault_Schedule_Export_Model_BLM
	{
		public Default_Schedule_Export_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
