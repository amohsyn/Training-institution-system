//modelType = Default_AuthrorizationApp_Index_Model

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
	public partial class BaseDefault_AuthrorizationApp_Index_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_AuthrorizationApp_Index_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual AuthrorizationApp ConverTo_AuthrorizationApp(Default_AuthrorizationApp_Index_Model Default_AuthrorizationApp_Index_Model)
        {
			AuthrorizationApp AuthrorizationApp = null;
            if (Default_AuthrorizationApp_Index_Model.Id != 0)
            {
                AuthrorizationApp = new AuthrorizationAppBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_AuthrorizationApp_Index_Model.Id);
            }
            else
            {
                AuthrorizationApp = new AuthrorizationApp();
            } 
			AuthrorizationApp.RoleApp = Default_AuthrorizationApp_Index_Model.RoleApp;
			AuthrorizationApp.ControllerApp = Default_AuthrorizationApp_Index_Model.ControllerApp;
			AuthrorizationApp.isAllAction = Default_AuthrorizationApp_Index_Model.isAllAction;
			AuthrorizationApp.ActionControllerApps = Default_AuthrorizationApp_Index_Model.ActionControllerApps;
			AuthrorizationApp.Id = Default_AuthrorizationApp_Index_Model.Id;
            return AuthrorizationApp;
        }
        public virtual Default_AuthrorizationApp_Index_Model ConverTo_Default_AuthrorizationApp_Index_Model(AuthrorizationApp AuthrorizationApp)
        {  
			Default_AuthrorizationApp_Index_Model Default_AuthrorizationApp_Index_Model = new Default_AuthrorizationApp_Index_Model();
			Default_AuthrorizationApp_Index_Model.toStringValue = AuthrorizationApp.ToString();
			Default_AuthrorizationApp_Index_Model.RoleApp = AuthrorizationApp.RoleApp;
			Default_AuthrorizationApp_Index_Model.ControllerApp = AuthrorizationApp.ControllerApp;
			Default_AuthrorizationApp_Index_Model.isAllAction = AuthrorizationApp.isAllAction;
			Default_AuthrorizationApp_Index_Model.ActionControllerApps = AuthrorizationApp.ActionControllerApps;
			Default_AuthrorizationApp_Index_Model.Id = AuthrorizationApp.Id;
            return Default_AuthrorizationApp_Index_Model;            
        }

		public virtual Default_AuthrorizationApp_Index_Model CreateNew()
        {
            AuthrorizationApp AuthrorizationApp = new AuthrorizationAppBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_AuthrorizationApp_Index_Model Default_AuthrorizationApp_Index_Model = this.ConverTo_Default_AuthrorizationApp_Index_Model(AuthrorizationApp);
            return Default_AuthrorizationApp_Index_Model;
        } 

		public virtual List<Default_AuthrorizationApp_Index_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            AuthrorizationAppBLO entityBLO = new AuthrorizationAppBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<AuthrorizationApp> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_AuthrorizationApp_Index_Model> ls_models = new List<Default_AuthrorizationApp_Index_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_AuthrorizationApp_Index_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_AuthrorizationApp_Index_ModelBLM : BaseDefault_AuthrorizationApp_Index_Model_BLM
	{
		public Default_AuthrorizationApp_Index_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
