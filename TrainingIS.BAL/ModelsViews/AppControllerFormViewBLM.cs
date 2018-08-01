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
            AppController AppController = null;
            if (AppControllerFormView.Id != 0)
            {
                AppController = new AppControllerBLO(this.UnitOfWork).FindBaseEntityByID(AppControllerFormView.Id);
            }
            else
            {
                AppController = new AppController(); 
            }

           
            AppController.Code = AppControllerFormView.Code;
            AppController.Description = AppControllerFormView.Description;
            AppController.Id = AppControllerFormView.Id;

            //
            // Many Relationship
            //
            //AppRoles
            AppRoleBLO appRoleBLO = new AppRoleBLO(this.UnitOfWork);
            AppController.AppRoles.Clear();
            foreach (var item in AppControllerFormView.RolesIds)
            {
                Int64 RoleId = Convert.ToInt64(item);
                AppRole appRole = appRoleBLO.FindBaseEntityByID(RoleId);
                AppController.AppRoles.Add(appRole);
            }
            return AppController;
        }
        public override AppControllerFormView ConverTo_AppControllerFormView(AppController AppController)
        {
            AppControllerFormView AppControllerFormView = new AppControllerFormView();
            AppControllerFormView.Code = AppController.Code;
            AppControllerFormView.Description = AppController.Description;
            AppControllerFormView.Id = AppController.Id;

            AppControllerFormView.RolesIds = AppController
                                                    .AppRoles?
                                                    .Select(role => role.Id.ToString())
                                                    .ToList<string>();
            var Roles = new AppRoleBLO(this.UnitOfWork).FindAll();
            AppControllerFormView.Roles = Roles.Select(role => new SelectListItem() { Value = role.Id.ToString(), Text = role.Code }).ToList();

            return AppControllerFormView;
        }
    }
}
