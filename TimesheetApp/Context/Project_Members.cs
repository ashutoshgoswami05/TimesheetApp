//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TimesheetApp.Context
{
    using System;
    using System.Collections.Generic;
    
    public partial class Project_Members
    {
        public string Project_Name { get; set; }
        public string Member_Id { get; set; }
        public int Id { get; set; }
        public bool is_Deleted { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public Nullable<System.DateTime> Updated_Date { get; set; }
    
        public virtual Project Projects_Table { get; set; }
        public virtual User Users_table { get; set; }
    }
}
