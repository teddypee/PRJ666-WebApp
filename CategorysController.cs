using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Time_Travel_Machine.Controllers
{
    public class CategorysController : Controller
    {
        private List<Category> Categorys;

        public CategorysController()
        {
            Categorys = new List<Category>();
            var Warfare = new Category();
            Warfare.categoryID = 1;
            Warfare.categoryName = "Wars";
            Warfare.lastUpdateDate = new DateTime(2018, 2, 13);
            Warfare.updateUserID = 101;

        }

        
        // GET: Categorys
        public ActionResult Index()
        {
            return View(Categorys);
        }

        // GET: Categorys/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Categorys/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categorys/Create
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

        // GET: Categorys/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Categorys/Edit/5
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

        // GET: Categorys/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Categorys/Delete/5
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
