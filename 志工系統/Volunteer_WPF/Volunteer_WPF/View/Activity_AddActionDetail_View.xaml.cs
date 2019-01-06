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
using System.Collections.ObjectModel;

namespace Volunteer_WPF.View
{
    /// <summary>
    /// Activity_AddActionDetail_View.xaml 的互動邏輯
    /// </summary>
    public partial class Activity_AddActionDetail_View : Window
    {

        Activity_AddAction_ViewModel AAVM;
        public Activity_AddActionDetail_View()
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
            this.tbx_ActName.Text = "Demo活動";
            this.tbx_ActPlace.Text = "Demo活動地點";
            this.tbx_ActOrganization.Text = "Demo講師";
            this.tbx_ActTaker.Text = "Demo承辦人";
            this.tbx_ActOrganizationPhone.Text = "Demo承辦電話";
            this.tbx_ActOrganizationEmail.Text = "Demo123456@gmail.com";
            this.tbx_ActCourseMemberMax.Text = "5";
            this.tbx_ActCourseStandbyMax.Text = "2";
            this.tbx_ActSummary.Text = "Demo內容";
        }
        //public WrapPanel getWrapPanel()
        //{
        //    return this.WrapPanel_Viewphoto;
        //}
    }
}
