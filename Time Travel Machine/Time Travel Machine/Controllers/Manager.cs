using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using System.Data;
using Time_Travel_Machine.Models;

namespace Time_Travel_Machine.Controllers
{
    public class Manager : Controller
    {
        // not done
        private const string mysqlconnection = "server=myvmlab.senecacollege.ca;port=6079;user id=student;password=Group09;database=g9;charset=utf8;";
        MySqlConnection conn = new MySqlConnection();
        //private ApplicationDbContext ds = new ApplicationDbContext();
        //public IMapper mapper = AutoMapperConfig.RegisterMappings();
        //temp ChangeRequest
        #region ChangeRequest
        public List<Change_Request_Index> GetAllChangeRequest_Index()
        {
            //directly using sql
            conn.ConnectionString = mysqlconnection;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            //ACTIVE = 1 : ACTIVE ACTIVE = 0 : INACTIVE
            string sqlcmd = @"SELECT cr.*,r.regionName AS Region_Name,c.Category_Name AS Category_Name,u.Name AS UserName,p.Picture_Path AS Path FROM change_request cr LEFT JOIN region r ON (r.regionID = cr.Region_Id) 
  LEFT JOIN category c ON (c.Category_Id = cr.Category_Id) LEFT JOIN user u ON(u.User_Id = cr.Update_User_Id) LEFT JOIN picture p ON(p.Picture_Id = cr.Picture_Id)
WHERE cr.Active = 1 ORDER BY cr.Last_Update_Date_Time";
            MySqlCommand cmd = new MySqlCommand(sqlcmd, conn);
            List<Change_Request_Index> c = new List<Change_Request_Index>();
            using (MySqlDataReader rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    Change_Request_Index ci = new Change_Request_Index();
                    ci.Change_Request_Id = Convert.ToInt32(rd["Change_Request_Id"]);

                    ci.Region_Id = Convert.ToInt32(rd["Region_Id"]);
                    ci.regionName = rd["Region_Name"].ToString();
                    ci.categoryName = rd["Category_Name"].ToString();
                    ci.Category_Id = Convert.ToInt32(rd["Category_Id"]);
                    ci.Content_Id = Convert.ToInt32(rd["Content_Id"]);
                    ci.Content_Name = rd["Content_Name"].ToString();
                    ci.year = Convert.ToInt32(rd["Year"]);
                    ci.userid = Convert.ToInt32(rd["Update_User_Id"]);
                    ci.usename = rd["UserName"].ToString();
                    ci.Lastupdatetime = Convert.ToDateTime(rd["Last_Update_Date_Time"]);
                    ci.picture_id = Convert.ToInt32(rd["Picture_Id"]);
                    ci.picname = rd["Path"].ToString();
                    
                    c.Add(ci);
                }
            }
            conn.Close();
            return c;
            //var allobj = ds.ChangeRequests.orderby(i => i.Change_Request_Id);
            //return (allobj == null) ? null : mapper.Map<IEnumerable<ChangeRequestBase>>(o);
        }
        public List<Change_Request_Index> GetAllChangeRequest_Index_with_filter(int rid, int cid, int yearcode)
        {
            /*              
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
             */
            conn.ConnectionString = mysqlconnection;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            //ACTIVE = 1 : ACTIVE ACTIVE = 0 : INACTIVE
            string sqlcmd = @"SELECT cr.*,r.regionName AS Region_Name,c.Category_Name AS Category_Name,u.Name AS UserName,p.Picture_Path AS Path FROM change_request cr LEFT JOIN region r ON (r.regionID = cr.Region_Id) 
  LEFT JOIN category c ON (c.Category_Id = cr.Category_Id) LEFT JOIN user u ON(u.User_Id = cr.Update_User_Id) LEFT JOIN picture p ON(p.Picture_Id = cr.Picture_Id)
WHERE cr.Active = 1";// ORDER BY cr.Last_Update_Date_Time";
            if(rid != 999)
            {
                sqlcmd = sqlcmd + @" AND cr.Region_Id = @rid ";
            }
            if(cid != 999)
            {
                sqlcmd = sqlcmd + @" AND cr.Category_Id = @cid ";
            }
            if(yearcode != 11)
            {
                if(yearcode == 1)
                {
                    sqlcmd = sqlcmd + @" AND cr.year >= 1990 ";
                }else if(yearcode == 2)
                {
                    sqlcmd = sqlcmd + @" AND cr.year >= 1914 AND cr.year < 1990 ";
                }
                else if (yearcode == 3)
                {
                    sqlcmd = sqlcmd + @" AND cr.year >= 1837 AND cr.year < 1914 ";
                }
                else if (yearcode == 4)
                {
                    sqlcmd = sqlcmd + @" AND cr.year >= 1776 AND cr.year < 1837 ";
                }
                else if (yearcode == 5)
                {
                    sqlcmd = sqlcmd + @" AND cr.year >= 1688 AND cr.year < 1776 ";
                }
                else if (yearcode == 6)
                {
                    sqlcmd = sqlcmd + @" AND cr.year >= 1453 AND cr.year < 1688 ";
                }
                else if (yearcode == 7)
                {
                    sqlcmd = sqlcmd + @" AND cr.year >= 962 AND cr.year < 1453 ";
                }
                else if (yearcode == 8)
                {
                    sqlcmd = sqlcmd + @" AND cr.year >= 476 AND cr.year < 962 ";
                }
                else if (yearcode == 9)
                {
                    sqlcmd = sqlcmd + @" AND cr.year >= 0 AND cr.year < 476 ";
                }
                else if (yearcode == 10)
                {
                    sqlcmd = sqlcmd + @" AND cr.year < 0 ";
                }

            }
            sqlcmd = sqlcmd + @" ORDER BY cr.Year, c.Category_Name,r.regionName";
            List<Change_Request_Index> c = new List<Change_Request_Index>();
            using (MySqlCommand cmd = new MySqlCommand(sqlcmd, conn)) {
                if (rid != 999)
                {
                    cmd.Parameters.AddWithValue("@rid", rid);
                }
                if (cid != 999)
                {
                    cmd.Parameters.AddWithValue("@cid", cid);
                }
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {

                    while (rd.Read())
                    {
                        Change_Request_Index ci = new Change_Request_Index();
                        ci.Change_Request_Id = Convert.ToInt32(rd["Change_Request_Id"]);

                        ci.Region_Id = Convert.ToInt32(rd["Region_Id"]);
                        ci.regionName = rd["Region_Name"].ToString();
                        ci.categoryName = rd["Category_Name"].ToString();
                        ci.Category_Id = Convert.ToInt32(rd["Category_Id"]);
                        ci.Content_Id = Convert.ToInt32(rd["Content_Id"]);
                        ci.Content_Name = rd["Content_Name"].ToString();
                        ci.year = Convert.ToInt32(rd["Year"]);
                        ci.userid = Convert.ToInt32(rd["Update_User_Id"]);
                        ci.usename = rd["UserName"].ToString();
                        ci.Lastupdatetime = Convert.ToDateTime(rd["Last_Update_Date_Time"]);
                        ci.picture_id = Convert.ToInt32(rd["Picture_Id"]);
                        ci.picname = rd["Path"].ToString();

                        c.Add(ci);
                    }
                }
            };
            conn.Close();
            return c;

        }

