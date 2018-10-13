using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32; //FileDialog
using Volunteer_WPF.Model;

namespace Volunteer_WPF.View
{
    /// <summary>
    /// Window1.xaml 的互動邏輯
    /// </summary>
    public partial class Volunteer_data_View : Window
    {

        public Volunteer_data_View()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            
            //System.Windows.Data.CollectionViewSource volunteerViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("volunteerViewSource")));
            
            dbContext.Volunteer.ToList();
            //volunteerViewSource.Source = dbContext.Volunteer.Local;
            // 透過設定 CollectionViewSource.Source 屬性載入資料: 
            // volunteerViewSource.Source = [泛用資料來源]

            //cbsex.Text = "請選擇";
            //cbLeaveReason.Text = "請選擇";
            //cbEducation.Text = "請選擇";

             cbIdentity_type.Text = "請輸入數字1到5";
            txtSeniority.Text= "請輸入數字";
            txtPhone_no.Text = "請輸入數字";
            txtMobile_no.Text = "請輸入數字";
            txtLssuing_unit_no.Text= "請輸入數字1到10";
            txtPostal_code.Text= "請輸入數字";

            //姓別combobox
            List<string> itemSex = GetDataSex();
            cbsex.ItemsSource = itemSex;

            //身份類別combobox

            List<string> itemIdentity_type = GetDataIdentity_type();
            cbIdentity_type.ItemsSource = itemIdentity_type;

            //birthday生日 年的combobox
            List<string> ItemYear = GetItemYear();
            cbBirthDayYear.ItemsSource = ItemYear;
            //月的combobox
            List<string> ItemMonth = GetItemMonth();
            cbBirthDayMonth.ItemsSource = ItemMonth;
            //
            List<string> ItemDay = GetItemDay();
            cbBirthDayDay.ItemsSource = ItemDay;

            cbJoin_dateYear.ItemsSource = ItemYear;
            cbJoin_dateMonth.ItemsSource = ItemMonth;
            cbJoin_dateDay.ItemsSource = ItemDay;

            cbLeave_dateYear.ItemsSource = ItemYear;
            cbLeave_dateMonth.ItemsSource = ItemMonth;
            cbLeave_dateDay.ItemsSource = ItemDay;

        }

        private List<string> GetDataIdentity_type()
        {
            List<string> list = new List<string>();
            list.Add("1");  //("試用期");
            list.Add("2");  //("固定班");
            list.Add("3");  //("長假");
            list.Add("4");  //("離隊");
            list.Add("5");  //("專才志工");
            return list;
        }

        private List<string> GetDataSex()
        {
            List<string> list = new List<string>();
            list.Add("男");
            list.Add("女");
            return list;
        }



        private List<string> GetItemDay()
        {
            List<string> list3 = new List<string>();
            for (int i = 1; i <= 31; i++)
            {
                list3.Add(Convert.ToString(i));
            }
            return list3;
        }

        private List<string> GetItemMonth()
        {
            List<string> list2 = new List<string>();
            for(int i=1;i<=12;i++)
            {
                list2.Add(Convert.ToString(i));
            }
            return list2;
        }

        private List<string> GetItemYear()
        {
            List<string> list1 = new List<string>();
            for(int i=DateTime.Now.Year-100;i<=DateTime.Now.Year;i++)
            {
                list1.Add(Convert.ToString( i));
            }
            return list1;
        }


        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime myBirthDay = new DateTime();
            myBirthDay = DateTime.Parse(cbBirthDayYear.Text + "/" + cbBirthDayMonth.Text + "/" + cbBirthDayDay.Text);

            DateTime myJoin_date = new DateTime();
            myJoin_date = DateTime.Parse(cbJoin_dateYear.Text + "/" + cbJoin_dateMonth.Text + "/" + cbJoin_dateDay.Text);

            DateTime myLeave_date = new DateTime();
            myJoin_date = DateTime.Parse(cbLeave_dateYear.Text + "/" + cbLeave_dateMonth.Text + "/" + cbLeave_dateDay.Text);



