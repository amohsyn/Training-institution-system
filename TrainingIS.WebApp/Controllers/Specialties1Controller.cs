﻿using System;
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
    public class Specialties1Controller : Controller
    {
        private TrainingISModel db = new TrainingISModel();

        // GET: Specialties1
        public ActionResult Index()
        {
            return View(db.Specialtys.ToList());
        }

        // GET: Specialties1/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specialty specialty = db.Specialtys.Find(id);
            if (specialty == null)
            {
                return HttpNotFound();
            }
            return View(specialty);
        }

        // GET: Specialties1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Specialties1/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Code")] Specialty specialty)
        {
            if (ModelState.IsValid)
            {

				SpecialtyBLO specialtyBLO = new SpecialtyBLO(db);
                specialtyBLO.Save(specialty);

                return RedirectToAction("Index");
            }

            return View(specialty);
        }

        // GET: Specialties1/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specialty specialty = db.Specialtys.Find(id);
            if (specialty == null)
            {
                return HttpNotFound();
            }
            return View(specialty);
        }

        // POST: Specialties1/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Code")] Specialty specialty)
        {
            if (ModelState.IsValid)
            {
                Specialty old_specialty = db.Specialtys.Find(specialty.Id);
                UpdateModel(old_specialty);

				SpecialtyBLO specialtyBLO = new SpecialtyBLO(db);
                specialtyBLO.Save(old_specialty);

                return RedirectToAction("Index");
            }
            return View(specialty);
        }

        // GET: Specialties1/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specialty specialty = db.Specialtys.Find(id);
            if (specialty == null)
            {
                return HttpNotFound();
            }
            return View(specialty);
        }

        // POST: Specialties1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Specialty specialty = db.Specialtys.Find(id);
            db.Specialtys.Remove(specialty);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