        public Change_Request GetOneChangeRequest(int crid)
        {
            conn.ConnectionString = mysqlconnection;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            string sqlcmd = @"SELECT cr.*,r.regionName AS Region_Name,c.Category_Name AS Category_Name,u.Name AS UserName,p.Picture_Path AS Path FROM change_request cr LEFT JOIN region r ON (r.regionID = cr.Region_Id) 
  LEFT JOIN category c ON (c.Category_Id = cr.Category_Id) LEFT JOIN user u ON(u.User_Id = cr.Update_User_Id) LEFT JOIN picture p ON(p.Picture_Id = cr.Picture_Id) WHERE Change_Request_Id = @change_id ";
            Change_Request ci = new Change_Request();
            using (var cmd = new MySqlCommand(sqlcmd, conn))
            {
                cmd.Parameters.AddWithValue("@change_id", crid);
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {

                        ci.Change_Request_Id = Convert.ToInt32(rd["Change_Request_Id"]);
                        // not sure what happened, testing
                        ci.Region_Id = (int)rd["Region_Id"];
                        ci.regionName = rd["Region_Name"].ToString();
                        ci.Category_Id = Convert.ToInt32(rd["Category_Id"]);
                        ci.categoryName = rd["Category_Name"].ToString();
                        ci.Content_Id = Convert.ToInt32(rd["Content_Id"]);
                        ci.Content_Name = rd["Content_Name"].ToString();
                        ci.year = Convert.ToInt32(rd["Year"]);
                        ci.userid = Convert.ToInt32(rd["Update_User_Id"]);
                        ci.Lastupdatetime = Convert.ToDateTime(rd["Last_Update_Date_Time"]);
                        ci.video_key = rd["Video_Key"].ToString();
                        ci.wiki_key = rd["Wiki_Key"].ToString();
                        ci.detail = rd["Detail"].ToString();
                        ci.picture_id = Convert.ToInt32(rd["Picture_Id"]);
                        ci.reason = (rd["Reason"]).ToString();
                        ci.picname = rd["Path"].ToString();

                    }
                }
            }
            conn.Close();
            return ci;
        }

        //maybe add a Active and Reason column for cr?
        public int AddOneChangeRequest(Change_Request newobj)
        {
            int result = 0;
            conn.ConnectionString = mysqlconnection;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            var sqlcmd = @"INSERT INTO change_request (Region_Id,Category_Id,Content_Id,
                            Content_Name,Year,Update_User_Id,Last_Update_Date_Time,Video_Key,
                            Wiki_Key,Detail,Picture_Id,Reason,Request_Type,Active 
                            )VALUE(@regionid,@categoryid,@contentid,@contentname,@year,
                            @userid,@updatetime,@video,@wiki,@detail,@pictureid,@reason,@type,@active)";
            using (var cmd = new MySqlCommand(sqlcmd, conn))
            {
                //cmd.Parameters.AddWithValue("@change_id", newobj.Change_Request_Id);
                cmd.Parameters.AddWithValue("@regionid", newobj.Region_Id);
                cmd.Parameters.AddWithValue("@categoryid", newobj.Category_Id);
                if (newobj.Content_Id != 0)
                {
                    // It a edit change request
                    cmd.Parameters.AddWithValue("@contentid", newobj.Content_Id);
                }
                else
                {
                    // add new 
                    cmd.Parameters.AddWithValue("@contentid", 0);
                }
                cmd.Parameters.AddWithValue("@contentname", newobj.Content_Name);
                cmd.Parameters.AddWithValue("@year", newobj.year);
                cmd.Parameters.AddWithValue("@userid", newobj.userid);
                cmd.Parameters.AddWithValue("@updatetime", DateTime.Now);
                cmd.Parameters.AddWithValue("@video", newobj.video_key);
                cmd.Parameters.AddWithValue("@wiki", newobj.wiki_key);
                cmd.Parameters.AddWithValue("@detail", newobj.detail);
                cmd.Parameters.AddWithValue("@reason", newobj.reason);
                if (newobj.Content_Id != 0)
                {
                    //edit
                    cmd.Parameters.AddWithValue("@type", 1);
                }
                else
                {
                    //add new
                    cmd.Parameters.AddWithValue("@type", 0);
                }
                cmd.Parameters.AddWithValue("@active", 1);
                cmd.Parameters.AddWithValue("@pictureid", newobj.picture_id);
                result = cmd.ExecuteNonQuery();
            }
            conn.Close();
            return result;
        }

        public int EditOneChangeRequest(Change_Request newobj)
        {
            int result = 0;
            conn.ConnectionString = mysqlconnection;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            var sqlcmd = @"UPDATE change_request SET 
                            Content_Name=@contentname,Year=@year,Last_Update_Date_Time=@updatetime,
                            Video_Key=@video,
                            Wiki_Key=@wiki,Detail=@detail,Picture_Id=@pictureid 
                            WHERE Change_Request_Id = @change_id";
            using (var cmd = new MySqlCommand(sqlcmd, conn))
            {
                //cmd.Parameters.AddWithValue("@change_id", newobj.Change_Request_Id);
                //cmd.Parameters.AddWithValue("@regionid", newobj.Region_Id);
                //cmd.Parameters.AddWithValue("@categoryid", newobj.Category_Id);
                //cmd.Parameters.AddWithValue("@contentid", newobj.Content_Id);
                //cmd.Parameters.AddWithValue("@contentname", newobj.Content_Name);
                cmd.Parameters.AddWithValue("@year", newobj.year);
                cmd.Parameters.AddWithValue("@userid", newobj.userid);
                cmd.Parameters.AddWithValue("@updatetime", DateTime.Now);
                cmd.Parameters.AddWithValue("@video", newobj.video_key);
                cmd.Parameters.AddWithValue("@wiki", newobj.wiki_key);
                cmd.Parameters.AddWithValue("@detail", newobj.detail);
                cmd.Parameters.AddWithValue("@pictureid", newobj.picture_id);
                result = cmd.ExecuteNonQuery();
            }
            conn.Close();
            return result;
        }

        public bool checkUserPromte(int crid,int uid)
        {
            bool result = false;
            var amount = 0;
            conn.ConnectionString = mysqlconnection;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            var sqlcmd = @"SELECT COUNT(*) AS amount FROM promote WHERE Change_Request_Id = @crid AND 
                        User_Id = @uid";
            using (var cmd = new MySqlCommand(sqlcmd,conn))
            {
                cmd.Parameters.AddWithValue("@crid", crid);
                cmd.Parameters.AddWithValue("@uid", uid);
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        amount = Convert.ToInt32(rd["amount"]);
                    }
                }
            }
            conn.Close();
            if(amount > 0)
            {
                result = true;
            }

            return result;
        }


        public int Promote(int crid, int uid)
        {
            int result = 0;
            conn.ConnectionString = mysqlconnection;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            var sqlcmd = @"INSERT INTO promote(Change_Request_Id,User_Id)VALUES(@crid,@uid)";
            using (var cmd = new MySqlCommand(sqlcmd, conn))
            {
                cmd.Parameters.AddWithValue("@crid", crid);
                cmd.Parameters.AddWithValue("@uid", uid);
                result = cmd.ExecuteNonQuery();
            }
            conn.Close();
            return result;
        }

        // return the amount of promotion for the specific crid
        public int checkPromote(int crid)
        {
            int result = 0;
            conn.ConnectionString = mysqlconnection;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            var sqlcmd = @"SELECT COUNT(*) AS sum FROM promote WHERE Change_Request_Id = @crid";
            using (var cmd = new MySqlCommand(sqlcmd, conn))
            {
                cmd.Parameters.AddWithValue("@crid", crid);
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        result = Convert.ToInt32(rd["sum"]);
                    }
                }
            }
            conn.Close();
            return result;
        }

        //not done 
        //insert into the content table
        public int transfer(int crid)
        {
            conn.ConnectionString = mysqlconnection;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            var promte_changeR = new Change_Request();
            var sqlcmd = @"select * from change_request where Change_Request_Id = @crid";
            using (var cmd = new MySqlCommand(sqlcmd, conn))
            {
                cmd.Parameters.AddWithValue("@crid", crid);
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {

                        promte_changeR.Change_Request_Id = Convert.ToInt32(rd["Change_Request_Id"]);
                        // not sure what happened, testing
                        promte_changeR.Region_Id = (int)rd["Region_Id"];
                        promte_changeR.Category_Id = Convert.ToInt32(rd["Category"]);
                        promte_changeR.Content_Id = Convert.ToInt32(rd["Content_Id"]);
                        promte_changeR.Content_Name = rd["Content_Name"].ToString();
                        promte_changeR.year = Convert.ToInt32(rd["Year"]);
                        promte_changeR.userid = Convert.ToInt32(rd["Update_User_Id"]);
                        promte_changeR.Lastupdatetime = Convert.ToDateTime(rd["Last_Update_Date_Time"]);
                        promte_changeR.video_key = rd["Video_Key"].ToString();
                        promte_changeR.wiki_key = rd["Wiki_Key"].ToString();
                        promte_changeR.detail = rd["Detail"].ToString();
                        promte_changeR.picture_id = Convert.ToInt32(rd["Picture_Id"]);
                        promte_changeR.request_type = Convert.ToInt32(rd["Request_Type"]);
                    }
                }
            }

            var result = 0;

            if (promte_changeR.request_type == 1)
            {
                //edit
                result = EditContent(promte_changeR);
            }
            else
            {
                //add new
                result = AddNewContent(promte_changeR);
            }
            RequestDeactivate(crid);

            return result;
        }

        public int AddNewContent(Change_Request newobj)
        {
            var result = 0;
            conn.ConnectionString = mysqlconnection;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            //MYSQL database set PK content_id auto increment 
            var sqlcmd = @"INSERT INTO content (Region_Id,Category_Id,
                            Content_Name,Year,Update_User_Id,Last_Update_Date_Time,Video_Key,
                            Wiki_Key,Detail,Picture_Id ) VALUES (@regionid,@categoryid,@contentid,@contentname,@year,
                            @userid,@updatetime,@video,@wiki,@detail,@pictureid)";
            using (var cmd = new MySqlCommand(sqlcmd, conn))
            {
                cmd.Parameters.AddWithValue("@change_id", newobj.Change_Request_Id);
                cmd.Parameters.AddWithValue("@regionid", newobj.Region_Id);
                cmd.Parameters.AddWithValue("@categoryid", newobj.Category_Id);
                cmd.Parameters.AddWithValue("@contentname", newobj.Content_Name);
                cmd.Parameters.AddWithValue("@year", newobj.year);
                cmd.Parameters.AddWithValue("@userid", newobj.userid);
                cmd.Parameters.AddWithValue("@updatetime", DateTime.Now);
                cmd.Parameters.AddWithValue("@video", newobj.video_key);
                cmd.Parameters.AddWithValue("@wiki", newobj.wiki_key);
                cmd.Parameters.AddWithValue("@detail", newobj.detail);
                cmd.Parameters.AddWithValue("@pictureid", newobj.picture_id);
                result = cmd.ExecuteNonQuery();
            }
            conn.Close();
            return result;
        }

        public int EditContent(Change_Request newobj)
        {
            var result = 0;
            conn.ConnectionString = mysqlconnection;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            //MYSQL database set PK content_id auto increment 
            var sqlcmd = @"UPDATE content SET Content_Name = @contentname,Year = @year,
                            Update_User_Id = @userid,
                        Last_Update_Date_Time = @updatetime,Video_Key = @video,
                            Wiki_Key = @wiki,Detail = @detail,Picture_Id = @pictureid 
                            WHERE Region_Id = @regionid AND Category_Id = @categoryid 
                            AND Content_Id = @contentid";
            using (var cmd = new MySqlCommand(sqlcmd, conn))
            {
                cmd.Parameters.AddWithValue("@contentid", newobj.Content_Id);
                cmd.Parameters.AddWithValue("@regionid", newobj.Region_Id);
                cmd.Parameters.AddWithValue("@categoryid", newobj.Category_Id);
                cmd.Parameters.AddWithValue("@contentname", newobj.Content_Name);
                cmd.Parameters.AddWithValue("@year", newobj.year);
                cmd.Parameters.AddWithValue("@userid", newobj.userid);
                cmd.Parameters.AddWithValue("@updatetime", DateTime.Now);
                cmd.Parameters.AddWithValue("@video", newobj.video_key);
                cmd.Parameters.AddWithValue("@wiki", newobj.wiki_key);
                cmd.Parameters.AddWithValue("@detail", newobj.detail);
                cmd.Parameters.AddWithValue("@pictureid", newobj.picture_id);
                result = cmd.ExecuteNonQuery();
            }
            conn.Close();
            return result;
        }

        

        public int RequestDeactivate(int crid)
        {
            var result = 0;
            conn.ConnectionString = mysqlconnection;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            var sqlcmd = @"UPDATE change_request SET Active = 0 WHERE Change_Request_Id = @crid";
            using (var cmd = new MySqlCommand(sqlcmd, conn))
            {
                cmd.Parameters.AddWithValue("@crid", crid);
                result = cmd.ExecuteNonQuery();
            }
            conn.Close();

            return result;
        }

        public List<KeyValuePair<string,string>> GetRegion()
        {
            var regions = new List<KeyValuePair<string, string>>();
            conn.ConnectionString = mysqlconnection;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            var sqlcmd = @"SELECT * FROM region ORDER BY regionName";
            using (var cmd = new MySqlCommand(sqlcmd, conn))
            {
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        regions.Add(new KeyValuePair<string, string> (rd["regionID"].ToString(), rd["regionName"].ToString()) );                            
                    }
                }
            }
            conn.Close();
            return regions;
        }

        public List<KeyValuePair<string, string>> GetCategory()
        {
            var categorys = new List<KeyValuePair<string, string>>();
            if (conn.ConnectionString != mysqlconnection) {
                conn.ConnectionString = mysqlconnection;
            };
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            var sqlcmd = @"SELECT * FROM category ORDER BY Category_Id";
            using (var cmd = new MySqlCommand(sqlcmd, conn))
            {
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        categorys.Add(new KeyValuePair<string, string>( rd["Category_Id"].ToString(), rd["Category_Name"].ToString()));
                    }
                }
            }
            conn.Close();
            return categorys;
        }

        public int SavePicturePath(string filename,int userid)
        {
            var picId = 0;
            conn.ConnectionString = mysqlconnection;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            var sqlgetid = @"SELECT MAX(Picture_Id) AS currId FROM picture";
            var sqlcmd = @"INSERT INTO picture VALUES(@picid,@path,@userid,@updatetime)";
            using (var cmd = new MySqlCommand(sqlgetid,conn))
            {
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        picId = Convert.ToInt32(rd["currId"]);
                    }
                }
            }
            picId = picId + 1;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            using (var cmd = new MySqlCommand(sqlcmd, conn))
            {
                cmd.Parameters.AddWithValue("@picid", picId);
                cmd.Parameters.AddWithValue("@path", filename);
                cmd.Parameters.AddWithValue("@userid", userid);
                cmd.Parameters.AddWithValue("@updatetime", DateTime.Now);
                cmd.ExecuteNonQuery();
            }
            conn.Close();
            return picId;
        }

        #endregion

        #region Account

        public Account GetOneAccount(int userid)
        {
            var account = new Account();
            conn.ConnectionString = mysqlconnection;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            var sqlcmd = @"SELECT * FROM user WHERE User_Id = @uid";

            using (var cmd = new MySqlCommand(sqlcmd, conn))
            {
                cmd.Parameters.AddWithValue("@uid", userid);
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        account.userid = userid;
                        account.email = rd["Email"].ToString();
                        account.username = rd["Name"].ToString();
                        account.password = rd["Password"].ToString();

                    }
                }
            }


            return account;
        }
        
        public void EditAccount(Account newobj)
        {
 
            conn.ConnectionString = mysqlconnection;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
        }


        #endregion

        #region Category
        public string GetRegionName(int rid)
        {
            var regionname = string.Empty;
            conn.ConnectionString = mysqlconnection;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            var sqlcmd = @"SELECT * FROM region WHERE regionID = @rid";
            using (var cmd = new MySqlCommand(sqlcmd, conn))
            {
                cmd.Parameters.AddWithValue("@rid", rid);
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        regionname = rd["regionName"].ToString();
                    }
                }
            }
            conn.Close();
            return regionname;
        }

        #endregion

        // GET: Manager
        public ActionResult Index()
        {
            return View();
        }

        // GET: Manager/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Manager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manager/Create
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

        // GET: Manager/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Manager/Edit/5
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

        // GET: Manager/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Manager/Delete/5
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
