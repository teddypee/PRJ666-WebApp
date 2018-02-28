using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Time_Travel_Machine.Controllers
{
    public class HisContentController : Controller
    {
        private Manager m = new Manager();
        // GET: Content

        [HttpGet]
        public ActionResult Index(int regionId = 0, int categoryId = 0)
        {



            var contents = m.GetContentIndex(regionId,categoryId);
            List<SelectListItem> yearList = new List<SelectListItem>();
            yearList.Add(new SelectListItem() { Text = "1990~ The End of the Cold War", Value = "1" });
            yearList.Add(new SelectListItem() { Text = "1914~ Great War", Value = "2" });
            yearList.Add(new SelectListItem() { Text = "1837~ The age of Victoria", Value = "3" });
            yearList.Add(new SelectListItem() { Text = "1776~ American Revolution", Value = "4" });
            yearList.Add(new SelectListItem() { Text = "1688~ Glorious Revolution", Value = "5" });
            yearList.Add(new SelectListItem() { Text = "1453~ The Fall of Constantinople", Value = "6" });
            yearList.Add(new SelectListItem() { Text = "962~ The Holly Roman Empire", Value = "7" });
            yearList.Add(new SelectListItem() { Text = "476~ The End of the Roman Empire", Value = "8" });
            yearList.Add(new SelectListItem() { Text = "A.D.", Value = "9" });
            yearList.Add(new SelectListItem() { Text = "B.C.", Value = "10" });
            yearList.Add(new SelectListItem() { Text = "None", Value = "11" });
            ViewBag.ddlyears = yearList;
            //string categoryname = 


            return View("hisCIndex",contents);
        }

        // GET: Content/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Content/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Content/Create
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

        // GET: Content/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Content/Edit/5
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

        // GET: Content/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Content/Delete/5
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
