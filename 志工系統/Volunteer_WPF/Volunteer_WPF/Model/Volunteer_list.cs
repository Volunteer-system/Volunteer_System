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
    
    public partial class Volunteer_list
    {
        public int Primary_key { get; set; }
        public int Application_unit_no { get; set; }
        public int Volunteer_no { get; set; }
        public Nullable<bool> blacklist_yn { get; set; }
    
        public virtual Application_unit Application_unit { get; set; }
        public virtual Volunteer Volunteer { get; set; }
    }
}
