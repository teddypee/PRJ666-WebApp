using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Time_Travel_Machine.Models
{
    public class Change_Request_Index
    {
        public int Change_Request_Id { get; set; }
        public int Region_Id { get; set; }
        public string regionName { get; set; }
        public int Category_Id { get; set; }
        public string categoryName { get; set; }
        public int Content_Id { get; set; }
        public string Content_Name { get; set; }
        public int year { get; set; }
        public int userid { get; set; }
        public string usename { get; set; }
        public DateTime Lastupdatetime { get; set; }
        public int picture_id { get; set; }
        public string picname { get; set; }
    }

    public class Change_Request : Change_Request_Index
    {
        public string wiki_key { get; set; }
        public string video_key { get; set; }
        public string reason { get; set; }
        public string detail { get; set; }
        public int request_type { get; set; }
        public int active { get; set; }


    }

    public class CRAddForum
    {
        public string Content_Name { get; set; }
        public int year { get; set; }
        public string wiki_key { get; set; }
        public string video_key { get; set; }
        public string reason { get; set; }
        public string detail { get; set; }
    }
}