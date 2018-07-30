using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingIS.Entities;
using TrainingIS.Entities.ModelsViews.GroupModelsViews;

namespace TrainingIS.WebApp.Controllers
{
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
    }
}