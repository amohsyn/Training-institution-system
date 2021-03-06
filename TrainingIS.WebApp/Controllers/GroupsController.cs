﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GApp.Models.Pages;
using TrainingIS.BLL;
using TrainingIS.BLL.ModelsViews;
using TrainingIS.Entities;
using TrainingIS.Entities.ModelsViews.GroupModelsViews;

namespace TrainingIS.WebApp.Controllers
{
    public partial class GroupsController
    {
        public ActionResult Get_Groups_By_SpecialtyId(long? Id)
        {
            // Objects
            List<Group> Objects = null;
            if (Id != null)
            {
                Objects = this.GroupBLO.Find_By_SpecialtyId((long)Id);
            }
            else
            {
                Objects = this.GroupBLO.FindAll();
            }

            // selectListItems
            IList<SelectListItem> selectListItems = Objects
                    .Select(m => new SelectListItem() { Value = m.Id.ToString(), Text = m.ToString() })
                    .ToList();
            return Json(new { list = selectListItems }, JsonRequestBehavior.AllowGet);
        }

        

    }
}