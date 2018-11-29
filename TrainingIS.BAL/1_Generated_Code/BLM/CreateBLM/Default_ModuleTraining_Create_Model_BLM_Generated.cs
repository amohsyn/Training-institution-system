//modelType = Default_ModuleTraining_Create_Model

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
	public partial class BaseDefault_ModuleTraining_Create_Model_BLM : BaseModelBLM
    {
       
        public GAppContext GAppContext {set;get;} 
		public Default_Form_ModuleTraining_ModelBLM Default_Form_ModuleTraining_ModelBLM {set;get;}
        
		public BaseDefault_ModuleTraining_Create_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
			Default_Form_ModuleTraining_ModelBLM = new Default_Form_ModuleTraining_ModelBLM(this.UnitOfWork, this.GAppContext);
        }

		public virtual ModuleTraining ConverTo_ModuleTraining(Default_ModuleTraining_Create_Model Default_ModuleTraining_Create_Model)
        {
            var ModuleTraining = Default_Form_ModuleTraining_ModelBLM.ConverTo_ModuleTraining(Default_ModuleTraining_Create_Model);
            return ModuleTraining;
        }

		public virtual Default_ModuleTraining_Create_Model ConverTo_Default_ModuleTraining_Create_Model(ModuleTraining ModuleTraining)
        {
            Default_ModuleTraining_Create_Model Default_ModuleTraining_Create_Model = new Default_ModuleTraining_Create_Model();
            Default_Form_ModuleTraining_ModelBLM.ConverTo_Default_Form_ModuleTraining_Model(Default_ModuleTraining_Create_Model, ModuleTraining);
            return Default_ModuleTraining_Create_Model;            
        }

		public virtual Default_ModuleTraining_Create_Model CreateNew()
        {
            ModuleTraining ModuleTraining = new ModuleTrainingBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_ModuleTraining_Create_Model Default_ModuleTraining_Create_Model = this.ConverTo_Default_ModuleTraining_Create_Model(ModuleTraining);
            return Default_ModuleTraining_Create_Model;
        } 

		public virtual List<Default_ModuleTraining_Create_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            ModuleTrainingBLO entityBLO = new ModuleTrainingBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<ModuleTraining> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_ModuleTraining_Create_Model> ls_models = new List<Default_ModuleTraining_Create_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_ModuleTraining_Create_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_ModuleTraining_Create_ModelBLM : BaseDefault_ModuleTraining_Create_Model_BLM
	{
		public Default_ModuleTraining_Create_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
