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
         

        //public override AppControllerDetailsView ConverTo_AppControllerDetailsView(AppController AppController)
        //{
        //    AppControllerDetailsView AppControllerDetailsView = base.ConverTo_AppControllerDetailsView(AppController);
        //    if(AppController.AppRoles != null)
        //    AppControllerDetailsView.Roles = string.Join(",", AppController.AppRoles?.Select(role => role.Code).ToList());
        //    return AppControllerDetailsView;
        //}

    }
}
