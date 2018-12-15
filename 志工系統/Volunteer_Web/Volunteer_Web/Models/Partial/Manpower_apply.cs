using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Volunteer_Web.Models
{
    [MetadataType(typeof(ManpowerApplyMetaData))]
    public partial class Manpower_apply
    {
        public class ManpowerApplyMetaData
        {
            [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
            public System.DateTime Apply_date { get; set; }
            [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
            public Nullable<System.DateTime> Reply_date { get; set; }
        }
    }
}