using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Time_Travel_Machine.Controllers
{
    public class AdminController : Controller
    {
        private Manager m = new Manager();
        // GET: Admin
        public ActionResult AdUsers(string ddlusertype = "",string searchstring = "")
        {
            var users = m.GetAllUsers();
            List<SelectListItem> usertypeList = new List<SelectListItem>();
            var usertypes = m.GetUserType();
            //use view model
            foreach (var r in usertypes)
            {
                usertypeList.Add(
                    new SelectListItem()
                    {
                        Text = r.Value,
                        Value = r.Key
                    });
            }
            usertypeList.Add(new SelectListItem() { Text = "None", Value = "999" });
            ViewBag.ddlusertype = usertypeList;

            if (!string.IsNullOrWhiteSpace(ddlusertype))
            {
                if(ddlusertype != "999")
                {
                    int utype = Convert.ToInt32(ddlusertype);
                    users = users.Where(u => u.usertype == utype).ToList();
                }
            }

            if (!string.IsNullOrWhiteSpace(searchstring))
            {
                bool isNumber = Regex.IsMatch(searchstring, @"^\d+$");
                if (isNumber)
                {
                    int searchNumber = Convert.ToInt32(searchstring);
                    users = users.Where(c => c.userid == searchNumber ).ToList();
                }
                else
                {
                    users = users.Where(c => c.username.ToLower().Contains(searchstring.ToLower()) || c.email.ToLower().Contains(searchstring.ToLower())).ToList();
                }
            }
            return View("Adusers",users);
        }
        [HttpPost]
        public JsonResult ChangeUsertype(string userid)
        {
            var result = m.ChangeUsertype(userid);
            return Json(result);
        }



        public ActionResult AdChangeRequest(string ddlregion = "", string ddlcategory = "", string ddlstartyear = "", string ddlendyear = "", string searchstring = "")
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

            if(cid != 0 || rid != 0)
            {
                if(cid != 0)
                {
                    changes = changes.Where(c => c.Category_Id == cid).ToList();
                }

                if(rid != 0)
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
            return View("AdChangeRequest", changes);
        }
        [HttpPost]
        public JsonResult ChangeCReqStatus(string changeRId)
        {
            var result = m.ChangeCReqStatus(changeRId);
            return Json(result);
        }
        public ActionResult AdBackup(string ddlregion = "", string ddlcategory = "", string ddlstartyear = "", string ddlendyear = "", string searchstring = "")
        {
            var backup = m.GetAllBackup();
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

            if(rid != 0)
            {
                backup = backup.Where(b => b.Region_Id == rid).ToList();
            }
            if (cid != 0)
            {
                backup = backup.Where(b => b.Category_Id == cid).ToList();
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
                backup = backup.Where(c => c.year >= startyear).ToList();
            }
            if (noend == false)
            {
                backup = backup.Where(c => c.year <= endyear).ToList();
            }


            //
            if (!string.IsNullOrWhiteSpace(searchstring))
            {
                bool isNumber = Regex.IsMatch(searchstring, @"^\d+$");
                if (isNumber)
                {
                    int searchNumber = Convert.ToInt32(searchstring);
                    backup = backup.Where(c => c.Content_Id == searchNumber || c.year == searchNumber).ToList();
                }
                else
                {
                    backup = backup.Where(c => c.Content_Name.ToLower().Contains(searchstring.ToLower())).ToList();
                }
            }
            backup = backup.OrderBy(b =>b.Lastupdatetime).ToList();
            return View("Adbackup",backup);
        }

        public ActionResult BackupDetails(int contentid)
        {
            var backup = m.GetOneBackup(contentid);
            return View("BackupDetails",backup);
        }

        public ActionResult AdContent(string ddlregion = "",string ddlcategory = "", string ddlstartyear = "",string ddlendyear = "",string searchstring = "")
        {
            var continentId = 0;
            int rid = 0, cid = 0;
            if (!string.IsNullOrWhiteSpace(ddlregion))
            {   
                
                if(ddlregion != "999")
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
            var contents = m.GetAllContentIndex(rid, cid).OrderBy(c => c.Content_Id).ToList();
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
                contents = contents.Where(c => c.year >= startyear).ToList();
            }
            if (noend == false)
            {
                contents = contents.Where(c => c.year <= endyear).ToList();
            }
            
            
            //
            if (!string.IsNullOrWhiteSpace(searchstring))
            {
                bool isNumber = Regex.IsMatch(searchstring, @"^\d+$");
                if (isNumber)
                {
                    int searchNumber = Convert.ToInt32(searchstring);
                    contents = contents.Where(c => c.Content_Id == searchNumber || c.year == searchNumber).ToList();
                } else {
                    contents = contents.Where(c => c.Content_Name.ToLower().Contains(searchstring.ToLower())).ToList();
                }
            }
            //
            return View("AdContent", contents);
        }

        [HttpPost]
        public JsonResult ChangeContentStatus(string contentId)
        {
            var result = m.ChangeContentStatus(contentId);
            return Json(result);
        }


        public ActionResult AdYear()
        {
            var years = m.GetAllYearOption();
            years = years.OrderBy(y => y.continentid).ThenBy(y => y.yearid).ToList();

            return View("AdYear",years);
        }
        [HttpPost]
        public JsonResult ChangeYearsStatus(string yearid,string continentid)
        {
            var result = m.ChangeYearoptionStatus(yearid,continentid);
            return Json(result);
        }

        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
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

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
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

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
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