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
    
    public partial class Service_period2
    {
        public int Primary_key { get; set; }
        public int Volunteer_no { get; set; }
        public int Wish_order { get; set; }
        public int Service_period_no { get; set; }
        public Nullable<int> Stage { get; set; }
        public Nullable<int> Srevice_group { get; set; }
        public Nullable<int> Application_unit { get; set; }
    
        public virtual Service_period1 Service_period1 { get; set; }
        public virtual Volunteer Volunteer { get; set; }
        public virtual Application_unit Application_unit1 { get; set; }
        public virtual Service_group Service_group { get; set; }
    }
}
