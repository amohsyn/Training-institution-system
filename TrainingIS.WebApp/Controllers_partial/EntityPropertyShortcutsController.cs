using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using TrainingIS.Entities;
using System.Web.Script.Serialization;

namespace TrainingIS.WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public partial class EntityPropertyShortcutsController
    {
        public override ActionResult Create()
        {
            var all_types = this._UnitOfWork.context.GetAllTypesInContextOrder();
            ViewBag.EntityName = new SelectList(all_types.AsEnumerable(), "Name", "Name");
            ViewBag.PropertyName = new SelectList(new List<object>().AsEnumerable());
            return base.Create();
        }

        public ActionResult GetPropertiesNamesList(string EntityName)
        {
            Type type = Type.GetType("TrainingIS.Entities." + EntityName + ", TrainingIS.Entities");
            ViewBag.PropertyName = new SelectList(type.GetProperties().AsEnumerable(), "Name", "Name");


            List<string> PropertiesNames = type.GetProperties().Select(p => p.Name).ToList<string>();

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            string result = javaScriptSerializer.Serialize(PropertiesNames);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}