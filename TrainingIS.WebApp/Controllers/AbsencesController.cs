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

namespace TrainingIS.WebApp.Controllers
{
    public partial class AbsencesController
    {
        /// <summary>
        /// Show All Absences
        /// </summary>
        public override ActionResult Index()
        {
            msgHelper.Index(msg);
            List<Index_Absence_Model> listIndex_Absence_Model = new List<Index_Absence_Model>();
            foreach (var item in AbsenceBLO.FindAll())
            {
                Index_Absence_Model Index_Absence_Model = new Index_Absence_ModelBLM(this._UnitOfWork, this.GAppContext)
                    .ConverTo_Index_Absence_Model(item);
                listIndex_Absence_Model.Add(Index_Absence_Model);
            }
            return View(listIndex_Absence_Model);
        }

        /// <summary>
        ///  Create Form Absences by Groups 
        /// </summary>
        /// <param name="AbsenceDate"></param>
        /// <param name="Seance_Number_Reference"></param>
        /// <returns></returns>
        public ActionResult Create_Group_Absences(string AbsenceDate, string Seance_Number_Reference)
        {

            // [Bug] localization
            msg["Create_Group_Title"] = string.Format("Saisie d'absence : {0} ", AbsenceDate);


            if (AbsenceDate != null)
            {
                Create_Group_Absences_ModelBLM Create_Group_Absences_BLM = new Create_Group_Absences_ModelBLM(this._UnitOfWork, this.GAppContext);

                Create_Group_Absences_Model create_Group_Absences_Model
                    = Create_Group_Absences_BLM.CreateInstance(Convert.ToDateTime(AbsenceDate), Seance_Number_Reference);

                ViewBag.SeanceNumberId = new SelectList(new SeanceNumberBLO(this._UnitOfWork, this.GAppContext).FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), create_Group_Absences_Model.SeanceNumberId);
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
           

            return RedirectToAction(nameof(Get_Absences_Forms), new { SeanceTainingId = seanceTraining.Id });
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
                return Content("Veuillz choisir une seance de plannig valide");
            }

            Entry_Absence_Model_BLM entry_Absence_Model_BLM = new Entry_Absence_Model_BLM(this._UnitOfWork, this.GAppContext);
            List<Entry_Absence_Model> Entry_Absences = entry_Absence_Model_BLM.Get_Entry_Absence_Models(seanceTraining);
            return View(Entry_Absences);
        }

        public ActionResult Create_Absence(Int64 TraineeId, Int64 SeanceTainingId)
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



    }
}