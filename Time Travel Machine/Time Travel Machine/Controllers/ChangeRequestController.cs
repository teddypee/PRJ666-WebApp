using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Time_Travel_Machine.Models;

namespace Time_Travel_Machine.Controllers
{
    public class ChangeRequestController : Controller
    {
        // GET: ChangeRequest
        private Manager m = new Manager();
        const int promote_required = 3;
        // GET: ChangeRequest
        //temp
        public ActionResult Index(int msg = 0)
        {
            List<SelectListItem> regionList = new List<SelectListItem>();
            List<SelectListItem> categoryList = new List<SelectListItem>();
            List<SelectListItem> yearList = new List<SelectListItem>();
            var regions = m.GetRegion();
            var categorys = m.GetCategory();

            foreach (var r in regions)
            {
                regionList.Add(
                    new SelectListItem()
                    {
                        Text = r.Value,
                        Value = r.Key
                    });
            }

            foreach (var c in categorys)
            {
                categoryList.Add(
                    new SelectListItem()
                    {
                        Text = c.Value,
                        Value = c.Key
                    });
            }
            regionList.Add(new SelectListItem() { Text = "None", Value = "999" });
            categoryList.Add(new SelectListItem() { Text = "None", Value = "999" });

            yearList.Add(new SelectListItem() { Text = "1990~ The End of the Cold War",Value = "1" });
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
            ViewBag.ddlregions = regionList;
            ViewBag.ddlcategorys = categoryList;
            ViewBag.ddlyears = yearList;
            var model = m.GetAllChangeRequest_Index();

            if (msg == 0)
            {
                ViewBag.msg = string.Empty;
            }else if (msg == 1)
            {
                ViewBag.msg = "You have already promote this request!";
            }else if(msg == 2)
            {
                ViewBag.msg = "The request has been transfered!";
            }else if(msg == 3)
            {
                ViewBag.msg = "Promote Success!";
            }

            return View("crIndex",model);
        }

        public ActionResult FilterIndex(string ddlregion,string ddlcategory, string ddlyear)
        {
            List<SelectListItem> regionList = new List<SelectListItem>();
            List<SelectListItem> categoryList = new List<SelectListItem>();
            List<SelectListItem> yearList = new List<SelectListItem>();
            var regions = m.GetRegion();
            var categorys = m.GetCategory();

            foreach (var r in regions)
            {
                regionList.Add(
                    new SelectListItem()
                    {
                        Text = r.Value,
                        Value = r.Key
                    });
            }
            regionList.Add(new SelectListItem() { Text = "None",Value = "999" });

            foreach (var c in categorys)
            {
                categoryList.Add(
                    new SelectListItem()
                    {
                        Text = c.Value,
                        Value = c.Key
                    });
            }
            categoryList.Add(new SelectListItem() { Text = "None", Value = "999" });

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

            ViewBag.ddlregions = regionList;
            ViewBag.ddlcategorys = categoryList;
            ViewBag.ddlyears = yearList;
            var model = m.GetAllChangeRequest_Index_with_filter(Convert.ToInt32(ddlregion), Convert.ToInt32(ddlcategory), Convert.ToInt32(ddlyear));




            return View("crfilterIndex", model);
        }

        //temp
        public ActionResult AddOneCR()
        {
            
            List<SelectListItem> regionList = new List<SelectListItem>();
            List<SelectListItem> categoryList = new List<SelectListItem>();
            var regions = m.GetRegion();
            var categorys = m.GetCategory();

            foreach (var r in regions)
            {
                regionList.Add(
                    new SelectListItem() {
                        Text = r.Value,Value = r.Key
                    });
            }

            foreach (var c in categorys)
            {
                categoryList.Add(
                    new SelectListItem()
                    {
                        Text = c.Value,
                        Value = c.Key
                    });
            }

            ViewBag.ddlregions = regionList;
            ViewBag.ddlcategorys = categoryList;
            
            return View("AddChangeRequest");
        }
        
        [HttpPost]
        public ActionResult AddOneChangeRequest(string ddlregionforCR,string ddlcategoryforCR,string contentname, string year ,string reason,string detail,string wiki ,string video, HttpPostedFileBase picfile)
        {
            var path = string.Empty;
            var userid = 1;//temp
            var picid = 0;
            if (picfile != null)
            {
                if(picfile.ContentLength > 0)
                {
                    var filename = Path.GetFileName(picfile.FileName);
                    path = Path.Combine(Server.MapPath("~/UploadPicture"),filename);
                    picfile.SaveAs(path);
                    picid = m.SavePicturePath(filename, userid);
                }
            }
            
            //if picid = 0 then user didn't upload a picture            
            var newchange = new Change_Request();
            newchange.Region_Id = Convert.ToInt32(ddlregionforCR);
            newchange.Category_Id = Convert.ToInt32(ddlcategoryforCR);
            newchange.picture_id = picid;

            if (string.IsNullOrWhiteSpace(year) )
            {
                int n;
                var isNumeric = int.TryParse(year , out n);
                if (isNumeric)
                {
                    newchange.year = Convert.ToInt32(year);
                }
            }
            newchange.video_key = video;
            newchange.wiki_key = wiki;
            newchange.Content_Name = contentname;
            newchange.detail = detail;
            newchange.reason = reason;
            newchange.picture_id = picid;

            var o = m.AddOneChangeRequest(newchange);


            return RedirectToAction("Index");
        }

        public void EditOne(Change_Request newobj)
        {
            var o = m.EditOneChangeRequest(newobj);
        }

        //[HttpPost]
        //public JsonResult Promote(int crid, int userid)
        public ActionResult Promote(int crid, int userid)
        {
            var promote_exist = m.checkUserPromte(crid,userid);
            if (promote_exist)// means the user already promote this change request
            {
                //return Json(100);
                return RedirectToAction("Index", new { msg = 1});
            }


            var result = m.Promote(crid, userid);

            //check the amount of promotes for crid
            var amount = m.checkPromote(crid);
            //if it reach the standord, transfer it
            if (amount >= promote_required)
            {
                m.transfer(crid);
                return RedirectToAction("Index", new { msg = 2});
            }
            
            //return Json(result);
            return RedirectToAction("Index", new { msg = 3});

        }

        // GET: ChangeRequest/Details/5
        public ActionResult Details(int crid)
        {
            var change = m.GetOneChangeRequest(crid);
            return View("crDetails",change);
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
