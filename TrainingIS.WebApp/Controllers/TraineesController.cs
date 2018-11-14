using GApp.BLL.Enums;
using GApp.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrainingIS.BLL;
using TrainingIS.BLL.ModelsViews;
using TrainingIS.Entities;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities.Resources.TraineeResources;
using TrainingIS.WebApp.Manager.Views.msgs;

namespace TrainingIS.WebApp.Controllers
{
    public partial class TraineesController
    {

        [HttpGet]
        public ActionResult Upload_Picturs()
        {
            return View();
        }
        [HttpPost, ActionName("Upload_Picturs") ]
        public ActionResult Upload_Picturs_Post()
        {
            string Trainee_Name = string.Empty;
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFile file = System.Web.HttpContext.Current.Request.Files[fileName]; 
                    

                    //Save file content goes here
                  
                    if (file != null && file.ContentLength > 0)
                    {
                        

                        string Trainee_CEF = Path.GetFileNameWithoutExtension(file.FileName);

                        // Check if Trainee Exist
                        Trainee trainee =  this.TraineeBLO.FindByCEF(Trainee_CEF);
                        if(trainee == null)
                        {
                            Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                            string msg_er = string.Format("Le stagiaire avec le CEF {0} n'exist pas ", Trainee_CEF);
                            return Json(msg_er);
                            
                        }
                        Trainee_Name = trainee.GetFullName();
                        // Save to Tmp Dictectory
                        GPictureBLO gpictureBLO = new GPictureBLO(this._UnitOfWork, this.GAppContext);
                        string _GPicture_Reference = gpictureBLO.Save_Tmp(file);


                        // Save to Upload 
                        Default_Form_Trainee_ModelBLM modelBLM = new Default_Form_Trainee_ModelBLM(this._UnitOfWork, this.GAppContext);
                        Default_Form_Trainee_Model default_Form_Trainee_Model = new Default_Form_Trainee_Model(); ;
                        modelBLM.ConverTo_Default_Form_Trainee_Model(default_Form_Trainee_Model, trainee);
                        default_Form_Trainee_Model.Photo_Reference = _GPicture_Reference;
                        trainee = modelBLM.ConverTo_Trainee(default_Form_Trainee_Model);

                        this.TraineeBLO.Save(trainee);
                    }

                }

            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;

                return Json(ex.Message);

            }


            return Json(Trainee_Name);
        }

        public virtual ActionResult Absences(long? id)
        {
            msgHelper.Details(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainee Trainee = TraineeBLO.FindBaseEntityByID((long)id);
            if (Trainee == null)
            {
                string msg = string.Format(msgManager.You_try_to_show_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Trainee.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }


            return View(Trainee);
        }

    }
}