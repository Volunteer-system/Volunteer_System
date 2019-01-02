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
    /// Activity_data_View.xaml 的互動邏輯
    /// </summary>
    public partial class Activity_data_View : Window
    {
        Activity_AddAction_ViewModel AAVM;
        public Activity_data_View()
        {
            InitializeComponent();
            //AAVM = new Activity_AddAction_ViewModel(2, WrapPanel_Viewphoto);  //資料綁定的方式!!
            //this.DataContext = AAVM;                                          //很重要!!   
        }
        public void AddNewActivity(int supervisionID)
        {
            AAVM = new Activity_AddAction_ViewModel(supervisionID, WrapPanel_Viewphoto);  //資料綁定的方式!!
            this.DataContext = AAVM;
        }
        public void AmendActivity(string activity_name, DateTime activity_starttime)
        {
            AAVM = new Activity_AddAction_ViewModel(activity_name, activity_starttime, WrapPanel_Viewphoto);  //資料綁定的方式!!
            this.DataContext = AAVM;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Activity_name = this.tbx_ActName.Text;
            DateTime Start_date = (DateTime)this.datepicker_start.SelectedDate;

            Window window = new Activity_volunteer_list_View(Activity_name, Start_date);
            window.ShowDialog();
        }
    }
}
