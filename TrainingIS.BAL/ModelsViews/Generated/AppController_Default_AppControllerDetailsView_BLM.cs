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
	public partial class BaseDefault_AppControllerDetailsViewBLM : ViewModelBLM
    {
        
        public BaseDefault_AppControllerDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual AppController ConverTo_AppController(Default_AppControllerDetailsView Default_AppControllerDetailsView)
        {
			AppController AppController = new AppController();
			AppController.Code = Default_AppControllerDetailsView.Code;
			AppController.Description = Default_AppControllerDetailsView.Description;
			AppController.Id = Default_AppControllerDetailsView.Id;
            return AppController;

        }
        public virtual Default_AppControllerDetailsView ConverTo_Default_AppControllerDetailsView(AppController AppController)
        {
            Default_AppControllerDetailsView Default_AppControllerDetailsView = new Default_AppControllerDetailsView();
			Default_AppControllerDetailsView.Code = AppController.Code;
			Default_AppControllerDetailsView.Description = AppController.Description;
			Default_AppControllerDetailsView.Id = AppController.Id;
            return Default_AppControllerDetailsView;            
        }
    }

	public partial class Default_AppControllerDetailsViewBLM : BaseDefault_AppControllerDetailsViewBLM
	{
		public Default_AppControllerDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
