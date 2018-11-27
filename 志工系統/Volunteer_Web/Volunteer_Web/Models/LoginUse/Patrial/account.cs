using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Volunteer_Web.Models
{   [MetadataType(typeof(MetaUse))]
    public partial class account
    {
        public class MetaUse
        {
            public int Primary_key { get; set; }
            [DisplayName("使用者名稱:")]
            [Required(ErrorMessage ="不可以空白")]
            public string Account_number { get; set; }
            [DisplayName("使用者密碼:")]
            [Required(ErrorMessage = "不可以空白")]
            public string Password { get; set; }
            public Nullable<int> User_ID { get; set; }
            public string Permission { get; set; }
        }
    }
}