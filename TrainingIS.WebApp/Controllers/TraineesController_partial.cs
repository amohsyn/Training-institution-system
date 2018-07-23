using ClosedXML.Excel;
using GApp.DAL.ReadExcel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingIS.BLL;
using TrainingIS.Entities;
using static TrainingIS.WebApp.Enums.Enums;

namespace TrainingIS.WebApp.Controllers
{
    [Authorize(Roles = "Admin,Supervisor,PedagogicalDirector")]
    public partial class TraineesController
    {
        [Authorize(Roles = "Supervisor")]
        public override ActionResult Create([Bind(Include = "Id,FirstName,LastName,FirstNameArabe,LastNameArabe,BirthPlace,Sex,CIN,Cellphone,TutorCellPhone,Email,Address,FaceBook,WebSite,CNE,CEF,isActif,DateRegistration,NationalityId,SchoollevelId,GroupId,CreateDate,UpdateDate,Birthdate")] Trainee trainee)
        {
            return base.Create(trainee);
        }

        [Authorize(Roles = "Supervisor")]
        public override ActionResult Create()
        {
            return base.Create();
        }

        [Authorize(Roles = "Supervisor")]
        public override ActionResult Delete(long? id)
        {
            return base.Delete(id);
        }

        [Authorize(Roles = "Supervisor")]
        public override ActionResult DeleteConfirmed(long id)
        {
            return base.DeleteConfirmed(id);
        }

        [Authorize(Roles = "Supervisor")]
        public override ActionResult Edit(long? id)
        {
            return base.Edit(id);
        }

        [Authorize(Roles = "Supervisor")]
        public override ActionResult Edit([Bind(Include = "Id,FirstName,LastName,FirstNameArabe,LastNameArabe,BirthPlace,Sex,CIN,Cellphone,TutorCellPhone,Email,Address,FaceBook,WebSite,CNE,CEF,isActif,DateRegistration,NationalityId,SchoollevelId,GroupId,CreateDate,UpdateDate,Birthdate")] Trainee trainee)
        {
            return base.Edit(trainee);
        }

        [Authorize(Roles = "Supervisor")]
        public override ActionResult Import()
        {
           return base.Import();
        }
 
    }
}