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
    
    public partial class Activity1
    {
        public int Primary_key { get; set; }
        public int Volunteer_no { get; set; }
        public int Activity_no { get; set; }
        public Nullable<System.DateTime> Registration_date { get; set; }
        public string Stage { get; set; }
        public Nullable<System.DateTime> Confirm_time { get; set; }
        public Nullable<bool> Vegetarian { get; set; }
    
        public virtual Activity Activity { get; set; }
        public virtual Volunteer Volunteer { get; set; }
    }
}
