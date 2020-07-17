using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimesheetApp.Models
{
    public class ManageViewModel
    {
        public string Employee_Name { get; set; }

        public string Project_Name { get; set; }

        public Nullable<System.DateTime> date { get; set; }

        public Nullable<bool> Mode { get; set; }

        public Nullable<System.TimeSpan> TimeSpent { get; set; }

        public string Workdone { get; set; }

        public int? Status_Id { get; set; }

        public bool? is_Deleted { get; set; }

        public string Employee_Id { get; set; }

        public int Id { get; set; }
    }
}