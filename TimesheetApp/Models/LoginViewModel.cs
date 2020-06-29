using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimesheetApp.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter Employee Id ")]
        [DisplayName("Employee Id")]
        public string Employee_Id { get; set; }

        [Required(ErrorMessage ="Please enter the password")]
        //[StringLength(int.MaxValue,ErrorMessage ="password length should be greater than 6",MinimumLength =6)]
        public string  Password { get; set; }
    }
}