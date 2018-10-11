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
    /// Signup_data_View.xaml 的互動邏輯
    /// </summary>
    public partial class Signup_data_View : Window
    {
        public Signup_data_View(int Signup_no)
        {
            InitializeComponent();

            Sign_up_ViewModel sign_Up_ViewModel = new Sign_up_ViewModel();
            List<string> Service_period = sign_Up_ViewModel.SelectSign_up_bySignup_no(Signup_no);

            this.Chinese_name_Label.Content = sign_Up_ViewModel.Chinese_name;
            this.English_name_Label.Content = sign_Up_ViewModel.English_name;
            this.Sex_Label.Content = sign_Up_ViewModel.Sex;
            this.Birthday_Label.Content = sign_Up_ViewModel.Birthday;
            this.Sign_up_date_Label.Content = sign_Up_ViewModel.Sign_up_date;
            this.Phone_Label.Content = sign_Up_ViewModel.Phone;
            this.Mobile_Label.Content = sign_Up_ViewModel.Mobile;
            this.Email_Label.Content = sign_Up_ViewModel.Email;
            this.Address_Label.Content = sign_Up_ViewModel.Address;
            this.Education_Label.Content = sign_Up_ViewModel.Education;
            this.Job_Label.Content = sign_Up_ViewModel.Job;
            this.Personality_scale_Label.Content = sign_Up_ViewModel.Personality_scale;
            this.Interview_date_Label.Content = sign_Up_ViewModel.Interview_date;

            //顯示服務意願時間
            foreach (string row in Service_period)
            {
                if (row.Contains("週一"))
                {
                    if (row.Contains("上午"))
                    {
                        this.Mon_am.Content = row.Substring(0, 1);
                    }
                    else if (row.Contains("下午"))
                    {
                        this.Mon_pm.Content = row.Substring(0, 1);
                    }
                    else
                    {
                        this.Mon_night.Content = row.Substring(0, 1);
                    }
                }

                if (row.Contains("週二"))
                {
                    if (row.Contains("上午"))
                    {
                        this.Tue_am.Content = row.Substring(0, 1);
                    }
                    else if (row.Contains("下午"))
                    {
                        this.Tue_pm.Content = row.Substring(0, 1);
                    }
                    else
                    {
                        this.Tue_night.Content = row.Substring(0, 1);
                    }
                }

                if (row.Contains("週三"))
                {
                    if (row.Contains("上午"))
                    {
                        this.Wed_am.Content = row.Substring(0, 1);
                    }
                    else if (row.Contains("下午"))
                    {
                        this.Wed_pm.Content = row.Substring(0, 1);
                    }
                    else
                    {
                        this.Wed_night.Content = row.Substring(0, 1);
                    }
                }

                if (row.Contains("週四"))
                {
                    if (row.Contains("上午"))
                    {
                        this.Thu_am.Content = row.Substring(0, 1);
                    }
                    else if (row.Contains("下午"))
                    {
                        this.Thu_pm.Content = row.Substring(0, 1);
                    }
                    else
                    {
                        this.Thu_night.Content = row.Substring(0, 1);
                    }
                }

                if (row.Contains("週五"))
                {
                    if (row.Contains("上午"))
                    {
                        this.Fri_am.Content = row.Substring(0, 1);
                    }
                    else if (row.Contains("下午"))
                    {
                        this.Fri_pm.Content = row.Substring(0, 1);
                    }
                    else
                    {
                        this.Fri_night.Content = row.Substring(0, 1);
                    }
                }

                if (row.Contains("週六"))
                {
                    if (row.Contains("上午"))
                    {
                        this.Sat_am.Content = row.Substring(0, 1);
                    }                    
                    else
                    {
                        this.Sat_pm.Content = row.Substring(0, 1);
                    }
                }

                if (row.Contains("週日"))
                {
                    if (row.Contains("上午"))
                    {
                        this.Sun_am.Content = row.Substring(0, 1);
                    }
                    else
                    {
                        this.Sun_pm.Content = row.Substring(0, 1);
                    }
                }
            }
        }
        
    }
}
