//modelType = Default_SeanceTraining_Export_Model

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
	public partial class BaseDefault_SeanceTraining_Export_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_SeanceTraining_Export_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual SeanceTraining ConverTo_SeanceTraining(Default_SeanceTraining_Export_Model Default_SeanceTraining_Export_Model)
        {
			SeanceTraining SeanceTraining = null;
            if (Default_SeanceTraining_Export_Model.Id != 0)
            {
                SeanceTraining = new SeanceTrainingBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_SeanceTraining_Export_Model.Id);
            }
            else
            {
                SeanceTraining = new SeanceTraining();
            } 
			SeanceTraining.SeanceDate = Default_SeanceTraining_Export_Model.SeanceDate;
			SeanceTraining.SeancePlanning = Default_SeanceTraining_Export_Model.SeancePlanning;
			SeanceTraining.Contained = Default_SeanceTraining_Export_Model.Contained;
			SeanceTraining.FormerValidation = Default_SeanceTraining_Export_Model.FormerValidation;
			SeanceTraining.Absences = Default_SeanceTraining_Export_Model.Absences;
			SeanceTraining.Id = Default_SeanceTraining_Export_Model.Id;
            return SeanceTraining;
        }
        public virtual Default_SeanceTraining_Export_Model ConverTo_Default_SeanceTraining_Export_Model(SeanceTraining SeanceTraining)
        {  
			Default_SeanceTraining_Export_Model Default_SeanceTraining_Export_Model = new Default_SeanceTraining_Export_Model();
			Default_SeanceTraining_Export_Model.toStringValue = SeanceTraining.ToString();
			Default_SeanceTraining_Export_Model.SeanceDate = ConversionUtil.DefaultValue_if_Null<DateTime>(SeanceTraining.SeanceDate);
			Default_SeanceTraining_Export_Model.SeancePlanning = SeanceTraining.SeancePlanning;
			Default_SeanceTraining_Export_Model.Contained = SeanceTraining.Contained;
			Default_SeanceTraining_Export_Model.FormerValidation = SeanceTraining.FormerValidation;
			Default_SeanceTraining_Export_Model.Absences = SeanceTraining.Absences;
			Default_SeanceTraining_Export_Model.Id = SeanceTraining.Id;
            return Default_SeanceTraining_Export_Model;            
        }

		public virtual Default_SeanceTraining_Export_Model CreateNew()
        {
            SeanceTraining SeanceTraining = new SeanceTrainingBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_SeanceTraining_Export_Model Default_SeanceTraining_Export_Model = this.ConverTo_Default_SeanceTraining_Export_Model(SeanceTraining);
            return Default_SeanceTraining_Export_Model;
        } 

		public virtual List<Default_SeanceTraining_Export_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            SeanceTrainingBLO entityBLO = new SeanceTrainingBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<SeanceTraining> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_SeanceTraining_Export_Model> ls_models = new List<Default_SeanceTraining_Export_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_SeanceTraining_Export_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_SeanceTraining_Export_ModelBLM : BaseDefault_SeanceTraining_Export_Model_BLM
	{
		public Default_SeanceTraining_Export_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
