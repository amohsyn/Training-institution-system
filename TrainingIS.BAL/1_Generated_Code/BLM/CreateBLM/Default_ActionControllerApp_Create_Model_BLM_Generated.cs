//modelType = Default_ActionControllerApp_Create_Model

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
using GApp.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_ActionControllerApp_Create_Model_BLM : BaseModelBLM
    {
       
        public GAppContext GAppContext {set;get;} 
		private Default_Form_ActionControllerApp_ModelBLM Default_Form_ActionControllerApp_ModelBLM {set;get;}
        
		public BaseDefault_ActionControllerApp_Create_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
			Default_Form_ActionControllerApp_ModelBLM = new Default_Form_ActionControllerApp_ModelBLM(this.UnitOfWork, this.GAppContext);
        }

		public virtual ActionControllerApp ConverTo_ActionControllerApp(Default_ActionControllerApp_Create_Model Default_ActionControllerApp_Create_Model)
        {
            var ActionControllerApp = Default_Form_ActionControllerApp_ModelBLM.ConverTo_ActionControllerApp(Default_ActionControllerApp_Create_Model);
            return ActionControllerApp;
        }

		public virtual Default_ActionControllerApp_Create_Model ConverTo_Default_ActionControllerApp_Create_Model(ActionControllerApp ActionControllerApp)
        {
            Default_ActionControllerApp_Create_Model Default_ActionControllerApp_Create_Model = new Default_ActionControllerApp_Create_Model();
            Default_Form_ActionControllerApp_ModelBLM.ConverTo_Default_Form_ActionControllerApp_Model(Default_ActionControllerApp_Create_Model, ActionControllerApp);
            return Default_ActionControllerApp_Create_Model;            
        }

		public virtual Default_ActionControllerApp_Create_Model CreateNew()
        {
            ActionControllerApp ActionControllerApp = new ActionControllerAppBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_ActionControllerApp_Create_Model Default_ActionControllerApp_Create_Model = this.ConverTo_Default_ActionControllerApp_Create_Model(ActionControllerApp);
            return Default_ActionControllerApp_Create_Model;
        } 

		public virtual List<Default_ActionControllerApp_Create_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            ActionControllerAppBLO entityBLO = new ActionControllerAppBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<ActionControllerApp> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_ActionControllerApp_Create_Model> ls_models = new List<Default_ActionControllerApp_Create_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_ActionControllerApp_Create_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_ActionControllerApp_Create_ModelBLM : BaseDefault_ActionControllerApp_Create_Model_BLM
	{
		public Default_ActionControllerApp_Create_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
