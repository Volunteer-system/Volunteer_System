using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteer_WPF.Model
{
    class Unit_volunteer_list_Model
    {
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
        //加入日期
        public string Seniority { get; set; }
        //志工背心號碼
        public string Vest_no { get; set; }
        //學歷
        public string Education { get; set; }        

        public List<Unit_volunteer_list_Model> Selectvolunteer_list_byApplication_unit(int Application_unit_no)
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from n1 in dbContext.Application_unit
                    join n2 in dbContext.Volunteer_list
                    on n1.Application_unit_no equals n2.Application_unit_no
                    join n3 in dbContext.Volunteer
                    on n2.Volunteer_no equals n3.Volunteer_no
                    join n4 in dbContext.Identity_type
                    on n3.Identity_type equals n4.Identity_type1
                    where n1.Application_unit_no == Application_unit_no
                    select new
                    {
                        Chinese_name = n3.Chinese_name,
                        English_name = n3.English_name,
                        Sex = n3.sex,
                        Birthday = n3.birthday,
                        IDcrad_no = n3.IDcrad_no,
                        Medical_record_no = n3.Medical_record_no,
                        Identity_type = n4.Identity_type_name,
                        Join_date = n3.Join_date,
                        Vest_no = n3.Vest_no,
                        Education = n3.Education
                    };

            List<Unit_volunteer_list_Model> unit_volunteer_list_Models = new List<Unit_volunteer_list_Model>();
            foreach (var row in q)
            {
                Unit_volunteer_list_Model unit_Volunteer_List_Model = new Unit_volunteer_list_Model();
                unit_Volunteer_List_Model.Chinese_name = row.Chinese_name;
                unit_Volunteer_List_Model.English_name = row.English_name;
                unit_Volunteer_List_Model.Sex = row.Sex;
                unit_Volunteer_List_Model.Birthday = row.Birthday.ToString();
                unit_Volunteer_List_Model.IDcrad_no = row.IDcrad_no;
                unit_Volunteer_List_Model.Medical_record_no = row.Medical_record_no;
                unit_Volunteer_List_Model.Identity_type = row.Identity_type;
                string Seniority = (DateTime.Now - row.Join_date).ToString();
                unit_Volunteer_List_Model.Seniority = Seniority;
                unit_Volunteer_List_Model.Vest_no = row.Vest_no;
                unit_Volunteer_List_Model.Education = row.Education;

                unit_volunteer_list_Models.Add(unit_Volunteer_List_Model);
            }

            return unit_volunteer_list_Models;
        }
    }
}
