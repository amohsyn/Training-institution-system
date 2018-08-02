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
	public partial class BaseDefault_AuthrorizationAppFormViewBLM : ViewModelBLM
    {
        
        public BaseDefault_AuthrorizationAppFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual AuthrorizationApp ConverTo_AuthrorizationApp(Default_AuthrorizationAppFormView Default_AuthrorizationAppFormView)
        {
			AuthrorizationApp AuthrorizationApp = new AuthrorizationApp();
			AuthrorizationApp.RoleAppId = Default_AuthrorizationAppFormView.RoleAppId;
			AuthrorizationApp.AppControllerId = Default_AuthrorizationAppFormView.AppControllerId;
			AuthrorizationApp.isAllAction = Default_AuthrorizationAppFormView.isAllAction;
			AuthrorizationApp.Id = Default_AuthrorizationAppFormView.Id;
            return AuthrorizationApp;

        }
        public virtual Default_AuthrorizationAppFormView ConverTo_Default_AuthrorizationAppFormView(AuthrorizationApp AuthrorizationApp)
        {
            Default_AuthrorizationAppFormView Default_AuthrorizationAppFormView = new Default_AuthrorizationAppFormView();
			Default_AuthrorizationAppFormView.RoleAppId = AuthrorizationApp.RoleAppId;
			Default_AuthrorizationAppFormView.AppControllerId = AuthrorizationApp.AppControllerId;
			Default_AuthrorizationAppFormView.isAllAction = AuthrorizationApp.isAllAction;
			Default_AuthrorizationAppFormView.Id = AuthrorizationApp.Id;
            return Default_AuthrorizationAppFormView;            
        }
    }

	public partial class Default_AuthrorizationAppFormViewBLM : BaseDefault_AuthrorizationAppFormViewBLM
	{
		public Default_AuthrorizationAppFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
