using GApp.WebApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingIS.BLL;
using TrainingIS.DAL;
using TrainingIS.Entities.Base;
using TrainingIS.Entities;
using TrainingIS.Models.StatisticAbsence;

namespace TrainingIS.WebApp.Controllers
{
    public class StatisticAbsenceController : BaseController<TrainingISModel>
    {

        public ActionResult Index()
        {
            StatisticAbsenceForm statisticAbsenceForm = new StatisticAbsenceForm();
            statisticAbsenceForm.StartDate = DateTime.Now;
            statisticAbsenceForm.EndDate = DateTime.Now;
            List<Group> AllGroups = new GroupBLO(this._UnitOfWork, this.GAppContext).FindAll();
            AllGroups.Add(new Group() { Id = 0, Code = "Tous les groupes" });
            ViewBag.GroupId = new SelectList(AllGroups, "Id", nameof(TrainingIS_BaseEntity.ToStringValue));
            return View(statisticAbsenceForm);
        }

        [HttpPost , ActionName("Index")]
        [ValidateAntiForgeryToken]
        public ActionResult ShowStatistics(StatisticAbsenceForm statisticAbsenceForm)
        {
            if (ModelState.IsValid)
            {

               

                StatisticAbsenceBLO statisticAbsenceBLO = new StatisticAbsenceBLO(this.GAppContext);
                Statistic statistic = statisticAbsenceBLO.Calculate(statisticAbsenceForm);

            }

            List<Group> AllGroups = new GroupBLO(this._UnitOfWork, this.GAppContext).FindAll();
            AllGroups.Add(new Group() { Id = 0, Code = "Tous les groupes" });
            ViewBag.GroupId = new SelectList(AllGroups, "Id", nameof(TrainingIS_BaseEntity.ToStringValue), statisticAbsenceForm.GroupId);
            return View(statisticAbsenceForm);
        }
    }
}