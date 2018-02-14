using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Time_Travel_Machine.Controllers
{
    public class ChangeRequestsController : Controller
    {
        // GET: ChangeRequests
        public ActionResult Index()
        {
            return View();
        }

        // GET: ChangeRequests/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ChangeRequests/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChangeRequests/Create
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

        // GET: ChangeRequests/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ChangeRequests/Edit/5
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

        // GET: ChangeRequests/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ChangeRequests/Delete/5
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
