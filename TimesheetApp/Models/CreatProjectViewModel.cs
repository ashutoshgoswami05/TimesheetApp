using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimesheetApp.Models
{
    public class CreatProjectViewModel
    {
        [Required(ErrorMessage ="please select project")]
        [Display(Name = "Project Name")]


        public string Project_Name { get; set; }
        [Required(ErrorMessage = "please enter Client")]
        public string Client { get; set; }

        [Display(Name ="Project Manager")]
        [Required(ErrorMessage = "please select project manager")]
        public string Project_Manager_Id { get; set; }

        [Required(ErrorMessage = "please enter deadline")]
        public System.DateTime Deadline { get; set; }

       
    }
}