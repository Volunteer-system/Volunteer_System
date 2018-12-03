using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;


namespace Volunteer_Web.Models   //namespace要一樣
{
    [MetadataType(typeof(ActivityMetaData))]
    public partial class Activity
    {
        public class ActivityMetaData
        {
            
            [DisplayName("活動編號")]
            public int Activity_no { get; set; }
            //[Required(ErrorMessage = "請輸入活動名稱")]
            [DisplayName("活動名稱")]
            public string Activity_name { get; set; }
            [DisplayName("活動類別")] 
            public int Activity_type_ID { get; set; }
            [DisplayName("組別")]
            public Nullable<int> Group_no { get; set; }
            [DisplayName("活動起始時間")]
            [DataType(DataType.DateTime)]
            [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}")]
            public Nullable<System.DateTime> Activity_startdate { get; set; }
            [DisplayName("活動結束時間")]
            [DataType(DataType.DateTime)]
            [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}")]
            public Nullable<System.DateTime> Activity_enddate { get; set; }
            [DisplayName("承辦單位")]
            public string Undertake_unit { get; set; }
            [DisplayName("承辦人")]
            public Nullable<int> Undertaker { get; set; }
            [DisplayName("承辦單位電話")]
            //[RegularExpression(@"^\d{10}$",ErrorMessage ="請輸入格式(02)12345678")]
            public string Undertake_phone { get; set; }
            //[Required]
            [DataType(DataType.EmailAddress)]
            [DisplayName("承辦單位e-mail")]
            public string Undertake_email { get; set; }
            [DisplayName("講師")]
            public string lecturer { get; set; }
            [DisplayName("課程人數")]
            public Nullable<int> Member { get; set; }
            [DisplayName("備取人數")]
            public Nullable<int> Spare { get; set; }
            [DisplayName("活動地點")]
            public string Place { get; set; }
            [DisplayName("活動簡介")]
            public string Summary { get; set; }
            [DisplayName("活動照片id")]
            public Nullable<int> Activity_Photo_id { get; set; }
        }
    }
}