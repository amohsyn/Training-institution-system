using GApp.BLL.Enums;
using GApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingIS.BLL;
using TrainingIS.BLL.ModelsViews;
using TrainingIS.Entities.Base;
using TrainingIS.Models.Absences;
using TrainingIS.WebApp.Manager.Views.msgs;
using TrainingIS.Entities;
using GApp.Exceptions;
using System.Net;
using TrainingIS.Entities.Resources.AbsenceResources;
using GApp.DAL.Exceptions;
using TrainingIS.Models.Seances;
using TrainingIS.Models.Paginations;
using System.Text;
using GApp.Models.Pages;

namespace TrainingIS.WebApp.Controllers
{
    public partial class AbsencesController
    {

        private Dictionary<string, string> GetHeaderTextAndIDs()
        {
            Dictionary<string, string> headerTextAndIDs = new Dictionary<string, string>();
            headerTextAndIDs.Add("AbsenceDate", "Date d'absence");
            headerTextAndIDs.Add("Trainee", "Stagiaire");
            headerTextAndIDs.Add("Group", "Groupe");
            headerTextAndIDs.Add("isHaveAuthorization", "Justification");
            return headerTextAndIDs;
        }

        /// <summary>
        /// Show All Absences
        /// </summary>
        public override ActionResult Index()
        {
            return this.Index(null, null, null, null);
            //msgHelper.Index(msg);
            //List<Index_Absence_Model> listIndex_Absence_Model = new List<Index_Absence_Model>();
            //foreach (var item in AbsenceBLO.FindAll().Take(200))
            //{
            //    Index_Absence_Model Index_Absence_Model = new Index_Absence_ModelBLM(this._UnitOfWork, this.GAppContext)
            //        .ConverTo_Index_Absence_Model(item);
            //    listIndex_Absence_Model.Add(Index_Absence_Model);
            //}
            //return View(listIndex_Absence_Model);
        }

        // GET: Student
        [HttpGet]
        public ActionResult Index(string OrderBy, string SearchBy, int? currentPage, int? pageSize)
        {
            Index_GAppPage index_page = new Index_GAppPage();
            index_page.Title = "Gestion d'absences";
            ViewBag.index_page = index_page;

            Dictionary<string, string> headerTextAndIDs = this.GetHeaderTextAndIDs();

            List<Absence> customerList = null;
            int totalRecords = 0;
            Pager pagerSettings = null;

            customerList = this._UnitOfWork.context.Absences.ToList();
            totalRecords = customerList.Count();
            pagerSettings = new Pager().GetPager(totalRecords, currentPage, pageSize);
            customerList = customerList.Skip(pagerSettings.startIndex).Take(pagerSettings.pageSize).ToList();

            StringBuilder htmlBuilder = new StringBuilder();

            htmlBuilder = Pager.CreateHtmlFilterSearchBlock(htmlBuilder, SearchBy, pagerSettings.pageSize);

            htmlBuilder = Pager.CreateHtmlTableStartBlock(htmlBuilder);

            htmlBuilder = Pager.CreateHtmlTableHeaderBlock(htmlBuilder, headerTextAndIDs, OrderBy);

            htmlBuilder = Pager.CreateHtmlTableBodyFromList(htmlBuilder, customerList.ToList<object>(), headerTextAndIDs, true);

            htmlBuilder = Pager.CreateHtmlTableEndBlock(htmlBuilder);

            htmlBuilder = Pager.CreateHtmlPagerLinksBlock(htmlBuilder, pagerSettings);

            ViewBag.HtmlStr = htmlBuilder.ToString();

            return View();
        }


