﻿using GApp.DAL.Exceptions;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingIS.BLL.Services.Identity;
using TrainingIS.Entities;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities.ModelsViews.FormerModelsViews;
using TrainingIS.Entities.Resources.FormerResources;
using TrainingIS.WebApp.Manager.Views.msgs;
using static TrainingIS.WebApp.Enums.Enums;

namespace TrainingIS.WebApp.Controllers
{
    public partial class FormersController
    {
        public override ActionResult Create([Bind(Include = "RegistrationNumber,FirstName,LastName,FirstNameArabe,LastNameArabe,NationalityId,Sex,Birthdate,BirthPlace,CIN,Cellphone,Email,Address,CreateUserAccount,Login,Password")] FormerFormView FormerFormView)
        {
            try
            {

                var ResaultView =  base.Create(FormerFormView);

                if (ResaultView is RedirectToRouteResult &&  FormerFormView.CreateUserAccount)
                {
                    ApplicationUserManager ApplicationUserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    this.FormerBLO.CreateAccount_IfNotExit(FormerFormView.Login, FormerFormView.Password, ApplicationUserManager);

                }
                return ResaultView;
            }
            catch (TrainingIS.BLL.Exceptions.CreateUserException ex)
            {
                msgHelper.Create(msg);
                Alert(ex.Message, Enums.Enums.NotificationType.error);
                return this.Edit(FormerFormView.Id);
            }
        }

        public override ActionResult Edit([Bind(Include = "RegistrationNumber,FirstName,LastName,FirstNameArabe,LastNameArabe,NationalityId,Sex,Birthdate,BirthPlace,CIN,Cellphone,Email,Address,CreateUserAccount,Id,Login,Password")] FormerFormView FormerFormView)
        {
            var ResaultView = base.Edit(FormerFormView);
            try
            {
               
                if (FormerFormView.CreateUserAccount)
                {
                    ApplicationUserManager ApplicationUserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    this.FormerBLO.CreateAccount_IfNotExit(FormerFormView.Login, FormerFormView.Password, ApplicationUserManager);

                }
                return ResaultView;
            }
            catch (TrainingIS.BLL.Exceptions.CreateUserException ex)
            {
                msgHelper.Create(msg);
                Alert(ex.Message, Enums.Enums.NotificationType.error);
                return ResaultView;
            }
           
        }

        public override ActionResult DeleteConfirmed(long id)
        {
            Former Former = FormerBLO.FindBaseEntityByID((long)id);
            if (Former == null)
            {
                string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Former.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }

            try
            {
                //
                // the Overrrided code
                // 
                ApplicationUserManager ApplicationUserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                FormerBLO.Delete(Former, ApplicationUserManager);
            }
            catch (GAppDbUpdate_ForeignKeyViolation_Exception ex)
            {
                string msg = string.Format(msgManager.You_can_not_delete_this_entity_because_it_is_already_related_to_another_object, Former.ToString());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            catch (GAppDbException ex)
            {
                Alert(ex.Message, NotificationType.error);
                return RedirectToAction("Index");
            }

            Alert(string.Format(msgManager.The_entity_has_been_removed, msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_Former.SingularName.ToLower(), Former), NotificationType.success);
            return RedirectToAction("Index");
        }




    }
}