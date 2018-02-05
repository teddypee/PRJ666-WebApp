using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Time_Travel_Machine.Controllers
{
    public class PromotesController : Controller
    {
        // GET: Promotes
        public ActionResult Index()
        {
            return View();
        }

        // GET: Promotes/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Promotes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Promotes/Create
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

        // GET: Promotes/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Promotes/Edit/5
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

        // GET: Promotes/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Promotes/Delete/5
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
