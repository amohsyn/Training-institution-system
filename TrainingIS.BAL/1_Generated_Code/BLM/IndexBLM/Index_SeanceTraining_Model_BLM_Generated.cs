//modelType = Index_SeanceTraining_Model

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
using TrainingIS.Models.SeanceTrainings;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseIndex_SeanceTraining_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseIndex_SeanceTraining_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual SeanceTraining ConverTo_SeanceTraining(Index_SeanceTraining_Model Index_SeanceTraining_Model)
        {
			SeanceTraining SeanceTraining = null;
            if (Index_SeanceTraining_Model.Id != 0)
            {
                SeanceTraining = new SeanceTrainingBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Index_SeanceTraining_Model.Id);
            }
            else
            {
                SeanceTraining = new SeanceTraining();
            } 
			SeanceTraining.SeanceDate = Index_SeanceTraining_Model.SeanceDate;
			SeanceTraining.Contained = Index_SeanceTraining_Model.Contained;
			SeanceTraining.FormerValidation = Index_SeanceTraining_Model.FormerValidation;
			SeanceTraining.Absences = Index_SeanceTraining_Model.Absences;
			SeanceTraining.Id = Index_SeanceTraining_Model.Id;
            return SeanceTraining;
        }
        public virtual Index_SeanceTraining_Model ConverTo_Index_SeanceTraining_Model(SeanceTraining SeanceTraining)
        {  
			Index_SeanceTraining_Model Index_SeanceTraining_Model = new Index_SeanceTraining_Model();
			Index_SeanceTraining_Model.toStringValue = SeanceTraining.ToString();
			Index_SeanceTraining_Model.SeanceDate = ConversionUtil.DefaultValue_if_Null<DateTime>(SeanceTraining.SeanceDate);
			Index_SeanceTraining_Model.Contained = SeanceTraining.Contained;
			Index_SeanceTraining_Model.FormerValidation = SeanceTraining.FormerValidation;
			Index_SeanceTraining_Model.Absences = SeanceTraining.Absences;
			Index_SeanceTraining_Model.Id = SeanceTraining.Id;
            return Index_SeanceTraining_Model;            
        }

		public virtual Index_SeanceTraining_Model CreateNew()
        {
            SeanceTraining SeanceTraining = new SeanceTrainingBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Index_SeanceTraining_Model Index_SeanceTraining_Model = this.ConverTo_Index_SeanceTraining_Model(SeanceTraining);
            return Index_SeanceTraining_Model;
        } 

		public virtual List<Index_SeanceTraining_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            SeanceTrainingBLO entityBLO = new SeanceTrainingBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<SeanceTraining> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Index_SeanceTraining_Model> ls_models = new List<Index_SeanceTraining_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Index_SeanceTraining_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Index_SeanceTraining_ModelBLM : BaseIndex_SeanceTraining_Model_BLM
	{
		public Index_SeanceTraining_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
