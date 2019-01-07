using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer_WPF.Model;

namespace Volunteer_WPF.View_Model
{
    class Abnormal_event_analysis_ViewModel
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
        public string Event_category { get; set; }
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
        //駁回原因
        public string Rejection_Reason { get; set; }
        //異常事件類別統計結果
        public List<Compute_Eventtype> Compute_Eventtypes { get; set; }
        //異常事件單位統計結果
        public List<Compute_Applicationunit> Compute_Applicationunits { get; set; }
        //異常事件月份統計結果
        public List<Compute_Eventmonth> Compute_Eventmonths { get; set; }

        public List<Abnormal_event_analysis_ViewModel> SelectAbnormal_event_analysis(DateTime startdate, DateTime enddate, string event_category, List<string> unit_lists)
        {
            Abnormal_event_Model abnormal_Event_Model = new Abnormal_event_Model();
            List<Abnormal_event_Model> abnormal_Events = abnormal_Event_Model.SelectAbnormal_event_byDate(startdate, enddate, event_category, unit_lists);
            List<Abnormal_event_analysis_ViewModel> abnormal_Event_Analysis_ViewModels = new List<Abnormal_event_analysis_ViewModel>();
            foreach (var row in abnormal_Events)
            {
                Abnormal_event_analysis_ViewModel abnormal_Event_Analysis_ViewModel = new Abnormal_event_analysis_ViewModel();
                abnormal_Event_Analysis_ViewModel.Abnormal_event_no = row.Abnormal_event_no;
                abnormal_Event_Analysis_ViewModel.Abnormal_event_ID = row.Abnormal_event_ID;
                abnormal_Event_Analysis_ViewModel.Abnormal_event = row.Abnormal_event;
                abnormal_Event_Analysis_ViewModel.Volunteer_name = row.Volunteer_name;
                abnormal_Event_Analysis_ViewModel.Application_unit = row.Application_unit;
                abnormal_Event_Analysis_ViewModel.Event_category = row.event_category;
                abnormal_Event_Analysis_ViewModel.Stage = row.Stage;
                abnormal_Event_Analysis_ViewModel.Notification_date = row.Notification_date;
                abnormal_Event_Analysis_ViewModel.Closing_date = row.Closing_date;
                abnormal_Event_Analysis_ViewModel.Supervisor = row.Supervisor;
                abnormal_Event_Analysis_ViewModel.Unit_descrition = row.Unit_descrition;
                abnormal_Event_Analysis_ViewModel.Volunteer_description = row.Volunteer_description;
                abnormal_Event_Analysis_ViewModel.Supervisor_description = row.Supervisor_description;
                abnormal_Event_Analysis_ViewModel.Result = row.Result;
                abnormal_Event_Analysis_ViewModel.Volunteer_leader = row.Volunteer_leader;
                abnormal_Event_Analysis_ViewModel.Supervisor_heads = row.Supervisor_heads;
                abnormal_Event_Analysis_ViewModel.Rejection_Reason = row.Rejection_Reason;

                abnormal_Event_Analysis_ViewModels.Add(abnormal_Event_Analysis_ViewModel);
            }
            //異常事件類別統計
            Compute_Event_type(abnormal_Event_Analysis_ViewModels);
            Compute_Application_unit(abnormal_Event_Analysis_ViewModels, unit_lists);
            Compute_Event_month(abnormal_Event_Analysis_ViewModels, startdate, enddate);

            return abnormal_Event_Analysis_ViewModels;
        }

        public void Compute_Event_type(List<Abnormal_event_analysis_ViewModel> abnormal_Event_Analysis_ViewModels)
        {
            Event_category_Model event_Category_Model = new Event_category_Model();
            List<string> event_Categorys = event_Category_Model.Selectevent_category();
            List<Compute_Eventtype> compute_Eventtypes = new List<Compute_Eventtype>();
            foreach (var row1 in event_Categorys)
            {
                int count = 0;
                foreach (var row2 in abnormal_Event_Analysis_ViewModels)
                {
                    if (row1 == row2.Event_category)
                    {
                        count += 1;
                    }
                }
                Compute_Eventtype compute_Eventtyp = new Compute_Eventtype();
                compute_Eventtyp.event_Category = row1;
                compute_Eventtyp.event_count = count;
                compute_Eventtypes.Add(compute_Eventtyp);
            }

            Compute_Eventtypes = compute_Eventtypes;
        }

        public void Compute_Application_unit(List<Abnormal_event_analysis_ViewModel> abnormal_Event_Analysis_ViewModels, List<string> unit_lists)
        {
            List<Compute_Applicationunit> compute_Applicationunits = new List<Compute_Applicationunit>();
            foreach (var row1 in unit_lists)
            {
                int count = 0;
                foreach (var row2 in abnormal_Event_Analysis_ViewModels)
                {
                    if (row1 == row2.Application_unit)
                    {
                        count += 1;
                    }
                }
                Compute_Applicationunit compute_Applicationunit = new Compute_Applicationunit();
                compute_Applicationunit.Application_unit = row1;
                compute_Applicationunit.event_count = count;
                compute_Applicationunits.Add(compute_Applicationunit);
            }
            Compute_Applicationunits = compute_Applicationunits;
        }

        public void Compute_Event_month(List<Abnormal_event_analysis_ViewModel> abnormal_Event_Analysis_ViewModels, DateTime startdate, DateTime enddate)
        {
            int _month = 12*(enddate.Year - startdate.Year) + (enddate.Month - startdate.Month);
            int month = startdate.Month;
            List<Compute_Eventmonth> compute_Eventmonths = new List<Compute_Eventmonth>();
            for (int i = 0; i <= _month; i++)
            {
                int count = 0;
                foreach (var row in abnormal_Event_Analysis_ViewModels)
                {
                    DateTime event_month = Convert.ToDateTime(row.Notification_date);
                    if (month == event_month.Month)
                    {
                        count += 1;
                    }
                }
                Compute_Eventmonth compute_Eventmonth = new Compute_Eventmonth();
                compute_Eventmonth.Month = month;
                compute_Eventmonth.event_count = count;
                compute_Eventmonths.Add(compute_Eventmonth);

                if (month == 12)
                {
                    month = 1;
                }
                else
                {
                    month++;
                }
            }
            Compute_Eventmonths = compute_Eventmonths;
        }
    }

    class Compute_Eventtype
    {
        public string event_Category { get; set; }
        public int event_count { get; set; }
    }

    class Compute_Applicationunit
    {
        public string Application_unit { get; set; }
        public int event_count { get; set; }
    }

    class Compute_Eventmonth
    {
        public int Month { get; set; }
        public int event_count { get; set; }
    }
}
