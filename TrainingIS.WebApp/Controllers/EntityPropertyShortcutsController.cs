using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using TrainingIS.Entities;
using System.Web.Script.Serialization;
using System.Reflection;
using System.Extentions;
namespace TrainingIS.WebApp.Controllers
{
    public partial class EntityPropertyShortcutsController
    {
        public override ActionResult Create()
        {
            var all_types = this._UnitOfWork.context.GetAllTypesInContextOrder();
            var Entities = all_types.Select(t => new { Id = t.Name, Value = t.getLocalName() }).ToList();
            ViewBag.EntityName = new SelectList(Entities, "Id", "Value");
            ViewBag.PropertyName = new SelectList(new List<object>().AsEnumerable());
            return base.Create();
        }

        public ActionResult GetPropertiesNamesList(string EntityName)
        {
            Type type = Type.GetType("TrainingIS.Entities." + EntityName + ", TrainingIS.Entities");
            var PropertiesNames = type.GetProperties().Select(p => new { Id = p.Name, Value = string.Format("{0} ({1})", p.getLocalName(), p.Name) }).ToList();

           

           

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            string result = javaScriptSerializer.Serialize(PropertiesNames);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}