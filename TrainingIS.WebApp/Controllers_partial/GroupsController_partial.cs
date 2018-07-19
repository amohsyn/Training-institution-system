using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingIS.Entities;

namespace TrainingIS.WebApp.Controllers
{
    [Authorize(Roles = "Admin,Supervisor,PedagogicalDirector")]
    public partial class GroupsController
    {
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
        public override ActionResult Import()
        {
            return base.Import();
        }

        [Authorize(Roles = "Supervisor")]
        public override ActionResult Create([Bind(Include = "Id,TrainingTypeId,TrainingYearId,SpecialtyId,YearStudyId,Code,Description,CreateDate,UpdateDate")] Group group)
        {
            return base.Create(group);
        }

        [Authorize(Roles = "Supervisor")]
        public override ActionResult Edit([Bind(Include = "Id,TrainingTypeId,TrainingYearId,SpecialtyId,YearStudyId,Code,Description,CreateDate,UpdateDate")] Group group)
        {
            return base.Edit(group);
        }
    }
}