        public ActionResult GetDataTable(string OrderBy, string SearchBy, int? currentPage, int? pageSize)
        {
            Dictionary<string, string> headerTextAndIDs = new Dictionary<string, string>();
            headerTextAndIDs.Add("AbsenceDate", "Date d'absence");
            headerTextAndIDs.Add("Trainee", "Stagiaire");
            headerTextAndIDs.Add("Group", "Groupe");
            headerTextAndIDs.Add("isHaveAuthorization", "Justification");

            int totalRecords = 0;
            Pager pagerSettings = null;
            bool IsOrderByAppliedOnAnyColumn = false;


            var absences_list = (from a in this._UnitOfWork.context.Absences
                                 select a).ToList();

            if (!string.IsNullOrEmpty(SearchBy))
            {
                absences_list = absences_list
                    .Where(absence => absence.Id.ToString().Contains(SearchBy)
                    || absence.AbsenceDate.ToString().Contains(SearchBy)

                    || absence.SeanceTraining.ToString().Contains(SearchBy)
                    || absence.Trainee.ToString().Contains(SearchBy)
                    )
                    .ToList();
            }


            if (!string.IsNullOrEmpty(OrderBy))
            {
                IsOrderByAppliedOnAnyColumn = true;

                switch (OrderBy)
                {
                    case "AbsenceDate_Asc":
                        absences_list = absences_list.OrderBy(O => O.AbsenceDate).ToList();
                        break;

                    case "AbsenceDate_Desc":
                        absences_list = absences_list.OrderByDescending(O => O.AbsenceDate).ToList();
                        break;

                    case "Trainee_Asc":
                        absences_list = absences_list.OrderBy(O => O.Trainee.FirstName).ToList();
                        break;

                    case "Trainee_Desc":
                        absences_list = absences_list.OrderByDescending(O => O.Trainee.FirstName).ToList();
                        break;

                    case "Group_Asc":
                        absences_list = absences_list.OrderBy(O => O.SeanceTraining.SeancePlanning.Training.Group.Code).ToList();
                        break;

                    case "Group_Desc":
                        absences_list = absences_list.OrderByDescending(O => O.SeanceTraining.SeancePlanning.Training.Group.Code).ToList();
                        break;

                    case "isHaveAuthorization_Asc":
                        absences_list = absences_list.OrderBy(O => O.isHaveAuthorization).ToList();
                        break;

                    case "isHaveAuthorization_Desc":
                        absences_list = absences_list.OrderByDescending(O => O.isHaveAuthorization).ToList();
                        break;
                }
            }


            totalRecords = absences_list.Count();
            pagerSettings = new Pager().GetPager(totalRecords, currentPage, pageSize);
            if (IsOrderByAppliedOnAnyColumn == false)
            {
                absences_list = absences_list.Skip(pagerSettings.startIndex).Take(pagerSettings.pageSize).ToList();
            }
            else
            {
                absences_list = absences_list.Skip(pagerSettings.startIndex).Take(pagerSettings.pageSize).ToList();
            }


            StringBuilder htmlBuilder = new StringBuilder();

            htmlBuilder = Pager.CreateHtmlFilterSearchBlock(htmlBuilder, SearchBy, pagerSettings.pageSize);

            htmlBuilder = Pager.CreateHtmlTableStartBlock(htmlBuilder);

            htmlBuilder = Pager.CreateHtmlTableHeaderBlock(htmlBuilder, headerTextAndIDs, OrderBy);

            htmlBuilder = Pager.CreateHtmlTableBodyFromList(htmlBuilder, absences_list.ToList<object>(), headerTextAndIDs, true);

            htmlBuilder = Pager.CreateHtmlTableEndBlock(htmlBuilder);

            htmlBuilder = Pager.CreateHtmlPagerLinksBlock(htmlBuilder, pagerSettings);


            return Json(new { data = htmlBuilder.ToString() }, JsonRequestBehavior.AllowGet);
        }



        //public JsonResult getTable(string OrderBy, string SearchBy, int? currentPage, int? pageSize)
        //{
        //    Dictionary<string, string> headerTextAndIDs = new Dictionary<string, string>();
        //    headerTextAndIDs.Add("AbsenceDate", "Date d'absence");
        //    headerTextAndIDs.Add("Trainee", "Stagiaire");
        //    headerTextAndIDs.Add("Group", "Groupe");
        //    headerTextAndIDs.Add("isHaveAuthorization", "Justification");

        //    int totalRecords = 0;
        //    Pager pagerSettings = null;
        //    bool IsOrderByAppliedOnAnyColumn = false;


        //    var absences_list = (from a in this._UnitOfWork.context.Absences
        //                        select a).ToList();

        //    if (!string.IsNullOrEmpty(SearchBy))
        //    {
        //        absences_list = absences_list
        //            .Where(absence => absence.Id.ToString().Contains(SearchBy) 
        //            || absence.AbsenceDate.ToString().Contains(SearchBy) 

        //            || absence.SeanceTraining.ToString().Contains(SearchBy)
        //            || absence.Trainee.ToString().Contains(SearchBy)
        //            )
        //            .ToList();
        //    }


