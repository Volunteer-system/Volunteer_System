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

        int Volunteer_no;
        public Volunteer_data_View()
        {
            InitializeComponent();
            this.btn_confirm.Click += Button_Click1;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public Volunteer_data_View(int a)
        {
            InitializeComponent();
            this.btn_confirm.Click += Button_Click;



            Volunteer_no = a;
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from v in dbContext.Volunteer
                        //join v2 in dbContext.Identity_type on v.Identity_type equals v2.Identity_type1
                    where v.Volunteer_no == Volunteer_no
                    select v;
            foreach (var v in q)
            {
                txtChineseName.Text = v.Chinese_name;
                txtEnglishName.Text = v.English_name;
                //??
                cbsex.Text = v.sex;

                //將birthday 拆開year, month, day
                if (v.birthday != null)
                {
                    string sDate = Convert.ToString(v.birthday);
                    DateTime datevalue = (Convert.ToDateTime(sDate));
                    string yy = datevalue.Year.ToString();
                    string mn = datevalue.Month.ToString();
                    string dy = datevalue.Day.ToString();

                    //WLC
                    //cbBirthDayYear.Text = yy;
                    //cbBirthDayMonth.Text = mn;
                    //cbBirthDayDay.Text = dy;
                    cbBirthDayYear.SelectedValue = yy;                    
                    cbBirthDayMonth.SelectedValue = mn;                    
                    cbBirthDayDay.SelectedValue = dy;
                }
                //
                txtIDcrad_no.Text = v.IDcrad_no;
                txtMedical_record_no.Text = v.Medical_record_no;
                cbIdentity_type.Text = v.Identity_type1.Identity_type_name;
                txtSeniority.Text = Convert.ToString(v.Seniority);

                //加入日期 
                if (v.Join_date != null)
                {
                    string sJoin_date = Convert.ToString(v.Join_date);
                    DateTime Join_dateValue = (Convert.ToDateTime(sJoin_date));
                    string yyJoin_date = Convert.ToString(Join_dateValue.Year);
                    string mnJoin_date = Convert.ToString(Join_dateValue.Date);
                    string dyJoin_date = Convert.ToString(Join_dateValue.Day);

                    cbJoin_dateYear.Text = yyJoin_date;
                    cbJoin_dateMonth.Text = mnJoin_date;
                    cbJoin_dateDay.Text = dyJoin_date;
                }
                //離開日期
                if (v.Leave_date != null)
                {
                    string sLeave_date = Convert.ToString(v.Leave_date);
                    DateTime Leave_dateValue = (Convert.ToDateTime(sLeave_date));
                    string yyLeave_date = Convert.ToString(Leave_dateValue.Year);
                    string mnLeave_date = Convert.ToString(Leave_dateValue.Month);
                    string dyLeave_date = Convert.ToString(Leave_dateValue.Date);

                    cbLeave_dateYear.Text = yyLeave_date;
                    cbLeave_dateMonth.Text = mnLeave_date;
                    cbLeave_dateDay.Text = dyLeave_date;
                }
                //離開原因
                cbLeaveReason.Text = v.Leave_reason;

                //地址
                txtAddress.Text = v.Address;

                //電話號碼
                txtPhone_no.Text = Convert.ToString(v.Phone_no);

                //手機號碼
                txtMobile_no.Text = Convert.ToString(v.Mobile_no);

                //志工背心號碼
                txtVest_no.Text = v.Vest_no;

                //郵遞區號
                txtPostal_code.Text = Convert.ToString(v.Postal_code);

                //學歷
                cbEducation.Text = v.Education;

                //發證單位
                txtLssuing_unit_no.Text = Convert.ToString(v.Lssuing_unit_no);

                //志工服務手冊編號
                txtService_manual_no.Text = v.Service_manual_no;

                //人格量表結果
                txtPersonality_scale.Text = v.Personality_scale;

                //照片
                if (v.Photo != null)
                {
                    byte[] vphoto = null;
                    vphoto = v.Photo;

                    myImage.Source = ConvertByteArrayToBitmapImage(vphoto);

                }
                
            }

        }
        //新增資料
        private void Button_Click1(object sender, RoutedEventArgs e)
        {        
            // WLC
            DateTime myBirthDay = new DateTime();
            DateTime myJoin_date = new DateTime();
            DateTime myLeave_date = new DateTime();
            if ((!string.IsNullOrEmpty(cbBirthDayYear.Text)) && (!string.IsNullOrEmpty(cbBirthDayMonth.Text)) && (!string.IsNullOrEmpty(cbBirthDayDay.Text)))
            {
                myBirthDay = DateTime.Parse(cbBirthDayYear.Text + "/" + cbBirthDayMonth.Text + "/" + cbBirthDayDay.Text);
            }

            if ((!string.IsNullOrEmpty(cbJoin_dateYear.Text)) && (!string.IsNullOrEmpty(cbJoin_dateMonth.Text)) && (!string.IsNullOrEmpty(cbJoin_dateDay.Text)))
            {
                myJoin_date = DateTime.Parse(cbJoin_dateYear.Text + "/" + cbJoin_dateMonth.Text + "/" + cbJoin_dateDay.Text);
            }

            if ((!string.IsNullOrEmpty(cbLeave_dateYear.Text)) && (!string.IsNullOrEmpty(cbLeave_dateMonth.Text)) && (!string.IsNullOrEmpty(cbLeave_dateDay.Text)))
            {
                myLeave_date = DateTime.Parse(cbLeave_dateYear.Text + "/" + cbLeave_dateMonth.Text + "/" + cbLeave_dateDay.Text);
            }

            int intIdentity_type = 0;
            int intPhone_no = 0;
            int intMobile_no = 0;
            int intPostal_code = 0;
            int intLssuing_unit_no = 0;
            int intSeniority = 0;            
            if (int.TryParse(cbIdentity_type.Text, out int Identity_type))
            {
                intIdentity_type = Identity_type;
            }
            if (int.TryParse(txtSeniority.Text, out int Seniority))
            {
                intSeniority = Seniority;
            }
            if (int.TryParse(txtPhone_no.Text, out int Phone_no))
            {
                intPhone_no = Phone_no;
            }
            if (int.TryParse(txtMobile_no.Text, out int Mobile_no))
            {
                intMobile_no = Mobile_no;
            }
            if (int.TryParse(txtPostal_code.Text, out int Postal_code))
            {
                intPostal_code = Postal_code;
            }
            if (int.TryParse(txtLssuing_unit_no.Text, out int Lssuing_unit_no))
            {
                intLssuing_unit_no = Lssuing_unit_no;
            }

            VolunteerEntities dbContext = new VolunteerEntities();
            // System.Windows.Data.CollectionViewSource volunteerViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("volunteerViewSource")));
            dbContext.Volunteer.ToList();
            // volunteerViewSource.Source = dbContext.Volunteer.Local;
            dbContext.Volunteer.Local.Add(new Volunteer
            {
                Chinese_name = txtChineseName.Text,
                English_name = txtEnglishName.Text,
                sex = cbsex.Text,
                birthday = myBirthDay,
                IDcrad_no = txtIDcrad_no.Text,
                Medical_record_no = txtMedical_record_no.Text,         
                //WLC
                //Identity_type = intIdentity_type,
                Seniority = intSeniority,
                Join_date = myJoin_date,
                Leave_date = myLeave_date,
                Leave_reason = cbLeaveReason.Text,
                Phone_no = intPhone_no,
                Mobile_no = intMobile_no,
                Vest_no = txtVest_no.Text,
                Postal_code = intPostal_code,
                Address = txtAddress.Text,
                Education = cbEducation.Text,
                Lssuing_unit_no = intLssuing_unit_no,
                Service_manual_no = txtService_manual_no.Text,
                Personality_scale = txtPersonality_scale.Text,
                Photo = imagebyte
            });

            dbContext.SaveChanges();
            MessageBox.Show("successful");
            
        }

        public static BitmapImage ConvertByteArrayToBitmapImage(Byte[] bytes)
        {
            var stream = new MemoryStream(bytes);
            stream.Seek(0, SeekOrigin.Begin);
            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = stream;
            image.EndInit();
            return image;
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

            // WLC
            if (string.IsNullOrEmpty(cbIdentity_type.Text))
            {
                cbIdentity_type.Text = "請輸入數字1到5";
            }
            if (string.IsNullOrEmpty(txtSeniority.Text))
            {
                txtSeniority.Text = "請輸入數字";
            }
            if (string.IsNullOrEmpty(txtPhone_no.Text))
            {
                txtPhone_no.Text = "請輸入數字";
            }
            if (string.IsNullOrEmpty(txtMobile_no.Text))
            {
                txtMobile_no.Text = "請輸入數字";
            }
            if (string.IsNullOrEmpty(txtLssuing_unit_no.Text))
            {
                txtLssuing_unit_no.Text = "請輸入數字1到10";
            }
            if (string.IsNullOrEmpty(txtPostal_code.Text))
            {
                txtPostal_code.Text = "請輸入數字";
            }

            

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
            for (int i = 1; i <= 12; i++)
            {
                list2.Add(Convert.ToString(i));
            }
            return list2;
        }

        private List<string> GetItemYear()
        {
            List<string> list1 = new List<string>();
            for (int i = DateTime.Now.Year - 100; i <= DateTime.Now.Year; i++)
            {
                list1.Add(Convert.ToString(i));
            }
            return list1;
        }


        //修改資料
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // WLC
            DateTime myBirthDay = new DateTime();
            DateTime myJoin_date = new DateTime();
            DateTime myLeave_date = new DateTime();
            if ((!string.IsNullOrEmpty(cbBirthDayYear.Text)) && (!string.IsNullOrEmpty(cbBirthDayMonth.Text)) && (!string.IsNullOrEmpty(cbBirthDayDay.Text)))
            {
                myBirthDay = DateTime.Parse(cbBirthDayYear.Text + "/" + cbBirthDayMonth.Text + "/" + cbBirthDayDay.Text);
            }

            if ((!string.IsNullOrEmpty(cbJoin_dateYear.Text)) && (!string.IsNullOrEmpty(cbJoin_dateMonth.Text)) && (!string.IsNullOrEmpty(cbJoin_dateDay.Text)))
            {
                myJoin_date = DateTime.Parse(cbJoin_dateYear.Text + "/" + cbJoin_dateMonth.Text + "/" + cbJoin_dateDay.Text);
            }

            if ((!string.IsNullOrEmpty(cbLeave_dateYear.Text)) && (!string.IsNullOrEmpty(cbLeave_dateMonth.Text)) && (!string.IsNullOrEmpty(cbLeave_dateDay.Text)))
            {
                myLeave_date = DateTime.Parse(cbLeave_dateYear.Text + "/" + cbLeave_dateMonth.Text + "/" + cbLeave_dateDay.Text);
            }
            

            VolunteerEntities dbContext = new VolunteerEntities();
            //// System.Windows.Data.CollectionViewSource volunteerViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("volunteerViewSource")));
            //dbContext.Volunteer.ToList();
            //// volunteerViewSource.Source = dbContext.Volunteer.Local;
            //dbContext.Volunteer.Local.Add(new Volunteer
            var q = from a in dbContext.Volunteer
                    where a.Volunteer_no == Volunteer_no
                    select a;
            foreach(var a in q)
            {
                a.Chinese_name = txtChineseName.Text;
                a.English_name = txtEnglishName.Text;
                a.sex = cbsex.Text;
                a.birthday = myBirthDay;
                a.IDcrad_no = txtIDcrad_no.Text;
                a.Medical_record_no = txtMedical_record_no.Text;
                // WLC
                if (int.TryParse(cbIdentity_type.Text, out int Identity_type))
                {
                    a.Identity_type = Identity_type;
                }
                if (int.TryParse(txtSeniority.Text, out int Seniority))
                {
                    a.Seniority = Seniority;
                }                    
                a.Join_date = myJoin_date;
                a.Leave_date = myLeave_date;
                a.Leave_reason = cbLeaveReason.Text;
                if (int.TryParse(txtPhone_no.Text, out int Phone_no))
                {
                    a.Phone_no = Phone_no;
                }
                if (int.TryParse(txtMobile_no.Text, out int Mobile_no))
                {
                    a.Mobile_no = Mobile_no;
                }                    
                a.Vest_no = txtVest_no.Text;
                if (int.TryParse(txtPostal_code.Text, out int Postal_code))
                {
                    a.Postal_code = Postal_code;
                }                    
                a.Address = txtAddress.Text;
                a.Education = cbEducation.Text;
                if (int.TryParse(txtLssuing_unit_no.Text, out int Lssuing_unit_no))
                {
                    a.Lssuing_unit_no = Lssuing_unit_no;
                }
                    
                a.Service_manual_no = txtService_manual_no.Text;
                a.Personality_scale = txtPersonality_scale.Text;
                a.Photo = imagebyte;
            }
            try
            { dbContext.SaveChanges(); }
            catch(Exception ex)
            { throw; }
            
            MessageBox.Show("successful");
        }

        private void txtChineseName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
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
            //cbIdentity_type.Text = "5";
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
