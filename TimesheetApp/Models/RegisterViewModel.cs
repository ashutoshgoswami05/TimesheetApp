using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TimesheetApp.Models
{
    public class RegisterViewModel
    {

        [Required(ErrorMessage = "Please enter Employee Name ")]
        [DisplayName("Employee Name")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Please enter valid name")]


        public string Employee_Name { get; set; }

        [Required(ErrorMessage = "Please enter Phone Number ")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please enter Email Address ")]

        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "Please enter valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter Role ")]
        public int Role_Id { get; set; }



        [Required(ErrorMessage = "Please enter Employee Id ")]
        [DisplayName("Employee Id")]
        public string Employee_Id { get; set; }

        [Required(ErrorMessage = "Please confirm password")]
        [DisplayName("Confirm Password")]
        [NotMapped]

        [Compare("Password", ErrorMessage = "Passwords do not Match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please enter  password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter  status")]
        public Nullable<bool> Status { get; set; }

        [Display(Name ="Technologies")]
        [Required(ErrorMessage = "Please select  technologies")]
        public List<int> TechnologyIds { get; set; }

    }
}