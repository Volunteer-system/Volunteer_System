using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteer_WPF.Model
{
    class Abnormal_event_Model
    {
        //事件編號
        public int Abnormal_event_no { get; set; }
        //事件案號
        public string Abnormal_event_ID { get; set; }
        //事件主旨
        public string Abnormal_event { get; set; }
        //志工姓名
        public string Volunteer_name { get; set; }
        //運用單位
        public string Application_unit { get; set; }
        //異常事件類別
        public string event_category { get; set; }
        //處理階段
        public string Stage { get; set; }
        //通報時間
        public string Notification_date { get; set; }
        //結案時間
        public string Closing_date { get; set; }
        //志工督導
        public string Supervisor { get; set; }
        //運用單位描述
        public string Unit_descrition { get; set; }
        //志工自述
        public string Volunteer_description { get; set; }
        //志工督導描述
        public string Supervisor_description { get; set; }
        //處理結果
        public string Result { get; set; }
        //志工幹部
        public string Volunteer_leader { get; set; }
        //志工督導主管
        public string Supervisor_heads { get; set; }


        public List<Abnormal_event_Model> SelectAbnormal_event_byStage(string stage,string category,string application_unit,DateTime startdate, DateTime enddate)
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from n1 in dbContext.Abnormal_event
                    join n2 in dbContext.Application_unit
                    on n1.Application_unit_no equals n2.Application_unit_no
                    join n3 in dbContext.Volunteer
                    on n1.Volunteer_no equals n3.Volunteer_no
                    join n4 in dbContext.Stages
                    on n1.Stage equals n4.Stage_ID
                    join n5 in dbContext.Volunteer_supervision
                    on n1.Supervisor_ID equals n5.supervision_ID
                    join n6 in dbContext.event_category
                    on n1.event_category_ID equals n6.event_category_ID
                    join n7 in dbContext.Volunteer
                    on n1.Volunteer_leader_ID equals n7.Volunteer_no
                    where ((stage == "") ? true : n4.Stage1 == stage) &&
                          (n4.Stage_type == "異常事件") &&
                          ((category == "") ? true:n6.event_category1 == category) &&
                          ((application_unit == "") ? true: n2.Application_unit1 == application_unit) &&
                          ((startdate == DateTime.MinValue)? true : n1.Notification_date >= startdate) &&
                          ((enddate == DateTime.MinValue) ? true: n1.Closing_date <= enddate)
                    select new
                    {
                        Abnormal_event_ID = n1.Abnormal_event_ID,
                        Abnormal_event = n1.Abnormal_event1,
                        Volunteer_name = n3.Chinese_name,
                        Application_unit = n2.Application_unit1,
                        event_category = n6.event_category1,
                        Stage = n4.Stage1,
                        Notification_date = n1.Notification_date,
                        Closing_date = n1.Closing_date,
                        Supervisor = n5.supervision_Name,
                        Unit_descrition = n1.Unit_descrition,
                        Volunteer_description = n1.Volunteer_description,
                        Supervisor_description = n1.Supervisor_description,
                        Result = n1.Result,
                        Volunteer_leader = n7.Chinese_name,
                        Supervisor_heads = n1.Supervisor_heads
                    };

            List < Abnormal_event_Model > abnormal_Event_Models = new List<Abnormal_event_Model>();
            foreach (var row in q)
            {
                Abnormal_event_Model abnormal_Event_Model = new Abnormal_event_Model();
                abnormal_Event_Model.Abnormal_event_ID = row.Abnormal_event_ID;
                abnormal_Event_Model.Abnormal_event = row.Abnormal_event;
                abnormal_Event_Model.Volunteer_name = row.Volunteer_name;
                abnormal_Event_Model.Application_unit = row.Application_unit;
                abnormal_Event_Model.event_category = row.event_category;
                abnormal_Event_Model.Stage = row.Stage;
                abnormal_Event_Model.Notification_date = row.Notification_date.ToString();
                abnormal_Event_Model.Closing_date = row.Closing_date.ToString();
                abnormal_Event_Model.Supervisor = row.Supervisor;
                abnormal_Event_Model.Unit_descrition = row.Unit_descrition;
                abnormal_Event_Model.Volunteer_description = row.Volunteer_description;
                abnormal_Event_Model.Supervisor_description = row.Supervisor_description;
                abnormal_Event_Model.Result = row.Result;
                abnormal_Event_Model.Volunteer_leader = row.Volunteer_leader;
                abnormal_Event_Model.Supervisor_heads = row.Supervisor_heads;

                abnormal_Event_Models.Add(abnormal_Event_Model);
            }


            return abnormal_Event_Models;
        }
    }
}
