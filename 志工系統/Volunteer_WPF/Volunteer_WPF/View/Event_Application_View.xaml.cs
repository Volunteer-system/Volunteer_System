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


namespace Volunteer_WPF.View
{
    /// <summary>
    /// Event_Application.xaml 的互動邏輯
    /// </summary>
    public partial class Event_Application : Window
    {
        string Event_ID;

        public Event_Application()
        {
            InitializeComponent();
        }

        public Event_Application(string r)
        {
            InitializeComponent();



            Event_ID = r;
            VolunteerEntities dbContext = new VolunteerEntities();

            var q1 = from a in dbContext.Abnormal_event join
                    s in dbContext.Stages on a.Stage equals s.Stage_ID
                    where a.Abnormal_event_ID == Event_ID
                    select new {s.Stage1 };

            foreach (var a in q1)
            {
                Lab_stage.Content = a.Stage1;//處理階段
            }

                var q = from a in dbContext.Abnormal_event 
                    where a.Abnormal_event_ID == Event_ID
                    select a;

            foreach (var a in q)
            {
                Lab_event_no.Content = a.Abnormal_event_ID; //事件案號
                Lab_event_name.Content = a.Abnormal_event1; //事件名字(主旨?)
                Lab_event_type.Content = a.event_category.event_category1; //事件類別
                Lab_volunteer.Content = a.Volunteer.Chinese_name; //志工中文姓名
                Lab_application_unit.Content = a.Application_unit.Application_unit1; //運用單位編號
                //Lab_stage.Content = a.Stage; //處理階段
                Lab_notification_date.Content = a.Notification_date; //通報時間

            }
            
            if((Lab_stage.Content).ToString()== "成案審核")
            {
                Btn_pass.Visibility = Visibility.Hidden;
                Btn_pass_second.Visibility = Visibility.Visible;
            }
        }

        private void Btn_nopass_Click(object sender, RoutedEventArgs e)
        {
            this.nopass_textbox.Visibility = Visibility.Visible;
            this.Lab_nopass.Visibility = Visibility.Visible;
            this.Btn_pass.Visibility = Visibility.Hidden;
            this.Btn_nopass.Visibility=Visibility.Hidden;
            this.Btn_cancel.Visibility = Visibility.Visible;
            this.Btn_send.Visibility = Visibility.Visible;

        }

        private void Btn_pass_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("確定成案?", "提示", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {

                VolunteerEntities dbContext = new VolunteerEntities();
                var q = from a in dbContext.Abnormal_event
                        where a.Abnormal_event_ID == Event_ID
                        select a;
                foreach (var a in q)
                {
                    a.Rejection_Reason = "NULL";
                    a.Stage = 9;
                }
                dbContext.SaveChanges();
                this.Close();
            }
        }

        private void Btn_send_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("確定駁回?", "提示", MessageBoxButton.OKCancel);
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from a in dbContext.Abnormal_event
                    where a.Abnormal_event_ID == Event_ID
                    select a;
            
            if(this.nopass_textbox.Text.Trim()=="")
            {
                System.Windows.MessageBox.Show("請填寫駁回原因");
            }
            else
            {
                if (result == MessageBoxResult.OK)
                {
                    foreach (var a in q)
                    {
                        a.Rejection_Reason = this.nopass_textbox.Text;
                        a.Stage = 7;
                    }
                    dbContext.SaveChanges();
                    this.Close();
                }
            }
            

        }

        private void Btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            if ((Lab_stage.Content).ToString() != "成案審核")
            {
                this.Btn_pass.Visibility = Visibility.Visible;
            }
            this.Btn_nopass.Visibility = Visibility.Visible;
            this.nopass_textbox.Visibility = Visibility.Hidden;
            this.Lab_nopass.Visibility = Visibility.Hidden;
            this.Btn_cancel.Visibility = Visibility.Hidden;
            this.Btn_send.Visibility = Visibility.Hidden;
        }

        private void Btn_pass_second_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("確定送出嗎?", "提示", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                VolunteerEntities dbContext = new VolunteerEntities();
                var q = from a in dbContext.Abnormal_event
                        where a.Abnormal_event_ID == Event_ID
                        select a;

                foreach (var a in q)
                {                    
                    a.Stage = 10;
                }
                dbContext.SaveChanges();
                this.Close();
            }
        }
    }
}
