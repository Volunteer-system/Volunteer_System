using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Drawing;
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
    /// Activity_detail_View.xaml 的互動邏輯
    /// </summary>
    public partial class Activity_detail_View : Window
    {
        public Activity_detail_View(int Activity_no)
        {
            InitializeComponent();

            Activity_ViewModel activity_ViewModel = new Activity_ViewModel();
            List<System.Drawing.Image> activity_photos = activity_ViewModel.SelectActivity_byActivity_no(Activity_no);
            this.Lab_activity_name.Content = activity_ViewModel.Activity_name;
            this.Lab_activity_type.Content = activity_ViewModel.Activity_type;
            this.Lab_group.Content = activity_ViewModel.Group;
            this.Lab_activity_startdate.Content = activity_ViewModel.Activity_startdate;
            this.Lab_activity_enddate.Content = activity_ViewModel.Activity_enddate;
            this.Lab_undertake_unit.Content = activity_ViewModel.Undertake_unit;
            this.Lab_undertake.Content = activity_ViewModel.Undertaker;
            this.Lab_undertake_phone.Content = activity_ViewModel.Undertake_phone;
            this.Lab_undertake_email.Content = activity_ViewModel.Undertake_email;
            this.Lab_lecturer.Content = activity_ViewModel.Lecturer;
            this.Lab_Member.Content = activity_ViewModel.Member;
            this.Lab_Spare.Content = activity_ViewModel.Spare;
            this.Lab_Place.Content = activity_ViewModel.Place;


        }
    }
}
