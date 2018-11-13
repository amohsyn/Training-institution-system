//modelType = Default_Details_SeancePlanning_Model

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
	public partial class BaseDefault_Details_SeancePlanning_Model_Index_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_SeancePlanning_Model_Index_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual SeancePlanning ConverTo_SeancePlanning(Default_Details_SeancePlanning_Model Default_Details_SeancePlanning_Model)
        {
			SeancePlanning SeancePlanning = null;
            if (Default_Details_SeancePlanning_Model.Id != 0)
            {
                SeancePlanning = new SeancePlanningBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_SeancePlanning_Model.Id);
            }
            else
            {
                SeancePlanning = new SeancePlanning();
            } 
			SeancePlanning.Schedule = Default_Details_SeancePlanning_Model.Schedule;
			SeancePlanning.Training = Default_Details_SeancePlanning_Model.Training;
			SeancePlanning.SeanceDay = Default_Details_SeancePlanning_Model.SeanceDay;
			SeancePlanning.SeanceNumber = Default_Details_SeancePlanning_Model.SeanceNumber;
			SeancePlanning.Classroom = Default_Details_SeancePlanning_Model.Classroom;
			SeancePlanning.Description = Default_Details_SeancePlanning_Model.Description;
			SeancePlanning.Id = Default_Details_SeancePlanning_Model.Id;
            return SeancePlanning;
        }
        public virtual Default_Details_SeancePlanning_Model ConverTo_Default_Details_SeancePlanning_Model(SeancePlanning SeancePlanning)
        {  
			Default_Details_SeancePlanning_Model Default_Details_SeancePlanning_Model = new Default_Details_SeancePlanning_Model();
			Default_Details_SeancePlanning_Model.toStringValue = SeancePlanning.ToString();
			Default_Details_SeancePlanning_Model.Schedule = SeancePlanning.Schedule;
			Default_Details_SeancePlanning_Model.Training = SeancePlanning.Training;
			Default_Details_SeancePlanning_Model.SeanceDay = SeancePlanning.SeanceDay;
			Default_Details_SeancePlanning_Model.SeanceNumber = SeancePlanning.SeanceNumber;
			Default_Details_SeancePlanning_Model.Classroom = SeancePlanning.Classroom;
			Default_Details_SeancePlanning_Model.Description = SeancePlanning.Description;
			Default_Details_SeancePlanning_Model.Id = SeancePlanning.Id;
            return Default_Details_SeancePlanning_Model;            
        }

		public virtual Default_Details_SeancePlanning_Model CreateNew()
        {
            SeancePlanning SeancePlanning = new SeancePlanningBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_SeancePlanning_Model Default_Details_SeancePlanning_Model = this.ConverTo_Default_Details_SeancePlanning_Model(SeancePlanning);
            return Default_Details_SeancePlanning_Model;
        } 

		public virtual List<Default_Details_SeancePlanning_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            SeancePlanningBLO entityBLO = new SeancePlanningBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<SeancePlanning> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_SeancePlanning_Model> ls_models = new List<Default_Details_SeancePlanning_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_SeancePlanning_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_SeancePlanning_ModelBLM : BaseDefault_Details_SeancePlanning_Model_Index_BLM
	{
		public Default_Details_SeancePlanning_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
