using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimesheetApp
{
    public class Pager
    {
        public Pager(int totalitems,int? page,int pagesize=10)
        {
            var totalpages = (int)Math.Ceiling((decimal)totalitems / (decimal)pagesize);
            var currentpage = page != null ? page.Value : 1;
            var startpage = 1;
            var endpage = totalpages;
        }
    }
}