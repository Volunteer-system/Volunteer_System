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
    /// Manpower_apply_View.xaml 的互動邏輯
    /// </summary>
    public partial class Manpower_apply_View : Window
    {
        string Stage = "";
        string apply_checked = "年度申請";

        public Manpower_apply_View()
        {
            InitializeComponent();
        }

        private void btn_selectabnormal_event_Click(object sender, RoutedEventArgs e)
        {
            Select(Stage);
        }

        private void rbtn_year_Checked(object sender, RoutedEventArgs e)
        {
            apply_checked = "年度申請";
        }

        private void rbtn_temporary_Checked(object sender, RoutedEventArgs e)
        {
            apply_checked = "臨時申請";
        }

        private void btn_endprocessing_Click(object sender, RoutedEventArgs e)
        {
            Stage = "申請完成";
            Select(Stage);
        }

        private void btn_rejectapply_Click(object sender, RoutedEventArgs e)
        {
            Stage = "申請駁回";
            Select(Stage);
        }

        private void btn_newapply_Click(object sender, RoutedEventArgs e)
        {
            Stage = "新申請";
            Select(Stage);
        }

        private void btn_processing_Click(object sender, RoutedEventArgs e)
        {
            Stage = "處理中";
            Select(Stage);
        }
               
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int apply_id = (this.dg_manpower_apply.SelectedItem as manpower_apply_list).申請ID;
            string type = "";
            if (Stage == "新申請" && !string.IsNullOrEmpty(Stage))
            {
                type = "新申請";
            }
            else
            {
                type = "修改";
            }

            Window window = new Manpower_apply_data_View(type, apply_id);
            window.ShowDialog();
        }

        private void Select(string stage)
        {
            DateTime startdate = ((dp_startdate.SelectedDate == null) ? DateTime.MinValue : (DateTime)dp_startdate.SelectedDate);
            DateTime enddate = ((dp_enddate.SelectedDate == null) ? DateTime.MinValue : (DateTime)dp_enddate.SelectedDate);
            ObservableCollection<manpower_apply_list> manpower_apply_itemlist = new ObservableCollection<manpower_apply_list>();

            Manpower_apply_ViewModel manpower_Apply_ViewModel = new Manpower_apply_ViewModel();
            List<Manpower_apply_ViewModel> Manpower_applys = manpower_Apply_ViewModel.SelectManpower_apply_byStage(stage, apply_checked, startdate, enddate);

            foreach (var row in Manpower_applys)
            {
                manpower_apply_itemlist.Add(new manpower_apply_list {
                    申請ID =  row.Apply_ID,
                    運用單位 = row.Application_unit,
                    申請人 = row.Applicant,
                    申請日期 = row.Apply_date,
                    申請人電話 = row.Applicant_phone,
                    申請人數 = row.Application_number,
                    志工督導 = row.Supervision,
                    評估日期 = row.Apply_date,
                    核定人數 = row.Reply_number
                });
            }

            this.dg_manpower_apply.ItemsSource = manpower_apply_itemlist;
            if (Stage == "")
            {
                this.dg_manpower_apply.Columns[0].Visibility = Visibility.Collapsed;
                this.dg_manpower_apply.Columns[1].Visibility = Visibility.Collapsed;
            }
            else
            {
                this.dg_manpower_apply.Columns[1].Visibility = Visibility.Collapsed;
            }
        }

        private void dg_manpower_apply_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dg_manpower_apply.SelectedItem != null)
            {
                manpower_apply_list v = dg_manpower_apply.SelectedItem as manpower_apply_list;
                int Apply_ID = v.申請ID;
                getManpower_apply(Apply_ID);

            }
        }

        private void getManpower_apply(int Apply_ID)
        {
            Manpower_apply_detail_View Manpower_apply_detail = new Manpower_apply_detail_View(Apply_ID, Stage);
            wondow_show.Children.Clear();
            object content = Manpower_apply_detail.Content;
            Manpower_apply_detail.Content = null;
            wondow_show.Children.Add(content as UIElement);
        }
    }

    public class manpower_apply_list
    {
        public int 申請ID { get; set; }
        public string 運用單位 { get; set; }
        public string 申請人 { get; set; }
        public string 申請日期 { get; set; }
        public string 申請人電話 { get; set; }
        public string 申請人數 { get; set; }
        public string 志工督導 { get; set; }
        public string 評估日期 { get; set; }
        public string 核定人數 { get; set; }
    }
}
