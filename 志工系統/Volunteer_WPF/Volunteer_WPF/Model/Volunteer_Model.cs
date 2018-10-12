using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace Volunteer_WPF.Model
{
    class Volunteer_Model
    {
        //志工編號
        public string Volunteer_no { get; set; }
        //中文姓名
        public string Chinese_name { get; set; }
        //英文姓名(同護照)
        public string English_name { get; set; }
        //性別
        public string Sex { get; set; }
        //生日
        public string Birthday { get; set; }
        //身分證字號(護照編號)
        public string IDcrad_no { get; set; }
        //病歷號
        public string Medical_record_no { get; set; }
        //身分類別
        public string Identity_type { get; set; }
        //服務年資
        public string Seniority { get; set; }
        //加入日期
        public string Join_date { get; set; }
        //離開日期
        public string Leave_date { get; set; }
        //離開原因(因病、工作因素、個人因素、不適任、家庭因素)
        public string Leave_reason { get; set; }
        //電話號碼
        public string Phone_no { get; set; }
        //手機號碼
        public string Mobile_no { get; set; }
        //志工背心號碼
        public string Vest_no { get; set; }
        //郵遞區號
        public string Postal_code { get; set; }
        //地址
        public string Address { get; set; }
        //學歷
        public string Education { get; set; }
        //發證單位編號
        public string Lssuing_unit_no { get; set; }
        //發證單位
        public string Lssuing_unit { get; set; }
        //志工服務手冊編號
         public string Service_manual_no { get; set; }
        //人格量表結果
        public string Personality_scale { get; set; }
        //個人照片
        public BitmapImage Photo { get; set; }

        //============================================================================================================
        //合併項目

        //組別
        public string Group { get; set; }
        //專長
        public string Experise { get; set; }
        //============================================================================================================

        public List<Volunteer_Model> Search_Volunteer(string Name, string Group, string Expertise)
        {
            VolunteerEntities dbcontext = new VolunteerEntities();
            var q = from p1 in dbcontext.Volunteer

                    join p2 in dbcontext.Expertise2 on p1.Volunteer_no equals p2.Volunteer_no
                    join p2_5 in dbcontext.Expertise1 on p2.Expertise_no equals p2_5.Expertise_no

                    join p3 in dbcontext.Service_Group1 on p1.Volunteer_no equals p3.Volunteer_no
                    join p3_5 in dbcontext.Service_group on p3.Group_no equals p3_5.Group_no

                    join p4 in dbcontext.Identity_type on p1.Identity_type equals p4.Identity_type1


                    where
                    (!(Experise == "") ? p2.Expertise1.Expertise == Expertise : true) &&
                    (!(Group == "") ? p3_5.Group_name == Group : true) &&
                    (!(Name == "") ? p1.Chinese_name.Contains(Name) : true)
                    select new
                    {
                        Volunteer_no = p1.Volunteer_no.ToString(),
                        Chinese_name = p1.Chinese_name.ToString(),
                        English_name = p1.English_name.ToString(),
                        Sex = p1.sex.ToString(),
                        Birthday = p1.birthday.ToString(),
                        IDcrad_no = p1.IDcrad_no.ToString(),
                        Medical_record_no = p1.Medical_record_no.ToString(),
                        Identity_type = p4.Identity_type_name.ToString(),
                        Seniority = p1.Seniority.ToString(),
                        Join_date = p1.Join_date.ToString(),
                        Leave_date = p1.Leave_date.ToString(),
                        Leave_reason = p1.Leave_reason.ToString(),
                        Phone_no = p1.Phone_no.ToString(),
                        Mobile_no = p1.Mobile_no.ToString(),
                        Vest_no = p1.Vest_no.ToString(),
                        Postal_code = p1.Postal_code.ToString(),
                        Address = p1.Address.ToString(),
                        Education = p1.Education.ToString(),
                        Lssuing_unit_no = p1.Lssuing_unit_no.ToString(),
                        Service_manual_no = p1.Service_manual_no.ToString(),
                        Personality_scale = p1.Personality_scale.ToString(),

                        //合併項
                        Experise = p2_5.Expertise.ToString(),
                        Group = p3_5.Group_name.ToString()

                        //Photo = p1.Photo.ToString()
                    };


            List<Volunteer_Model> volunteer_Models = new List<Volunteer_Model>();

            foreach (var row in q)
            {
                Volunteer_Model model = new Volunteer_Model();
                model.Volunteer_no = row.Volunteer_no;
                model.Chinese_name = row.Chinese_name;
                model.English_name = row.English_name;
                model.Sex = row.Sex;
                model.Birthday = row.Birthday;
                model.IDcrad_no = row.IDcrad_no;
                model.Medical_record_no = row.Medical_record_no;
                model.Identity_type = row.Identity_type;
                model.Seniority = row.Seniority;
                model.Join_date = row.Join_date;
                model.Leave_date = row.Leave_date;
                model.Leave_reason = row.Leave_reason;
                model.Phone_no = row.Phone_no;
                model.Mobile_no = row.Mobile_no;
                model.Vest_no = row.Vest_no;
                model.Postal_code = row.Postal_code;
                model.Address = row.Address;
                model.Education = row.Education;
                model.Lssuing_unit_no = row.Lssuing_unit_no;
                model.Service_manual_no = row.Service_manual_no;
                model.Personality_scale = row.Personality_scale;
                volunteer_Models.Add(model);
            }

            return volunteer_Models;
        }

        public void SelectVolunteer_byVolunteerno(int Volunteer_no)
        {
            VolunteerEntities dbContext = new VolunteerEntities();

            var q = from n1 in dbContext.Volunteer
                    join n2 in dbContext.Identity_type
                    on n1.Identity_type equals n2.Identity_type1
                    join n3 in dbContext.Lssuing_unit
                    on n1.Lssuing_unit_no equals n3.Lssuing_unit_no
                    where n1.Volunteer_no == Volunteer_no
                    select new
                    {
                        Chinese_name = n1.Chinese_name.ToString(),
                        English_name = n1.English_name.ToString(),
                        sex = n1.sex.ToString(),
                        Birthday = n1.birthday.ToString(),
                        IDcrad_no = n1.IDcrad_no.ToString(),
                        Medical_record_no = n1.Medical_record_no.ToString(),
                        Identity_type = n2.Identity_type_name.ToString(),
                        Seniority = n1.Seniority.ToString(),
                        Join_date = n1.Join_date.ToString(),
                        Leave_date = n1.Leave_date.ToString(),
                        Leave_reason = n1.Leave_reason.ToString(),
                        Phone_no = n1.Phone_no.ToString(),
                        Mobile_no = n1.Mobile_no.ToString(),
                        Vest_no = n1.Vest_no.ToString(),
                        Postal_code = n1.Postal_code.ToString(),
                        Address = n1.Address.ToString(),
                        Education = n1.Education.ToString(),
                        Lssuing_unit = n3.Lssuing_unit1.ToString(),
                        Service_manual_no = n1.Service_manual_no.ToString(),
                        Personality_scale = n1.Personality_scale.ToString(),
                        Photo = n1.Photo
                    };

            foreach (var row in q)
            {
                Chinese_name = row.Chinese_name;
                English_name = row.English_name;
                Sex = row.sex;
                Birthday = row.Birthday;
                IDcrad_no = row.IDcrad_no;
                Medical_record_no = row.Medical_record_no;
                Identity_type = row.Identity_type;
                Seniority = row.Seniority;
                Join_date = row.Join_date;
                Leave_date = row.Leave_date;
                Leave_reason = row.Leave_reason;
                Phone_no = row.Phone_no;
                Mobile_no = row.Mobile_no;
                Vest_no = row.Vest_no;
                Postal_code = row.Postal_code;
                Address = row.Address;
                Education = row.Education;
                Lssuing_unit = row.Lssuing_unit;
                Service_manual_no = row.Service_manual_no;
                Personality_scale = row.Personality_scale;

                BitmapImage image = new BitmapImage();
                System.IO.MemoryStream ms = new System.IO.MemoryStream(row.Photo);
                image.BeginInit();
                image.StreamSource = ms;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
                Photo = image;
            }
        }

        //public void SelectVolunteer_LeaderbyVolunteerno(int Volunteer_no)
        //{

        //}
    }
}
