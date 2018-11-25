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
using System.Text;
using GApp.Models.Pages.Components;
using GApp.Models.Pages;
using GApp.Models.GAppComponents;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using GApp.Models.DataAnnotations;

namespace TrainingIS.WebApp.Controllers
{
    public partial class AbsencesController
    {
        #region Entry Absence from SeanceTeaining
        /// <summary>
        ///  Entry Absences by Groups from SeanceTrainings
        /// </summary>
        /// <param name="AbsenceDate">Date of Seance</param>
        /// <param name="Seance_Number_Reference">SeanceNumber reference</param>
        /// <returns></returns>
        public ActionResult Create_Group_Absences(string AbsenceDate, long? SeanceNumberId)
        {
            // [Localization]
            msg["Create_Group_Title"] = string.Format("Saisie d'absence : {0} ", AbsenceDate);

            try
            {
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
                    List<SeanceModel> Seances = new SeanceModelBLM(this._UnitOfWork, this.GAppContext).GetSeances(SeanceDate, SeanceNumberId);
                    ViewBag.Seances = Seances;

                    List<Specialty> Specialties = Seances.Select(s => s.SeancePlanning.Training.Group.Specialty).Distinct().ToList();
                    List<ClassroomCategory> ClassroomCategories = Seances.Select(s => s.SeancePlanning.Classroom.ClassroomCategory).Distinct().ToList();

                    ViewBag.Specialties = Specialties;
                    ViewBag.ClassroomCategories = ClassroomCategories;

                    return View(create_Group_Absences_Model);
                }
            }
            catch (GAppException ex)
            {
                Alert(ex.Message, NotificationType.error);
                return RedirectToAction("Index");
            }
           

            // [Bug]
            string msg_e = string.Format("This page does not exist");
            Alert(msg_e, NotificationType.error);
            return RedirectToAction("Index");
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

        #endregion

        #region Create and Delete absence in SeanceTraining
        public ActionResult Create_Absence(Int64 TraineeId, Int64 SeanceTainingId)
        {

            // Create The SeanceTraining if not yet exist
            //  SeanceTraining seanceTraining = new SeanceTrainingBLO(this._UnitOfWork, this.GAppContext).CreateIfNotExist(AbsenceDate, SeancePlanningId);
            SeanceTraining seanceTraining = new SeanceTrainingBLO(this._UnitOfWork, this.GAppContext).FindBaseEntityByID(SeanceTainingId);

            // Create Absence if not exist
            Absence absence = this.AbsenceBLO.Find_By_TraineeId_SeanceTraining(TraineeId, SeanceTainingId);
            if (absence == null)
            {
                absence = this.AbsenceBLO.CreateInstance();
                absence.TraineeId = TraineeId;
                absence.Trainee = new TraineeBLO(this._UnitOfWork, this.GAppContext).FindBaseEntityByID(TraineeId);
                absence.AbsenceDate = Convert.ToDateTime(seanceTraining.SeanceDate);
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
        #endregion


        #region Entry Absence by Supervisor 
        public ActionResult Get_Absences_Forms_With_Create_SeanceTraining(Int64? SeancePlanningId, DateTime SeanceDate)
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
            if (result == null)
            {
                var ContentResult = this.Get_Absences_Forms(seanceTraining.Id) as ContentResult;
                return Content(ContentResult.Content);
            }
            else
            {
                return View("Get_Absences_Forms", result.Model);
            }


        }

        #endregion

        #region Valide and Invalide absence by Supervisor
        public virtual ActionResult Validate(long? id, FilterRequestParams filterRequestParams)
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
                this.AbsenceBLO.ChangeState_to_Valid(Absence);
                
            }
            catch (GAppException ex)
            {
                Alert(ex.Message, NotificationType.error);

                return RedirectToAction("Index");
            }

            //  Alert(string.Format(msgManager.The_entity_has_been_changed, msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_Absence.SingularName.ToLower(), Absence), NotificationType.success);
            return RedirectToAction("Index");

        }

        public virtual ActionResult Unvalidate(long? id, FilterRequestParams filterRequestParams)
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
                this.AbsenceBLO.ChangeState_to_InValid(Absence);
            }
            catch (GAppException ex)
            {
                Alert(ex.Message, NotificationType.error);
                return RedirectToAction("Index");
            }

            //            Alert(string.Format(msgManager.The_entity_has_been_changed, msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_Absence.SingularName.ToLower(), Absence), NotificationType.success);
            return RedirectToAction("Index");

        }
        #endregion

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

        public ActionResult Delete_SeanceTrainings(long Id, string returnUrl)
        {
            SeanceTrainingBLO seanceTrainingBLO = new SeanceTrainingBLO(this._UnitOfWork, this.GAppContext);
            SeanceTraining SeanceTraining = seanceTrainingBLO.FindBaseEntityByID((long)Id);

            if (SeanceTraining.FormerValidation)
            {
                Alert(string.Format("Vous ne pouvez pas supprimer une séance valider par le formateur"), NotificationType.warning);
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Delete", "SeanceTrainings", new { Id = Id, returnUrl = returnUrl });
            }
        }

        [Obsolete("this Action is used to correct the AbsenceState in version 0.0.6")]
        public ActionResult Correct_Absence_State()
        {
            if(this.GAppContext.Current_User_Name == RoleBLO.Root_ROLE)
            {
                this.AbsenceBLO.Correct_Absence_State();
                Alert("Absences States are Updated", NotificationType.info);
            }
            else
            {
                Alert("You must be root to execute this action", NotificationType.warning);
            }
           
            return RedirectToAction("Index");
        }
    }
}