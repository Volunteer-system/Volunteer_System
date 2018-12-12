using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Volunteer_Web.Models
{
    [MetadataType(typeof(AbnormaleventMetaData))]

    public partial class Abnormal_event
    {
        public class AbnormaleventMetaData
        {
            [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
            public System.DateTime Notification_date { get; set; }

            [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
            public Nullable<System.DateTime> Closing_date { get; set; }
        }
    }
}