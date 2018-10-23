using System;
using System.Collections.Generic;
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
    /// Abnormal_event_detail.xaml 的互動邏輯
    /// </summary>
    public partial class Abnormal_event_detail : Window
    {
        public Abnormal_event_detail(string event_ID)
        {
            InitializeComponent();

            Abnormal_event_ViewModel abnormal_Event_ViewModel = new Abnormal_event_ViewModel();
            abnormal_Event_ViewModel.SelectAbnormal_event_byAbnormal_event_ID(event_ID);
            this.lab_abnormal_event_ID.Content = abnormal_Event_ViewModel.Abnormal_event_ID;
            this.lab_supervisor.Content = abnormal_Event_ViewModel.Supervisor;
            this.lab_abnormal_event.Content = abnormal_Event_ViewModel.Abnormal_event;
            this.lab_supervisor_heads.Content = abnormal_Event_ViewModel.Supervisor_heads;
            this.lab_event_category.Content = abnormal_Event_ViewModel.event_category;
            this.lab_Volunteer_leader.Content = abnormal_Event_ViewModel.Volunteer_leader;
            this.lab_volunteer_name.Content = abnormal_Event_ViewModel.Volunteer_name;
            this.lab_application_unit.Content = abnormal_Event_ViewModel.Application_unit;
            this.lab_stage.Content = abnormal_Event_ViewModel.Stage;
            this.lab_notification_date.Content = abnormal_Event_ViewModel.Notification_date;
            this.lab_closing_date.Content = abnormal_Event_ViewModel.Closing_date;
            this.lab_result.Content = abnormal_Event_ViewModel.Result;
            this.txt_unit_descrition.Text = abnormal_Event_ViewModel.Unit_descrition;
            this.txt_volunteer_description.Text = abnormal_Event_ViewModel.Volunteer_description;
            this.txt_supervisor_description.Text = abnormal_Event_ViewModel.Supervisor_description;
            this.txt_rejection_reason.Text = abnormal_Event_ViewModel.Rejection_Reason;
        }
    }
}
