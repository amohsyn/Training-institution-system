using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrainingIS.DAL;
using TrainingIS.Entities;
using TrainingIS.BLL;

namespace TrainingIS.WebApp.Controllers
{
    public class TrainingYearsController : BaseController
    {
        private TrainingYearBLO trainingYearBLO = new TrainingYearBLO();

        public ActionResult Index()
        {
           return View(trainingYearBLO.FindAll());
        }

        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TrainingYear trainingYear = trainingYearBLO.FindBaseEntityByID((long) id);
            if (trainingYear == null)
            {
                return HttpNotFound();
            }
            return View(trainingYear);
        }

        public ActionResult Create()
        {
            TrainingYear trainingYear = new TrainingYear();
            return View(trainingYear);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Code,StartDate,EndtDate")] TrainingYear trainingYear)
        {
            if (ModelState.IsValid)
            {
                trainingYearBLO.Save(trainingYear);

                return RedirectToAction("Index");
            }

            return View(trainingYear);
        }

        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TrainingYear trainingYear = trainingYearBLO.FindBaseEntityByID((long)id);
            if (trainingYear == null)
            {
                return HttpNotFound();
            }
            return View(trainingYear);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,StartDate,EndtDate")] TrainingYear trainingYear)
        {
            if (ModelState.IsValid)
            {
                TrainingYear old_trainingYear = trainingYearBLO.FindBaseEntityByID(trainingYear.Id);
                UpdateModel(old_trainingYear);
                trainingYearBLO.Save(old_trainingYear);
                return RedirectToAction("Index");
            }
            return View(trainingYear);
        }

        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TrainingYear trainingYear = trainingYearBLO.FindBaseEntityByID((long)id);
            if (trainingYear == null)
            {
                return HttpNotFound();
            }
            return View(trainingYear);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
			TrainingYear trainingYear = trainingYearBLO.FindBaseEntityByID((long)id);
            trainingYearBLO.Delete(trainingYear);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                trainingYearBLO.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
