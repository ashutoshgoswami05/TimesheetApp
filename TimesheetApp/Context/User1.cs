using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TimesheetApp.Context
{
    [MetadataType(typeof(UsersMetaData))]
    public partial class User
    {
    }

    public class UsersMetaData
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
        public int Employee_Id { get; set; }

      

        [Required(ErrorMessage = "Please enter  password")]     
        public string Password { get; set; }



    }
}