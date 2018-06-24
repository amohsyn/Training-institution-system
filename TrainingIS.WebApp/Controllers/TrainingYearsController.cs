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
using X.PagedList;

namespace TrainingIS.WebApp.Controllers
{
    public class TrainingYearsController : BaseController
    {
        private TrainingYearBLO trainingYearBLO = new TrainingYearBLO();



        // GET: Student
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
           
            if (string.IsNullOrEmpty(ViewBag.CodeSortParm)) ViewBag.CodeSortParm = "CodeSort";
            if (string.IsNullOrEmpty(ViewBag.StartDateSortParm)) ViewBag.StartDateSortParm = "StartDateSort";
            ViewBag.CurrentSort = sortOrder;

            TrainingISModel db = TrainingISModel.CreateContext();

  
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var trainingYears = from s in db.TrainingYears
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                trainingYears = trainingYears.Where(s => s.Code.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "code_desc":
                    trainingYears = trainingYears.OrderByDescending(s => s.Code);
                    break;
                case "code":
                    trainingYears = trainingYears.OrderBy(s => s.Code);
                    break;
                case "StartDate":
                    trainingYears = trainingYears.OrderByDescending(s => s.StartDate);
                    break;
                default:  // Name ascending 
                    trainingYears = trainingYears.OrderBy(s => s.DateModification);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(trainingYears.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Index2(string sortOrder, string CurrentSort, int? page)
        {
            TrainingISModel db = TrainingISModel.CreateContext();

            int pageSize = 5;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

            ViewBag.CurrentSort = sortOrder;
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "ID" : sortOrder;

           // IPagedList<TrainingYear> emp = null;


            return View(db.TrainingYears.OrderBy(m=>m.Id).ToPagedList(pageIndex, pageSize));
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
