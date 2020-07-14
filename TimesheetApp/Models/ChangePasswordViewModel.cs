using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimesheetApp.Models
{
    public class ChangePasswordViewModel
    {
        
        public string Employee_Id { get; set; }

        [Required(ErrorMessage ="Please enter Current Password")]
        [Display(Name ="Current Password")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Please enter New Password")]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Please re-enter New Password")]
        [Display(Name = "Confirm Password")]
        [Compare("NewPassword",ErrorMessage ="Passwords do not match")]
        public string ConfirmPassword { get; set; }


    }
}