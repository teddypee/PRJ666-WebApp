using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Time_Travel_Machine.Controllers
{
    public class Picture
    {
        public Picture()
        {

        }
        public int pictureID { get; set; }
        public string picture { get; set; }
        public DateTime lastUpdateDate { get; set; }
        public int lastUpdateUserID { get; set; }
    }
}