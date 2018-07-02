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
    public class GroupsController : BaseController
    {
        private GroupBLO groupBLO = new GroupBLO();

        public ActionResult Index()
        {
           return View(groupBLO.FindAll());
        }

        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Group group = groupBLO.FindBaseEntityByID((long) id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        public ActionResult Create()
        {
            ViewBag.SpecialtyId = new SelectList(new SpecialtyBLO().FindAll(), "Id", "Code");
            ViewBag.TrainingTypeId = new SelectList(new TrainingTypeBLO().FindAll(), "Id", "Code");
            ViewBag.TrainingYearId = new SelectList(new TrainingYearBLO().FindAll(), "Id", "Code");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TrainingTypeId,TrainingYearId,SpecialtyId,Year,Code,Name,Description")] Group group)
        {
            if (ModelState.IsValid)
            {
                groupBLO.Save(group);

                return RedirectToAction("Index");
            }

            ViewBag.SpecialtyId = new SelectList(new SpecialtyBLO().FindAll(), "Id", "Code");
            ViewBag.TrainingTypeId = new SelectList(new TrainingTypeBLO().FindAll(), "Id", "Code");
            ViewBag.TrainingYearId = new SelectList(new TrainingYearBLO().FindAll(), "Id", "Code");
            return View(group);
        }

        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Group group = groupBLO.FindBaseEntityByID((long)id);
            if (group == null)
            {
                return HttpNotFound();
            }

            ViewBag.SpecialtyId = new SelectList(new SpecialtyBLO().FindAll(), "Id", "Code");
            ViewBag.TrainingTypeId = new SelectList(new TrainingTypeBLO().FindAll(), "Id", "Code");
            ViewBag.TrainingYearId = new SelectList(new TrainingYearBLO().FindAll(), "Id", "Code");
            return View(group);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TrainingTypeId,TrainingYearId,SpecialtyId,Year,Code,Name,Description")] Group group)
        {
            if (ModelState.IsValid)
            {
                Group old_group = groupBLO.FindBaseEntityByID(group.Id);
                UpdateModel(old_group);
                groupBLO.Save(old_group);
                return RedirectToAction("Index");
            }

            ViewBag.SpecialtyId = new SelectList(new SpecialtyBLO().FindAll(), "Id", "Code");
            ViewBag.TrainingTypeId = new SelectList(new TrainingTypeBLO().FindAll(), "Id", "Code");
            ViewBag.TrainingYearId = new SelectList(new TrainingYearBLO().FindAll(), "Id", "Code");
            return View(group);
        }

        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Group group = groupBLO.FindBaseEntityByID((long)id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
			Group group = groupBLO.FindBaseEntityByID((long)id);
            groupBLO.Delete(group);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                groupBLO.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
