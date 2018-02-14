using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Time_Travel_Machine.Controllers
{
    public class User
    {
        public User()
        {

        }
        public int userID { get; set; }
        public string userName { get; set; }
        public string userEmail { get; set; }
        public int userTypeID { get; set; }
    }

}