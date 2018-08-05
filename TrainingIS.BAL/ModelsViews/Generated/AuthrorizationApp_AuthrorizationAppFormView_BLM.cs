using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.ModelsViews.Authorizations;
using TrainingIS.Entities;
using TrainingIS.DAL;
using GApp.Core.Utils;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseAuthrorizationAppFormViewBLM : ViewModelBLM
    {
        
        public BaseAuthrorizationAppFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual AuthrorizationApp ConverTo_AuthrorizationApp(AuthrorizationAppFormView AuthrorizationAppFormView)
        {
			AuthrorizationApp AuthrorizationApp = null;
            if (AuthrorizationAppFormView.Id != 0)
            {
                AuthrorizationApp = new AuthrorizationAppBLO(this.UnitOfWork).FindBaseEntityByID(AuthrorizationAppFormView.Id);
            }
            else
            {
                AuthrorizationApp = new AuthrorizationApp();
            } 
			AuthrorizationApp.RoleAppId = AuthrorizationAppFormView.RoleAppId;
			AuthrorizationApp.ControllerAppId = AuthrorizationAppFormView.ControllerAppId;
			AuthrorizationApp.isAllAction = AuthrorizationAppFormView.isAllAction;
			// ActionControllerApp
            ActionControllerAppBLO ActionControllerAppBLO = new ActionControllerAppBLO(this.UnitOfWork);

			if (AuthrorizationApp.ActionControllerApps != null)
                AuthrorizationApp.ActionControllerApps.Clear();
            else
                AuthrorizationApp.ActionControllerApps = new List<ActionControllerApp>();

			if(AuthrorizationAppFormView.Selected_ActionControllerApps != null)
			{
				foreach (string Selected_ActionControllerApp_Id in AuthrorizationAppFormView.Selected_ActionControllerApps)
				{
					Int64 Selected_ActionControllerApp_Id_Int64 = Convert.ToInt64(Selected_ActionControllerApp_Id);
					ActionControllerApp ActionControllerApp =ActionControllerAppBLO.FindBaseEntityByID(Selected_ActionControllerApp_Id_Int64);
					AuthrorizationApp.ActionControllerApps.Add(ActionControllerApp);
				}
			}
	
			AuthrorizationApp.Id = AuthrorizationAppFormView.Id;
            return AuthrorizationApp;
        }
        public virtual AuthrorizationAppFormView ConverTo_AuthrorizationAppFormView(AuthrorizationApp AuthrorizationApp)
        {  
			AuthrorizationAppFormView AuthrorizationAppFormView = new AuthrorizationAppFormView();
			AuthrorizationAppFormView.toStringValue = AuthrorizationApp.ToString();
			AuthrorizationAppFormView.RoleAppId = AuthrorizationApp.RoleAppId;
			AuthrorizationAppFormView.ControllerAppId = AuthrorizationApp.ControllerAppId;
			AuthrorizationAppFormView.isAllAction = AuthrorizationApp.isAllAction;

			// ActionControllerApp
            ActionControllerAppBLO ActionControllerAppBLO = new ActionControllerAppBLO(this.UnitOfWork);
            AuthrorizationAppFormView.All_ActionControllerApps = ActionControllerAppBLO.FindAll();
       


            if (AuthrorizationApp.ActionControllerApps != null && AuthrorizationApp.ActionControllerApps.Count > 0)
            {
                AuthrorizationAppFormView.Selected_ActionControllerApps = AuthrorizationApp
                                                        .ActionControllerApps
                                                        .Select(entity => entity.Id.ToString())
                                                        .ToList<string>();
            }
            else
            {
                AuthrorizationAppFormView.Selected_ActionControllerApps = new List<string>();
            }			
			AuthrorizationAppFormView.Id = AuthrorizationApp.Id;
            return AuthrorizationAppFormView;            
        }
    }

	public partial class AuthrorizationAppFormViewBLM : BaseAuthrorizationAppFormViewBLM
	{
		public AuthrorizationAppFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
