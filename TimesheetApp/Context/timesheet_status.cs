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
    
    public partial class timesheet_status
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public timesheet_status()
        {
            this.Timesheets_table = new HashSet<Timesheets>();
        }
    
        public int Id { get; set; }
        public string Status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Timesheets> Timesheets_table { get; set; }
    }
}
