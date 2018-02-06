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
        public IEnumerable<Change_Request_Index> GetAllChangeRequest_Index()
        {
            //directly using sql
            conn.ConnectionString = mysqlconnection;
            if(conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            string sqlcmd = @"select * from change_request order by Last_Update_Date_Time";
            MySqlCommand cmd = new MySqlCommand(sqlcmd,conn);
            List<Change_Request_Index> c = new List<Change_Request_Index>();
            using (MySqlDataReader rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    Change_Request_Index ci = new Change_Request_Index();
                    ci.Change_Request_Id = Convert.ToInt32(rd["Change_Request_Id"]);
                    // not sure what happened, testing
                    ci.Region_Id = (int)rd["Region_Id"];
                    ci.Category_Id = Convert.ToInt32(rd["Category"]);
                    ci.Content_Id = Convert.ToInt32(rd["Content_Id"]);
                    ci.Content_Name = rd["Category"].ToString();
                    ci.year = Convert.ToInt32(rd["Year"]);
                    ci.userid = Convert.ToInt32(rd["Update_User_Id"]);
                    ci.Lastupdatetime = Convert.ToDateTime(rd["Last_Update_Date_Time"]);
                    ci.picture_id = Convert.ToInt32(rd["Picture_Id"]);
                    //add picture later
                    c.Add(ci);
                }
            }

            return c;
            //var allobj = ds.ChangeRequests.orderby(i => i.Change_Request_Id);
            //return (allobj == null) ? null : mapper.Map<IEnumerable<ChangeRequestBase>>(o);
        }

        public Change_Request GetOneChangeRequest(int crid)
        {
            conn.ConnectionString = mysqlconnection;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            string sqlcmd = @"select * from change_request where Change_Request_Id = @change_id ";
            Change_Request ci = new Change_Request();
            using (var cmd = new MySqlCommand(sqlcmd, conn))
            {
                cmd.Parameters.AddWithValue("@change_id",crid);
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {

                        ci.Change_Request_Id = Convert.ToInt32(rd["Change_Request_Id"]);
                        // not sure what happened, testing
                        ci.Region_Id = (int)rd["Region_Id"];
                        ci.Category_Id = Convert.ToInt32(rd["Category"]);
                        ci.Content_Id = Convert.ToInt32(rd["Content_Id"]);
                        ci.Content_Name = rd["Category"].ToString();
                        ci.year = Convert.ToInt32(rd["Year"]);
                        ci.userid = Convert.ToInt32(rd["Update_User_Id"]);
                        ci.Lastupdatetime = Convert.ToDateTime(rd["Last_Update_Date_Time"]);
                        ci.video_key = rd["Video_Key"].ToString();
                        ci.wiki_key = rd["Wiki_Key"].ToString();
                        ci.detail = rd["Detail"].ToString();
                        ci.picture_id = Convert.ToInt32(rd["Picture_Id"]);
                        //add picture later
                    }
                }
            }
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
            var sqlcmd = @"INSERT INTO change_request (Change_Request_Id,Region_Id,Category_Id,Content_Id
                            Content_Name,Year,Update_User_Id,Last_Update_Date_Time,Video_Key,
                            Wiki_Key,Detail,Picture_Id 
                            )VALUE(@change_id,@regionid,@categoryid,@contentid,@contentname,@year,
                            @userid,@updatetime,@video,@wiki,@detail,@pictureid)";
            using (var cmd = new MySqlCommand(sqlcmd, conn))
            {
                cmd.Parameters.AddWithValue("@change_id", newobj.Change_Request_Id);
                cmd.Parameters.AddWithValue("@regionid",newobj.Region_Id);
                cmd.Parameters.AddWithValue("@categoryid", newobj.Category_Id);
                cmd.Parameters.AddWithValue("@contentid", newobj.Content_Id);
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

        //not done
        public Change_Request GetChangeDetail(int crid)
        {
            var c = new Change_Request();


            return c;
        }


        public int Promote(int crid,int uid)
        {
            int result = 0;
            conn.ConnectionString = mysqlconnection;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            var sqlcmd = @"INSERT INTO PROMTE(Change_Request_Id,User_Id)VALUES(@crid,@uid)";
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
            var sqlcmd = @"SELECT COUNT(*) AS sum FROM promte WHERE Change_Request_Id = @crid";
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
            return result;
        }

        //not done 
        //insert into the content table
        public int transfer(int crid)
        {

            var result = 0;

            return result;
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
