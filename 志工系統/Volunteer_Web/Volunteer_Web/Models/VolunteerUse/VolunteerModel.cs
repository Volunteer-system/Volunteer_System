using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Volunteer_Web.Models.VolunteerUse
{
    public class VolunteerModel
    {
        VolunteerEntities db = new VolunteerEntities();
        //public IEnumerable<Volunteer> volunteers { get; set; }
        //public IEnumerable<Lssuing_unit> lssuing_unit { get; set; }
        //public IEnumerable<Identity_type> identity_type { get; set; }
        public int Volunteer_no { get; set; }
        public string Chinese_name { get; set; }
        public string English_name { get; set; }
        public string sex { get; set; }
        public Nullable<System.DateTime> birthday { get; set; }
        public string IDcrad_no { get; set; }
        public string Medical_record_no { get; set; }
        public string Identity_type { get; set; }
        public int Seniority { get; set; }
        public Nullable<System.DateTime> Join_date { get; set; }
        public Nullable<System.DateTime> Leave_date { get; set; }
        public string Leave_reason { get; set; }
        public Nullable<int> Phone_no { get; set; }
        public Nullable<int> Mobile_no { get; set; }
        public string Vest_no { get; set; }
        public Nullable<int> Postal_code { get; set; }
        public string Address { get; set; }
        public string Education { get; set; }
        public string Lssuing_unit { get; set; }
        public string Service_manual_no { get; set; }
        public string Personality_scale { get; set; }
        public byte[] Photo { get; set; }
        //============================================查詢出志工專長=======================================================
        public List<Expertise1> GetVolunteerExpertise(int id)
        { 
            List<Expertise1> VE = new List<Expertise1>();
            var expertise = from v in db.Volunteers
                            join e in db.Expertise2 on v.Volunteer_no equals e.Volunteer_no
                            join em in db.Expertise1 on e.Expertise_no equals em.Expertise_no
                            where v.Volunteer_no == id
                            select new { em.Expertise};
            foreach (var es in expertise)
            {
                VE.Add(new Expertise1()
                {
                    Expertise = es.Expertise
                });
            }                           
            return VE;
        }
        public Service_group GetVolunteerGroup(int id)
        {
            Service_group ServiceGroup = new Service_group();
            var group = from volunteergroup in db.Service_Group1
                    join servicegroup in db.Service_group on volunteergroup.Group_no equals servicegroup.Group_no
                    where volunteergroup.Volunteer_no == id
                    select new { servicegroup.Group_name };
            foreach (var result in group)
            {
                ServiceGroup.Group_name = result.Group_name;
            }
            return ServiceGroup;
        }
        //===================================查詢出志工資料============================================================
        public VolunteerModel GetAllVolunteer(int id)
        {
            VolunteerModel VM = new VolunteerModel();
            DateTime DTNull = new DateTime(1911-01-01);
            byte[] photoNull = new byte[0];
            var all = from v in db.Volunteers
                      join i in db.Identity_type on v.Identity_type equals i.Identity_type1  //join身分類別
                      join l in db.Lssuing_unit on v.Lssuing_unit_no equals l.Lssuing_unit_no//join發證單位
                      where v.Volunteer_no == id
                      select new { v.Volunteer_no, v.Chinese_name, v.English_name, v.sex, v.birthday, v.IDcrad_no, v.Medical_record_no, i.Identity_type_name, v.Seniority, v.Join_date, v.Leave_date, v.Leave_reason, v.Phone_no, v.Mobile_no, v.Vest_no, v.Postal_code, v.Address, v.Education, l.Lssuing_unit1, v.Service_manual_no, v.Personality_scale, v.Photo};
            foreach (var v in all)
            {
                VM.Volunteer_no = v.Volunteer_no;
                VM.Chinese_name = v.Chinese_name == null? "無" : v.Chinese_name;
                VM.English_name = v.English_name == null ? "無" : v.English_name;
                VM.sex = v.sex;
                VM.birthday = v.birthday;// == null ? DTNull : v.birthday;
                VM.IDcrad_no = v.IDcrad_no;
                VM.Medical_record_no = v.Medical_record_no == null ? "無" : v.Medical_record_no;
                VM.Identity_type = v.Identity_type_name;
                VM.Seniority = v.Seniority;
                VM.Join_date = v.Join_date;// == null ? DTNull : v.Join_date;
                VM.Leave_date = v.Leave_date;// == null ? DTNull : v.Leave_date;
                VM.Leave_reason = v.Leave_reason == null ? "無" : v.Leave_reason;
                VM.Phone_no = v.Phone_no;// == null ? 0 : v.Phone_no;
                VM.Mobile_no = v.Mobile_no;// == null ? 0 : v.Mobile_no;
                VM.Vest_no = v.Vest_no == null ? "無" : v.Vest_no;
                VM.Postal_code = v.Postal_code;// == null ? 0 : v.Postal_code;
                VM.Address = v.Address == null ? "無" : v.Address;
                VM.Education = v.Education == null ? "無" : v.Education;
                VM.Lssuing_unit = v.Lssuing_unit1;
                VM.Service_manual_no = v.Service_manual_no;
                VM.Personality_scale = v.Personality_scale == null ? "無" : v.Personality_scale;
                VM.Photo = v.Photo == null ? photoNull : v.Photo;
            }
            return VM ;
        }
    }
}