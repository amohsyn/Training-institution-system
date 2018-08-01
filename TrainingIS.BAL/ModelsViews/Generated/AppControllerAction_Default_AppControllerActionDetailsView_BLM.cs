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
	public partial class BaseDefault_AppControllerActionDetailsViewBLM : ViewModelBLM
    {
        
        public BaseDefault_AppControllerActionDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual AppControllerAction ConverTo_AppControllerAction(Default_AppControllerActionDetailsView Default_AppControllerActionDetailsView)
        {
			AppControllerAction AppControllerAction = new AppControllerAction();
			AppControllerAction.Code = Default_AppControllerActionDetailsView.Code;
			AppControllerAction.Description = Default_AppControllerActionDetailsView.Description;
			AppControllerAction.AppControllerId = Default_AppControllerActionDetailsView.AppControllerId;
			AppControllerAction.Id = Default_AppControllerActionDetailsView.Id;
            return AppControllerAction;

        }
        public virtual Default_AppControllerActionDetailsView ConverTo_Default_AppControllerActionDetailsView(AppControllerAction AppControllerAction)
        {
            Default_AppControllerActionDetailsView Default_AppControllerActionDetailsView = new Default_AppControllerActionDetailsView();
			Default_AppControllerActionDetailsView.Code = AppControllerAction.Code;
			Default_AppControllerActionDetailsView.Description = AppControllerAction.Description;
			Default_AppControllerActionDetailsView.AppControllerId = AppControllerAction.AppControllerId;
			Default_AppControllerActionDetailsView.Id = AppControllerAction.Id;
            return Default_AppControllerActionDetailsView;            
        }
    }

	public partial class Default_AppControllerActionDetailsViewBLM : BaseDefault_AppControllerActionDetailsViewBLM
	{
		public Default_AppControllerActionDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
