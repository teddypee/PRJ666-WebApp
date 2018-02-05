using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Time_Travel_Machine.Controllers
{
    public class ChangeRequest
    {
        public ChangeRequest()
        {

        }

        public int changeRequestID { get; set; }
        public int regionID { get; set; }
        public int categoryID { get; set; }
        public int contentID { get; set; }
        public string contentName { get; set; }
        public int years { get; set; }
        public DateTime lastUpdateDate { get; set; }
        public int lastUpdateUserID { get; set; }
        public string wikiKey { get; set; }
        public string ytKey { get; set; }
        public string detail { get; set; }
        public int pictureID { get; set; }



    }
}