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
    
    public partial class Abnormal_event
    {
        public int Abnormal_event_no { get; set; }
        public string Abnormal_event_ID { get; set; }
        public string Abnormal_event1 { get; set; }
        public int Volunteer_no { get; set; }
        public int Application_unit_no { get; set; }
        public Nullable<int> event_category_ID { get; set; }
        public int Stage { get; set; }
        public System.DateTime Notification_date { get; set; }
        public Nullable<System.DateTime> Closing_date { get; set; }
        public Nullable<int> Supervisor_ID { get; set; }
        public string Unit_descrition { get; set; }
        public string Volunteer_description { get; set; }
        public string Supervisor_description { get; set; }
        public string Result { get; set; }
        public Nullable<int> Volunteer_leader_ID { get; set; }
        public string Supervisor_heads { get; set; }
        public string Rejection_Reason { get; set; }
    
        public virtual Application_unit Application_unit { get; set; }
        public virtual event_category event_category { get; set; }
        public virtual Volunteer Volunteer { get; set; }
        public virtual Volunteer_supervision Volunteer_supervision { get; set; }
        public virtual Volunteer Volunteer1 { get; set; }
    }
}
