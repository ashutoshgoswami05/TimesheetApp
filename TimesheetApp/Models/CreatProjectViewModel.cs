using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimesheetApp.Models
{
    public class CreatProjectViewModel
    {
        [Display(Name = "Project Name")]

        public string Project_Name { get; set; }
        public string Client { get; set; }
        [Display(Name ="Project Manager")]
        public string Project_Manager_Id { get; set; }
        public System.DateTime Deadline { get; set; }

        public List<int> Technologies { get; set; }
    }
}