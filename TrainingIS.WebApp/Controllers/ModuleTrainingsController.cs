using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingIS.Entities;

namespace TrainingIS.WebApp.Controllers
{
    public partial class ModuleTrainingsController
    {

        public ActionResult Get_ModuleTrainings_By_SpecialtyId(long? Id)
        {
            // Objects
            List<ModuleTraining> Objects = null;
            if (Id != null)
            {
                Objects = this.ModuleTrainingBLO.Find_By_SpecialtyId((long)Id);
            }
            else
            {
                Objects = this.ModuleTrainingBLO.FindAll();
            }

            // selectListItems
            IList<SelectListItem> selectListItems = Objects
                    .Select(m => new SelectListItem() { Value = m.Id.ToString(), Text = m.ToString() })
                    .ToList();
            return Json(new { list = selectListItems }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Get_ModuleTraining_By_Id(long? Id)
        {
            // Object
            ModuleTraining obj = null;
            if (Id != null)
            {
                obj = this.ModuleTrainingBLO.FindBaseEntityByID((long)Id);
            }

            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}