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

namespace Volunteer_WPF.View
{
    /// <summary>
    /// Activity_volunteer_list_View.xaml 的互動邏輯
    /// </summary>
    public partial class Activity_volunteer_list_View : Window
    {
        List<string> insert_list = new List<string>();
        List<string> delete_list = new List<string>();

        public Activity_volunteer_list_View()
        {
            InitializeComponent();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            this.txt_Addvolunteer.Visibility = Visibility.Visible;
            this.btn_Addvolunteer.Visibility = Visibility.Visible;
        }

        private void CB_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void CB_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.txt_Addvolunteer.Visibility = Visibility.Collapsed;
            this.btn_Addvolunteer.Visibility = Visibility.Collapsed;
        }
    }
}