            VolunteerEntities dbContext = new VolunteerEntities();
            System.Windows.Data.CollectionViewSource volunteerViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("volunteerViewSource")));
            //dbContext.Volunteer.ToList();
            volunteerViewSource.Source = dbContext.Volunteer.Local;
            dbContext.Volunteer.Local.Add(new Volunteer {
                                                          Chinese_name = txtChineseName.Text,
                                                          English_name = txtEnglishName.Text,
                                                          sex = cbsex.Text,
                                                          birthday = myBirthDay,
                                                          IDcrad_no = txtIDcrad_no.Text,
                                                          Medical_record_no = txtMedical_record_no.Text,
                                                          Identity_type = int.Parse(cbIdentity_type.Text),
                                                          Seniority = int.Parse(txtSeniority.Text),
                                                          Join_date = myJoin_date,
                                                          Leave_date = myLeave_date,
                                                          Leave_reason = cbLeaveReason.Text,
                                                          Phone_no = int.Parse(txtPhone_no.Text),
                                                          Mobile_no = int.Parse(txtMobile_no.Text),
                                                          Vest_no = txtVest_no.Text,
                                                          Postal_code = int.Parse(txtPostal_code.Text),
                                                          Address = txtAddress.Text,
                                                          Education = cbEducation.Text,
                                                          Lssuing_unit_no = int.Parse(txtLssuing_unit_no.Text),
                                                          Service_manual_no = txtService_manual_no.Text,
                                                          Personality_scale = txtPersonality_scale.Text,
                                                          Photo = imagebyte
                                                          
                                                          });
                                                         
            dbContext.SaveChanges();
            MessageBox.Show("successful");
        }

        private void txtChineseName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Enter)
            {
                txtEnglishName.Focus();
            }
        }

        private void txtEnglishName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                cbsex.Focus();
            }
            
        }

        private void cbsex_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtIDcrad_no.Focus();
            }
        }

        public byte[] imagebyte;


        public void btnOpen_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog f = new OpenFileDialog();

            if (f.ShowDialog() == true)
            {
                myImage.Source = new BitmapImage(new Uri(f.FileName));
                imagebyte = getJPGFromImageControl(myImage.Source as BitmapImage);

            }
        }

        public byte[] getJPGFromImageControl(BitmapImage imageC)
        {
            MemoryStream memStream = new MemoryStream();
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(imageC));
            encoder.Save(memStream);
            return memStream.ToArray();
        }

        private void btnDEMO_Click(object sender, RoutedEventArgs e)
        {
            txtChineseName.Text = "庫克";
            txtEnglishName.Text = "cock";
            cbsex.Text = "男";
            cbBirthDayYear.Text = "2000";
            cbBirthDayMonth.Text = "12";
            cbBirthDayDay.Text = "30";
            txtIDcrad_no.Text = "aaa";
            txtMedical_record_no.Text = "bbb";
            cbIdentity_type.Text = "5";
            txtSeniority.Text = "2";
            cbJoin_dateYear.Text = "2000";
            cbJoin_dateMonth.Text = "3";
            cbJoin_dateDay.Text = "3";
            cbLeave_dateYear.Text = "2001";
            cbLeave_dateMonth.Text = "3";
            cbLeave_dateDay.Text = "4";
            cbLeaveReason.Text = "因病";
            txtAddress.Text = "台北市";
            txtPhone_no.Text = "9999";
            txtMobile_no.Text = "1111";
            txtVest_no.Text = "aaaa";
            txtPostal_code.Text = "2222";
            cbEducation.Text = "小學";
            txtLssuing_unit_no.Text = "8";
            txtService_manual_no.Text = "rrrr";
            txtPersonality_scale.Text = "bbbb";
        }




        private void txtIdentity_type_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtSeniority.Focus();
            }
        }
        private void txtLeave_date_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
               cbLeaveReason.Focus();
            }
        }
        private void cbLeaveReason_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtPhone_no.Focus();
            }
        }
        private void txtPhone_no_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtMobile_no.Focus();
            }
        }
        private void txtMobile_no_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtVest_no.Focus();
            }
        }
        private void txtVest_no_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtPostal_code.Focus();
            }
        }
        private void txtPostal_code_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtAddress.Focus();
            }
        }
        private void txtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                cbEducation.Focus();
            }
        }
        private void cbEducation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtLssuing_unit_no.Focus();
            }
        }
        private void txtLssuing_unit_no_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtService_manual_no.Focus();
            }
        }
        private void txtService_manual_no_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtPersonality_scale.Focus();
            }
        }


    }
}
