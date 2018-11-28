using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Volunteer_Web.Models;

namespace Volunteer_Web.ViewModel
{
    public class Sign_up_session
    {
        [DisplayName("姓名")]
        public string Chinese_name { get; set; }
        public string English_name { get; set; }
        [DisplayName("性別")]
        public string Sex { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayName("生日")]
        public DateTime Birthday { get; set; }
        [DisplayName("申請身分")]
        public string Sign_up_type { get; set; }
        public DateTime Sign_up_date { get; set; }
        [DisplayName("市話")]
        public string Phone { get; set; }
        [DisplayName("手機")]
        public string Mobile { get; set; }
        [DataType(DataType.EmailAddress)]
        [DisplayName("電子郵件")]
        public string Email { get; set; }
        [DisplayName("地址")]
        public string Address { get; set; }
        [DisplayName("教育程度")]
        public string Education { get; set; }
        [DisplayName("工作類別")]
        public string Job { get; set; }
        [DisplayName("專長")]
        public IEnumerable<string> Expertises { get; set; }
    }
}