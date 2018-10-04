//modelType = Default_Details_Schedule_Model

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
	public partial class BaseDefault_Details_Schedule_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_Schedule_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Schedule ConverTo_Schedule(Default_Details_Schedule_Model Default_Details_Schedule_Model)
        {
			Schedule Schedule = null;
            if (Default_Details_Schedule_Model.Id != 0)
            {
                Schedule = new ScheduleBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_Schedule_Model.Id);
            }
            else
            {
                Schedule = new Schedule();
            } 
			Schedule.TrainingYear = Default_Details_Schedule_Model.TrainingYear;
			Schedule.StartDate = DefaultDateTime_If_Empty(Default_Details_Schedule_Model.StartDate);
			Schedule.EndtDate = DefaultDateTime_If_Empty(Default_Details_Schedule_Model.EndtDate);
			Schedule.Description = Default_Details_Schedule_Model.Description;
			Schedule.Id = Default_Details_Schedule_Model.Id;
            return Schedule;
        }
        public virtual Default_Details_Schedule_Model ConverTo_Default_Details_Schedule_Model(Schedule Schedule)
        {  
			Default_Details_Schedule_Model Default_Details_Schedule_Model = new Default_Details_Schedule_Model();
			Default_Details_Schedule_Model.toStringValue = Schedule.ToString();
			Default_Details_Schedule_Model.TrainingYear = Schedule.TrainingYear;
			Default_Details_Schedule_Model.StartDate = DefaultDateTime_If_Empty(Schedule.StartDate);
			Default_Details_Schedule_Model.EndtDate = DefaultDateTime_If_Empty(Schedule.EndtDate);
			Default_Details_Schedule_Model.Description = Schedule.Description;
			Default_Details_Schedule_Model.Id = Schedule.Id;
            return Default_Details_Schedule_Model;            
        }

		public virtual Default_Details_Schedule_Model CreateNew()
        {
            Schedule Schedule = new ScheduleBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_Schedule_Model Default_Details_Schedule_Model = this.ConverTo_Default_Details_Schedule_Model(Schedule);
            return Default_Details_Schedule_Model;
        } 

		public virtual List<Default_Details_Schedule_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            ScheduleBLO entityBLO = new ScheduleBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Schedule> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_Schedule_Model> ls_models = new List<Default_Details_Schedule_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_Schedule_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_Schedule_ModelBLM : BaseDefault_Details_Schedule_ModelBLM
	{
		public Default_Details_Schedule_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
