using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Time_Travel_Machine.Models;

namespace Time_Travel_Machine.Controllers
{
    public class ChangeRequestController : Controller
    {
        private Manager m = new Manager();
        const int promote_required = 3;
        // GET: ChangeRequest
        //temp
        public ActionResult Index()
        {
            var list = m.GetAllChangeRequest_Index();
            return View();
        }

        //temp
        public void AddOne(Change_Request newobj)
        {
            var o = m.AddOneChangeRequest(newobj);
        }

        public void EditOne(Change_Request newobj)
        {
            var o = m.EditOneChangeRequest(newobj);
        }

        public void Promote(int crid,int userid)
        {
            m.Promote(crid,userid);

            //check the amount of promotes for crid
            var amount = m.checkPromote(crid);
            //if it reach the standord, transfer it
            if(crid == promote_required)
            {
                m.transfer(crid);
            }
             

        }

        // GET: ChangeRequest/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ChangeRequest/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChangeRequest/Create
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

        // GET: ChangeRequest/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ChangeRequest/Edit/5
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

        // GET: ChangeRequest/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ChangeRequest/Delete/5
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
