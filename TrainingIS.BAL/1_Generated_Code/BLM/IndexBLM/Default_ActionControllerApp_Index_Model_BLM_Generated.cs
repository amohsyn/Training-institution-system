//modelType = Default_ActionControllerApp_Index_Model

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
	public partial class BaseDefault_ActionControllerApp_Index_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_ActionControllerApp_Index_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual ActionControllerApp ConverTo_ActionControllerApp(Default_ActionControllerApp_Index_Model Default_ActionControllerApp_Index_Model)
        {
			ActionControllerApp ActionControllerApp = null;
            if (Default_ActionControllerApp_Index_Model.Id != 0)
            {
                ActionControllerApp = new ActionControllerAppBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_ActionControllerApp_Index_Model.Id);
            }
            else
            {
                ActionControllerApp = new ActionControllerApp();
            } 
			ActionControllerApp.Code = Default_ActionControllerApp_Index_Model.Code;
			ActionControllerApp.Name = Default_ActionControllerApp_Index_Model.Name;
			ActionControllerApp.Description = Default_ActionControllerApp_Index_Model.Description;
			ActionControllerApp.ControllerApp = Default_ActionControllerApp_Index_Model.ControllerApp;
			ActionControllerApp.Id = Default_ActionControllerApp_Index_Model.Id;
            return ActionControllerApp;
        }
        public virtual Default_ActionControllerApp_Index_Model ConverTo_Default_ActionControllerApp_Index_Model(ActionControllerApp ActionControllerApp)
        {  
			Default_ActionControllerApp_Index_Model Default_ActionControllerApp_Index_Model = new Default_ActionControllerApp_Index_Model();
			Default_ActionControllerApp_Index_Model.toStringValue = ActionControllerApp.ToString();
			Default_ActionControllerApp_Index_Model.Code = ActionControllerApp.Code;
			Default_ActionControllerApp_Index_Model.Name = ActionControllerApp.Name;
			Default_ActionControllerApp_Index_Model.Description = ActionControllerApp.Description;
			Default_ActionControllerApp_Index_Model.ControllerApp = ActionControllerApp.ControllerApp;
			Default_ActionControllerApp_Index_Model.Id = ActionControllerApp.Id;
            return Default_ActionControllerApp_Index_Model;            
        }

		public virtual Default_ActionControllerApp_Index_Model CreateNew()
        {
            ActionControllerApp ActionControllerApp = new ActionControllerAppBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_ActionControllerApp_Index_Model Default_ActionControllerApp_Index_Model = this.ConverTo_Default_ActionControllerApp_Index_Model(ActionControllerApp);
            return Default_ActionControllerApp_Index_Model;
        } 

		public virtual List<Default_ActionControllerApp_Index_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            ActionControllerAppBLO entityBLO = new ActionControllerAppBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<ActionControllerApp> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_ActionControllerApp_Index_Model> ls_models = new List<Default_ActionControllerApp_Index_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_ActionControllerApp_Index_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_ActionControllerApp_Index_ModelBLM : BaseDefault_ActionControllerApp_Index_Model_BLM
	{
		public Default_ActionControllerApp_Index_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
