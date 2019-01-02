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
    /// Activity_View.xaml 的互動邏輯
    /// </summary>
    public partial class Activity_View : Window
    {
        public Activity_View()
        {
            InitializeComponent();

            Activity_type_ViewModel activity_Type_ViewModel = new Activity_type_ViewModel();
            List<string> activity_Types = activity_Type_ViewModel.SelectActivity_type();
            this.cbb_activitytype.ItemsSource = activity_Types;

            //foreach (var row in activity_Types)
            //{
            //    this.cbb_activitytype.Items.Add(row);
            //}

            Service_group_ViewModel service_Group_ViewModel = new Service_group_ViewModel();
            List<string> service_Groups = service_Group_ViewModel.SelectService_group();
            this.cbb_activitygroup.ItemsSource = service_Groups;

            //foreach (var row in service_Groups)
            //{
            //    this.cbb_activitygroup.Items.Add(row);
            //}
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (dg_activity.SelectedItem != null)
            {
                string activity_name = (this.dg_activity.SelectedItem as activity_list).活動名稱;
                DateTime activity_startdate = Convert.ToDateTime((this.dg_activity.SelectedItem as activity_list).活動起始時間);

                //Activity_AddActionDetail_View activity_addaction = new Activity_AddActionDetail_View();
                Activity_data_View activity_Data_View = new Activity_data_View();
                activity_Data_View.AmendActivity(activity_name, activity_startdate);
                activity_Data_View.ShowDialog();
            }
            
        }

        private void btn_selectactivity_Click(object sender, RoutedEventArgs e)
        {
            Activity_ViewModel activity_ViewModel = new Activity_ViewModel();
            
            DateTime startdate = ((dp_startdate.SelectedDate == null) ? DateTime.Parse("2000/01/01") : (DateTime)dp_startdate.SelectedDate);            
            DateTime enddate = ((dp_startdate.SelectedDate == null) ? DateTime.Parse("3000/01/01") : (DateTime)dp_enddate.SelectedDate);
            string activitytype = cbb_activitytype.Text;
            string activitygroup = cbb_activitygroup.Text;
            List<Activity_ViewModel> activity_viewmodels = activity_ViewModel.SelectActivity_byActivity_no(startdate, enddate, activitytype, activitygroup);

            ObservableCollection<activity_list> activity_itemlist = new ObservableCollection<activity_list>();
            foreach (var row in activity_viewmodels)
            {
                activity_itemlist.Add(new activity_list() { 活動名稱 = row.Activity_name, 活動類別 =row.Activity_type, 組別  = row.Group, 活動起始時間 = row.Activity_startdate, 活動結束時間  = row.Activity_enddate, 課程人數 = row.Member});
            }

            this.dg_activity.ItemsSource = activity_itemlist;
        }

        private void btn_addactivity_Click(object sender, RoutedEventArgs e)
        {
            Activity_AddActionDetail_View activity_AddActionDetail_View = new Activity_AddActionDetail_View();
            activity_AddActionDetail_View.AddNewActivity(Account_ViewModel.User_ID);
            activity_AddActionDetail_View.ShowDialog();
        }

        private void dg_activity_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dg_activity.SelectedItem != null)
            {
                activity_list v = dg_activity.SelectedItem as activity_list;
                string activity_name = v.活動名稱;
                DateTime activity_startdate = Convert.ToDateTime(v.活動起始時間);
                getactivity_detail_view(activity_name, activity_startdate);

            }            
        }

        public void getactivity_detail_view(string Activity_name, DateTime startdate)
        {
            Activity_detail_View activity_detail_View = new Activity_detail_View(Activity_name, startdate);
            wondow_show.Children.Clear();
            object content = activity_detail_View.Content;
            activity_detail_View.Content = null;
            wondow_show.Children.Add(content as UIElement);
        }
    }

    public class activity_list
    {
        public string 活動名稱 { get; set; }
        public string 活動類別 { get; set; }
        public string 組別 { get; set; }
        public string 活動起始時間 { get; set; }
        public string 活動結束時間 { get; set; }
        public string 課程人數 { get; set; }
    }
}
