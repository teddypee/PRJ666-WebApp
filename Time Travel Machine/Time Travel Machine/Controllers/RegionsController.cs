using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Time_Travel_Machine.Controllers
{
    public class RegionsController : Controller
    {
        // GET: Regions
        public ActionResult Index()
        {
            return View();
        }

        // GET: Regions/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Regions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Regions/Create
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

        // GET: Regions/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Regions/Edit/5
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

        // GET: Regions/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Regions/Delete/5
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
