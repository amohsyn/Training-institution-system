using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.ModelsViews;
		 
namespace TrainingIS.Entities
{
    public partial class AppController
    {
 

        // 
        // AppControllerDetailsView
        //
        public static implicit operator AppController(AppControllerDetailsView AppControllerDetailsView)
        {
            AppController AppController = new AppController();
			AppController.Code = AppControllerDetailsView.Code;
			AppController.Id = AppControllerDetailsView.Id;
            return AppController;
        }
        public static implicit operator AppControllerDetailsView(AppController AppController)
        { 
            AppControllerDetailsView AppControllerDetailsView = new AppControllerDetailsView();
			AppControllerDetailsView.Code = AppController.Code;
			AppControllerDetailsView.Id = AppController.Id;
            return AppControllerDetailsView;
        }

      



        // 
        // AppControllerFormView
        //
        public static implicit operator AppController(AppControllerFormView AppControllerFormView)
        {
            AppController AppController = new AppController();
			AppController.Code = AppControllerFormView.Code;
			AppController.Description = AppControllerFormView.Description;
			AppController.AppRoles = AppControllerFormView.AppRoles;
			AppController.Id = AppControllerFormView.Id;
            return AppController;
        }
        public static implicit operator AppControllerFormView(AppController AppController)
        { 
            AppControllerFormView AppControllerFormView = new AppControllerFormView();
			AppControllerFormView.Code = AppController.Code;
			AppControllerFormView.Description = AppController.Description;
			AppControllerFormView.AppRoles = AppController.AppRoles;
			AppControllerFormView.Id = AppController.Id;
            return AppControllerFormView;
        }
	

	}
}
