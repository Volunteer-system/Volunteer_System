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
    /// Abnormal_event.xaml 的互動邏輯
    /// </summary>   

    public partial class Abnormal_event : Window
    {
        enum SelectStage { nostage, newevent, review, processing, endprocessing }
        private SelectStage selectstage = SelectStage.nostage;
        ObservableCollection<abnormal_event_list> abnormal_event_itemlist = new ObservableCollection<abnormal_event_list>();

        public Abnormal_event()
        {
            InitializeComponent();
        }

        private void btn_unprocessed_Click(object sender, RoutedEventArgs e)
        {
            selectstage = SelectStage.newevent;
            Abnormal_event_ViewModel abnormal_Event_ViewModel = new Abnormal_event_ViewModel();
            List<Abnormal_event_ViewModel> abnormal_events = abnormal_Event_ViewModel.SelectAbnormal_Event_byStage("新事件");

            datagrid_show(abnormal_events);
        }

        private void btn_processing_Click(object sender, RoutedEventArgs e)
        {
            selectstage = SelectStage.processing;
            Abnormal_event_ViewModel abnormal_Event_ViewModel = new Abnormal_event_ViewModel();
            List<Abnormal_event_ViewModel> abnormal_events = abnormal_Event_ViewModel.SelectAbnormal_Event_byStage("處理中");

            datagrid_show(abnormal_events);
        }

        private void btn_endprocessing_Click(object sender, RoutedEventArgs e)
        {
            selectstage = SelectStage.endprocessing;
            Abnormal_event_ViewModel abnormal_Event_ViewModel = new Abnormal_event_ViewModel();
            List<Abnormal_event_ViewModel> abnormal_events = abnormal_Event_ViewModel.SelectAbnormal_Event_byStage("事件完成");

            datagrid_show(abnormal_events);
        }
        
        private void btn_review_Click(object sender, RoutedEventArgs e)
        {
            selectstage = SelectStage.review;
            Abnormal_event_ViewModel abnormal_Event_ViewModel = new Abnormal_event_ViewModel();
            List<Abnormal_event_ViewModel> abnormal_events = abnormal_Event_ViewModel.SelectAbnormal_Event_byStage("成案審核");

            datagrid_show(abnormal_events);
        }

        private void dg_abnormal_event_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }

        private void btn_selectabnormal_event_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void datagrid_show(List<Abnormal_event_ViewModel> datagrid_list)
        {
            foreach (var row in datagrid_list)
            {
                abnormal_event_itemlist.Add(new abnormal_event_list()
                {
                    事件案號 = row.Abnormal_event_ID,
                    事件名稱 = row.Abnormal_event,
                    異常事件類別 = row.event_category,
                    志工 = row.Volunteer_name,
                    運用單位 = row.Application_unit,
                    處理階段 = row.Stage,
                    通報時間 = row.Notification_date,
                    結案時間 = row.Closing_date
                });
            }

            this.dg_abnormal_event.ItemsSource = abnormal_event_itemlist;
        }
    }

    public class abnormal_event_list
    {
        public string 事件案號 { get; set; }
        public string 事件名稱 { get; set; }
        public string 異常事件類別 { get; set; }
        public string 志工 { get; set; }
        public string 運用單位 { get; set; }
        public string 處理階段 { get; set; }
        public string 通報時間 { get; set; }
        public string 結案時間 { get; set; }
    }
}