        //    if (!string.IsNullOrEmpty(OrderBy))
        //    {
        //        IsOrderByAppliedOnAnyColumn = true;

        //        switch (OrderBy)
        //        {
        //            case "AbsenceDate_Asc":
        //                absences_list = absences_list.OrderBy(O => O.AbsenceDate).ToList();
        //                break;

        //            case "AbsenceDate_Desc":
        //                absences_list = absences_list.OrderByDescending(O => O.AbsenceDate).ToList();
        //                break;

        //            case "Trainee_Asc":
        //                absences_list = absences_list.OrderBy(O => O.Trainee.FirstName).ToList();
        //                break;

        //            case "Trainee_Desc":
        //                absences_list = absences_list.OrderByDescending(O => O.Trainee.FirstName).ToList();
        //                break;

        //            case "Group_Asc":
        //                absences_list = absences_list.OrderBy(O => O.SeanceTraining.SeancePlanning.Training.Group.Code).ToList();
        //                break;

        //            case "Group_Desc":
        //                absences_list = absences_list.OrderByDescending(O => O.SeanceTraining.SeancePlanning.Training.Group.Code).ToList();
        //                break;

        //            case "isHaveAuthorization_Asc":
        //                absences_list = absences_list.OrderBy(O => O.isHaveAuthorization).ToList();
        //                break;

        //            case "isHaveAuthorization_Desc":
        //                absences_list = absences_list.OrderByDescending(O => O.isHaveAuthorization).ToList();
        //                break;
        //        }
        //    }


        //    totalRecords = absences_list.Count();
        //    pagerSettings = new Pager().GetPager(totalRecords, currentPage, pageSize);
        //    if (IsOrderByAppliedOnAnyColumn == false)
        //    {
        //        absences_list = absences_list.Skip(pagerSettings.startIndex).Take(pagerSettings.pageSize).ToList();
        //    }
        //    else
        //    {
        //        absences_list = absences_list.Skip(pagerSettings.startIndex).Take(pagerSettings.pageSize).ToList();
        //    }

        //    StringBuilder htmlBuilder = new StringBuilder();

        //    htmlBuilder = Pager.CreateHtmlFilterSearchBlock(htmlBuilder, SearchBy, pagerSettings.pageSize);

        //    htmlBuilder = Pager.CreateHtmlTableStartBlock(htmlBuilder);

        //    htmlBuilder = Pager.CreateHtmlTableHeaderBlock(htmlBuilder, headerTextAndIDs, OrderBy);

        //    htmlBuilder = Pager.CreateHtmlTableBodyFromList(htmlBuilder, absences_list.ToList<object>(), headerTextAndIDs, true);

        //    htmlBuilder = Pager.CreateHtmlTableEndBlock(htmlBuilder);

        //    htmlBuilder = Pager.CreateHtmlPagerLinksBlock(htmlBuilder, pagerSettings);


        //    return Json(new { data = htmlBuilder.ToString() }, JsonRequestBehavior.AllowGet);
        //}



        /// <summary>
        ///  Entry Absences by Groups from SeancePlannings
        /// </summary>
        /// <param name="AbsenceDate">Date of Seance</param>
        /// <param name="Seance_Number_Reference">SeanceNumber reference</param>
        /// <returns></returns>
        public ActionResult Create_Group_Absences(string AbsenceDate, long? SeanceNumberId)
        {
            // [Bug] localization
            msg["Create_Group_Title"] = string.Format("Saisie d'absence : {0} ", AbsenceDate);


            if (AbsenceDate != null)
            {
                // Create Model Instance
                Create_Group_Absences_ModelBLM Create_Group_Absences_BLM = new Create_Group_Absences_ModelBLM(this._UnitOfWork, this.GAppContext);

                Create_Group_Absences_Model create_Group_Absences_Model
                    = Create_Group_Absences_BLM.CreateInstance(Convert.ToDateTime(AbsenceDate), SeanceNumberId);

                // SeanceNumber ComboBox
                List<SeanceNumber> listeSeanceNumber = new SeanceNumberBLO(this._UnitOfWork, this.GAppContext).FindAll();
                listeSeanceNumber.Add(new SeanceNumber() { Id = 0, Code = "Tous les séances" });
                ViewBag.SeanceNumberId = new SelectList(listeSeanceNumber, "Id", nameof(TrainingIS_BaseEntity.ToStringValue), create_Group_Absences_Model.SeanceNumberId);

                // Create Seances Model
                DateTime SeanceDate = Convert.ToDateTime(AbsenceDate);
                List<SeanceModel> Seances = new SeanceModelBLM(this._UnitOfWork,this.GAppContext).GetSeances(SeanceDate, SeanceNumberId);
                ViewBag.Seances = Seances;

                List<Specialty> Specialties = Seances.Select(s => s.SeancePlanning.Training.Group.Specialty).Distinct().ToList();
                List<ClassroomCategory> ClassroomCategories = Seances.Select(s => s.SeancePlanning.Classroom.ClassroomCategory).Distinct().ToList();

                ViewBag.Specialties = Specialties;
                ViewBag.ClassroomCategories = ClassroomCategories;

                return View(create_Group_Absences_Model);
            }

            // [Bug]
            string msg_e = string.Format("This page does not exist");
            Alert(msg_e, NotificationType.error);
            return RedirectToAction("Index");
        }


