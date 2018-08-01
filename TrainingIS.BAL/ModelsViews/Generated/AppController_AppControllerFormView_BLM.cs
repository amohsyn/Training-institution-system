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
	public partial class BaseAppControllerFormViewBLM : ViewModelBLM
    {
        
        public BaseAppControllerFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual AppController ConverTo_AppController(AppControllerFormView AppControllerFormView)
        {
			AppController AppController = new AppController();
			AppController.Code = AppControllerFormView.Code;
			AppController.Description = AppControllerFormView.Description;
			AppController.Id = AppControllerFormView.Id;
            return AppController;

        }
        public virtual AppControllerFormView ConverTo_AppControllerFormView(AppController AppController)
        {
            AppControllerFormView AppControllerFormView = new AppControllerFormView();
			AppControllerFormView.Code = AppController.Code;
			AppControllerFormView.Description = AppController.Description;
			AppControllerFormView.Id = AppController.Id;
            return AppControllerFormView;            
        }
    }

	public partial class AppControllerFormViewBLM : BaseAppControllerFormViewBLM
	{
		public AppControllerFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
