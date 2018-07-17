using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingIS.Entities;

namespace TrainingIS.WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public partial class FormersController
    {


        public override ActionResult Create([Bind(Include = "Id,FirstName,LastName,Sex,CIN,Cellphone,Email,Address,FaceBook,WebSite,RegistrationNumber,CreateDate,UpdateDate")] Former former)
        {
            try
            {
                return base.Create(former);
            }
            catch (TrainingIS.BLL.Exceptions.CreateUserException ex)
            {
                msgHelper.Create(msg);
                Alert(ex.Message, Enums.Enums.NotificationType.error);
                return this.Edit(former);
            }
           
        }

        public override ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Sex,CIN,Cellphone,Email,Address,FaceBook,WebSite,RegistrationNumber,CreateDate,UpdateDate")] Former former)
        {
            try
            {
                return base.Edit(former);
            }
            catch (TrainingIS.BLL.Exceptions.CreateUserException ex)
            {
                msgHelper.Edit(msg);
                Alert(ex.Message, Enums.Enums.NotificationType.error);
                return View(former);
            }
           
        }
    }
}