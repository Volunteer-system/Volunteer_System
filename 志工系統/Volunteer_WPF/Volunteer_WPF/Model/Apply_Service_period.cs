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
    
    public partial class Apply_Service_period
    {
        public int Primary_key { get; set; }
        public int Apply_ID { get; set; }
        public int Service_period_no { get; set; }
        public Nullable<int> Volunteer_number { get; set; }
    
        public virtual Manpower_apply Manpower_apply { get; set; }
        public virtual Service_period1 Service_period1 { get; set; }
    }
}
