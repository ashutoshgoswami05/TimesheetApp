using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimesheetApp.Models
{
    public class DisplayProjectsViewModel
    {
        public  string Project_Name { get; set; }

        public string Client { get; set; }

        public DateTime? Deadline { get; set; }

        public string Manager_Name { get; set; }

        public bool? is_Deleted { get; set; }

    }
}