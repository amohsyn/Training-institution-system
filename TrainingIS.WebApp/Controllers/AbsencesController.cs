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

namespace TrainingIS.WebApp.Controllers
{
    public partial class AbsencesController
    {
        public ActionResult Create_Group_Absences(string AbsenceDate, string Seance_Number_Reference)
        {
            
            // [Bug] localization
            msg["Create_Group_Title"] = "Create Group Title";


            if(AbsenceDate != null)
            {
                Create_Group_Absences_ModelBLM Create_Group_Absences_BLM = new Create_Group_Absences_ModelBLM(this._UnitOfWork, this.GAppContext);
                Create_Group_Absences_Model create_Group_Absences_Model
                    = Create_Group_Absences_BLM.CreateInstance(Convert.ToDateTime(AbsenceDate), Seance_Number_Reference);


                ViewBag.SeanceNumberId = new SelectList(new SeanceNumberBLO(this._UnitOfWork, this.GAppContext).FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), create_Group_Absences_Model.SeanceNumberId);

               //// ViewBag.GroupId = new SelectList(new GroupBLO(this._UnitOfWork, this.GAppContext).FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), create_Group_Absences_Model.GroupId);
               // ViewBag.FormerId = new SelectList(new FormerBLO(this._UnitOfWork, this.GAppContext).FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), create_Group_Absences_Model.FormerId);
               // ViewBag.ModuleTrainingId = new SelectList(new ModuleTrainingBLO(this._UnitOfWork, this.GAppContext).FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), create_Group_Absences_Model.ModuleTrainingId);
               // ViewBag.ClassroomId = new SelectList(new ClassroomBLO(this._UnitOfWork, this.GAppContext).FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), create_Group_Absences_Model.ClassroomId);

               // ViewBag.Data_Groups = new GroupBLO(this._UnitOfWork, this.GAppContext).FindAll().Cast<BaseEntity>().ToList<BaseEntity>();

                Dictionary<Entities.Group,Entities.SeancePlanning> Groups = new Dictionary<Group, SeancePlanning>(); 


                return View(create_Group_Absences_Model);
            }


           
            // [Bug]
            string msg_e = string.Format("This page does not exist");
            Alert(msg_e, NotificationType.error);
            return RedirectToAction("Index");
        }
    }
}