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
    [Authorize(Roles ="Admin,Director")]
    public class SpecialtiesController : BaseController
    {
        private SpecialtyBLO specialtyBLO = new SpecialtyBLO();
        
        public ActionResult Index()
        {
            return View(specialtyBLO.FindAll());
        }

        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specialty specialty = specialtyBLO.FindBaseEntityByID((long) id);
            if (specialty == null)
            {
                return HttpNotFound();
            }
            return View(specialty);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Code")] Specialty specialty)
        {
            if (ModelState.IsValid)
            {
                specialtyBLO.Save(specialty);
                return RedirectToAction("Index");
            }

            return View(specialty);
        }

        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specialty specialty = specialtyBLO.FindBaseEntityByID((long)id);
            if (specialty == null)
            {
                return HttpNotFound();
            }
            return View(specialty);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Code")] Specialty specialty)
        {
            if (ModelState.IsValid)
            {
                Specialty old_specialty = specialtyBLO.FindBaseEntityByID(specialty.Id);
                UpdateModel(old_specialty);
                specialtyBLO.Save(old_specialty);
                return RedirectToAction("Index");
            }
            return View(specialty);
        }

        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specialty specialty = specialtyBLO.FindBaseEntityByID((long)id);
            if (specialty == null)
            {
                return HttpNotFound();
            }
            return View(specialty);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Specialty specialty = specialtyBLO.FindBaseEntityByID((long)id);
            specialtyBLO.Delete(specialty);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                specialtyBLO.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
