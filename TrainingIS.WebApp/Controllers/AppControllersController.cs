﻿using System;
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
using System.Net;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers
{
    public partial class AppControllersController
    {
        
        public override ActionResult Create()
        {
            AppControllerFormView appControllerCreateView = new AppControllerFormView();

            appControllerCreateView.Roles = new AppRoleBLO(this._UnitOfWork).FindAll().Select(role => new SelectListItem() { Value = role.Id.ToString() , Text = role.Code }).ToList<SelectListItem>();
            appControllerCreateView.RolesIds = new List<string>();
            msgHelper.Create(msg);
            return View(appControllerCreateView);
        }

        public override ActionResult Create([Bind(Include = "Code,Description,SelectedRoles")] AppControllerFormView AppControllerCreateView)
        {
            AppController AppController = new AppController();
            AppController = new AppControllerFormViewBLM(this._UnitOfWork)
                                .ConverTo_AppController(AppControllerCreateView);

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
        public override ActionResult Edit(long? id)
        {
            bool dataBaseException = false;
            msgHelper.Edit(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AppController AppController = AppControllerBLO.FindBaseEntityByID((long)id);
            if (AppController == null)
            {
                return HttpNotFound();
            }
            AppControllerFormView AppControllerFormView = new AppControllerFormViewBLM(this._UnitOfWork)
                                                                .ConverTo_AppControllerFormView(AppController) ;
            return View(AppControllerFormView);
        }
        public override ActionResult Edit([Bind(Include = "Code,Description,SelectListRoles,SelectedRoles,AppRoles,Id")] AppControllerFormView AppControllerFormView)
        {
            AppController AppController = new AppControllerFormViewBLM(this._UnitOfWork)
                .ConverTo_AppController( AppControllerFormView);
            bool dataBaseException = false;
            if (ModelState.IsValid)
            {
                try
                {
                    AppControllerBLO.Save(AppController);
                    Alert(string.Format(msgManager.The_entity_has_been_changed, msg_AppController.SingularName, AppController), NotificationType.success);
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
            msgHelper.Edit(msg);

            return View(AppControllerFormView);
        }
    }
}