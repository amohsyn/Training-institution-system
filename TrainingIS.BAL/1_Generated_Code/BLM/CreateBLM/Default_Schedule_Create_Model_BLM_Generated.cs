//modelType = Default_Schedule_Create_Model

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
	public partial class BaseDefault_Schedule_Create_Model_BLM : BaseModelBLM
    {
       
        public GAppContext GAppContext {set;get;} 
		public Default_Form_Schedule_ModelBLM Default_Form_Schedule_ModelBLM {set;get;}
        
		public BaseDefault_Schedule_Create_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
			Default_Form_Schedule_ModelBLM = new Default_Form_Schedule_ModelBLM(this.UnitOfWork, this.GAppContext);
        }

		public virtual Schedule ConverTo_Schedule(Default_Schedule_Create_Model Default_Schedule_Create_Model)
        {
            var Schedule = Default_Form_Schedule_ModelBLM.ConverTo_Schedule(Default_Schedule_Create_Model);
            return Schedule;
        }

		public virtual Default_Schedule_Create_Model ConverTo_Default_Schedule_Create_Model(Schedule Schedule)
        {
            Default_Schedule_Create_Model Default_Schedule_Create_Model = new Default_Schedule_Create_Model();
            Default_Form_Schedule_ModelBLM.ConverTo_Default_Form_Schedule_Model(Default_Schedule_Create_Model, Schedule);
            return Default_Schedule_Create_Model;            
        }

		public virtual Default_Schedule_Create_Model CreateNew()
        {
            Schedule Schedule = new ScheduleBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Schedule_Create_Model Default_Schedule_Create_Model = this.ConverTo_Default_Schedule_Create_Model(Schedule);
            return Default_Schedule_Create_Model;
        } 

		public virtual List<Default_Schedule_Create_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            ScheduleBLO entityBLO = new ScheduleBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Schedule> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Schedule_Create_Model> ls_models = new List<Default_Schedule_Create_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Schedule_Create_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Schedule_Create_ModelBLM : BaseDefault_Schedule_Create_Model_BLM
	{
		public Default_Schedule_Create_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
