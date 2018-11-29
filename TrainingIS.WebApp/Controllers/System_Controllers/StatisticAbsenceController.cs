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
using ClosedXML.Excel;
using System.Data;
using System.IO;
using GApp.Exceptions;
using GApp.BLL.Enums;

namespace TrainingIS.WebApp.Controllers
{
    public class StatisticAbsenceController : BaseController<TrainingISModel>
    {

        public ActionResult Index()
        {
            StatisticAbsenceForm statisticAbsenceForm = new StatisticAbsenceForm();

            TrainingYear trainingYear = new TrainingYearBLO(this._UnitOfWork, this.GAppContext).getCurrentTrainingYear();

            statisticAbsenceForm.StartDate = trainingYear.StartDate;
            statisticAbsenceForm.EndDate = trainingYear.EndtDate;

            this.Index_Fill_ViewBag();

            return View(statisticAbsenceForm);
        }

        private void Index_Fill_ViewBag()
        {
            // Groups
            UserBLO userBLO = new UserBLO(this._UnitOfWork, this.GAppContext);
            List<Group> AllGroups = null;
            if (userBLO.Is_Current_User_Has_Role(RoleBLO.Former_ROLE))
            {
                TrainingBLO TrainingBLO = new TrainingBLO(this._UnitOfWork, this.GAppContext);

                FormerBLO formerBLO = new FormerBLO(this._UnitOfWork, this.GAppContext);
                Former Current_Former = formerBLO.Get_Current_Former();
                AllGroups = TrainingBLO.Get_Groups_Of_Former(Current_Former);
            }
            else
            {
                AllGroups = new GroupBLO(this._UnitOfWork, this.GAppContext).FindAll();
                AllGroups.Add(new Group() { Id = 0, Code = "Tous les groupes" });
            }
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Export(StatisticAbsenceForm statisticAbsenceForm)
        {
            if (ModelState.IsValid)
            {
                StatisticAbsenceBLO statisticAbsenceBLO = new StatisticAbsenceBLO(this.GAppContext);
                Statistic statistic = statisticAbsenceBLO.Calculate(statisticAbsenceForm);
                

                using (XLWorkbook wb = new XLWorkbook())
                {
                    DataTable dataTable = statistic.DataTable;
                    statistic.DataTable.TableName = "Statistiques";
                    wb.Worksheets.Add(dataTable);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        string FileName = string.Format("{0}-{1}", statistic.Name, DateTime.Now.ToShortDateString());
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", FileName + ".xlsx");
                    }
                }

            }
            Alert(string.Format("Veuillez Saisir des informations valide"), NotificationType.success);
            return RedirectToAction("Index");
        }

    }
}