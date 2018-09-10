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
using GApp.Entities;

namespace TrainingIS.WebApp.Controllers
{
    public class StatisticAbsenceController : BaseController<TrainingISModel>
    {

        public ActionResult Index()
        {
            StatisticAbsenceForm statisticAbsenceForm = new StatisticAbsenceForm();
            // 1 mois
            statisticAbsenceForm.StartDate = DateTime.Now.AddDays(-30);
            statisticAbsenceForm.EndDate = DateTime.Now;

            this.Index_Fill_ViewBag();

            return View(statisticAbsenceForm);
        }

        private void Index_Fill_ViewBag()
        {
            // Groups
            List<Group> AllGroups = new GroupBLO(this._UnitOfWork, this.GAppContext).FindAll();
            AllGroups.Add(new Group() { Id = 0, Code = "Tous les groupes" });
            ViewBag.GroupId = new SelectList(AllGroups, "Id", nameof(TrainingIS_BaseEntity.ToStringValue), 0);

            Dictionary<string, string> All_StatisitcBy = new Dictionary<string, string>();
            List<BaseEntity> Data_StatisticSelectors = new List<BaseEntity>();

            Data_StatisticSelectors.Add(new StatisticSelector() { Reference = nameof(Trainee), Name = "Stagiaire" });
            Data_StatisticSelectors.Add(new StatisticSelector() { Reference = nameof(Group), Name = "Groupe" });
            Data_StatisticSelectors.Add(new StatisticSelector() { Reference = nameof(ModuleTraining), Name = "Module" });
            Data_StatisticSelectors.Add(new StatisticSelector() { Reference = nameof(Former), Name = "Formateur" });
            Data_StatisticSelectors.Add(new StatisticSelector() { Reference = nameof(SeanceNumber), Name = "Numéro de la séance" });
            Data_StatisticSelectors.Add(new StatisticSelector() { Reference = nameof(SeanceDay), Name = "Jour de la séance" });
            ViewBag.Data_StatisticSelectors = Data_StatisticSelectors;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(StatisticAbsenceForm statisticAbsenceForm)
        {
            if (ModelState.IsValid)
            {
                StatisticAbsenceBLO statisticAbsenceBLO = new StatisticAbsenceBLO(this.GAppContext);
                Statistic statistic = statisticAbsenceBLO.Calculate(statisticAbsenceForm);
                ViewBag.Statistic = statistic;
            }

            this.Index_Fill_ViewBag();
            return View(statisticAbsenceForm);
        }

    }
}