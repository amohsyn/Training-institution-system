//modelType = Default_Form_AuthrorizationApp_Model

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
	public partial class BaseDefault_Form_AuthrorizationApp_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_AuthrorizationApp_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual AuthrorizationApp ConverTo_AuthrorizationApp(Default_Form_AuthrorizationApp_Model Default_Form_AuthrorizationApp_Model)
        {
			AuthrorizationApp AuthrorizationApp = null;
            if (Default_Form_AuthrorizationApp_Model.Id != 0)
            {
                AuthrorizationApp = new AuthrorizationAppBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_AuthrorizationApp_Model.Id);
            }
            else
            {
                AuthrorizationApp = new AuthrorizationApp();
            } 
			AuthrorizationApp.RoleAppId = Default_Form_AuthrorizationApp_Model.RoleAppId;
			AuthrorizationApp.RoleApp = new RoleAppBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_AuthrorizationApp_Model.RoleAppId)) ;
			AuthrorizationApp.ControllerAppId = Default_Form_AuthrorizationApp_Model.ControllerAppId;
			AuthrorizationApp.ControllerApp = new ControllerAppBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_AuthrorizationApp_Model.ControllerAppId)) ;
			AuthrorizationApp.isAllAction = Default_Form_AuthrorizationApp_Model.isAllAction;
			// ActionControllerApp
            ActionControllerAppBLO ActionControllerAppBLO = new ActionControllerAppBLO(this.UnitOfWork,this.GAppContext);

			if (AuthrorizationApp.ActionControllerApps != null)
                AuthrorizationApp.ActionControllerApps.Clear();
            else
                AuthrorizationApp.ActionControllerApps = new List<ActionControllerApp>();

			if(Default_Form_AuthrorizationApp_Model.Selected_ActionControllerApps != null)
			{
				foreach (string Selected_ActionControllerApp_Id in Default_Form_AuthrorizationApp_Model.Selected_ActionControllerApps)
				{
					Int64 Selected_ActionControllerApp_Id_Int64 = Convert.ToInt64(Selected_ActionControllerApp_Id);
					ActionControllerApp ActionControllerApp =ActionControllerAppBLO.FindBaseEntityByID(Selected_ActionControllerApp_Id_Int64);
					AuthrorizationApp.ActionControllerApps.Add(ActionControllerApp);
				}
			}
	
			AuthrorizationApp.Id = Default_Form_AuthrorizationApp_Model.Id;
            return AuthrorizationApp;
        }
        public virtual Default_Form_AuthrorizationApp_Model ConverTo_Default_Form_AuthrorizationApp_Model(AuthrorizationApp AuthrorizationApp)
        {  
			Default_Form_AuthrorizationApp_Model Default_Form_AuthrorizationApp_Model = new Default_Form_AuthrorizationApp_Model();
			Default_Form_AuthrorizationApp_Model.toStringValue = AuthrorizationApp.ToString();
			Default_Form_AuthrorizationApp_Model.RoleAppId = AuthrorizationApp.RoleAppId;
			Default_Form_AuthrorizationApp_Model.ControllerAppId = AuthrorizationApp.ControllerAppId;
			Default_Form_AuthrorizationApp_Model.isAllAction = AuthrorizationApp.isAllAction;

			// ActionControllerApp
            if (AuthrorizationApp.ActionControllerApps != null && AuthrorizationApp.ActionControllerApps.Count > 0)
            {
                Default_Form_AuthrorizationApp_Model.Selected_ActionControllerApps = AuthrorizationApp
                                                        .ActionControllerApps
                                                        .Select(entity => entity.Id.ToString())
                                                        .ToList<string>();
            }  
            else
            {
                Default_Form_AuthrorizationApp_Model.Selected_ActionControllerApps = new List<string>();
            }			
			Default_Form_AuthrorizationApp_Model.Id = AuthrorizationApp.Id;
            return Default_Form_AuthrorizationApp_Model;            
        }

		public virtual Default_Form_AuthrorizationApp_Model CreateNew()
        {
            AuthrorizationApp AuthrorizationApp = new AuthrorizationAppBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_AuthrorizationApp_Model Default_Form_AuthrorizationApp_Model = this.ConverTo_Default_Form_AuthrorizationApp_Model(AuthrorizationApp);
            return Default_Form_AuthrorizationApp_Model;
        } 

		public virtual List<Default_Form_AuthrorizationApp_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            AuthrorizationAppBLO entityBLO = new AuthrorizationAppBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<AuthrorizationApp> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Form_AuthrorizationApp_Model> ls_models = new List<Default_Form_AuthrorizationApp_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Form_AuthrorizationApp_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Form_AuthrorizationApp_ModelBLM : BaseDefault_Form_AuthrorizationApp_ModelBLM
	{
		public Default_Form_AuthrorizationApp_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
