//modelType = Default_Training_Index_Model

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
	public partial class BaseDefault_Training_Index_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Training_Index_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Training ConverTo_Training(Default_Training_Index_Model Default_Training_Index_Model)
        {
			Training Training = null;
            if (Default_Training_Index_Model.Id != 0)
            {
                Training = new TrainingBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Training_Index_Model.Id);
            }
            else
            {
                Training = new Training();
            } 
			Training.TrainingYear = Default_Training_Index_Model.TrainingYear;
			Training.ModuleTraining = Default_Training_Index_Model.ModuleTraining;
			Training.Hourly_Mass_To_Teach = Default_Training_Index_Model.Hourly_Mass_To_Teach;
			Training.Former = Default_Training_Index_Model.Former;
			Training.Group = Default_Training_Index_Model.Group;
			Training.Code = Default_Training_Index_Model.Code;
			Training.Description = Default_Training_Index_Model.Description;
			Training.Id = Default_Training_Index_Model.Id;
            return Training;
        }
        public virtual Default_Training_Index_Model ConverTo_Default_Training_Index_Model(Training Training)
        {  
			Default_Training_Index_Model Default_Training_Index_Model = new Default_Training_Index_Model();
			Default_Training_Index_Model.toStringValue = Training.ToString();
			Default_Training_Index_Model.TrainingYear = Training.TrainingYear;
			Default_Training_Index_Model.ModuleTraining = Training.ModuleTraining;
			Default_Training_Index_Model.Hourly_Mass_To_Teach = Training.Hourly_Mass_To_Teach;
			Default_Training_Index_Model.Former = Training.Former;
			Default_Training_Index_Model.Group = Training.Group;
			Default_Training_Index_Model.Code = Training.Code;
			Default_Training_Index_Model.Description = Training.Description;
			Default_Training_Index_Model.Id = Training.Id;
            return Default_Training_Index_Model;            
        }

		public virtual Default_Training_Index_Model CreateNew()
        {
            Training Training = new TrainingBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Training_Index_Model Default_Training_Index_Model = this.ConverTo_Default_Training_Index_Model(Training);
            return Default_Training_Index_Model;
        } 

		public virtual List<Default_Training_Index_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            TrainingBLO entityBLO = new TrainingBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Training> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Training_Index_Model> ls_models = new List<Default_Training_Index_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Training_Index_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Training_Index_ModelBLM : BaseDefault_Training_Index_Model_BLM
	{
		public Default_Training_Index_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
