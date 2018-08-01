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
	public partial class BaseDefault_AppControllerActionFormViewBLM : ViewModelBLM
    {
        
        public BaseDefault_AppControllerActionFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual AppControllerAction ConverTo_AppControllerAction(Default_AppControllerActionFormView Default_AppControllerActionFormView)
        {
			AppControllerAction AppControllerAction = new AppControllerAction();
			AppControllerAction.Code = Default_AppControllerActionFormView.Code;
			AppControllerAction.Description = Default_AppControllerActionFormView.Description;
			AppControllerAction.AppControllerId = Default_AppControllerActionFormView.AppControllerId;
			AppControllerAction.Id = Default_AppControllerActionFormView.Id;
            return AppControllerAction;

        }
        public virtual Default_AppControllerActionFormView ConverTo_Default_AppControllerActionFormView(AppControllerAction AppControllerAction)
        {
            Default_AppControllerActionFormView Default_AppControllerActionFormView = new Default_AppControllerActionFormView();
			Default_AppControllerActionFormView.Code = AppControllerAction.Code;
			Default_AppControllerActionFormView.Description = AppControllerAction.Description;
			Default_AppControllerActionFormView.AppControllerId = AppControllerAction.AppControllerId;
			Default_AppControllerActionFormView.Id = AppControllerAction.Id;
            return Default_AppControllerActionFormView;            
        }
    }

	public partial class Default_AppControllerActionFormViewBLM : BaseDefault_AppControllerActionFormViewBLM
	{
		public Default_AppControllerActionFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
