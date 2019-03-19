using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Time_Travel_Machine.Models
{
    public class ContentIndex
    {
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
        public string detail { get; set; }
        public int Active { get; set; }
    }



    public class Content : ContentIndex
    {
        public string wiki { get; set; }
        public string video { get; set; }
        public string extent { get; set; }
    }

    public class Backup_Content : Content
    {

    }
}