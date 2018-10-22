using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer_WPF.Model;

namespace Volunteer_WPF.View_Model
{
    class Abnormal_event_ViewModel
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

        public List<string> Selectevent_category()
        {
            Event_category_Model event_Category_Model = new Event_category_Model();
            List<string> event_categorys = event_Category_Model.Selectevent_category();

            return event_categorys;
        }

        public List<string> SelectApplication_unit_name ()
        {
            Application_unit_Model application_unit_Model = new Application_unit_Model();
            List<string> application_units = application_unit_Model.SelectApplication_unit_name();

            return application_units;
        }

        public List<Abnormal_event_ViewModel> SelectAbnormal_Event_byStage(string stage, string category, string application_unit, DateTime startdate, DateTime enddate)
        {
            Abnormal_event_Model abnormal_Event_Model = new Abnormal_event_Model();
            List<Abnormal_event_Model> abnormal_Event_Models = abnormal_Event_Model.SelectAbnormal_event_byStage(stage, category, application_unit, startdate, enddate);

            List<Abnormal_event_ViewModel> abnormal_Event_ViewModels = new List<Abnormal_event_ViewModel>();
            foreach (var row in abnormal_Event_Models)
            {
                Abnormal_event_ViewModel abnormal_Event_ViewModel = new Abnormal_event_ViewModel();
                abnormal_Event_ViewModel.Abnormal_event_no = row.Abnormal_event_no;
                abnormal_Event_ViewModel.Abnormal_event_ID = row.Abnormal_event_ID;
                abnormal_Event_ViewModel.Abnormal_event = row.Abnormal_event;
                abnormal_Event_ViewModel.Volunteer_name = row.Volunteer_name;
                abnormal_Event_ViewModel.Application_unit = row.Application_unit;
                abnormal_Event_ViewModel.event_category = row.event_category;
                abnormal_Event_ViewModel.Stage = row.Stage;
                abnormal_Event_ViewModel.Notification_date = row.Notification_date;
                abnormal_Event_ViewModel.Closing_date = row.Closing_date;
                abnormal_Event_ViewModel.Supervisor = row.Supervisor;
                abnormal_Event_ViewModel.Unit_descrition = row.Unit_descrition;
                abnormal_Event_ViewModel.Volunteer_description = row.Volunteer_description;
                abnormal_Event_ViewModel.Supervisor_description = row.Supervisor_description;
                abnormal_Event_ViewModel.Result = row.Result;
                abnormal_Event_ViewModel.Volunteer_leader = row.Volunteer_leader;
                abnormal_Event_ViewModel.Supervisor_heads = row.Supervisor_heads;

                abnormal_Event_ViewModels.Add(abnormal_Event_ViewModel);
            }
            
            return abnormal_Event_ViewModels;
        }
    }
}
