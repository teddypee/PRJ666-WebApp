using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Time_Travel_Machine.Models
{
    public class Account
    {
        public int userid { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public int usertype { get; set; }
        public string password { get; set; }

    }
}