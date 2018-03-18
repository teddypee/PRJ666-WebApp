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
            var continentId = 0;
            if (regionId > 0)
            {
                continentId = m.GetContinentId(regionId.ToString());
            }
            else
            {
                continentId = m.GetContinentId("1");
            }
            var years = m.GetYearDDL(continentId);

            var yearselectlist = new SelectList(years, "yearddlvalue", "description");
            ViewBag.yearselectlist = yearselectlist;
            ViewBag.seletedreid = regionId;
            ViewBag.seletedcaid = categoryId;
            var contents = m.GetContentIndex(regionId,categoryId);
            /*
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
            //string categoryname = */


            return View("hisCIndex",contents);
        }

        public ActionResult FilterIndex(int regionId = 0,int categoryId = 0 ,string ddlstartyear = "" ,string ddlendyear = "",string searchstring = "")
        {
            //normal ddl
            var continentId = 0;
            if (regionId > 0)
            {
                continentId = m.GetContinentId(regionId.ToString());
            }
            else
            {
                continentId = m.GetContinentId("1");
            }
            var years = m.GetYearDDL(continentId);

            var yearselectlist = new SelectList(years, "yearddlvalue", "description");
            ViewBag.yearselectlist = yearselectlist;
            ViewBag.seletedreid = regionId;
            ViewBag.seletedcaid = categoryId;
            var contents = m.GetContentIndex(regionId, categoryId);
            int startyear = 0,endyear = 0;
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
                }else
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
                }else
                {
                    //2 possibilities default "------" or None: "999X"
                    noend = true;
                }
            }else
            {
                //default
                noend = true;
            }
            //
            if(nostart == false)
            {
                contents = contents.Where(c => c.year >= startyear).ToList();
            }
            if(noend == false)
            {
                contents = contents.Where(c => c.year <= endyear).ToList();
            }


            //
            if (!string.IsNullOrWhiteSpace(searchstring))
            {
                contents = contents.Where(c => c.Content_Name.Contains(searchstring)).ToList();
            }
            return View("FilterctIndex", contents);
        }


        // GET: Content/Details/5
        public ActionResult Details(int conId)
        {
            var content = m.GetOneHisContent(conId);
            return View("HisConDetails",content);
        }

        [HttpPost]
        public JsonResult GetEndYearDDL(string startyearcode)
        {
            var yearlist = new List<KeyValuePair<string, string>>();
            var startyear = 0;
            if (!string.IsNullOrEmpty(startyearcode))
            {
                var continentid = startyearcode.Substring(startyearcode.Length - 1, 1);
                var seletedyearid = startyearcode.Substring(0, startyearcode.Length - 1);
                startyear = m.GetYearByYearIdandContinentId(seletedyearid,continentid);
                
                //get endyear ddl
                var years = m.GetYearDDL(continentid,startyear);
                if (years.Count() > 0)
                {
                    foreach (var y in years)
                    {
                        yearlist.Add(new KeyValuePair<string, string>(y.yearddlvalue, y.description));
                    }
                }
            }


            return Json(yearlist);
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
