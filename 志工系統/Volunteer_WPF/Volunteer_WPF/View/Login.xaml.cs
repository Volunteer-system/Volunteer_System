using System;
using System.Collections.Generic;
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
    /// Login.xaml 的互動邏輯
    /// </summary>
    public partial class Login : Window
    {
        public bool login_ok;

        public Login()
        {
            InitializeComponent();

            this.Closed += Login_Closed;        }

        private void Login_Closed(object sender, EventArgs e)
        {
            if (!login_ok)
            {
                login_ok = false;
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Account_number = this.Account_number_TextBox.Text;
            string Password = this.Password_TextBox.Text;

            bool login_pass = Account_ViewModel.Selectaccount(Account_number, Password);

            if (login_pass == true)
            {
                login_ok = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("請檢查帳號密碼");
                return;
            }            
        }
    }
}
