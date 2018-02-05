using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Time_Travel_Machine.Controllers
{
    public class Category
    {
        public Category()
        {

        }
        public int categoryID { get; set; }
        public string categoryName { get; set; }
        public DateTime lastUpdateDate { get; set; }
        public int updateUserID { get; set; }

    }
}