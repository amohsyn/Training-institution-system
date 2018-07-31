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
        // IndexGroupView
        //
        public static implicit operator AppController(AppControllerDetailsView AppControllerDetailsView)
        {
            AppController AppController = new AppController();
			AppController.Code = AppControllerDetailsView.Code;
            return AppController;
        }
        public static implicit operator AppControllerDetailsView(AppController AppController)
        { 
            AppControllerDetailsView AppControllerDetailsView = new AppControllerDetailsView();
			AppControllerDetailsView.Code = AppController.Code;
            return AppControllerDetailsView;
        }
		
		
        // 
        // CreateGroupView
        //
        public static implicit operator AppController(AppControllerFormView AppControllerFormView)
        {
            AppController AppController = new AppController();
			AppController.Code = AppControllerFormView.Code;
			AppController.Description = AppControllerFormView.Description;
			AppController.AppRoles = AppControllerFormView.AppRoles;
            return AppController;
        } 
        public static implicit operator AppControllerFormView(AppController AppController)
        { 

            AppControllerFormView AppControllerFormView = new AppControllerFormView();
			AppControllerFormView.Code = AppController.Code;
			AppControllerFormView.Description = AppController.Description;
			AppControllerFormView.AppRoles = AppController.AppRoles;
            return AppControllerFormView;
        }
		
        
		
 
		    }
}
