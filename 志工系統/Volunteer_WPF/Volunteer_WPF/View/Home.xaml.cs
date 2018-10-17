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
    /// Home.xaml 的互動邏輯
    /// </summary>
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();
            AddSelectedDates();
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

            //今日活動label
            DateTime dt1 = DateTime.Now.Date;
            var q = from n in dbContext.Activities
                    where n.Activity_startdate <= dt1 && n.Activity_enddate >= dt1
                    select n;

            TodayActivityLabel.Content = q.Count();

            if ((int)(TodayActivityLabel.Content) > 0)
            {
                TodayActivityLabel.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                TodayActivityLabel.Foreground = new SolidColorBrush(Colors.White);
            }

            //未審核項目label
            var q1 = from n in dbContext.Sign_up
                     where n.Approval_date == null
                     select n;

            ApprovalLabel.Content = q1.Count();

            if ((int)(ApprovalLabel.Content) > 0)
            {
                ApprovalLabel.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                ApprovalLabel.Foreground = new SolidColorBrush(Colors.White);
            }
            
        }
    }
}
