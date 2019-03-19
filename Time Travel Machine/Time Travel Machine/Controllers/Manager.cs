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
        //use in out owned machine
        private const string mysqlconnection = "server=myvmlab.senecacollege.ca;port=6079;user id=student;password=Group09;database=g9;charset=utf8;";
        //publish on server use the folloing
        //private const string mysqlconnection = "server=localhost;port=3306;user id=root;password=Group09;database=g9;charset=utf8;";
        MySqlConnection conn = new MySqlConnection();

        #region ChangeRequest
        public List<Change_Request_Index> GetChangeRequest_Index()
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
                    if (rd["Year"] != DBNull.Value)
                    {
                        ci.year = Convert.ToInt32(rd["Year"]);
                    }
                    if (rd["Update_User_Id"] != DBNull.Value)
                    {
                        ci.userid = Convert.ToInt32(rd["Update_User_Id"]);
                    }
                    ci.usename = rd["UserName"].ToString();
                    if (rd["Last_Update_Date_Time"] != DBNull.Value)
                    {
                        ci.Lastupdatetime = Convert.ToDateTime(rd["Last_Update_Date_Time"]);
                    }
                    if (rd["Picture_Id"] != DBNull.Value)
                    {
                        ci.picture_id = Convert.ToInt32(rd["Picture_Id"]);
                    }
                    ci.picname = rd["Path"].ToString();
                    if (rd["Active"] != DBNull.Value)
                    {
                        ci.active = Convert.ToInt32(rd["Active"]);
                    }
                    c.Add(ci);
                }
            }
            conn.Close();
            return c;

        }
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
 ORDER BY cr.Last_Update_Date_Time";
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
                    if (rd["Year"] != DBNull.Value)
                    {
                        ci.year = Convert.ToInt32(rd["Year"]);
                    }
                    if (rd["Update_User_Id"] != DBNull.Value)
                    {
                        ci.userid = Convert.ToInt32(rd["Update_User_Id"]);
                    }
                    ci.usename = rd["UserName"].ToString();
                    if (rd["Last_Update_Date_Time"] != DBNull.Value)
                    {
                        ci.Lastupdatetime = Convert.ToDateTime(rd["Last_Update_Date_Time"]);
                    }
                    if (rd["Picture_Id"] != DBNull.Value)
                    {
                        ci.picture_id = Convert.ToInt32(rd["Picture_Id"]);
                    }
                    ci.picname = rd["Path"].ToString();
                    if(rd["Active"] != DBNull.Value)
                    {
                        ci.active = Convert.ToInt32(rd["Active"]);
                    }
                    c.Add(ci);
                }
            }
            conn.Close();
            return c;

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
                        if (rd["Year"] != DBNull.Value)
                        {
                            ci.year = Convert.ToInt32(rd["Year"]);
                        }
                        if (rd["Update_User_Id"] != DBNull.Value)
                        {
                            ci.userid = Convert.ToInt32(rd["Update_User_Id"]);
                        }
                        ci.usename = rd["UserName"].ToString();
                        if (rd["Last_Update_Date_Time"] != DBNull.Value)
                        {
                            ci.Lastupdatetime = Convert.ToDateTime(rd["Last_Update_Date_Time"]);
                        }
                        if (rd["Picture_Id"] != DBNull.Value)
                        {
                            ci.picture_id = Convert.ToInt32(rd["Picture_Id"]);
                        }
                        ci.picname = rd["Path"].ToString();
                        if (rd["Active"] != DBNull.Value)
                        {
                            ci.active = Convert.ToInt32(rd["Active"]);
                        }
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
                        if (rd["Year"] != DBNull.Value)
                        {
                            ci.year = Convert.ToInt32(rd["Year"]);
                        }
                        if (rd["Update_User_Id"] != DBNull.Value)
                        {
                            ci.userid = Convert.ToInt32(rd["Update_User_Id"]);
                        }
                        
                        ci.Lastupdatetime = Convert.ToDateTime(rd["Last_Update_Date_Time"]);
                        ci.video_key = rd["Video_Key"].ToString();
                        ci.wiki_key = rd["Wiki_Key"].ToString();
                        ci.detail = rd["Detail"].ToString();
                        if (rd["Picture_Id"] != DBNull.Value)
                        {
                            ci.picture_id = Convert.ToInt32(rd["Picture_Id"]);
                        }
                        ci.reason = (rd["Reason"]).ToString();
                        ci.picname = rd["Path"].ToString();
                        ci.extent = rd["extent"].ToString();

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
                            Wiki_Key,Detail,Picture_Id,Reason,Request_Type,Active,extent 
                            )VALUE(@regionid,@categoryid,@contentid,@contentname,@year,
                            @userid,@updatetime,@video,@wiki,@detail,@pictureid,@reason,@type,@active,@extent)";
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
                cmd.Parameters.AddWithValue("@extent", newobj.extent);
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
                        promte_changeR.extent = rd["extent"].ToString();
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
                            Wiki_Key,Detail,Picture_Id,Extent ) VALUES (@regionid,@categoryid,@contentid,@contentname,@year,
                            @userid,@updatetime,@video,@wiki,@detail,@pictureid,@extent)";
            //var sqlcmd = @"INSERT INTO content (Region_Id,Category_Id,
            //                Content_Name,Year,Update_User_Id,Last_Update_Date_Time,Video_Key,
            //                Wiki_Key,Detail,Picture_Id,Extent ) VALUES (@regionid,@categoryid,@contentid,@contentname,@year,
            //                @userid,@updatetime,@video,@wiki,@detail,@pictureid,@extent)";
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
                cmd.Parameters.AddWithValue("@detail", string.Empty);
                //cmd.Parameters.AddWithValue("@detail", newobj.detail);
                cmd.Parameters.AddWithValue("@pictureid", newobj.picture_id);
                cmd.Parameters.AddWithValue("@extent", newobj.extent);
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
                            Wiki_Key = @wiki,Detail = @detail,Picture_Id = @pictureid,Extent = @extent 
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
                cmd.Parameters.AddWithValue("@extent", newobj.extent);
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

        #region Content

        public List<ContentIndex> GetContentIndex(int rid,int cid)
        {
            //decide the length of the partial introduction
            int substringlength = 40;

            conn.ConnectionString = mysqlconnection;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            var contentList = new List<ContentIndex>();
            //active = 1 means its an active row
            var sqlcmd = @"SELECT c.* ,r.regionName AS Region_Name,ca.Category_Name AS Category_Name,u.Name AS UserName,p.Picture_Path AS Path FROM content c LEFT JOIN region r ON (r.regionID = c.Region_Id) 
  LEFT JOIN category ca ON (ca.Category_Id = c.Category_Id) LEFT JOIN user u ON(u.User_Id = c.Last_Update_User_Id) LEFT JOIN picture p ON(p.Picture_Id = c.Picture_Id) WHERE c.Active = 1 ";
            if (rid != 0) {
                sqlcmd = sqlcmd + @" AND c.Region_Id = @rid ";
            }

            if(cid != 0)
            {
                sqlcmd = sqlcmd + @" AND c.Category_Id = @cid ";
            }
            sqlcmd = sqlcmd + @" ORDER BY c.Content_Name";
            using (var cmd = new MySqlCommand(sqlcmd, conn))
            {
                if (rid != 0)
                {
                    cmd.Parameters.AddWithValue("@rid", rid);
                }
                if (cid != 0)
                {
                    cmd.Parameters.AddWithValue("@cid", cid);
                }
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        var content = new ContentIndex();
                        content.Region_Id = Convert.ToInt32(rd["Region_Id"]);
                        content.regionName = rd["Region_Name"].ToString();
                        content.Category_Id = Convert.ToInt32(rd["Category_Id"]);
                        content.categoryName = rd["Category_Name"].ToString();
                        content.Content_Id = Convert.ToInt32(rd["Content_Id"]);
                        content.Content_Name = rd["Content_Name"].ToString();
                        
                        if (rd["Detail"].ToString().Length > substringlength)
                        {
                            content.detail = rd["Detail"].ToString().Substring(0, substringlength);
                        }
                        else
                        {
                            content.detail = rd["Detail"].ToString();
                        }
                        if (rd["Year"] != DBNull.Value)
                        {
                            content.year = Convert.ToInt32(rd["Year"]);
                        }
                        if (rd["Picture_Id"] != DBNull.Value)
                        {
                            content.picture_id = Convert.ToInt32(rd["Picture_Id"]);
                        }
                        if (rd["Path"] != DBNull.Value)
                        {
                            content.picname = rd["Path"].ToString();
                        }
                        if (rd["Last_Update_User_Id"] != DBNull.Value)
                        {
                            content.userid = Convert.ToInt32(rd["Last_Update_User_Id"]);
                        }
                        if (rd["UserName"] != DBNull.Value)
                        {

                            content.usename = rd["UserName"].ToString();
                        }
                        if (rd["Last_Update_Date_Time"] != DBNull.Value)
                        {
                            content.Lastupdatetime = Convert.ToDateTime(rd["Last_Update_Date_Time"]);
                        }
                        if(rd["Active"] != DBNull.Value)
                        {
                            content.Active = Convert.ToInt32(rd["Active"]);
                        }


                        contentList.Add(content);
                    }
                }
            }
            conn.Close();


            return contentList;
        }
        public List<ContentIndex> GetAllContentIndex(int rid, int cid)
        {
            //decide the length of the partial introduction
            int substringlength = 40;

            conn.ConnectionString = mysqlconnection;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            var contentList = new List<ContentIndex>();
            //active = 1 means its an active row
            var sqlcmd = @"SELECT c.* ,r.regionName AS Region_Name,ca.Category_Name AS Category_Name,u.Name AS UserName,p.Picture_Path AS Path FROM content c LEFT JOIN region r ON (r.regionID = c.Region_Id) 
  LEFT JOIN category ca ON (ca.Category_Id = c.Category_Id) LEFT JOIN user u ON(u.User_Id = c.Last_Update_User_Id) LEFT JOIN picture p ON(p.Picture_Id = c.Picture_Id) ";
            if(rid != 0 || cid != 0)
            {
                sqlcmd = sqlcmd + @" WHERE ";
            }

            if (rid != 0)
            {
                sqlcmd = sqlcmd + @" c.Region_Id = @rid ";
            }

            if(rid != 0 && cid != 0)
            {
                sqlcmd = sqlcmd + @" AND ";
            }

            if (cid != 0)
            {
                sqlcmd = sqlcmd + @" c.Category_Id = @cid ";
            }
            sqlcmd = sqlcmd + @" ORDER BY c.Content_Name";
            using (var cmd = new MySqlCommand(sqlcmd, conn))
            {
                if (rid != 0)
                {
                    cmd.Parameters.AddWithValue("@rid", rid);
                }
                if (cid != 0)
                {
                    cmd.Parameters.AddWithValue("@cid", cid);
                }
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        var content = new ContentIndex();
                        content.Region_Id = Convert.ToInt32(rd["Region_Id"]);
                        content.regionName = rd["Region_Name"].ToString();
                        content.Category_Id = Convert.ToInt32(rd["Category_Id"]);
                        content.categoryName = rd["Category_Name"].ToString();
                        content.Content_Id = Convert.ToInt32(rd["Content_Id"]);
                        content.Content_Name = rd["Content_Name"].ToString();

                        if (rd["Detail"].ToString().Length > substringlength)
                        {
                            content.detail = rd["Detail"].ToString().Substring(0, substringlength);
                        }
                        else
                        {
                            content.detail = rd["Detail"].ToString();
                        }
                        if (rd["Year"] != DBNull.Value)
                        {
                            content.year = Convert.ToInt32(rd["Year"]);
                        }
                        if (rd["Picture_Id"] != DBNull.Value)
                        {
                            content.picture_id = Convert.ToInt32(rd["Picture_Id"]);
                        }
                        if (rd["Path"] != DBNull.Value)
                        {
                            content.picname = rd["Path"].ToString();
                        }
                        if (rd["Last_Update_User_Id"] != DBNull.Value)
                        {
                            content.userid = Convert.ToInt32(rd["Last_Update_User_Id"]);
                        }
                        if (rd["UserName"] != DBNull.Value)
                        {

                            content.usename = rd["UserName"].ToString();
                        }
                        if (rd["Last_Update_Date_Time"] != DBNull.Value)
                        {
                            content.Lastupdatetime = Convert.ToDateTime(rd["Last_Update_Date_Time"]);
                        }
                        if (rd["Active"] != DBNull.Value)
                        {
                            content.Active = Convert.ToInt32(rd["Active"]);
                        }


                        contentList.Add(content);
                    }
                }
            }
            conn.Close();


            return contentList;
        }
        public Content GetOneHisContent(int conId)
        {
            var content = new Content();

            conn.ConnectionString = mysqlconnection;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            var sqlcmd = @"SELECT c.* ,r.regionName AS Region_Name,ca.Category_Name AS Category_Name,u.Name AS UserName,p.Picture_Path AS Path FROM content c LEFT JOIN region r ON (r.regionID = c.Region_Id) 
  LEFT JOIN category ca ON (ca.Category_Id = c.Category_Id) LEFT JOIN user u ON(u.User_Id = c.Last_Update_User_Id) LEFT JOIN picture p ON(p.Picture_Id = c.Picture_Id) WHERE c.Content_Id = @conId ";
            

            using (var cmd = new MySqlCommand(sqlcmd, conn))
            {
                cmd.Parameters.AddWithValue("@conId", conId);
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        
                        content.Region_Id = Convert.ToInt32(rd["Region_Id"]);
                        content.regionName = rd["Region_Name"].ToString();
                        content.Category_Id = Convert.ToInt32(rd["Category_Id"]);
                        content.categoryName = rd["Category_Name"].ToString();
                        content.Content_Id = Convert.ToInt32(rd["Content_Id"]);
                        content.Content_Name = rd["Content_Name"].ToString();
                        content.detail = rd["Detail"].ToString();
                        if (rd["Year"] != DBNull.Value)
                        {
                            content.year = Convert.ToInt32(rd["Year"]);
                        }
                        if (rd["Picture_Id"] != DBNull.Value)
                        {
                            content.picture_id = Convert.ToInt32(rd["Picture_Id"]);
                        }
                        if (rd["Path"] != DBNull.Value)
                        {
                            content.picname = rd["Path"].ToString();
                        }
                        if (rd["Last_Update_User_Id"] != DBNull.Value)
                        {
                            content.userid = Convert.ToInt32(rd["Last_Update_User_Id"]);
                        }
                        if (rd["UserName"] != DBNull.Value)
                        {

                            content.usename = rd["UserName"].ToString();
                        }
                        if (rd["Last_Update_Date_Time"] != DBNull.Value)
                        {
                            content.Lastupdatetime = Convert.ToDateTime(rd["Last_Update_Date_Time"]);
                        }
                        content.video = rd["Video_Key"].ToString();
                        content.wiki = rd["Wiki_Key"].ToString();
                        content.extent = rd["extent"].ToString();
                    }
                }
            }
            conn.Close();
            return content;
        }


        #endregion

        #region Filter

        public int GetContinentId(string selectedregionId)
        {
            var continentId = 0;
            conn.ConnectionString = mysqlconnection;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            var sqlcmd = @"SELECT Continent_Id FROM region WHERE regionID = @rid";
            using (var cmd = new MySqlCommand(sqlcmd, conn))
            {
                cmd.Parameters.AddWithValue("@rid", selectedregionId);
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        continentId = Convert.ToInt32(rd["Continent_Id"]);


                    }
                }
            }
            conn.Close();
            return continentId;

        }

        public List<YearDDL> GetYearDDL(int continentId)
        {
            var years = new List<YearDDL>();
            conn.ConnectionString = mysqlconnection;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            var sqlcmd = @"SELECT y.* FROM year y WHERE y.Continent_Id = @contId  ORDER BY y.Year_Id";
            using (var cmd = new MySqlCommand(sqlcmd, conn))
            {
                cmd.Parameters.AddWithValue("@contId", continentId);
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        var year = new YearDDL();
                        year.Year = Convert.ToInt32(rd["Continent_Id"]);
                        year.description = rd["description"].ToString();
                        year.YearId = rd["Year_Id"].ToString();
                        year.continentId = continentId.ToString();
                        //yearddlvalue - last digit = orginal year_id, in order to be used in Filters
                        year.yearddlvalue = year.YearId + year.continentId;
                        year.active = Convert.ToInt32(rd["Active"]);
                        years.Add(year);
                    }
                }
            }
            conn.Close();

            return years;
        }
        public List<YearDDL> GetYearDDL(string continentId,int year)
        {
            var years = new List<YearDDL>();
            conn.ConnectionString = mysqlconnection;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            var sqlcmd = @"SELECT y.* FROM year y WHERE (y.Continent_Id = @contId AND y.year > @year ) OR (y.Continent_Id = @contId AND y.Year_Id = 999 )   ORDER BY y.Year_Id";
            using (var cmd = new MySqlCommand(sqlcmd, conn))
            {
                cmd.Parameters.AddWithValue("@contId", continentId);
                cmd.Parameters.AddWithValue("@year", year);
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        var y = new YearDDL();
                        y.Year = Convert.ToInt32(rd["Continent_Id"]);
                        y.description = rd["description"].ToString();
                        y.YearId = rd["Year_Id"].ToString();
                        y.continentId = continentId.ToString();
                        //yearddlvalue - last digit(continent_Id) = orginal year_id, in order to be used in Filters
                        y.yearddlvalue = y.YearId + y.continentId;
                        y.active = Convert.ToInt32(rd["Active"]);
                        years.Add(y);
                    }
                }
            }
            conn.Close();

            return years;
        }
        public int GetYearByYearIdandContinentId(string yearid,string continentid)
        {
            conn.ConnectionString = mysqlconnection;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            int year = 0;
            var sqlcmd = @"SELECT y.year FROM year y WHERE y.Year_Id = @yId and y.Continent_Id = @contId";
            using (var cmd = new MySqlCommand(sqlcmd, conn))
            {
                cmd.Parameters.AddWithValue("@contId", continentid);
                cmd.Parameters.AddWithValue("@yId", yearid);
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {

                        year= Convert.ToInt32( rd["year"]);
                    }
                }
            }
            conn.Close();
            return year;
        }
        #endregion

        #region Admin

        public List<KeyValuePair<string,string>> GetUserType()
        {
            conn.ConnectionString = mysqlconnection;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            var usertypes = new List<KeyValuePair<string, string>>();
            var sqlcmd = @"SELECT * FROM user_type"; 
            using (var cmd = new MySqlCommand(sqlcmd, conn))
            {
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        usertypes.Add(new KeyValuePair<string, string>(rd["User_Type_Id"].ToString(),rd["Description"].ToString()));
                    }
                }
            }
            conn.Close();
            return usertypes;
        }

        public List<HisUser> GetAllUsers()
        {
            var users = new List<HisUser>();
            conn.ConnectionString = mysqlconnection;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            var sqlcmd = @"SELECT u.*,t.Description AS descri FROM user u JOIN user_type t ON (t.User_Type_Id = u.User_Type_Id)";
            using (var cmd = new MySqlCommand(sqlcmd, conn))
            {
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        var user = new HisUser();
                        user.userid = Convert.ToInt32(rd["User_Id"]);
                        user.username = rd["Name"].ToString();
                        user.email = rd["Email"].ToString();
                        user.usertypeDescription = rd["descri"].ToString();
                        user.usertype = Convert.ToInt32(rd["User_Type_Id"]);
                        users.Add(user);
                    }
                }
            }
            conn.Close();


            return users;
        }


        public int ChangeContentStatus(string conId)
        {
            var result = 0;
            int active = 0;
            conn.ConnectionString = mysqlconnection;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            var sqlcmd = @"SELECT Active FROM content WHERE Content_Id = @conId";
            using (var cmd = new MySqlCommand(sqlcmd, conn))
            {
                cmd.Parameters.AddWithValue("@conId", conId);
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        active = Convert.ToInt32(rd["Active"]);
                    }
                }
            }
            string sqlcmd2;
            if(active == 0)//activate
            {
                sqlcmd2 = @"UPDATE content SET Active = 1 WHERE Content_id = @conId";
            }else //deactivate
            {
                sqlcmd2 = @"UPDATE content SET Active = 0 WHERE Content_id = @conId";
            }
            using (var cmd = new MySqlCommand(sqlcmd2,conn))
            {
                cmd.Parameters.AddWithValue("@conId", conId);
                result = cmd.ExecuteNonQuery();
            }


            return result;
        }

        public int ChangeCReqStatus(string crId)
        {
            var result = 0;
            int active = 0;
            conn.ConnectionString = mysqlconnection;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            var sqlcmd = @"SELECT Active FROM change_request WHERE Change_Request_Id = @crId";
            using (var cmd = new MySqlCommand(sqlcmd, conn))
            {
                cmd.Parameters.AddWithValue("@crId", crId);
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        active = Convert.ToInt32(rd["Active"]);
                    }
                }
            }
            string sqlcmd2;
            if (active == 0)//activate
            {
                sqlcmd2 = @"UPDATE change_request SET Active = 1 WHERE Change_Request_Id = @crId";
            }
            else //deactivate
            {
                sqlcmd2 = @"UPDATE change_request SET Active = 0 WHERE Change_Request_Id = @crId";
            }
            using (var cmd = new MySqlCommand(sqlcmd2, conn))
            {
                cmd.Parameters.AddWithValue("@crId", crId);
                result = cmd.ExecuteNonQuery();
            }


            return result;
        }
        public int ChangeUsertype(string userid)
        {
            var result = 0;
            int isAdmin = 0;
            conn.ConnectionString = mysqlconnection;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            var sqlcmd = @"SELECT User_Type_Id FROM user WHERE User_Id = @userid";
            using (var cmd = new MySqlCommand(sqlcmd, conn))
            {
                cmd.Parameters.AddWithValue("@userid", userid);
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        isAdmin = Convert.ToInt32(rd["User_Type_Id"]);
                    }
                }
            }
            string sqlcmd2;
            if (isAdmin == 1)//1 = regular user
            {
                sqlcmd2 = @"UPDATE user SET User_Type_Id = 2 WHERE User_Id = @userid";
            }
            else //2 = admin
            {
                sqlcmd2 = @"UPDATE user SET User_Type_Id = 1 WHERE User_Id = @userid";
            }
            using (var cmd = new MySqlCommand(sqlcmd2, conn))
            {
                cmd.Parameters.AddWithValue("@userid", userid);
                result = cmd.ExecuteNonQuery();
            }


            return result;
        }

        public List<YearOptions> GetAllYearOption()
        {
            conn.ConnectionString = mysqlconnection;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            var sqlcmd = @"SELECT * FROM year";
            var years = new List<YearOptions>();
            using (var cmd = new MySqlCommand(sqlcmd, conn))
            {

                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        var y = new YearOptions();
                        y.continentid = Convert.ToInt32(rd["Continent_Id"]);
                        y.yearid = Convert.ToInt32(rd["Year_Id"]);
                        y.year = Convert.ToInt32(rd["year"]);
                        y.description = rd["description"].ToString();
                        y.active = Convert.ToInt32(rd["Active"]); //1= active , 0 = inactive
                        years.Add(y);
                    }
                }
            }

            return years;
        }

        public int ChangeYearoptionStatus(string yearid,string continent_id)
        {
            var result = 0;
            int isActive = 0;
            conn.ConnectionString = mysqlconnection;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            var sqlcmd = @"SELECT * FROM year WHERE Year_Id = @yearid AND Continent_Id = @conid";
            using (var cmd = new MySqlCommand(sqlcmd, conn))
            {
                cmd.Parameters.AddWithValue("@yearid", yearid);
                cmd.Parameters.AddWithValue("@conid", continent_id);
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        isActive = Convert.ToInt32(rd["Active"]);
                    }
                }
            }
            string sqlcmd2;
            if (isActive == 1)//1 = active
            {
                sqlcmd2 = @"UPDATE year SET Active = 0 WHERE Year_Id = @yearid AND Continent_Id = @conid";
            }
            else //0 = inactive
            {
                sqlcmd2 = @"UPDATE year SET Active = 1 WHERE Year_Id = @yearid AND Continent_Id = @conid";
            }
            using (var cmd = new MySqlCommand(sqlcmd2, conn))
            {
                cmd.Parameters.AddWithValue("@yearid", yearid);
                cmd.Parameters.AddWithValue("@conid", continent_id);
                result = cmd.ExecuteNonQuery();
            }


            return result;
        }

        public List<Backup_Content> GetAllBackup()
        {
            int substringlength = 40;

            conn.ConnectionString = mysqlconnection;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            var contentList = new List<Backup_Content>();
            //active = 1 means its an active row
            var sqlcmd = @"SELECT c.* ,r.regionName AS Region_Name,ca.Category_Name AS Category_Name,u.Name AS UserName,p.Picture_Path AS Path FROM back_up c LEFT JOIN region r ON (r.regionID = c.Region_Id) 
  LEFT JOIN category ca ON (ca.Category_Id = c.Category_Id) LEFT JOIN user u ON(u.User_Id = c.Last_Update_User_Id) LEFT JOIN picture p ON(p.Picture_Id = c.Picture_Id)";


            using (var cmd = new MySqlCommand(sqlcmd, conn))
            {

                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        var content = new Backup_Content();
                        content.Region_Id = Convert.ToInt32(rd["Region_Id"]);
                        content.regionName = rd["Region_Name"].ToString();
                        content.Category_Id = Convert.ToInt32(rd["Category_Id"]);
                        content.categoryName = rd["Category_Name"].ToString();
                        content.Content_Id = Convert.ToInt32(rd["Content_Id"]);
                        content.Content_Name = rd["Content_Name"].ToString();

                        if (rd["Detail"].ToString().Length > substringlength)
                        {
                            content.detail = rd["Detail"].ToString().Substring(0, substringlength);
                        }
                        else
                        {
                            content.detail = rd["Detail"].ToString();
                        }
                        if (rd["Year"] != DBNull.Value)
                        {
                            content.year = Convert.ToInt32(rd["Year"]);
                        }
                        if (rd["Picture_Id"] != DBNull.Value)
                        {
                            content.picture_id = Convert.ToInt32(rd["Picture_Id"]);
                        }
                        if (rd["Path"] != DBNull.Value)
                        {
                            content.picname = rd["Path"].ToString();
                        }
                        if (rd["Last_Update_User_Id"] != DBNull.Value)
                        {
                            content.userid = Convert.ToInt32(rd["Last_Update_User_Id"]);
                        }
                        if (rd["UserName"] != DBNull.Value)
                        {

                            content.usename = rd["UserName"].ToString();
                        }
                        if (rd["Last_Update_Date_Time"] != DBNull.Value)
                        {
                            content.Lastupdatetime = Convert.ToDateTime(rd["Last_Update_Date_Time"]);
                        }
                        if (rd["Active"] != DBNull.Value)
                        {
                            content.Active = Convert.ToInt32(rd["Active"]);
                        }


                        contentList.Add(content);
                    }
                }
            }
            conn.Close();


            return contentList;
        }

        public Backup_Content GetOneBackup(int contentid)
        {
            var content = new Backup_Content();

            conn.ConnectionString = mysqlconnection;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            var sqlcmd = @"SELECT c.* ,r.regionName AS Region_Name,ca.Category_Name AS Category_Name,u.Name AS UserName,p.Picture_Path AS Path FROM back_up c LEFT JOIN region r ON (r.regionID = c.Region_Id) 
  LEFT JOIN category ca ON (ca.Category_Id = c.Category_Id) LEFT JOIN user u ON(u.User_Id = c.Last_Update_User_Id) LEFT JOIN picture p ON(p.Picture_Id = c.Picture_Id) WHERE c.Content_Id = @conId ";


            using (var cmd = new MySqlCommand(sqlcmd, conn))
            {
                cmd.Parameters.AddWithValue("@conId", contentid);
                using (MySqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {

                        content.Region_Id = Convert.ToInt32(rd["Region_Id"]);
                        content.regionName = rd["Region_Name"].ToString();
                        content.Category_Id = Convert.ToInt32(rd["Category_Id"]);
                        content.categoryName = rd["Category_Name"].ToString();
                        content.Content_Id = Convert.ToInt32(rd["Content_Id"]);
                        content.Content_Name = rd["Content_Name"].ToString();
                        content.detail = rd["Detail"].ToString();
                        if (rd["Year"] != DBNull.Value)
                        {
                            content.year = Convert.ToInt32(rd["Year"]);
                        }
                        if (rd["Picture_Id"] != DBNull.Value)
                        {
                            content.picture_id = Convert.ToInt32(rd["Picture_Id"]);
                        }
                        if (rd["Path"] != DBNull.Value)
                        {
                            content.picname = rd["Path"].ToString();
                        }
                        if (rd["Last_Update_User_Id"] != DBNull.Value)
                        {
                            content.userid = Convert.ToInt32(rd["Last_Update_User_Id"]);
                        }
                        if (rd["UserName"] != DBNull.Value)
                        {

                            content.usename = rd["UserName"].ToString();
                        }
                        if (rd["Last_Update_Date_Time"] != DBNull.Value)
                        {
                            content.Lastupdatetime = Convert.ToDateTime(rd["Last_Update_Date_Time"]);
                        }
                        content.video = rd["Video_Key"].ToString();
                        content.wiki = rd["Wiki_Key"].ToString();
                        content.extent = rd["extent"].ToString();
                    }
                }
            }
            conn.Close();
            return content;
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
