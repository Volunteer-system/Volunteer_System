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
    
    public partial class Manpower_apply
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Manpower_apply()
        {
            this.Apply_Assessment = new HashSet<Apply_Assessment>();
            this.Apply_result = new HashSet<Apply_result>();
        }
    
        public int Apply_ID { get; set; }
        public int Application_unit_no { get; set; }
        public string Applicant { get; set; }
        public System.DateTime Apply_date { get; set; }
        public string Applicant_phone { get; set; }
        public string Work_place { get; set; }
        public string Apply_description { get; set; }
        public string Application_unit_Supervisor { get; set; }
        public string Application_unit_heads { get; set; }
        public Nullable<int> Apply_state { get; set; }
        public Nullable<int> Supervision_ID { get; set; }
        public Nullable<System.DateTime> Reply_date { get; set; }
        public string Repply_description { get; set; }
        public string Supervision_heads { get; set; }
        public Nullable<int> Application_number { get; set; }
        public Nullable<int> Reply_number { get; set; }
        public string Apply_type { get; set; }
    
        public virtual Application_unit Application_unit { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Apply_Assessment> Apply_Assessment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Apply_result> Apply_result { get; set; }
        public virtual Volunteer_supervision Volunteer_supervision { get; set; }
    }
}
