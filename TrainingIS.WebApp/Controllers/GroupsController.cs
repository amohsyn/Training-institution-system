using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingIS.BLL.ModelsViews;
using TrainingIS.Entities;
using TrainingIS.Entities.ModelsViews.GroupModelsViews;

namespace TrainingIS.WebApp.Controllers
{
    public partial class GroupsController
    {
 
        public override ActionResult Index()
        {
            msgHelper.Index(msg);
            List<IndexGroupView> listIndexGroupView = new List<IndexGroupView>();
            Dictionary<string, object> Filter = new Dictionary<string, object>();
            foreach (var item in GroupBLO.FindAll(Filter, null))
            {
                IndexGroupView IndexGroupView = new IndexGroupViewBLM(this._UnitOfWork).ConverTo_IndexGroupView(item);
                listIndexGroupView.Add(IndexGroupView);

            }
            return View(listIndexGroupView);

        }
    }
}