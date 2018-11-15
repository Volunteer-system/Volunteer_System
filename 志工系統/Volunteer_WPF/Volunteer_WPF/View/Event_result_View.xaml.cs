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
using Volunteer_WPF.Model;
using Volunteer_WPF.View_Model;

namespace Volunteer_WPF.View
{
    /// <summary>
    /// Event_result.xaml 的互動邏輯
    /// </summary>
    public partial class Event_result : Window
    {
        string Event_ID;


        public Event_result()
        {           
            InitializeComponent();
           
        }

        public Event_result(string r)
        {
            InitializeComponent();



            Event_ID = r;
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from a in dbContext.Abnormal_event
                    where a.Abnormal_event_ID == Event_ID
                    select a;

            foreach (var a in q)
            {
                Lab_event_no.Content = a.Abnormal_event_ID; //事件案號
                Lab_event_name.Content = a.Abnormal_event1; //事件名字(主旨?)
                Lab_event_type.Content = a.event_category.event_category1; //事件類別
                Lab_volunteer.Content = a.Volunteer.Chinese_name; //志工中文姓名
                Lab_application_unit.Content =a.Application_unit.Application_unit1; //運用單位編號
                Lab_notification_date.Content =a.Notification_date; //通報時間
                Lab_closing_date.Content =a.Closing_date; //結案時間
                tb_Volunteer.Text = a.Volunteer_description; //志工自述
                tb_Supervisor.Text = a.Supervisor_description; //督導敘述
                tb_result.Text = a.Result; //處理結果

            }


            var q1 = from a in dbContext.Abnormal_event
                     join s in dbContext.Stages on a.Stage equals s.Stage_ID
                     where a.Abnormal_event_ID == Event_ID
                     select new { s.Stage1 };

            foreach (var a in q1)
            {
                Lab_stage.Content = a.Stage1;//處理階段
            }
        }

        private void btn_send_Click(object sender, RoutedEventArgs e)
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from a in dbContext.Abnormal_event
                    where a.Abnormal_event_ID == Event_ID
                    select a;

            if (this.tb_Volunteer.Text.Trim() == "" || this.tb_Supervisor.Text.Trim() == "" || (this.tb_result.Text.Trim() == ""))
            {
                MessageBox.Show("不可空白");
            }
            else
            {
                if (MessageBox.Show("確定要送出嗎？", "送出", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    foreach (var a in q)
                    {
                        a.Stage = 11;
                        a.Volunteer_description = this.tb_Volunteer.Text;
                        a.Supervisor_description = this.tb_Supervisor.Text;
                        a.Result = this.tb_result.Text;
                        a.Closing_date = DateTime.Now;
                    }
                    dbContext.SaveChanges();
                    this.Close();
                }
            }
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
