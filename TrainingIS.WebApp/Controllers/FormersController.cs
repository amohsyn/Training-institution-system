using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingIS.Entities;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities.ModelsViews.FormerModelsViews;

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
                    this.FormerBLO.CreateAccount_IfNotExit(FormerFormView.Login, FormerFormView.Password);

                }
                return ResaultView;
            }
            catch (TrainingIS.BLL.Exceptions.CreateUserException ex)
            {
                msgHelper.Create(msg);
                Alert(ex.Message, Enums.Enums.NotificationType.error);
                return this.Edit(FormerFormView);
            }
        }

        public override ActionResult Edit([Bind(Include = "RegistrationNumber,FirstName,LastName,FirstNameArabe,LastNameArabe,NationalityId,Sex,Birthdate,BirthPlace,CIN,Cellphone,Email,Address,CreateUserAccount,Id,Login,Password")] FormerFormView FormerFormView)
        {
            try
            {
                var ResaultView = base.Edit(FormerFormView);
                if (FormerFormView.CreateUserAccount)
                {
                    this.FormerBLO.CreateAccount_IfNotExit(FormerFormView.Login, FormerFormView.Password);

                }
                return ResaultView;
            }
            catch (TrainingIS.BLL.Exceptions.CreateUserException ex)
            {
                msgHelper.Create(msg);
                Alert(ex.Message, Enums.Enums.NotificationType.error);
                return this.Edit(FormerFormView);
            }

        }



    }
}