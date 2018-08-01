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
	public partial class BaseDefault_AppControllerFormViewBLM : ViewModelBLM
    {
        
        public BaseDefault_AppControllerFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual AppController ConverTo_AppController(Default_AppControllerFormView Default_AppControllerFormView)
        {
			AppController AppController = new AppController();
			AppController.Code = Default_AppControllerFormView.Code;
			AppController.Description = Default_AppControllerFormView.Description;
			AppController.Id = Default_AppControllerFormView.Id;
            return AppController;

        }
        public virtual Default_AppControllerFormView ConverTo_Default_AppControllerFormView(AppController AppController)
        {
            Default_AppControllerFormView Default_AppControllerFormView = new Default_AppControllerFormView();
			Default_AppControllerFormView.Code = AppController.Code;
			Default_AppControllerFormView.Description = AppController.Description;
			Default_AppControllerFormView.Id = AppController.Id;
            return Default_AppControllerFormView;            
        }
    }

	public partial class Default_AppControllerFormViewBLM : BaseDefault_AppControllerFormViewBLM
	{
		public Default_AppControllerFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
