﻿using GApp.BLL.Enums;
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

namespace TrainingIS.WebApp.Controllers
{
    public partial class AbsencesController
    {
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

        public ViewResult Get_Absences_Forms(Int64 SeancePlanningId)
        {
            List<Index_Absence_Model> model = new List<Index_Absence_Model>();

            SeancePlanning seancePlanning = new SeancePlanningBLO(this._UnitOfWork, this.GAppContext).FindBaseEntityByID(SeancePlanningId);
            if (seancePlanning == null)
            {
                string msg_exception = string.Format("SeancePlanningId does not exist in database");
                throw new ArgumentNullException("SeancePlanningId", msg_exception);
            }

            List<Trainee> Trainees = new TraineeBLO(this._UnitOfWork, this.GAppContext).Find_By_GroupId(seancePlanning.Training.Group.Id);

            List<Absence> Absences = seancePlanning.Absences;
            List<Int64> Trainees_Abscences = Absences.Select(a => a.Trainee.Id).ToList();

            foreach (Trainee trainee in Trainees)
            {
                Index_Absence_Model index_Absence_Model = null;
                if (Trainees_Abscences.Contains(trainee.Id))
                {
                    Absence absence = Absences[Trainees_Abscences.IndexOf(trainee.Id)];
                    index_Absence_Model = new Index_Absence_ModelBLM(this._UnitOfWork, this.GAppContext).ConverTo_Index_Absence_Model(absence);

                }
                else
                {
                    index_Absence_Model = new Index_Absence_ModelBLM(this._UnitOfWork, this.GAppContext).CreateNew();
                    index_Absence_Model.Trainee = trainee;
                    index_Absence_Model.Absent = false;
                    index_Absence_Model.SeancePlanning = seancePlanning;
                }
                model.Add(index_Absence_Model);
            }
            return View(model);
        }

        public ViewResult Create_Absence(Int64 TraineeId, Int64 SeancePlanningId)
        {
            // Create Absence if not exist
            Absence absence = this.AbsenceBLO.Find_By_TraineeId_SeancePlanningId(TraineeId, SeancePlanningId);
            if(absence == null)
            {
                absence = this.AbsenceBLO.CreateInstance();
                absence.TraineeId = TraineeId;
                absence.SeancePlanningId = SeancePlanningId;
                absence.AbsenceDate = DateTime.Now;
                this.AbsenceBLO.Save(absence);
            }

            Index_Absence_Model Index_Absence_Model = new Index_Absence_ModelBLM(this._UnitOfWork, this.GAppContext).ConverTo_Index_Absence_Model(absence);
            return View(Index_Absence_Model);
        }
        public ViewResult Delete_Absence(Int64 TraineeId, Int64 SeancePlanningId)
        {
            Absence absence = this.AbsenceBLO.Find_By_TraineeId_SeancePlanningId(TraineeId, SeancePlanningId);

            Index_Absence_Model index_Absence_Model = new Index_Absence_ModelBLM(this._UnitOfWork, this.GAppContext).CreateNew();
            Trainee trainee = null;
            SeancePlanning seancePlanning = null;
            if (absence != null)
            {
                trainee = absence.Trainee;
                seancePlanning = absence.SeancePlanning;
                this.AbsenceBLO.Delete(absence);
            }
            else
            {
                trainee = new TraineeBLO(this._UnitOfWork, this.GAppContext).FindBaseEntityByID(TraineeId);
                seancePlanning =new SeancePlanningBLO(this._UnitOfWork, this.GAppContext).FindBaseEntityByID(SeancePlanningId);
            }
         
            index_Absence_Model.Trainee = trainee;
            index_Absence_Model.Absent = false;
            index_Absence_Model.SeancePlanning = seancePlanning;
            return View(index_Absence_Model);
        }
    }
}