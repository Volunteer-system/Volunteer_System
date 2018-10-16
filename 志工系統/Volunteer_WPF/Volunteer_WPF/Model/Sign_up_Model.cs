using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Volunteer_WPF.Model
{
    class Sign_up_Model
    {    
        //申請編號
        public int Sign_up_no { get; set; }
        //中文姓名
        public string Chinese_name { get; set; }
        //英文姓名(同護照姓名)
        public string English_name { get; set; }
        //性別
        public string Sex { get; set; }
        //生日
        public string Birthday { get; set; }
        //申請日期
        public string Sign_up_date { get; set; }
        //電話號碼(H)
        public string Phone { get; set; }
        //手機號碼
        public string Mobile { get; set; }
        //電子郵件
        public string Email { get; set; }
        //聯絡地址
        public string Address { get; set; }
        //教育程度(小學、國中、高中職、專科、大學、研究所)
        public string Education { get; set; }
        //目前職業(軍、公、教、家庭、退休、服務、自由、工、商、農、學生)
        public string Job { get; set; }
        //人格量表結果        
        public string Personality_scale { get; set; }
        //申請階段        
        public string Stage { get; set; }
        //核准日期        
        public string Approval_date { get; set; }
        //志工督導        
        public string supervision_Name { get; set; }
       

        public void SelectSign_up_bySignup_no(int Signup_no)
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from n1 in dbContext.Sign_up
                    join n2 in dbContext.Stages
                    on n1.Stage equals n2.Stage_ID
                    join n3 in dbContext.Volunteer_supervision
                    on n1.supervision_ID equals n3.supervision_ID                    
                    where n1.Sign_up_no == Signup_no 
                    select new
                    {
                        Chinese_name = n1.Chinese_name.ToString(),
                        English_name =  n1.English_name.ToString(),
                        Sex = n1.Sex.ToString(),
                        Birthday = n1.Birthday.ToString(),
                        Sign_up_date =  n1.Sign_up_date.ToString(),
                        Phone = n1.Phone.ToString(),
                        Mobile = n1.Mobile.ToString(),
                        Email = n1.Email.ToString(),
                        Address = n1.Address.ToString(),
                        Education = n1.Education.ToString(),
                        Job = n1.Job.ToString(),
                        Personality_scale = n1.Personality_scale.ToString(),
                        Stage = n2.Stage1.ToString(),
                        Approval_date = n1.Approval_date.ToString(),
                        supervision_Name = n3.supervision_Name.ToString(),
                        
                    };

            foreach (var row in q)
            {
                Chinese_name = row.Chinese_name;
                English_name = row.English_name;
                Sex = row.Sex;
                Birthday = row.Birthday;
                Sign_up_date = row.Sign_up_date;
                Phone = row.Phone;
                Mobile = row.Mobile;
                Email = row.Email;
                Address = row.Address;
                Education = row.Education;
                Job = row.Job;
                Personality_scale = row.Personality_scale;
                Stage = row.Stage;
                Approval_date = row.Approval_date;
                supervision_Name = row.supervision_Name;
                
            }            
        }

        public void UpdateSign_up_byStage(int Signup_no,string Stage)
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from n1 in dbContext.Sign_up
                    where n1.Sign_up_no == Signup_no
                    select n1;

            var q2 = from n in dbContext.Stages
                     where n.Stage1.Contains(Stage)
                     select n;

            foreach (var Signup_row in q)
            {
                foreach (var Stage_row in q2)
                    Signup_row.Stage = Stage_row.Stage_ID;
            }

            dbContext.SaveChanges();
        }

    }
}
