using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimesheetApp.Models
{
    public class ProjectMembersViewModel
    {
        public string Employee_Name { get; set; }

        public bool? IsDeleted { get; set; }

        public string Project_Name { get; set; }

        public string Employee_Id { get; set; }

        public int Id { get; set; }
    }
}