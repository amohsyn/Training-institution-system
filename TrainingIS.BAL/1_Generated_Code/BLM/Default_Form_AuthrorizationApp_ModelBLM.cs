using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;
using GApp.Core.Utils;
using GApp.Entities;
using GApp.DAL;
using TrainingIS.Entities.ModelsViews;
using GApp.Entities;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Form_AuthrorizationApp_ModelBLM : BaseModelBLM
    {
        
        public BaseDefault_Form_AuthrorizationApp_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork)
        {

        }

        public virtual AuthrorizationApp ConverTo_AuthrorizationApp(Default_Form_AuthrorizationApp_Model Default_Form_AuthrorizationApp_Model)
        {
			AuthrorizationApp AuthrorizationApp = null;
            if (Default_Form_AuthrorizationApp_Model.Id != 0)
            {
                AuthrorizationApp = new AuthrorizationAppBLO(this.UnitOfWork).FindBaseEntityByID(Default_Form_AuthrorizationApp_Model.Id);
            }
            else
            {
                AuthrorizationApp = new AuthrorizationApp();
            } 
			AuthrorizationApp.RoleApp = Default_Form_AuthrorizationApp_Model.RoleApp;
			AuthrorizationApp.ControllerApp = Default_Form_AuthrorizationApp_Model.ControllerApp;
			AuthrorizationApp.isAllAction = Default_Form_AuthrorizationApp_Model.isAllAction;
			// ActionControllerApp
            ActionControllerAppBLO ActionControllerAppBLO = new ActionControllerAppBLO(this.UnitOfWork);

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
			Default_Form_AuthrorizationApp_Model.RoleApp = AuthrorizationApp.RoleApp;
			Default_Form_AuthrorizationApp_Model.ControllerApp = AuthrorizationApp.ControllerApp;
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
            AuthrorizationApp AuthrorizationApp = new AuthrorizationApp();
            Default_Form_AuthrorizationApp_Model Default_Form_AuthrorizationApp_Model = this.ConverTo_Default_Form_AuthrorizationApp_Model(AuthrorizationApp);
            return Default_Form_AuthrorizationApp_Model;
        } 
    }

	public partial class Default_Form_AuthrorizationApp_ModelBLM : BaseDefault_Form_AuthrorizationApp_ModelBLM
	{
		public Default_Form_AuthrorizationApp_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
