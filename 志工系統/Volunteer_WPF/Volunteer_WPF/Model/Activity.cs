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
    
    public partial class Activity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Activity()
        {
            this.Activity1 = new HashSet<Activity1>();
            this.Activity_Schedule = new HashSet<Activity_Schedule>();
            this.Educations = new HashSet<Education>();
        }
    
        public int Activity_no { get; set; }
        public string Activity_name { get; set; }
        public int Activity_type_ID { get; set; }
        public Nullable<int> Group_no { get; set; }
        public string Undertake_unit { get; set; }
        public Nullable<int> Undertaker { get; set; }
        public string Undertake_phone { get; set; }
        public string Undertake_email { get; set; }
        public Nullable<int> Member { get; set; }
        public Nullable<int> Spare { get; set; }
        public string Place { get; set; }
        public string Summary { get; set; }
        public Nullable<int> Activity_Photo_id { get; set; }
        public Nullable<System.DateTime> Activity_startdate { get; set; }
        public Nullable<System.DateTime> Activity_enddate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Activity1> Activity1 { get; set; }
        public virtual Activity_photo Activity_photo { get; set; }
        public virtual Activity_type Activity_type { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Activity_Schedule> Activity_Schedule { get; set; }
        public virtual Service_group Service_group { get; set; }
        public virtual Volunteer_supervision Volunteer_supervision { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Education> Educations { get; set; }
    }
}
