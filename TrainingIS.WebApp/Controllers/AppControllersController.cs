using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using TrainingIS.WebApp.Manager.Views.msgs;
using TrainingIS.Entities.Resources.AppControllerResources;
using static TrainingIS.WebApp.Enums.Enums;
using GApp.DAL.Exceptions;
using TrainingIS.BLL;

namespace TrainingIS.WebApp.Controllers
{
    public partial class AppControllersController
    {
        public AppController ConvertoTo (AppControllerFormView AppControllerCreateView)
        {
            AppController AppController = new AppController();
            AppController.Code = AppControllerCreateView.Code;
            AppController.Description = AppControllerCreateView.Description;

            // Many
            //
            //AppRoles
            AppRoleBLO roleBLO = new AppRoleBLO(this._UnitOfWork);
            AppController.AppRoles = new List<AppRole>();
            foreach (var item in AppControllerCreateView.SelectedRoles)
            {
                Int64 RoleId = Convert.ToInt64(item);
                AppRole appRole = roleBLO.FindBaseEntityByID(RoleId);
                AppController.AppRoles.Add(appRole);
            }
            AppController.AppRoles = AppControllerCreateView.AppRoles;
            return AppController;
        }

        public override ActionResult Create()
        {
            AppControllerFormView appControllerCreateView = new AppControllerFormView();
            appControllerCreateView.SelectListRoles = new AppRoleBLO(this._UnitOfWork).FindAll().Select(role => new SelectListItem() { Value = role.Id.ToString() , Text = role.Code }).ToList<SelectListItem>();
            appControllerCreateView.SelectedRoles = new List<string>();
            msgHelper.Create(msg);
            return View(appControllerCreateView);
        }

        public override ActionResult Create([Bind(Include = "Code,Description,SelectedRoles")] AppControllerFormView AppControllerCreateView)
        {
            AppController AppController = new AppController();
            AppController = this.ConvertoTo(AppControllerCreateView);
            bool dataBaseException = false;
            if (ModelState.IsValid)
            {
                try
                {
                    AppControllerBLO.Save(AppController);
                    Alert(string.Format(msgManager.The_Entity_was_well_created, msg_AppController.SingularName, AppController), NotificationType.success);
                    return RedirectToAction("Index");
                }
                catch (GAppDataBaseException ex)
                {
                    dataBaseException = true;
                    Alert(ex.Message, NotificationType.error);
                }
            }
            if (!dataBaseException)
            {
                Alert(msgManager.The_information_you_have_entered_is_not_valid, NotificationType.warning);
            }
            msgHelper.Create(msg);
            return View(AppControllerCreateView);
        }
    }
}