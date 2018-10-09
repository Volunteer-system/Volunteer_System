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
        public Signup_data_View()
        {
            InitializeComponent();

            Sign_up_ViewModel sign_Up_ViewModel = new Sign_up_ViewModel();
            sign_Up_ViewModel.SelectSign_up_bySignup_no(1);

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

        }
    }
}
