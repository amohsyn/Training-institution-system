using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using TrainingIS.Entities.ModelsViews.Authorizations;

namespace TrainingIS.BLL.ModelsViews
{
    public partial class AuthrorizationAppFormViewBLM
    {
        public override AuthrorizationAppFormView ConverTo_AuthrorizationAppFormView(AuthrorizationApp AuthrorizationApp)
        {
            AuthrorizationAppFormView AuthrorizationAppFormView = new AuthrorizationAppFormView();
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
}
