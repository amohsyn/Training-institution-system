using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using TrainingIS.DAL;
using GApp.Core.Utils;
using GApp.Entities;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_AuthrorizationAppFormViewBLM : ViewModelBLM
    {
        
        public BaseDefault_AuthrorizationAppFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual AuthrorizationApp ConverTo_AuthrorizationApp(Default_AuthrorizationAppFormView Default_AuthrorizationAppFormView)
        {
			AuthrorizationApp AuthrorizationApp = null;
            if (Default_AuthrorizationAppFormView.Id != 0)
            {
                AuthrorizationApp = new AuthrorizationAppBLO(this.UnitOfWork).FindBaseEntityByID(Default_AuthrorizationAppFormView.Id);
            }
            else
            {
                AuthrorizationApp = new AuthrorizationApp();
            } 
			AuthrorizationApp.RoleAppId = Default_AuthrorizationAppFormView.RoleAppId;
			AuthrorizationApp.ControllerAppId = Default_AuthrorizationAppFormView.ControllerAppId;
			AuthrorizationApp.isAllAction = Default_AuthrorizationAppFormView.isAllAction;
			// ActionControllerApp
            ActionControllerAppBLO ActionControllerAppBLO = new ActionControllerAppBLO(this.UnitOfWork);

			if (AuthrorizationApp.ActionControllerApps != null)
                AuthrorizationApp.ActionControllerApps.Clear();
            else
                AuthrorizationApp.ActionControllerApps = new List<ActionControllerApp>();

			if(Default_AuthrorizationAppFormView.Selected_ActionControllerApps != null)
			{
				foreach (string Selected_ActionControllerApp_Id in Default_AuthrorizationAppFormView.Selected_ActionControllerApps)
				{
					Int64 Selected_ActionControllerApp_Id_Int64 = Convert.ToInt64(Selected_ActionControllerApp_Id);
					ActionControllerApp ActionControllerApp =ActionControllerAppBLO.FindBaseEntityByID(Selected_ActionControllerApp_Id_Int64);
					AuthrorizationApp.ActionControllerApps.Add(ActionControllerApp);
				}
			}
	
			AuthrorizationApp.Id = Default_AuthrorizationAppFormView.Id;
            return AuthrorizationApp;
        }
        public virtual Default_AuthrorizationAppFormView ConverTo_Default_AuthrorizationAppFormView(AuthrorizationApp AuthrorizationApp)
        {  
			Default_AuthrorizationAppFormView Default_AuthrorizationAppFormView = new Default_AuthrorizationAppFormView();
			Default_AuthrorizationAppFormView.toStringValue = AuthrorizationApp.ToString();
			Default_AuthrorizationAppFormView.RoleAppId = AuthrorizationApp.RoleAppId;
			Default_AuthrorizationAppFormView.ControllerAppId = AuthrorizationApp.ControllerAppId;
			Default_AuthrorizationAppFormView.isAllAction = AuthrorizationApp.isAllAction;

			// ActionControllerApp
            ActionControllerAppBLO ActionControllerAppBLO = new ActionControllerAppBLO(this.UnitOfWork);
            Default_AuthrorizationAppFormView.All_ActionControllerApps = ActionControllerAppBLO.FindAll().ToList<BaseEntity>();
           
            if (AuthrorizationApp.ActionControllerApps != null && AuthrorizationApp.ActionControllerApps.Count > 0)
            {
                Default_AuthrorizationAppFormView.Selected_ActionControllerApps = AuthrorizationApp
                                                        .ActionControllerApps
                                                        .Select(entity => entity.Id.ToString())
                                                        .ToList<string>();
            }  
            else
            {
                Default_AuthrorizationAppFormView.Selected_ActionControllerApps = new List<string>();
            }			
			Default_AuthrorizationAppFormView.Id = AuthrorizationApp.Id;
            return Default_AuthrorizationAppFormView;            
        }

		public virtual Default_AuthrorizationAppFormView CreateNew()
        {
            AuthrorizationApp AuthrorizationApp = new AuthrorizationApp();
            Default_AuthrorizationAppFormView Default_AuthrorizationAppFormView = this.ConverTo_Default_AuthrorizationAppFormView(AuthrorizationApp);
            return Default_AuthrorizationAppFormView;
        } 
    }

	public partial class Default_AuthrorizationAppFormViewBLM : BaseDefault_AuthrorizationAppFormViewBLM
	{
		public Default_AuthrorizationAppFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
