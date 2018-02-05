using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Time_Travel_Machine.Controllers
{
    public class userTypesController : Controller
    {
        // GET: userTypes
        public ActionResult Index()
        {
            return View();
        }

        // GET: userTypes/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: userTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: userTypes/Create
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

        // GET: userTypes/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: userTypes/Edit/5
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

        // GET: userTypes/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: userTypes/Delete/5
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
