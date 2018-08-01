using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TrainingIS.DAL;
using TrainingIS.Entities;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLL.ModelsViews
{
    public partial class AppControllerFormViewBLM  
    {
        
       

        public override AppController ConverTo_AppController(AppControllerFormView AppControllerFormView)
        {
            AppController AppController = new AppController();
            AppController.Code = AppControllerFormView.Code;
            AppController.Description = AppControllerFormView.Description;
            AppController.Id = AppControllerFormView.Id;

            //
            // Many Relationship
            //
            //AppRoles
            AppRoleBLO roleBLO = new AppRoleBLO(this.UnitOfWork);
            AppController.AppRoles = new List<AppRole>();
            foreach (var item in AppControllerFormView.SelectedRoles)
            {
                Int64 RoleId = Convert.ToInt64(item);
                AppRole appRole = roleBLO.FindBaseEntityByID(RoleId);
                AppController.AppRoles.Add(appRole);
            }
            AppController.AppRoles = AppControllerFormView.AppRoles;
            return AppController;
        }
        public override AppControllerFormView ConverTo_AppControllerFormView(AppController AppController)
        {
            AppControllerFormView AppControllerFormView = new AppControllerFormView();
            AppControllerFormView.Code = AppController.Code;
            AppControllerFormView.Description = AppController.Description;
            AppControllerFormView.Id = AppController.Id;

            AppControllerFormView.SelectedRoles = AppController
                                                    .AppRoles?
                                                    .Select(role => role.Id.ToString())
                                                    .ToList<string>();
            var Roles = new AppRoleBLO(this.UnitOfWork).FindAll();
            AppControllerFormView.SelectListRoles = Roles.Select(role => new SelectListItem() { Value = role.Id.ToString(), Text = role.Code }).ToList();

            return AppControllerFormView;
        }
    }
}
