using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Time_Travel_Machine.Controllers
{
    public class Region
    {
        public Region()
        {

        }

        public int regionID { get; set; }
        public string regionName { get; set; }
        public DateTime lastUpdateDate { get;  set; }
        public int lastUpdateUserID { get; set; }
    }
}