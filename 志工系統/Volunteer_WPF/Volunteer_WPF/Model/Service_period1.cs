//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Volunteer_WPF.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Service_period1
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Service_period1()
        {
            this.Service_period2 = new HashSet<Service_period>();
            this.Service_period21 = new HashSet<Service_period2>();
            this.Shift_schedule = new HashSet<Shift_schedule>();
            this.Sign_up_Service_period = new HashSet<Sign_up_Service_period>();
        }
    
        public int Service_period_no { get; set; }
        public string Service_period { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Service_period> Service_period2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Service_period2> Service_period21 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Shift_schedule> Shift_schedule { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sign_up_Service_period> Sign_up_Service_period { get; set; }
    }
}
