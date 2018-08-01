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
	public partial class BaseAppControllerDetailsViewBLM : ViewModelBLM
    {
        
        public BaseAppControllerDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual AppController ConverTo_AppController(AppControllerDetailsView AppControllerDetailsView)
        {
			AppController AppController = new AppController();
			AppController.Code = AppControllerDetailsView.Code;
			AppController.Id = AppControllerDetailsView.Id;
            return AppController;

        }
        public virtual AppControllerDetailsView ConverTo_AppControllerDetailsView(AppController AppController)
        {
            AppControllerDetailsView AppControllerDetailsView = new AppControllerDetailsView();
			AppControllerDetailsView.Code = AppController.Code;
			AppControllerDetailsView.Id = AppController.Id;
            return AppControllerDetailsView;            
        }
    }

	public partial class AppControllerDetailsViewBLM : BaseAppControllerDetailsViewBLM
	{
		public AppControllerDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
