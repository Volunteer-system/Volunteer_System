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
    
    public partial class Sign_up
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sign_up()
        {
            this.Sign_up_interview_period = new HashSet<Sign_up_interview_period>();
            this.Sign_up_questionnaire = new HashSet<Sign_up_questionnaire>();
            this.Sign_up_Service_period = new HashSet<Sign_up_Service_period>();
        }
    
        public int Sign_up_no { get; set; }
        public string Chinese_name { get; set; }
        public string English_name { get; set; }
        public string Sex { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public string Sign_up_type { get; set; }
        public System.DateTime Sign_up_date { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Education { get; set; }
        public string Job { get; set; }
        public string Personality_scale { get; set; }
        public Nullable<int> Stage { get; set; }
        public Nullable<System.DateTime> Approval_date { get; set; }
        public Nullable<int> supervision_ID { get; set; }
    
        public virtual Volunteer_supervision Volunteer_supervision { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sign_up_interview_period> Sign_up_interview_period { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sign_up_questionnaire> Sign_up_questionnaire { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sign_up_Service_period> Sign_up_Service_period { get; set; }
    }
}
