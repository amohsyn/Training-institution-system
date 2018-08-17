using GApp.BLL.Enums;
using GApp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingIS.BLL;
using TrainingIS.DAL;
using TrainingIS.Entities;
using TrainingIS.WebApp.Controllers;
using TrainingIS.WebApp.Views.Base;

namespace TrainingIS.WebApp.Filters
{
    public class TrainingIS_Context_Filter : BaseFilterAttribute , IActionFilter
    {
       

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
          


        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            TrainingYear CurrentTrainingYear =  this.CheckCurrentTrainingYear(filterContext);


            this._Controller.GAppContext.Session.Add("CurrentTrainingYear", CurrentTrainingYear);
        }
 
        /// <summary>
        /// Check CurrentTrainingYear from Session or DataBase
        /// </summary>
        private TrainingYear CheckCurrentTrainingYear(ActionExecutingContext filterContext)
        {
            UnitOfWork<TrainingISModel> unitOfWork = new UnitOfWork<TrainingISModel>();
            TrainingYearBLO trainingYearBLO = new TrainingYearBLO(unitOfWork, this._Controller.GAppContext);
            TrainingYear currentTrainingYear = null;

            // Chek Session value
            if (filterContext.HttpContext.Session[ApplicationParamBLO.CURRENT_TrainingYear_Reference] != null)
            {
                string CurrentTrainingYear_Reference = filterContext.HttpContext.Session[ApplicationParamBLO.CURRENT_TrainingYear_Reference] as string;
                currentTrainingYear = trainingYearBLO.FindBaseEntityByReference(CurrentTrainingYear_Reference);
            }
            else
            {
                currentTrainingYear = trainingYearBLO.getCurrentTrainingYear();
                if (currentTrainingYear == null)
                {

                    this._Controller.Alert(msg_Base.You_have_to_add_a_year_of_training, NotificationType.warning);
                    if(this._Controller.GetType().Name != nameof(TrainingYearsController))
                    {
                       if( this._Controller.HasPermission.ToAction("TrainingYears", "Create"))
                        {
                            filterContext.Result = new RedirectToRouteResult(
                                new System.Web.Routing.RouteValueDictionary(
                                new { controller = "TrainingYears", action = "Index" }));
                        }
                    }

                }

            }

            this._Controller.ViewBag.CurrentTrainingYear = currentTrainingYear;
            this._Controller.ViewBag.TrainingYears = trainingYearBLO.FindAll();

            return currentTrainingYear;

        }
    }
}