        public ActionResult Get_Absences_Forms_With_Create_SeanceTraining(Int64? SeancePlanningId,DateTime SeanceDate)
        {
            SeanceTraining seanceTraining = null;
            try
            {
                seanceTraining = new SeanceTrainingBLO(this._UnitOfWork, this.GAppContext).CreateIfNotExist(SeanceDate, Convert.ToInt64(SeancePlanningId));
            }
            catch (GAppException ex)
            {

                return Content(ex.Message);
            }

            ViewResult result = this.Get_Absences_Forms(seanceTraining.Id) as ViewResult;
            if(result == null)
            {
               var ContentResult = this.Get_Absences_Forms(seanceTraining.Id) as ContentResult;
                return Content(ContentResult.Content);
            }
            else
            {
                return View("Get_Absences_Forms", result.Model);
            }

          
        }

        /// <summary>
        /// Get the list of Trainees with Entry_Absence_Model
        /// </summary>
        /// <param name="SeanceTainingId"></param>
        /// <returns></returns>
        public ActionResult Get_Absences_Forms(Int64? SeanceTainingId)
        {

            // Check existance of SeancePlanningId
            SeanceTraining seanceTraining = new SeanceTrainingBLO(this._UnitOfWork, this.GAppContext).FindBaseEntityByID(Convert.ToInt64(SeanceTainingId));
            if (seanceTraining == null)
            {
                string msg_alert = "Veuillz choisir une seance de plannig valide, La seance de formation que vous avez demandée n'exist pas";

                return Content(msg_alert);
              
            }

            Entry_Absence_Model_BLM entry_Absence_Model_BLM = new Entry_Absence_Model_BLM(this._UnitOfWork, this.GAppContext);
            List<Entry_Absence_Model> Entry_Absences = entry_Absence_Model_BLM.Get_Entry_Absence_Models(seanceTraining);
            return View(Entry_Absences);
        }

