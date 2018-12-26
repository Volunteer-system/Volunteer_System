using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteer_WPF.Model
{
    class Application_unit_Model
    {
        //運用單位編號
        public int Application_unit_no { get; set; }
        //運用單位
        public string Application_unit { get; set; }
        //組別
        public string Group { get; set; }
        //運用單位聯絡電話
        public string Application_phone_no { get; set; }
        //負責人
        public string Principal { get; set; }
        //負責人聯絡電話
        public string Principal_phone_no { get; set; }
        //運用單位地址
        public string Application_address { get; set; }
        //工作內容描述
        public string Work_content { get; set; }
        //志工總人數
        public string Total_volunteers { get; set; }

        public void SelectApplication_unit_byApplication_unit(string application_unit)
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from n1 in dbContext.Application_unit
                    join n2 in dbContext.Service_group
                    on n1.Group_no equals n2.Group_no
                    where n1.Application_unit1 == application_unit
                    select new
                    {
                        Application_unit_no = n1.Application_unit_no,
                        Application_unit = n1.Application_unit1,
                        Group = n2.Group_name,
                        Application_phone_no = n1.Application_phone_no,
                        Principal = n1.Principal,
                        Principal_phone_no = n1.Principal_phone_no,
                        Application_address = n1.Application_address,
                        Work_content = n1.Work_content,
                        Total_volunteers = n1.Total_volunteers
                    };

            foreach (var row in q)
            {
                Application_unit_no = row.Application_unit_no;
                Application_unit = row.Application_unit;
                Group = row.Group;
                Application_phone_no = row.Application_phone_no;
                Principal = row.Principal;
                Principal_phone_no = row.Principal_phone_no;
                Application_address = row.Application_address;
                Work_content = row.Work_content;
                Total_volunteers = row.Total_volunteers.ToString();
            }

        }

        public List<Application_unit_Model> SelectApplication_unit(string name, string group_name, int membersmin, int membersmax)
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from n1 in dbContext.Application_unit
                    join n2 in dbContext.Service_group
                    on n1.Group_no equals n2.Group_no
                    where ((name == "") ? true : n1.Application_unit1 == name) &&
                          ((group_name == "") ? true : n2.Group_name == group_name) &&
                          ((membersmin == 0)? true : n1.Total_volunteers >= membersmin) &&
                          ((membersmax == 0)? true : n1.Total_volunteers <= membersmax)
                    select new
                    {
                        Application_unit_no = n1.Application_unit_no,
                        Application_unit = n1.Application_unit1,
                        Group = n2.Group_name,
                        Application_phone_no = n1.Application_phone_no,
                        Principal = n1.Principal,
                        Principal_phone_no = n1.Principal_phone_no,
                        Application_address = n1.Application_address,
                        Work_content = n1.Work_content,
                        Total_volunteers = n1.Total_volunteers
                    };

            List<Application_unit_Model> Application_units = new List<Application_unit_Model>();
            foreach (var row in q)
            {
                Application_unit_Model application_Unit_Model = new Application_unit_Model();
                application_Unit_Model.Application_unit_no = row.Application_unit_no;
                application_Unit_Model.Application_unit = row.Application_unit;
                application_Unit_Model.Group = row.Group;
                application_Unit_Model.Application_phone_no = row.Application_phone_no;
                application_Unit_Model.Principal = row.Principal;
                application_Unit_Model.Principal_phone_no = row.Principal_phone_no;
                application_Unit_Model.Application_address = row.Application_address;
                application_Unit_Model.Work_content = row.Work_content;
                application_Unit_Model.Total_volunteers = row.Total_volunteers.ToString();

                Application_units.Add(application_Unit_Model);
            }

            return Application_units;
        }

        public List<Application_unit_Model> SelectApplication_unit()
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from n in dbContext.Application_unit
                    select n; ;

            List<Application_unit_Model> application_units = new List<Application_unit_Model>();
            foreach (var row in q)
            {
                Application_unit_Model application_Unit_Model = new Application_unit_Model();
                application_Unit_Model.Application_unit_no = row.Application_unit_no;
                application_Unit_Model.Application_unit = row.Application_unit1;

                application_units.Add(application_Unit_Model);
            }

            return application_units;
        }

        public List<string> SelectApplication_unit_name()
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from n in dbContext.Application_unit
                    select new { application_unit = n.Application_unit1 };

            List<string> application_units = new List<string>();
            foreach (var row in q)
            {
                application_units.Add(row.application_unit);
            }

            return application_units;
        }

        public void InsertApplication_unit(Application_unit_Model application_Unit_Model)
        {
            int group_num = 0;
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from n in dbContext.Service_group
                    where n.Group_name == application_Unit_Model.Group
                    select n;
            foreach (var row in q)
            {
                group_num = row.Group_no;
            }
            
            dbContext.Application_unit.ToList();
            dbContext.Application_unit.Local.Add(new Application_unit
            {
                Application_unit1 = application_Unit_Model.Application_unit,
                Group_no = group_num,
                Application_phone_no = application_Unit_Model.Application_phone_no,
                Principal = application_Unit_Model.Principal,
                Principal_phone_no = application_Unit_Model.Principal_phone_no,
                Application_address = application_Unit_Model.Application_address,
                Work_content = application_Unit_Model.Work_content
            });

            dbContext.SaveChanges();

            var q2 = from n in dbContext.Application_unit
                     where n.Application_unit1 == application_Unit_Model.Application_unit
                     select n;
            foreach (var row in q2)
            {
                Application_unit_no = row.Application_unit_no;
            }
        }

        public void UpdateApplication_unit(Application_unit_Model application_Unit_Model)
        {
            VolunteerEntities dbContext = new VolunteerEntities();

            var q = from n in dbContext.Application_unit
                    where n.Application_unit_no == application_Unit_Model.Application_unit_no
                    select n;
            foreach (var row in q)
            {
                var q2 = from n in dbContext.Service_group
                         where n.Group_name == application_Unit_Model.Group
                         select n;
                foreach (var row2 in q2)
                {
                    if (row.Group_no != row2.Group_no)
                    {
                        row.Group_no = row2.Group_no;
                    }
                }

                row.Application_unit1 = application_Unit_Model.Application_unit;
                row.Application_phone_no = application_Unit_Model.Application_phone_no;
                row.Principal = application_Unit_Model.Principal;
                row.Principal_phone_no = application_Unit_Model.Principal_phone_no;
                row.Application_address = application_Unit_Model.Application_address;
                row.Work_content = application_Unit_Model.Work_content;
            }

            dbContext.SaveChanges();            
        }

    }
}
