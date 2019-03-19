using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
        public ActionResult Index(string ddlregion = "", string ddlcategory = "", string ddlstartyear = "", string ddlendyear = "", string searchstring = "")
        {
            var changes = m.GetAllChangeRequest_Index();
            var continentId = 0;
            int rid = 0, cid = 0;
            if (!string.IsNullOrWhiteSpace(ddlregion))
            {

                if (ddlregion != "999")
                {
                    rid = Convert.ToInt32(ddlregion);
                    continentId = m.GetContinentId(ddlregion);
                }
                else
                {
                    continentId = m.GetContinentId("1");
                }
            }
            else
            {
                continentId = m.GetContinentId("1");
            }
            var years = m.GetYearDDL(continentId);
            years = years.Where(y => y.active == 1).ToList();
            if (!string.IsNullOrWhiteSpace(ddlcategory))
            {
                if (ddlcategory != "999")
                {
                    cid = Convert.ToInt32(ddlcategory);
                }
            }

            if (cid != 0 || rid != 0)
            {
                if (cid != 0)
                {
                    changes = changes.Where(c => c.Category_Id == cid).ToList();
                }

                if (rid != 0)
                {
                    changes = changes.Where(c => c.Region_Id == rid).ToList();
                }
            }


            List<SelectListItem> regionList = new List<SelectListItem>();
            List<SelectListItem> categoryList = new List<SelectListItem>();

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
            ViewBag.ddlregions = regionList;
            ViewBag.ddlcategorys = categoryList;

            var yearselectlist = new SelectList(years, "yearddlvalue", "description");
            ViewBag.yearselectlist = yearselectlist;
            //ViewBag.seletedreid = regionId;
            //ViewBag.seletedcaid = categoryId;
            //var contents = m.GetAllContentIndex(rid, cid).OrderBy(c => c.Content_Id).ToList();
            int startyear = 0, endyear = 0;
            bool nostart = false, noend = false;
            //yearddlvalue - last digit(continent_id) = orginal year_id, in order to be used in Filters
            //None value = "999" + 1/+"2"/+"3" legth = 4
            var yearcode = ddlstartyear;
            var endyearcode = ddlendyear;
            if (!string.IsNullOrWhiteSpace(yearcode))
            {
                if (yearcode.Length < 4)
                {
                    var seletedyearid = yearcode.Substring(0, yearcode.Length - 1);
                    var seletedcontinentId = yearcode.Substring(yearcode.Length - 1, 1);
                    startyear = m.GetYearByYearIdandContinentId(seletedyearid, seletedcontinentId);
                }
                else
                {
                    //1 possibility None:"999x"
                    nostart = true;
                }
            }
            if (!string.IsNullOrWhiteSpace(endyearcode))
            {
                if (endyearcode.Length < 4)
                {
                    var endyearid = endyearcode.Substring(0, endyearcode.Length - 1);
                    var endyearcontinentId = endyearcode.Substring(endyearcode.Length - 1, 1);
                    endyear = m.GetYearByYearIdandContinentId(endyearid, endyearcontinentId);
                }
                else
                {
                    //2 possibilities default "------" or None: "999X"
                    noend = true;
                }
            }
            else
            {
                //default
                noend = true;
            }
            //
            if (nostart == false)
            {
                changes = changes.Where(c => c.year >= startyear).ToList();
            }
            if (noend == false)
            {
                changes = changes.Where(c => c.year <= endyear).ToList();
            }


            //
            if (!string.IsNullOrWhiteSpace(searchstring))
            {
                bool isNumber = Regex.IsMatch(searchstring, @"^\d+$");
                if (isNumber)
                {
                    int searchNumber = Convert.ToInt32(searchstring);
                    changes = changes.Where(c => c.Content_Id == searchNumber || c.year == searchNumber || c.Change_Request_Id == searchNumber).ToList();
                }
                else
                {
                    changes = changes.Where(c => c.Content_Name.ToLower().Contains(searchstring.ToLower())).ToList();
                }
            }

            changes = changes.Where(c => c.active == 1).ToList();


            return View("crIndex",changes);
        }
        /*
        public ActionResult FilterIndex(string ddlregion,string ddlcategory, string ddlyear)
        {
            List<SelectListItem> regionList = new List<SelectListItem>();
            List<SelectListItem> categoryList = new List<SelectListItem>();
            //List<SelectListItem> yearList = new List<SelectListItem>();
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

            var model = m.GetAllChangeRequest_Index_with_filter(Convert.ToInt32(ddlregion), Convert.ToInt32(ddlcategory), Convert.ToInt32(ddlyear));




            return View("crfilterIndex", model);
        }*/

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
            //note for changes, make detail umchangeable, create extent for changes 
            //newchange.detail = detail;
            newchange.extent = detail;
            newchange.reason = reason;
            newchange.picture_id = picid;

            var o = m.AddOneChangeRequest(newchange);


            return RedirectToAction("Index");
        }



        public void EditOne(Change_Request newobj)
        {
            var o = m.EditOneChangeRequest(newobj);
        }

        [HttpPost]
        public JsonResult Promote(int crid, int userid = 1)
        //public ActionResult Promote(int crid, int userid = 0)
        {
            var promote_exist = m.checkUserPromte(crid,userid);
            string result = string.Empty;
            if (promote_exist)// means the user already promote this change request
            {
                result = "exist";
                return Json(result);
                //return RedirectToAction("Index", new { msg = 1});
            }


            m.Promote(crid, userid);

            //check the amount of promotes for crid
            var amount = m.checkPromote(crid);
            //if it reach the standord, transfer it
            if (amount >= promote_required)
            {
                m.transfer(crid);
                result = "tran";
                return Json(result);
                //return RedirectToAction("Index", new { msg = 2});
            }
            result = "succ";
            return Json(result);
            //return RedirectToAction("Index", new { msg = 3});

        }

        // GET: ChangeRequest/Details/5
        public ActionResult Details(int crid)
        {
            var change = m.GetOneChangeRequest(crid);
            return View("crDetails",change);
        }
        public ActionResult CreateCRforEdit(int contentid)
        {
            var content = m.GetOneHisContent(contentid);


            return View("AddCRforEdit", content);
        }
        public ActionResult EditExistContent(string reason, string extent, string wiki, string video,string contentid)
        {
            var oldcontent = m.GetOneHisContent(Convert.ToInt32( contentid));
            var newcr = new Change_Request();
            newcr.Content_Id = oldcontent.Content_Id;
            newcr.categoryName = oldcontent.categoryName;
            newcr.Content_Name = oldcontent.Content_Name;
            newcr.Region_Id = oldcontent.Region_Id;
            newcr.Category_Id = oldcontent.Category_Id;
            newcr.detail = oldcontent.detail;            
            newcr.year = oldcontent.year;
            newcr.picname = oldcontent.picname;
            newcr.picture_id = oldcontent.picture_id;
            //new changes
            newcr.extent = extent;
            newcr.reason = reason;
            newcr.Lastupdatetime = DateTime.Now;
            newcr.video_key = video;
            newcr.wiki_key = wiki;
            newcr.userid = 0;
            m.AddOneChangeRequest(newcr);


            return RedirectToAction("Index");
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
