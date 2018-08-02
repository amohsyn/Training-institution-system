using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using TrainingIS.DAL;
namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_AuthrorizationAppDetailsViewBLM : ViewModelBLM
    {
        
        public BaseDefault_AuthrorizationAppDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual AuthrorizationApp ConverTo_AuthrorizationApp(Default_AuthrorizationAppDetailsView Default_AuthrorizationAppDetailsView)
        {
			AuthrorizationApp AuthrorizationApp = new AuthrorizationApp();
			AuthrorizationApp.RoleAppId = Default_AuthrorizationAppDetailsView.RoleAppId;
			AuthrorizationApp.AppControllerId = Default_AuthrorizationAppDetailsView.AppControllerId;
			AuthrorizationApp.isAllAction = Default_AuthrorizationAppDetailsView.isAllAction;
			AuthrorizationApp.Id = Default_AuthrorizationAppDetailsView.Id;
            return AuthrorizationApp;

        }
        public virtual Default_AuthrorizationAppDetailsView ConverTo_Default_AuthrorizationAppDetailsView(AuthrorizationApp AuthrorizationApp)
        {
            Default_AuthrorizationAppDetailsView Default_AuthrorizationAppDetailsView = new Default_AuthrorizationAppDetailsView();
			Default_AuthrorizationAppDetailsView.RoleAppId = AuthrorizationApp.RoleAppId;
			Default_AuthrorizationAppDetailsView.AppControllerId = AuthrorizationApp.AppControllerId;
			Default_AuthrorizationAppDetailsView.isAllAction = AuthrorizationApp.isAllAction;
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
