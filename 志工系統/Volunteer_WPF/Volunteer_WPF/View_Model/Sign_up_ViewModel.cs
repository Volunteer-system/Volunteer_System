using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer_WPF.Model;

namespace Volunteer_WPF.View_Model
{
    class Sign_up_ViewModel
    {       
        private Sign_up_Model sign_Up_Model = new Sign_up_Model();
        private Sign_up_Service_period_Model sign_Up_Service_Period_Model = new Sign_up_Service_period_Model();

        //中文姓名
        public string Chinese_name
        {
            get { return sign_Up_Model.Chinese_name; }
            set { sign_Up_Model.Chinese_name = value; }
        }
        //英文姓名(同護照姓名)
        public string English_name
        {
            get { return sign_Up_Model.English_name; }
            set { sign_Up_Model.English_name = value; }
        }
        //性別
        public string Sex
        {
            get { return sign_Up_Model.Sex; }
            set { sign_Up_Model.Sex = value; }
        }
        //生日
        public string Birthday
        {
            get { return sign_Up_Model.Birthday; }
            set { sign_Up_Model.Birthday = value; }
        }
        //申請日期
        public string Sign_up_date
        {
            get { return sign_Up_Model.Sign_up_date; }
            set { sign_Up_Model.Sign_up_date = value; }
        }
        //電話號碼(H)
        public string Phone
        {
            get { return sign_Up_Model.Phone; }
            set { sign_Up_Model.Phone = value; }
        }
        //手機號碼
        public string Mobile
        {
            get { return sign_Up_Model.Mobile; }
            set { sign_Up_Model.Mobile = value; }
        }
        //電子郵件
        public string Email
        {
            get { return sign_Up_Model.Email; }
            set { sign_Up_Model.Email = value; }
        }
        //聯絡地址
        public string Address
        {
            get { return sign_Up_Model.Address; }
            set { sign_Up_Model.Address = value; }
        }
        //教育程度(小學、國中、高中職、專科、大學、研究所)
        public string Education
        {
            get { return sign_Up_Model.Education; }
            set { sign_Up_Model.Education = value; }
        }
        //目前職業(軍、公、教、家庭、退休、服務、自由、工、商、農、學生)
        public string Job
        {
            get { return sign_Up_Model.Job; }
            set { sign_Up_Model.Job = value; }
        }
        //人格量表結果
        public string Personality_scale
        {
            get { return sign_Up_Model.Personality_scale; }
            set { sign_Up_Model.Personality_scale = value; }
        }
        //申請階段
        public string Stage
        {
            get { return sign_Up_Model.Stage; }
            set { sign_Up_Model.Stage = value; }
        }
        //核准日期
        public string Approval_date
        {
            get { return sign_Up_Model.Approval_date; }
            set { sign_Up_Model.Approval_date = value; }
        }
        //志工督導
        public string supervision_Name
        {
            get { return sign_Up_Model.supervision_Name; }
            set { sign_Up_Model.supervision_Name = value; }
        }
        //面試意願時間
        public string Interview_date
        {
            get { return sign_Up_Model.Interview_date; }
            set { sign_Up_Model.Interview_date = value; }
        }

        public Sign_up_ViewModel()
        {
            //Sign_up_Model sign_Up_Model = new Sign_up_Model();
        }

        public List<string> SelectSign_up_bySignup_no(int Signup_no)
        {
            //呼叫Sign_up Model
            sign_Up_Model.SelectSign_up_bySignup_no(Signup_no);

            Chinese_name = sign_Up_Model.Chinese_name;
            English_name = sign_Up_Model.English_name;
            Sex = sign_Up_Model.Sex;
            Birthday = sign_Up_Model.Birthday;
            Sign_up_date = sign_Up_Model.Sign_up_date;
            Phone = sign_Up_Model.Phone;
            Mobile = sign_Up_Model.Mobile;
            Email = sign_Up_Model.Email;
            Address = sign_Up_Model.Address;
            Education = sign_Up_Model.Education;
            Job = sign_Up_Model.Job;
            Personality_scale = sign_Up_Model.Personality_scale;
            Stage = sign_Up_Model.Stage;
            Approval_date = sign_Up_Model.Approval_date;
            supervision_Name = sign_Up_Model.supervision_Name;
            Interview_date = sign_Up_Model.Interview_date;

            //呼叫Sign_up_Service_Period Model
            List<string> List_Service_period = sign_Up_Service_Period_Model.SelectSign_up_Service_periodbySignup_no(Signup_no);
            return List_Service_period;
        }
    }
}
