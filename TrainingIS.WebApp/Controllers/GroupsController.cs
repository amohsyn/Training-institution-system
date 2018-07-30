using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingIS.Entities;
using TrainingIS.Entities.ModelsViews.GroupModelsViews;

namespace TrainingIS.WebApp.Controllers
{
    [Authorize(Roles = "Admin,Supervisor,PedagogicalDirector")]
    public partial class GroupsController
    {
        public override ActionResult Index()
        {
            msgHelper.Index(msg);
            IndexGroupView IndexGroupView = new IndexGroupView();
            Dictionary<string, object> Filter = new Dictionary<string, object>();
            Filter.Add(nameof(Group.TrainingYear), this._UnitOfWork.CurrentTrainingYear.Id);
            foreach (var item in GroupBLO.FindAll(Filter, null)) { IndexGroupView.Data.Add(item); }
            return View(IndexGroupView);
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
        public override ActionResult Import()
        {
            return base.Import();
        }

        [Authorize(Roles = "Supervisor")]
        public override ActionResult Create([Bind(Include = "TrainingType,TrainingTypeId,TrainingYear,TrainingYearId,Specialty,SpecialtyId,YearStudy,YearStudyId,Code,Description,Id")] CreateGroupView CreateGroupView)
        {
            return base.Create(CreateGroupView);
        }

        [Authorize(Roles = "Supervisor")]
        public override ActionResult Edit([Bind(Include = "TrainingType,TrainingTypeId,TrainingYear,TrainingYearId,Specialty,SpecialtyId,YearStudy,YearStudyId,Code,Description,Id")] EditGroupView EditGroupView)
        {
            return base.Edit(EditGroupView);
        }
    }
}