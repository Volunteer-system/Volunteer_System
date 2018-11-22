using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Volunteer_WPF.View_Model;

namespace Volunteer_WPF.View
{
    /// <summary>
    /// Abnormal_event_analysis_View.xaml 的互動邏輯
    /// </summary>
    public partial class Abnormal_event_analysis_View : Window
    {
        List<string> unit_Names = new List<string>();

        public Abnormal_event_analysis_View()
        {
            InitializeComponent();

            choose_unit_analysis_ViewModel choose_Unit_Analysis_ViewModel = new choose_unit_analysis_ViewModel();
            List<string> event_categorys = choose_Unit_Analysis_ViewModel.Selectevent_category();
            this.cbb_category.ItemsSource = event_categorys;

            this.dp_startdate.SelectedDate = DateTime.Now.Date.AddMonths(-3);
            this.dp_enddate.SelectedDate = DateTime.Now.Date;
        }

        private void btn_selectabnormal_event_Click(object sender, RoutedEventArgs e)
        {            
            DateTime startdate = ((dp_startdate.SelectedDate == null) ? DateTime.MinValue : (DateTime)dp_startdate.SelectedDate);
            DateTime enddate = ((dp_enddate.SelectedDate == null) ? DateTime.MinValue : (DateTime)dp_enddate.SelectedDate);
            string event_category = this.cbb_category.Text;
            Abnormal_event_analysis_ViewModel abnormal_Event_Analysis_ViewModel = new Abnormal_event_analysis_ViewModel();
            List<Abnormal_event_analysis_ViewModel> event_analysiss = abnormal_Event_Analysis_ViewModel.SelectAbnormal_event_analysis(startdate, enddate, event_category, unit_Names);
            ObservableCollection<Abnormal_event_analysiss> abnormal_event_analysislist = new ObservableCollection<Abnormal_event_analysiss>();
            foreach (var row in event_analysiss)
            {
                abnormal_event_analysislist.Add(new Abnormal_event_analysiss {
                    事件案號 = row.Abnormal_event_ID,
                    事件主旨 = row.Abnormal_event,
                    志工姓名 = row.Volunteer_name,
                    運用單位 = row.Application_unit,
                    異常事件類別 = row.Event_category,
                    處理階段 = row.Stage,
                    通報時間 = row.Notification_date,
                    結案時間 = row.Closing_date,
                    志工督導 = row.Supervisor,
                    處理結果 = row.Result,
                    運用單位描述 = row.Unit_descrition,
                    志工自述 = row.Volunteer_description,
                    志工督導描述 = row.Supervisor_description,
                    志工幹部 = row.Volunteer_leader,
                    志工督導主管 = row.Supervisor_heads,
                    駁回原因 = row.Rejection_Reason
                });
            }

            this.dg_abnormal_event.ItemsSource = abnormal_event_analysislist;

            Abnormal_event_analysis_chart_View abnormal_Event_Analysis_Chart_View = new Abnormal_event_analysis_chart_View(abnormal_Event_Analysis_ViewModel);
            //abnormal_Event_Analysis_Chart_View.Show();
            wondow_show.Children.Clear();
            object content = abnormal_Event_Analysis_Chart_View.Content;
            abnormal_Event_Analysis_Chart_View.Content = null;
            wondow_show.Children.Add(content as UIElement);
        }

        private void btn_eventlist_excel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dg_abnormal_event_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }

        private void btn_application_unit_Click(object sender, RoutedEventArgs e)
        {
            choose_unit_analysis_View windows = new choose_unit_analysis_View();
            windows.ShowDialog();
            unit_Names = windows.Application_unit_Names;
            string str = "";
            foreach (var row in unit_Names)
            {
                str += row + ", ";
            }

            this.txt_unit.Text = str;
        }
    }

    class Application_unit_Name
    {
        public string 運用單位 { get; set; }
    }

    class Abnormal_event_analysiss
    {
        public string 事件案號 { get; set; }
        public string 事件主旨 { get; set; }
        public string 志工姓名 { get; set; }
        public string 運用單位 { get; set; }
        public string 異常事件類別 { get; set; }
        public string 處理階段 { get; set; }
        public string 通報時間 { get; set; }
        public string 結案時間 { get; set; }
        public string 志工督導 { get; set; }
        public string 處理結果 { get; set; }
        public string 運用單位描述 { get; set; }
        public string 志工自述 { get; set; }
        public string 志工督導描述 { get; set; }
        public string 志工幹部 { get; set; }
        public string 志工督導主管 { get; set; }
        public string 駁回原因 { get; set; }
    }
}
