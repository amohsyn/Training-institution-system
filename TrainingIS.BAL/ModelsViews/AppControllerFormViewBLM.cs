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
    public partial class Default_ControllerAppFormViewBLM
    {

        public override ControllerApp ConverTo_ControllerApp(Default_ControllerAppFormView Default_ControllerAppFormView)
        {
            ControllerApp ControllerApp = null;
            if (Default_ControllerAppFormView.Id != 0)
            {
                ControllerApp = new ControllerAppBLO(this.UnitOfWork).FindBaseEntityByID(Default_ControllerAppFormView.Id);
            }
            else
            {
                ControllerApp = new ControllerApp();
            }

            ControllerApp.Code = Default_ControllerAppFormView.Code;
            ControllerApp.Name = Default_ControllerAppFormView.Name;
            ControllerApp.Description = Default_ControllerAppFormView.Description;
            ControllerApp.Id = Default_ControllerAppFormView.Id;

            

            //
            // Many Relationship
            //
            //AppRoles
            //AppRoleBLO appRoleBLO = new AppRoleBLO(this.UnitOfWork);
            //AppController.AppRoles.Clear();
            //foreach (var item in AppControllerFormView.RolesIds)
            //{
            //    Int64 RoleId = Convert.ToInt64(item);
            //    AppRole appRole = appRoleBLO.FindBaseEntityByID(RoleId);
            //    AppController.AppRoles.Add(appRole);
            //}
            return ControllerApp;

          
        }


       
        //public override AppControllerFormView ConverTo_AppControllerFormView(AppController AppController)
        //{
        //    AppControllerFormView AppControllerFormView = new AppControllerFormView();
        //    AppControllerFormView.Code = AppController.Code;
        //    AppControllerFormView.Description = AppController.Description;
        //    AppControllerFormView.Id = AppController.Id;

        //    AppControllerFormView.RolesIds = AppController
        //                                            .AppRoles?
        //                                            .Select(role => role.Id.ToString())
        //                                            .ToList<string>();
        //    var Roles = new AppRoleBLO(this.UnitOfWork).FindAll();
        //    AppControllerFormView.Roles = Roles.Select(role => new SelectListItem() { Value = role.Id.ToString(), Text = role.Code }).ToList();

        //    return AppControllerFormView;
        //}
    }
}
