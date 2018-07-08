using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrainingIS.WebApp.Controllers
{
    public class llController : Controller
    {
        // GET: ll
        public ActionResult Index()
        {
            return View();
        }

        // GET: ll/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ll/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ll/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ll/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ll/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ll/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ll/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
