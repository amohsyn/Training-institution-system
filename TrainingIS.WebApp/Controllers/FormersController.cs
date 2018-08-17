using GApp.BLL.Enums;
using GApp.DAL.Exceptions;
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

namespace TrainingIS.WebApp.Controllers
{
    public partial class FormersController
    {
        private void Add_GAppContet_Params()
        {
            ApplicationUserManager ApplicationUserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            this.GAppContext.Session.Add("ApplicationUserManager", ApplicationUserManager);
        }
        public override ActionResult Create([Bind(Include = "RegistrationNumber,FirstName,LastName,FirstNameArabe,LastNameArabe,NationalityId,Sex,Birthdate,BirthPlace,CIN,Cellphone,Email,Address,CreateUserAccount,Login,Password")] FormerFormView FormerFormView)
        {
            try
            {
                this.Add_GAppContet_Params();
                return base.Create(FormerFormView);
            }
            catch (TrainingIS.BLL.Exceptions.CreateUserException ex)
            {
                msgHelper.Create(msg);
                Alert(ex.Message, NotificationType.error);
                return this.Edit(FormerFormView.Id);
            }
        }

        public override ActionResult Edit([Bind(Include = "RegistrationNumber,FirstName,LastName,FirstNameArabe,LastNameArabe,NationalityId,Sex,Birthdate,BirthPlace,CIN,Cellphone,Email,Address,CreateUserAccount,Id,Login,Password")] FormerFormView FormerFormView)
        {
           
            try
            {

                ApplicationUserManager ApplicationUserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                this.GAppContext.Session.Add("ApplicationUserManager", ApplicationUserManager);
                return base.Edit(FormerFormView);
            }
            catch (TrainingIS.BLL.Exceptions.CreateUserException ex)
            {
                msgHelper.Create(msg);
                Alert(ex.Message, NotificationType.error);
                return this.Edit(FormerFormView.Id);
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

        public override ActionResult Import()
        {
            this.Add_GAppContet_Params();
            return base.Import();
        }



    }
}