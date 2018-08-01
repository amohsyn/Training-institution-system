using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingIS.Entities;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.WebApp.Controllers
{
    public partial class FormersController
    {

        public override ActionResult Create([Bind(Include = "FirstName,LastName,Sex,CIN,Cellphone,Email,Address,FaceBook,WebSite,RegistrationNumber")] Default_FormerFormView Default_FormerFormView)
        {
           

            try
            {
                return base.Create(Default_FormerFormView);
            }
            catch (TrainingIS.BLL.Exceptions.CreateUserException ex)
            {
                msgHelper.Create(msg);
                Alert(ex.Message, Enums.Enums.NotificationType.error);
                return this.Edit(Default_FormerFormView);
            }
        }

        public override ActionResult Edit([Bind(Include = "FirstName,LastName,Sex,CIN,Cellphone,Email,Address,FaceBook,WebSite,RegistrationNumber,Id")] Default_FormerFormView Default_FormerFormView)
        {
           

            try
            {
                return base.Edit(Default_FormerFormView);
            }
            catch (TrainingIS.BLL.Exceptions.CreateUserException ex)
            {
                msgHelper.Edit(msg);
                Alert(ex.Message, Enums.Enums.NotificationType.error);
                return View(Default_FormerFormView);
            }
        }
 
    }
}