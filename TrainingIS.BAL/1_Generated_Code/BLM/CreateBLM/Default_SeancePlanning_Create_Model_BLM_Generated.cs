//modelType = Default_SeancePlanning_Create_Model

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
	public partial class BaseDefault_SeancePlanning_Create_Model_BLM : BaseModelBLM
    {
       
        public GAppContext GAppContext {set;get;} 
		private Default_Form_SeancePlanning_ModelBLM Default_Form_SeancePlanning_ModelBLM {set;get;}
        
		public BaseDefault_SeancePlanning_Create_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
			Default_Form_SeancePlanning_ModelBLM = new Default_Form_SeancePlanning_ModelBLM(this.UnitOfWork, this.GAppContext);
        }

		public virtual SeancePlanning ConverTo_SeancePlanning(Default_SeancePlanning_Create_Model Default_SeancePlanning_Create_Model)
        {
            var SeancePlanning = Default_Form_SeancePlanning_ModelBLM.ConverTo_SeancePlanning(Default_SeancePlanning_Create_Model);
            return SeancePlanning;
        }

		public virtual Default_SeancePlanning_Create_Model ConverTo_Default_SeancePlanning_Create_Model(SeancePlanning SeancePlanning)
        {
            Default_SeancePlanning_Create_Model Default_SeancePlanning_Create_Model = new Default_SeancePlanning_Create_Model();
            Default_Form_SeancePlanning_ModelBLM.ConverTo_Default_Form_SeancePlanning_Model(Default_SeancePlanning_Create_Model, SeancePlanning);
            return Default_SeancePlanning_Create_Model;            
        }

		public virtual Default_SeancePlanning_Create_Model CreateNew()
        {
            SeancePlanning SeancePlanning = new SeancePlanningBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_SeancePlanning_Create_Model Default_SeancePlanning_Create_Model = this.ConverTo_Default_SeancePlanning_Create_Model(SeancePlanning);
            return Default_SeancePlanning_Create_Model;
        } 

		public virtual List<Default_SeancePlanning_Create_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            SeancePlanningBLO entityBLO = new SeancePlanningBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<SeancePlanning> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_SeancePlanning_Create_Model> ls_models = new List<Default_SeancePlanning_Create_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_SeancePlanning_Create_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_SeancePlanning_Create_ModelBLM : BaseDefault_SeancePlanning_Create_Model_BLM
	{
		public Default_SeancePlanning_Create_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
