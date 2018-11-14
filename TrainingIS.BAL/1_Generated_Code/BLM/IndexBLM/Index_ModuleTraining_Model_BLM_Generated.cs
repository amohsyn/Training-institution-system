//modelType = Index_ModuleTraining_Model

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
using TrainingIS.Models.ModuleTrainings;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseIndex_ModuleTraining_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseIndex_ModuleTraining_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual ModuleTraining ConverTo_ModuleTraining(Index_ModuleTraining_Model Index_ModuleTraining_Model)
        {
			ModuleTraining ModuleTraining = null;
            if (Index_ModuleTraining_Model.Id != 0)
            {
                ModuleTraining = new ModuleTrainingBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Index_ModuleTraining_Model.Id);
            }
            else
            {
                ModuleTraining = new ModuleTraining();
            } 
			ModuleTraining.Specialty = Index_ModuleTraining_Model.Specialty;
			ModuleTraining.Metier = Index_ModuleTraining_Model.Metier;
			ModuleTraining.YearStudy = Index_ModuleTraining_Model.YearStudy;
			ModuleTraining.Name = Index_ModuleTraining_Model.Name;
			ModuleTraining.Code = Index_ModuleTraining_Model.Code;
			ModuleTraining.HourlyMass = Index_ModuleTraining_Model.HourlyMass;
			ModuleTraining.Id = Index_ModuleTraining_Model.Id;
            return ModuleTraining;
        }
        public virtual Index_ModuleTraining_Model ConverTo_Index_ModuleTraining_Model(ModuleTraining ModuleTraining)
        {  
			Index_ModuleTraining_Model Index_ModuleTraining_Model = new Index_ModuleTraining_Model();
			Index_ModuleTraining_Model.toStringValue = ModuleTraining.ToString();
			Index_ModuleTraining_Model.Specialty = ModuleTraining.Specialty;
			Index_ModuleTraining_Model.Metier = ModuleTraining.Metier;
			Index_ModuleTraining_Model.YearStudy = ModuleTraining.YearStudy;
			Index_ModuleTraining_Model.Name = ModuleTraining.Name;
			Index_ModuleTraining_Model.Code = ModuleTraining.Code;
			Index_ModuleTraining_Model.HourlyMass = ModuleTraining.HourlyMass;
			Index_ModuleTraining_Model.Id = ModuleTraining.Id;
            return Index_ModuleTraining_Model;            
        }

		public virtual Index_ModuleTraining_Model CreateNew()
        {
            ModuleTraining ModuleTraining = new ModuleTrainingBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Index_ModuleTraining_Model Index_ModuleTraining_Model = this.ConverTo_Index_ModuleTraining_Model(ModuleTraining);
            return Index_ModuleTraining_Model;
        } 

		public virtual List<Index_ModuleTraining_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            ModuleTrainingBLO entityBLO = new ModuleTrainingBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<ModuleTraining> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Index_ModuleTraining_Model> ls_models = new List<Index_ModuleTraining_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Index_ModuleTraining_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Index_ModuleTraining_ModelBLM : BaseIndex_ModuleTraining_Model_BLM
	{
		public Index_ModuleTraining_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
