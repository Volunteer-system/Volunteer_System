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
    /// Volunteer_detail.xaml 的互動邏輯
    /// </summary>
    public partial class Volunteer_detail : Window
    {
        public Volunteer_detail()
        {
            InitializeComponent();

            Volunteer_ViewModel volunteer_ViewModel = new Volunteer_ViewModel();
            volunteer_ViewModel.SelectVolunteer_byVolunteerno(1);

            this.Chinese_name_Label.Content = volunteer_ViewModel.Chinese_name;
            this.English_name_Label.Content = volunteer_ViewModel.English_name;
            this.Sex_Label.Content = volunteer_ViewModel.Sex;
            this.Birthday_Label.Content = volunteer_ViewModel.Birthday;
            this.IDcrad_Label.Content = volunteer_ViewModel.IDcrad_no;
            this.Medical_record_no_Label.Content = volunteer_ViewModel.Medical_record_no;
            this.Identity_type_Label.Content = volunteer_ViewModel.Identity_type;
            this.Seniority_Label.Content = volunteer_ViewModel.Seniority;
            this.Join_date_Label.Content = volunteer_ViewModel.Join_date;
            this.Leave_date_Label.Content = volunteer_ViewModel.Leave_date;
            this.Leave_reason_Label.Content = volunteer_ViewModel.Leave_reason;
            this.Phone_no_Label.Content = volunteer_ViewModel.Phone_no;
            this.Mobile_no_Label.Content = volunteer_ViewModel.Mobile_no;
            this.Vest_no_Label.Content = volunteer_ViewModel.Vest_no;
            this.Postal_code_Label.Content = volunteer_ViewModel.Postal_code;
            this.Address_Label.Content = volunteer_ViewModel.Address;
            this.Education_Label.Content = volunteer_ViewModel.Education;
            this.Lssuing_unit_Label.Content = volunteer_ViewModel.Lssuing_unit;
            this.Service_manual_no_Label.Content = volunteer_ViewModel.Service_manual_no;
            this.Personality_scale_Label.Content = volunteer_ViewModel.Personality_scale;            
            this.Photo.Source = volunteer_ViewModel.Photo;
        }
    }
}
