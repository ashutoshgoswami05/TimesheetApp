using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimesheetApp.Context
{
    [MetadataType(typeof(TimesheetMetaData))]
    public partial class Timesheets
    {

    }

    public class TimesheetMetaData
    {
        [Required(ErrorMessage ="Please select Project Name")]
        public string Project_Name { get; set; }

       

        [Required(ErrorMessage = "Please enter timespent")]
        public Nullable<System.TimeSpan> TimeSpent { get; set; }

        [Required(ErrorMessage = "Please enter Work done")]

        public string Wokrdone { get; set; }

        [Required(ErrorMessage ="Please select Mode of work")]
        public Nullable<bool> Mode { get; set; }
        [Required(ErrorMessage = "Please select date")]
        public Nullable<System.DateTime> date { get; set; }

    }
}