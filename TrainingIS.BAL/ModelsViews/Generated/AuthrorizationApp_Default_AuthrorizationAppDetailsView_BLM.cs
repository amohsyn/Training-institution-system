using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using TrainingIS.DAL;
using GApp.Core.Utils;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_AuthrorizationAppDetailsViewBLM : ViewModelBLM
    {
        
        public BaseDefault_AuthrorizationAppDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual AuthrorizationApp ConverTo_AuthrorizationApp(Default_AuthrorizationAppDetailsView Default_AuthrorizationAppDetailsView)
        {
			AuthrorizationApp AuthrorizationApp = null;
            if (Default_AuthrorizationAppDetailsView.Id != 0)
            {
                AuthrorizationApp = new AuthrorizationAppBLO(this.UnitOfWork).FindBaseEntityByID(Default_AuthrorizationAppDetailsView.Id);
            }
            else
            {
                AuthrorizationApp = new AuthrorizationApp();
            } 
			AuthrorizationApp.RoleApp = Default_AuthrorizationAppDetailsView.RoleApp;
			AuthrorizationApp.ControllerApp = Default_AuthrorizationAppDetailsView.ControllerApp;
			AuthrorizationApp.isAllAction = Default_AuthrorizationAppDetailsView.isAllAction;
			AuthrorizationApp.ActionControllerApps = Default_AuthrorizationAppDetailsView.ActionControllerApps;
			AuthrorizationApp.Id = Default_AuthrorizationAppDetailsView.Id;
            return AuthrorizationApp;
        }
        public virtual Default_AuthrorizationAppDetailsView ConverTo_Default_AuthrorizationAppDetailsView(AuthrorizationApp AuthrorizationApp)
        {  
			Default_AuthrorizationAppDetailsView Default_AuthrorizationAppDetailsView = new Default_AuthrorizationAppDetailsView();
			Default_AuthrorizationAppDetailsView.toStringValue = AuthrorizationApp.ToString();
			Default_AuthrorizationAppDetailsView.RoleApp = AuthrorizationApp.RoleApp;
			Default_AuthrorizationAppDetailsView.ControllerApp = AuthrorizationApp.ControllerApp;
			Default_AuthrorizationAppDetailsView.isAllAction = AuthrorizationApp.isAllAction;
			Default_AuthrorizationAppDetailsView.ActionControllerApps = AuthrorizationApp.ActionControllerApps;
			Default_AuthrorizationAppDetailsView.Id = AuthrorizationApp.Id;
            return Default_AuthrorizationAppDetailsView;            
        }
    }

	public partial class Default_AuthrorizationAppDetailsViewBLM : BaseDefault_AuthrorizationAppDetailsViewBLM
	{
		public Default_AuthrorizationAppDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
