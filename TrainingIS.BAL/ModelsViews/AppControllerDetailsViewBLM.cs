using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;
using TrainingIS.Entities;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLL.ModelsViews
{
    public partial class AppControllerDetailsViewBLM 
    {
    
        public override AppController ConverTo_AppController(AppControllerDetailsView AppControllerDetailsView)
        {
            AppController AppController = new AppController();
            AppController.Code = AppControllerDetailsView.Code;
            AppController.Id = AppControllerDetailsView.Id;
            return AppController;
        }

        public override AppControllerDetailsView ConverTo_AppControllerDetailsView(AppController AppController)
        {
            AppControllerDetailsView AppControllerDetailsView = new AppControllerDetailsView();
            AppControllerDetailsView.Code = AppController.Code;
            AppControllerDetailsView.Id = AppController.Id;
            return AppControllerDetailsView;
        }
    }
}
