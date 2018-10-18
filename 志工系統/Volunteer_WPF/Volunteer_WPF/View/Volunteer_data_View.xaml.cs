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

        //按下新增鍵後，到空白的表單
        public Volunteer_data_View()
        {
            InitializeComponent();
            this.btn_confirm.Click += Button_Click1;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        //按下修改表單後, 表單從volunteer資料讀取資料
        public Volunteer_data_View(int a)
        {
            InitializeComponent();
            //按下"確認"按鈕
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
                cbsex.SelectedValue= v.sex.Trim();

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
                    //可以把ComboBox的資料顯示出來
                    cbBirthDayYear.SelectedValue = yy.Trim();                    
                    cbBirthDayMonth.SelectedValue = mn.Trim();                    
                    cbBirthDayDay.SelectedValue = dy.Trim();
                }
                //身份證字號
                txtIDcrad_no.Text = v.IDcrad_no;
                
                //病歷號
                txtMedical_record_no.Text = v.Medical_record_no;
                
                //身份類別
                cbIdentity_type.SelectedValue = (Convert.ToString(v.Identity_type)).Trim();
                //cbIdentity_type.Text = v.Identity_type.ToString();
                
                //服務年資
                txtSeniority.Text = Convert.ToString(v.Seniority);

                //加入日期 
                if (v.Join_date != null)
                {
                    string sJoin_date = Convert.ToString(v.Join_date);
                    DateTime Join_dateValue = (Convert.ToDateTime(sJoin_date));
                    string yyJoin_date = Convert.ToString(Join_dateValue.Year);
                    string mnJoin_date = Convert.ToString(Join_dateValue.Month);
                    string dyJoin_date = Convert.ToString(Join_dateValue.Day);

                    cbJoin_dateYear.SelectedValue = yyJoin_date.Trim();
                    cbJoin_dateMonth.SelectedValue = mnJoin_date.Trim();
                    cbJoin_dateDay.SelectedValue = dyJoin_date.Trim();
                }
                //離開日期
                if (v.Leave_date != null)
                {
                    string sLeave_date = Convert.ToString(v.Leave_date);
                    DateTime Leave_dateValue = (Convert.ToDateTime(sLeave_date));
                    string yyLeave_date = Convert.ToString(Leave_dateValue.Year);
                    string mnLeave_date = Convert.ToString(Leave_dateValue.Month);
                    string dyLeave_date = Convert.ToString(Leave_dateValue.Day);

                    cbLeave_dateYear.SelectedValue = yyLeave_date.Trim();
                    cbLeave_dateMonth.SelectedValue = mnLeave_date.Trim();
                    cbLeave_dateDay.SelectedValue = dyLeave_date.Trim();
                }
                //離開原因
                cbLeaveReason.Text = v.Leave_reason.Trim();

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

        //亞衡
        ////加入一個靜態變數 用來存放當前加入的Volunteer
        //public static int _Volunteer_no;

        //從空白的表單 填完資料後 按"確認"按鈕 新增資料
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
                Identity_type = intIdentity_type,
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

            var q = (from n in dbContext.Volunteer

                     select new { n.Volunteer_no }).ToList().LastOrDefault();
            
            foreach(var n in AAA)
            {
               Expertise2 e1 = new Expertise2 { Volunteer_no = q.Volunteer_no, Expertise_no = n };
               dbContext.Expertise2.Add(e1);
            }
             

            dbContext.SaveChanges();
            MessageBox.Show("志工資料新增成功");
            
            // int numberb=0;


            //foreach(var b in q )
            //{
            //    numberb = b.Volunteer_no;
            //}
            //dbContext.Expertise2.ToList();
            //dbContext.Expertise2.Local.Add(new Expertise2
            //{
            //    Volunteer_no = numberb

            //});
           
            

            //亞衡 連線 作法 找出volunteer_no=========================================================================
            //try
            //{
            //    using (SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=Volunteer;Integrated Security=True"))
            //    {
            //        using (SqlCommand cmd = new SqlCommand())
            //        {
            //            cmd.CommandText = "insert into [Volunteer].[Volunteer](Chinese_name,English_name,sex,birthday,IDcrad_no,Medical_record_no,Identity_type,Seniority,Join_date,Leave_date,Leave_reason,Phone_no,Mobile_no,Vest_no,Postal_code,Address,Education,Lssuing_unit_no,Service_manual_no,Personality_scale,Photo)" +
            //                "values(@Chinese_name,@English_name,@sex,@birthday,@IDcrad_no,@Medical_record_no,@Identity_type,@Seniority,@Join_date,@Leave_date,@Leave_reason,@Phone_no,@Mobile_no,@Vest_no,@Postal_code,@Address,@Education,@Lssuing_unit_no,@Service_manual_no,@Personality_scale,@Photo);" +
            //                "select Volunteer_no from [Volunteer].[Volunteer] where Volunteer_no=Scope_Identity()";
            //            cmd.Connection = conn;
            //            cmd.Parameters.Add("@Chinese_name", SqlDbType.NVarChar).Value = txtChineseName.Text;
            //            cmd.Parameters.Add("@English_name", SqlDbType.NVarChar).Value = txtEnglishName.Text;
            //            cmd.Parameters.Add("@sex", SqlDbType.NChar).Value = cbsex.Text;
            //            cmd.Parameters.Add("@birthday", SqlDbType.DateTime).Value = myBirthDay;
            //            cmd.Parameters.Add("@IDcrad_no", SqlDbType.NVarChar).Value = txtIDcrad_no.Text;
            //            cmd.Parameters.Add("@Medical_record_no", SqlDbType.NChar).Value = txtMedical_record_no.Text;
            //            cmd.Parameters.Add("@Identity_type", SqlDbType.Int).Value = intIdentity_type;
            //            cmd.Parameters.Add("@Seniority", SqlDbType.Int).Value = intSeniority;
            //            cmd.Parameters.Add("@Join_date", SqlDbType.Date).Value = myJoin_date;
            //            cmd.Parameters.Add("@Leave_date", SqlDbType.Date).Value = myLeave_date;
            //            cmd.Parameters.Add("@Leave_reason", SqlDbType.NChar).Value = cbLeaveReason.Text;
            //            cmd.Parameters.Add("@Phone_no", SqlDbType.Int).Value = intPhone_no;
            //            cmd.Parameters.Add("@Mobile_no", SqlDbType.Int).Value = intMobile_no;
            //            cmd.Parameters.Add("@Vest_no", SqlDbType.NChar).Value = txtVest_no.Text;
            //            cmd.Parameters.Add("@Postal_code", SqlDbType.Int).Value = intPostal_code;
            //            cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = txtAddress.Text;
            //            cmd.Parameters.Add("@Education", SqlDbType.NVarChar).Value = cbEducation.Text;
            //            cmd.Parameters.Add("@Lssuing_unit_no", SqlDbType.Int).Value = Lssuing_unit_no;
            //            cmd.Parameters.Add("@Service_manual_no", SqlDbType.NChar).Value = txtService_manual_no.Text;
            //            cmd.Parameters.Add("@Personality_scale", SqlDbType.NVarChar).Value = txtPersonality_scale.Text;
            //            cmd.Parameters.Add("@Photo", SqlDbType.VarBinary).Value = imagebyte;
            //            conn.Open();


            //            _Volunteer_no = (int)cmd.ExecuteScalar();
            //=================================================================
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
            //日的comboBox
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


        //按下修改表單後, 表單從volunteer資料讀取資料
        //修改要修改的東西後 按"確認"按鈕 修改資料
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
            catch
            { throw; }
            
            MessageBox.Show("修改成功");
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


        public void btnOpenFile_Click(object sender, RoutedEventArgs e)
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
            txtChineseName.Text = "克里斯";
            txtEnglishName.Text = "Chris";
            cbsex.Text = "男";
            cbBirthDayYear.Text = "2000";
            cbBirthDayMonth.Text = "12";
            cbBirthDayDay.Text = "30";
            txtIDcrad_no.Text = "aaa";
            txtMedical_record_no.Text = "bbb";
            cbIdentity_type.Text = "1";
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
            txtLssuing_unit_no.Text = "1";
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

        List<int> AAA;
        //跳到"選專長"頁面
        private void btnSelectExpert_Click(object sender, RoutedEventArgs e)
        {
            VolunteerExpertise v = new VolunteerExpertise();
            v.ShowDialog();
            AAA = v.STR();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }
}
