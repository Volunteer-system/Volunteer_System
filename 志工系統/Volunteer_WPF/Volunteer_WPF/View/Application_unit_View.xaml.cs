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
    /// Application_unit_View.xaml 的互動邏輯
    /// </summary>
    public partial class Application_unit_View : Window
    {
        public Application_unit_View()
        {
            InitializeComponent();

            Application_unit_ViewModel application_Unit_ViewModel = new Application_unit_ViewModel();
            List<string> service_groups = application_Unit_ViewModel.Selectservice_group();
            this.cbb_group.ItemsSource = service_groups;
        }

        private void btn_newApplication_unit_Click(object sender, RoutedEventArgs e)
        {
            Application_unit_data_View application_Unit_Data_View = new Application_unit_data_View("新增");
            application_Unit_Data_View.ShowDialog();
        }

        private void btn_selectApplication_unit_Click(object sender, RoutedEventArgs e)
        {
            string name = this.nametext.Text;
            string group = this.cbb_group.Text;
            int membermin = 0;
            int membermax = 0;
            if (this.members_10.IsChecked == true)
            {
                membermin = 1;
                membermax = 10;
            }
            if (this.members_20.IsChecked == true)
            {
                membermin = 11;
                membermax = 20;
            }
            if (this.members_30.IsChecked == true)
            {
                membermin = 21;
                membermax = 30;
            }
            if (this.members_40.IsChecked == true)
            {
                membermin = 31;
                membermax = 40;
            }
            if (this.members_40up.IsChecked == true)
            {
                membermin = 41;
                membermax = 0;
            }

            Application_unit_ViewModel application_Unit_ViewModel = new Application_unit_ViewModel();
            List<Application_unit_ViewModel> Application_unit_ViewModels = application_Unit_ViewModel.SelectApplication_unit(name, group, membermin, membermax);
            ObservableCollection<application_Unit_list> application_Unit_lists = new ObservableCollection<application_Unit_list>();

            foreach (var row in Application_unit_ViewModels)
            {
                application_Unit_lists.Add(new application_Unit_list()
                {
                    運用單位 = row.Application_unit,
                    組別 = row.Group,
                    運用單位聯絡電話 = row.Application_phone_no,
                    負責人 = row.Principal,
                    負責人聯絡電話 = row.Principal_phone_no,
                    志工總人數 = row.Total_volunteers                    
                });
            }

            this.dg_Application_unit.ItemsSource = application_Unit_lists;
        }

        private void dg_Application_unit_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dg_Application_unit.SelectedItem != null)
            {
                application_Unit_list v = dg_Application_unit.SelectedItem as application_Unit_list;
                string Application_unit = v.運用單位;
                getApplication_unit_detai(Application_unit);
            }                
        }

        public void getApplication_unit_detai(string Application_unit)
        {
            Application_unit_detail_View application_Unit_Detail_View = new Application_unit_detail_View(Application_unit);
            wondow_show.Children.Clear();
            object content = application_Unit_Detail_View.Content;
            application_Unit_Detail_View.Content = null;
            wondow_show.Children.Add(content as UIElement);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (dg_Application_unit.SelectedItem != null)
            {
                application_Unit_list v = dg_Application_unit.SelectedItem as application_Unit_list;
                string Application_unit = v.運用單位;
                Application_unit_data_View application_Unit_date_View = new Application_unit_data_View("修改",Application_unit);
                application_Unit_date_View.ShowDialog();

                getApplication_unit_detai(application_Unit_date_View.Application_unit_name);
            }
            
        }       
    }

    public class application_Unit_list
    {
        public string 運用單位 { get; set; }
        public string 組別 { get; set; }
        public string 運用單位聯絡電話 { get; set; }
        public string 負責人 { get; set; }
        public string 負責人聯絡電話 { get; set; }
        public string 志工總人數 { get; set; }
    }
}
