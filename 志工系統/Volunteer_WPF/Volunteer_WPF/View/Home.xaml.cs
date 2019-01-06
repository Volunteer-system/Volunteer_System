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
using System.Windows.Threading;
using Volunteer_WPF.Model;

namespace Volunteer_WPF.View
{
    /// <summary>
    /// Home.xaml 的互動邏輯
    /// </summary>
    public partial class Home : Window
    {
        private DispatcherTimer dispatcherTimer;

        public Home()
        {
            InitializeComponent();
            AddSelectedDates();
            Today();
            AddLabel();

            //計時每五分鐘重新搜尋一次
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            Today();
        }

        private void AddSelectedDates()
        {
            Activity_Model activity_Model = new Activity_Model();

            DateTime SrartDate = DateTime.Now.Date;
            DateTime EndDate = DateTime.Now.AddDays(31);
            List<DateTime> dateTimes = activity_Model.SelectActivity_bydatetime(SrartDate, EndDate);

            for (int i = 0; i < dateTimes.Count; i++)
            {               
                Calendar1.SelectedDates.Add(dateTimes[i]);
            }
        }

        private void Today()
        {
            VolunteerEntities dbContext = new VolunteerEntities();

            //未審核項目label
            var q1 = from n in dbContext.Sign_up
                     join n2 in dbContext.Stages
                     on n.Stage equals n2.Stage_ID
                     where n2.Stage1 == "初次申請" &&
                           n2.Stage_type == "申請階段"
                     select n;

            label_Approval.Content = q1.Count();

            if ((int)(label_Approval.Content) > 0)
            {
                label_Approval.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                label_Approval.Foreground = new SolidColorBrush(Colors.White);
            }

            //未處理異常事件處理
            var q2 = from n in dbContext.Abnormal_event
                     where n.Stage == 8
                     select n;

            label_Abnormal_event.Content = q2.Count();

            if ((int)(label_Abnormal_event.Content) > 0)
            {
                label_Abnormal_event.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                label_Abnormal_event.Foreground = new SolidColorBrush(Colors.White);
            }

            //今日活動label
            DateTime dt1 = DateTime.Now.Date;
            var q = from n in dbContext.Activities
                    where n.Activity_startdate <= dt1 && n.Activity_enddate >= dt1
                    select n;

            label_TodayActivity.Content = q.Count();

            if ((int)(label_TodayActivity.Content) > 0)
            {
                label_TodayActivity.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                label_TodayActivity.Foreground = new SolidColorBrush(Colors.White);
            }

            //年度申請申請階段
            var q3 = from n1 in dbContext.Manpower_apply
                     join n2 in dbContext.Stages
                     on n1.Apply_state equals n2.Stage_ID
                     where n2.Stage1 == "新申請" &&
                           n2.Stage_type == "人力申請" &&
                           n1.Apply_type == "年度申請"
                     select n1;

            label_apply_year.Content = q3.Count();
            
            if ((int)(label_apply_year.Content) > 0)
            {
                label_apply_year.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                label_apply_year.Foreground = new SolidColorBrush(Colors.White);
            }

            var q4 = from n1 in dbContext.Manpower_apply
                     join n2 in dbContext.Stages
                     on n1.Apply_state equals n2.Stage_ID
                     where n2.Stage1 == "新申請" &&
                           n2.Stage_type == "人力申請" &&
                           n1.Apply_type == "臨時申請"
                     select n1;

            label_apply_temporary.Content = q4.Count();

            if ((int)(label_apply_temporary.Content) > 0)
            {
                label_apply_temporary.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                label_apply_temporary.Foreground = new SolidColorBrush(Colors.White);
            }
        }



        private void AddLabel() //加入行事曆細項(未來7天)
        {
            VolunteerEntities dbContext = new VolunteerEntities();


            DateTime dt1 = DateTime.Now.Date.AddDays(7);                           
            var q = from a in dbContext.Activities
                    where (a.Activity_startdate <= dt1 && a.Activity_startdate>=DateTime.Now)||(a.Activity_enddate<=dt1&&a.Activity_enddate>=DateTime.Now)
                    select a;

            int WeekActivity = q.Count(); //7天內活動數           
            Activity_Model activity_Model = new Activity_Model();
            
            
            foreach (var a in q)
            {
                
                ActivityPanel.Children.Add(new Label
                {
                    Content =a.Activity_startdate?.ToShortDateString()+"—"+a.Activity_enddate?.ToShortDateString()+"   "+a.Activity_name
                    });
            }
        }

    }
}