        public ActionResult  Create_Absence(Int64 TraineeId, Int64 SeanceTainingId)
        {

            // Create The SeanceTraining if not yet exist
            //  SeanceTraining seanceTraining = new SeanceTrainingBLO(this._UnitOfWork, this.GAppContext).CreateIfNotExist(AbsenceDate, SeancePlanningId);
            SeanceTraining seanceTraining = new SeanceTrainingBLO(this._UnitOfWork, this.GAppContext).FindBaseEntityByID(SeanceTainingId);
           
            // Create Absence if not exist
            Absence absence = this.AbsenceBLO.Find_By_TraineeId_SeanceTraining(TraineeId, SeanceTainingId);
            if(absence == null)
            {
                absence = this.AbsenceBLO.CreateInstance();
                absence.TraineeId = TraineeId;
                absence.Trainee = new TraineeBLO(this._UnitOfWork, this.GAppContext).FindBaseEntityByID(TraineeId);
                absence.AbsenceDate = Convert.ToDateTime( seanceTraining.SeanceDate);
                absence.SeanceTraining = seanceTraining;
                absence.SeanceTrainingId = seanceTraining.Id;
                try
                {
                    this.AbsenceBLO.Save(absence);
                }
                catch (GAppException ex)
                {
                    // [Bug] must log the exception
                    return Content(ex.Message);
                }
               
            }

            Entry_Absence_Model_BLM entry_Absence_Model_BLM = new Entry_Absence_Model_BLM(this._UnitOfWork, this.GAppContext);
            Entry_Absence_Model Entry_Absence_Model = entry_Absence_Model_BLM.Get_Trainee_Entry_Absence_Model(seanceTraining, TraineeId);
            return View(Entry_Absence_Model);
        }
        public ActionResult Delete_Absence(Int64 TraineeId, Int64 SeanceTainingId)
        {
            Absence absence = this.AbsenceBLO.Find_By_TraineeId_SeanceTraining(TraineeId, SeanceTainingId);
            Trainee trainee = null;
            SeanceTraining seanceTraining = null;
            if (absence != null)
            {
                trainee = absence.Trainee;
                seanceTraining = absence.SeanceTraining;

                try
                {
                    this.AbsenceBLO.Delete(absence);
                }
                catch (GAppException ex)
                {
                    return Content(ex.Message);
                }

                
            }
            else
            {
                trainee = new TraineeBLO(this._UnitOfWork, this.GAppContext).FindBaseEntityByID(TraineeId);
                seanceTraining = new SeanceTrainingBLO(this._UnitOfWork, this.GAppContext).FindBaseEntityByID(SeanceTainingId);
            }

            Entry_Absence_Model_BLM entry_Absence_Model_BLM = new Entry_Absence_Model_BLM(this._UnitOfWork, this.GAppContext);
            Entry_Absence_Model Entry_Absence_Model = entry_Absence_Model_BLM.Get_Trainee_Entry_Absence_Model(seanceTraining, TraineeId);
            return View(Entry_Absence_Model);
        }

        public virtual ActionResult Validate(long? id)
        {
            msgHelper.Delete(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Absence Absence = AbsenceBLO.FindBaseEntityByID((long)id);
            if (Absence == null)
            {
                // [Bug] Localization
                string msg = string.Format("Vous essayer de valider une absence qui n'exist pas", msgHelper.UndefindedArticle(), msg_Absence.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
 
            try
            {
                Absence.Valide = true;
                AbsenceBLO.Save(Absence);
            }
            catch (GAppDbException ex)
            {
                Alert(ex.Message, NotificationType.error);
                return RedirectToAction("Index");
            }

            Alert(string.Format(msgManager.The_entity_has_been_changed, msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_Absence.SingularName.ToLower(), Absence), NotificationType.success);
            return RedirectToAction("Index");
 
        }

        public virtual ActionResult Unvalidate(long? id)
        {
            msgHelper.Delete(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Absence Absence = AbsenceBLO.FindBaseEntityByID((long)id);
            if (Absence == null)
            {
                // [Bug] Localization
                string msg = string.Format("Vous essayer de valider une absence qui n'exist pas", msgHelper.UndefindedArticle(), msg_Absence.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }

            try
            {
                Absence.Valide = false;
                AbsenceBLO.Save(Absence);
            }
            catch (GAppDbException ex)
            {
                Alert(ex.Message, NotificationType.error);
                return RedirectToAction("Index");
            }

            Alert(string.Format(msgManager.The_entity_has_been_changed, msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_Absence.SingularName.ToLower(), Absence), NotificationType.success);
            return RedirectToAction("Index");

        }


        public virtual ActionResult Validate_Absences()
        {
            return View();
        }

        [HttpPost, ActionName("Validate_Absences")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Validate_Absences_Confirm()
        {

            try
            {
                AbsenceBLO.Validate_All_Absences();
            }
            catch (GAppDbException ex)
            {
                Alert(ex.Message, NotificationType.error);
                return RedirectToAction("Index");
            }

            Alert(string.Format("Toutes les absences ont été valider"), NotificationType.success);
            return RedirectToAction("Index");
        }

        public ActionResult Delete_SeanceTrainings(long Id,string returnUrl)
        {
            SeanceTrainingBLO seanceTrainingBLO = new SeanceTrainingBLO(this._UnitOfWork, this.GAppContext);
            SeanceTraining SeanceTraining = seanceTrainingBLO.FindBaseEntityByID((long)Id);

            if(SeanceTraining.FormerValidation)
            {
                Alert(string.Format("Vous ne pouvez pas supprimer une séance valider par le formateur"), NotificationType.warning);
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Delete", "SeanceTrainings", new { Id = Id, returnUrl = returnUrl });
            }
        }


    }
}