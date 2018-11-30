//modelType = SeanceTraining_Export_Model

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
	public partial class BaseSeanceTraining_Export_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseSeanceTraining_Export_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual SeanceTraining ConverTo_SeanceTraining(SeanceTraining_Export_Model SeanceTraining_Export_Model)
        {
			SeanceTraining SeanceTraining = null;
            if (SeanceTraining_Export_Model.Id != 0)
            {
                SeanceTraining = new SeanceTrainingBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(SeanceTraining_Export_Model.Id);
            }
            else
            {
                SeanceTraining = new SeanceTraining();
            } 
			SeanceTraining.SeanceDate = SeanceTraining_Export_Model.SeanceDate;
			SeanceTraining.Plurality = SeanceTraining_Export_Model.Plurality;
			SeanceTraining.Contained = SeanceTraining_Export_Model.Contained;
			SeanceTraining.Absences = SeanceTraining_Export_Model.Absences;
			SeanceTraining.Id = SeanceTraining_Export_Model.Id;
            return SeanceTraining;
        }
        public virtual SeanceTraining_Export_Model ConverTo_SeanceTraining_Export_Model(SeanceTraining SeanceTraining)
        {  
			SeanceTraining_Export_Model SeanceTraining_Export_Model = new SeanceTraining_Export_Model();
			SeanceTraining_Export_Model.toStringValue = SeanceTraining.ToString();
			SeanceTraining_Export_Model.SeanceDate = ConversionUtil.DefaultValue_if_Null<DateTime>(SeanceTraining.SeanceDate);
			SeanceTraining_Export_Model.Plurality = SeanceTraining.Plurality;
			SeanceTraining_Export_Model.Contained = SeanceTraining.Contained;
			SeanceTraining_Export_Model.Absences = SeanceTraining.Absences;
			SeanceTraining_Export_Model.Id = SeanceTraining.Id;
            return SeanceTraining_Export_Model;            
        }

		public virtual SeanceTraining_Export_Model CreateNew()
        {
            SeanceTraining SeanceTraining = new SeanceTrainingBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            SeanceTraining_Export_Model SeanceTraining_Export_Model = this.ConverTo_SeanceTraining_Export_Model(SeanceTraining);
            return SeanceTraining_Export_Model;
        } 

		public virtual List<SeanceTraining_Export_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            SeanceTrainingBLO entityBLO = new SeanceTrainingBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<SeanceTraining> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<SeanceTraining_Export_Model> ls_models = new List<SeanceTraining_Export_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_SeanceTraining_Export_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class SeanceTraining_Export_ModelBLM : BaseSeanceTraining_Export_Model_BLM
	{
		public SeanceTraining_Export_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
