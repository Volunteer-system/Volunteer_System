//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Volunteer_Web.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Volunteer_supervision
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Volunteer_supervision()
        {
            this.Abnormal_event = new HashSet<Abnormal_event>();
            this.Manpower_apply = new HashSet<Manpower_apply>();
            this.Activities = new HashSet<Activity>();
            this.Sign_up = new HashSet<Sign_up>();
        }
    
        public int supervision_ID { get; set; }
        public string supervision_Name { get; set; }
        public Nullable<int> supervision_phone { get; set; }
        public string supervision_address { get; set; }
        public string supervision_email { get; set; }
        public byte[] supervision_photo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Abnormal_event> Abnormal_event { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Manpower_apply> Manpower_apply { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Activity> Activities { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sign_up> Sign_up { get; set; }
    }
}
