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
using System.Collections.ObjectModel;
using System.Collections;
using System.Net.Mail;
using Volunteer_WPF.Model;
using System.Threading;
using OfficeOpenXml;
using System.IO;

namespace Volunteer_WPF.View
{
    /// <summary>
    /// Sign_up_View.xaml 的互動邏輯
    /// </summary>
    public partial class Sign_up_View : Window
    {
        MainWindow m = new MainWindow();   //登入頁面的表單
        public Sign_up_View()
        {
            InitializeComponent();
            this.DataGrid_1.IsReadOnly = true;
            this.DataGrid_1.Columns[0].Visibility = Visibility.Hidden;
            this.DataGrid_1.Columns[1].Visibility = Visibility.Hidden;
            superversion_name = m.username.Content.ToString(); //接收登入者名稱
            supervisionID =  getloginID(superversion_name);   //抓取登入者ID ，沒有就回傳 1     
        }

        private int getloginID(string username)
        {
            int n = 1;
            if (username != null || username != string.Empty)
            {
                var q = from s in Myentity.Volunteer_supervision
                         where s.supervision_Name == username
                         select s;
                foreach (var r in q)
                {
                    n =  r.supervision_ID;
                }
            }
            else
            {
               n= 1;
            }
            return n;
        }

        List<int> l_click_ok = new List<int>();        //存打勾人
        List<int> l_click_delete = new List<int>();    //存打X的人

        bool first;                                    //判斷是否點選初次審核
        bool interview;                                //判斷是否點選面試審核
        bool pass_applicant;                           //判斷是否點選查詢通過審核者
        bool reject;                                   //判斷是否點選查詢拒絕審核
        bool selection;
        bool Issend_pass = true;                       //判斷信件類形是審核通過的
        bool Issend_delete = false;                    //判斷信件類型是刪除的
             
        Thread send_mail;                              //發mail用的背景執行緒
        string stage_first = "初次申請";//1            //審核階段的代表值變數
        string stage_interview = "面試";//4
        string stage_pass_applicant = "通過";//5
        string stage_reject = "駁回";                  //代表駁回
        string stage_NotUse = "其他";                  //非審核階段的代表值
        string sign_up_type_use;                       //身分類別使用

        List<string> select_use = new List<string>();   //已勾選的
        List<string> select_canuse = new List<string>();//可供勾選的欄位
        Window w;                                       //產生新視窗
        StackPanel sp = new StackPanel();               //新視窗內的容器

        string[] select_everytime = new string[] { "申請人姓名", "生日", "電話號碼", "手機號碼", "電子郵件", "聯絡地址" }; //常用輸出項目
        string superversion_name = "";                  //接收登入者姓名
        int supervisionID = 1;                          //判斷登入者的ID

        ObservableCollection<Volunteer_Applicant> volunteer = new ObservableCollection<Volunteer_Applicant>();  //存資料庫資料類別用

        VolunteerEntities Myentity = new VolunteerEntities();       //學校用

        //審核初次申請的人▼
        private void btn_firstcheck_Click(object sender, RoutedEventArgs e)
        {//審核初次申請的人
            first = true;                                   //判斷點選的是哪個功能
            interview = false;
            pass_applicant = false;
            reject = false;
            selection = false;
            this.DataGrid_1.ItemsSource = get_volunteer(stage_first); //呼叫要顯示的資料(初申請狀態)  
            this.DataGrid_1.Columns[2].Visibility = Visibility.Hidden;
        }
        //審核面試階段的人▼
        private void btn_interview_Click(object sender, RoutedEventArgs e)
        {//審核面試階段的人
            first = false;                                  //判斷點選的是哪個功能
            interview = true;
            pass_applicant = false;
            reject = false;
            selection = false;
            this.DataGrid_1.ItemsSource = get_volunteer(stage_interview); //呼叫要顯示的資料(要求面試狀態)   
            this.DataGrid_1.Columns[2].Visibility = Visibility.Hidden;

        }
        //查看所有通過審核的人▼
        private void btn_Allpass_Click(object sender, RoutedEventArgs e)
        {//查看所有通過審核的人    
            first = false;                                               //判斷點選的是哪個功能
            interview = false;
            pass_applicant = true;
            reject = false;
            selection = false;

            this.DataGrid_1.ItemsSource = get_volunteer(stage_pass_applicant);              //呼叫要顯示的資料(通過審核狀態) 

            this.DataGrid_1.Columns[0].Visibility = Visibility.Visible;  //顯示原本隱藏的刪除欄位
            this.DataGrid_1.Columns[1].Visibility = Visibility.Hidden;   //隱藏審核設定的欄位
        }
        //查詢已拒絕審核▼
        private void btn_reject_Click(object sender, RoutedEventArgs e)
        {//查詢已拒絕審核
            first = false;                                               //判斷點選的是哪個功能
            interview = false;
            pass_applicant = false;
            reject = true;
            selection = false;

            this.DataGrid_1.ItemsSource = get_volunteer(stage_reject);              //呼叫要顯示的資料(拒絕狀態) 

            this.DataGrid_1.Columns[0].Visibility = Visibility.Visible;  //顯示原本隱藏的刪除欄位
            this.DataGrid_1.Columns[1].Visibility = Visibility.Hidden;   //隱藏審核設定的欄位
        }
        //按下查詢時▼
        private void btn_selectDate_Click(object sender, RoutedEventArgs e)
        {//按下查詢時
            if (first)                                          //判斷現在頁面做的查詢是哪種
            {
                this.DataGrid_1.ItemsSource = get_volunteer(stage_first); //呼叫要顯示的資料(初申請狀態)
            }
            else if (interview)
            {
                this.DataGrid_1.ItemsSource = get_volunteer(stage_interview);//呼叫要顯示的資料(要求面試狀態)
            }
            else if (pass_applicant)
            {
                this.DataGrid_1.ItemsSource = get_volunteer(stage_pass_applicant);//呼叫要顯示的資料(通過審核狀態)
            }
            else if (reject)
            {
                this.DataGrid_1.ItemsSource = get_volunteer(stage_reject);    //呼叫要顯示的資料(拒絕狀態)
            }
            else                                               //非以設定的查詢
            {
                selection = true;
                this.DataGrid_1.ItemsSource = get_volunteer(stage_NotUse);//呼叫要顯示的資料(9:非任何狀態使用)
                this.DataGrid_1.Columns[2].Visibility = Visibility.Hidden;
                this.DataGrid_1.Columns[1].Visibility = Visibility.Hidden;
            }
        }
        //存取資料的類別▼
        public class Volunteer_Applicant  
        {  //存取資料庫資料用          
            public int 申請編號 { get; set; }
            public string 申請階段 { get; set; }
            public string 申請人姓名 { get; set; }
            public string 性別 { get; set; }
            public string 生日 { get; set; }
            public string 人格量表結果 { get; set; }
            public string 電話號碼 { get; set; }
            public string 手機號碼 { get; set; }
            public string 電子郵件 { get; set; }
            public string 教育程度 { get; set; }
            public string 職業 { get; set; }
            public string 聯絡地址 { get; set; }
            public string 申請日期 { get; set; }
            public string 審核日期 { get; set; }
            public string 身分類別 { get; set; }
        }
        //點選資料時▼
        private void DataGrid_1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {//點選資料時
            Volunteer_Applicant v = this.DataGrid_1.SelectedItem as Volunteer_Applicant;
            if (v != null)
            {
                getsignup_Data(v.申請編號);
            }
        }
        //儲存按下了勾勾的人▼
        private void Check_interview_Click(object sender, EventArgs e)
        { //儲存按下了勾勾的人
            getcheck_delete(l_click_ok, l_click_delete,lab_check,lab_Dcheck); //呼叫方法         
        }
       
        //儲存按下XX的人▼
        private void Check_interview_MU_Click_Delete(object sender, EventArgs e)
        {//儲存按下XX的人
            getcheck_delete(l_click_delete, l_click_ok, lab_Dcheck, lab_check);//呼叫方法         
        }       

        //按下確認▼
        private void btn_check_Click(object sender, RoutedEventArgs e)
        {
            if (l_click_ok.Count != 0 || l_click_delete.Count != 0)  //如果勾勾或XX集合內有值的話進入
            {
                if (first)                                           //如果是在初次審核的頁面
                {
                    getclick(l_click_ok, l_click_delete, "初次審核", stage_interview);                  
                    this.Myentity.SaveChanges();                               //將修改存回資料庫
                    this.DataGrid_1.ItemsSource = null;                        //更新該頁面
                    this.DataGrid_1.ItemsSource = get_volunteer(stage_first);  //重新呼叫資料
                }
                else if (interview || selection)                                            //如果是在面試審核的頁面
                {
                    getclick(l_click_ok, l_click_delete, "面試審核", stage_pass_applicant);                    
                    this.Myentity.SaveChanges();                              //將修改存回資料庫

                    Inertgroup_andexpertise(l_click_ok);

                    this.DataGrid_1.ItemsSource = null;                       //更新該頁面
                    this.DataGrid_1.ItemsSource = get_volunteer(stage_interview); //重新呼叫資料

                }
            }
            else
            {
                if (((Button)e.Source).Content.ToString() == "刪除")          //如果是審核通過頁面按下刪除鍵
                {                                                             //秀出一個確認視窗
                    if (System.Windows.Forms.MessageBox.Show("確認刪除這筆資料?\n如果電子郵件欄位為空，就不會發送信件", "刪除確認", System.Windows.Forms.MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                    {
                        Volunteer_Applicant VA = this.DataGrid_1.SelectedItem as Volunteer_Applicant;
                        var q = (from v in Myentity.Sign_up
                                 select v).ToList();
                        for (int i = 0; i < q.Count; i += 1)
                        {
                            if (VA.申請編號 == q[i].Sign_up_no)
                            {                                                //TODO刪除功能
                                this.Myentity.Sign_up.Remove(q[i]);        //執行刪除的動作
                                ThreadStart TS = new ThreadStart(
                                            delegate ()
                                            {
                                                sending_email(q[i].Email, q[i].Chinese_name+q[i].English_name, Issend_delete);//寄送郵件
                                                System.Windows.Forms.Application.DoEvents();
                                            });
                                send_mail = new Thread(TS);
                                send_mail.IsBackground = true;
                                send_mail.Start();                            
                                break;
                            }
                        }
                    }
                }
                this.Myentity.SaveChanges();                                 //將修改存回資料庫
                this.DataGrid_1.ItemsSource = null;                          //更新該頁面
                if (pass_applicant)
                {
                    this.DataGrid_1.ItemsSource = get_volunteer(stage_pass_applicant); //重新呼叫資料
                }
                else
                {
                    this.DataGrid_1.ItemsSource = get_volunteer(stage_reject); //重新呼叫資料
                }
            }
            
        }       

        //呼叫顯示資料的方法▼
        private IEnumerable get_volunteer(string n)
        {                                                                    //呼叫顯示資料的方法
            volunteer.Clear();                                               //清空資料集
            this.l_click_ok.Clear();                                         //清空這項功能以外的選擇項目
            this.l_click_delete.Clear();                                     //清空這項功能以外的選擇項目
            lab_check.Content = l_click_ok.Count.ToString();                 //計算勾選數
            lab_Dcheck.Content = l_click_delete.Count.ToString();            //計算打X數
            if (n == stage_pass_applicant || n==stage_reject)
            {
                this.DataGrid_1.Columns[0].Visibility = Visibility.Visible;     //將刪除欄顯示
                this.DataGrid_1.Columns[1].Visibility = Visibility.Hidden;      //隱藏審核設定欄
            }
            else
            {
                this.DataGrid_1.Columns[0].Visibility = Visibility.Hidden;       //將刪除欄隱藏
                this.DataGrid_1.Columns[1].Visibility = Visibility.Visible;      //顯示審核設定欄              
            }
            if ((D_Start_date.Text != "" || D_End_date.Text != "") || n!=stage_NotUse)  //判斷點查詢鈕時的查詢條件
            {  
               var q = (from v in Myentity.Sign_up join s in Myentity.Stages on v.Stage equals s.Stage_ID
                        where s.Stage1 == n && (D_Start_date.Text == "" ? true : v.Sign_up_date >= D_Start_date.SelectedDate) &&( D_End_date.Text == "" ? true : v.Sign_up_date <= D_End_date.SelectedDate) && v.Sign_up_type == sign_up_type_use
                     select new { Sign_up_no = v.Sign_up_no, Stage  = s.Stage1,Stagetype = s.Stage_type,SignupName = v.Chinese_name + " " + v.English_name ,Sex = v.Sex, Birthday =v.Birthday, Personality_scale  = v.Personality_scale,Phone = v.Phone,Mobile = v.Mobile,Email = v.Email,Education=v.Education,Job = v.Job,Address=v.Address, Sign_up_date =v.Sign_up_date, Approval_date = v.Approval_date, Sign_up_type = v.Sign_up_type }).ToList();
                foreach (var v in q)
                {  
                    volunteer.Add(new Volunteer_Applicant() { 申請編號 = v.Sign_up_no,申請階段 =v.Stage, 申請人姓名 =v.SignupName, 性別 = v.Sex, 生日 = Convert.ToDateTime(v.Birthday).ToShortDateString(), 人格量表結果 = v.Personality_scale, 電話號碼 = v.Phone, 手機號碼 = v.Mobile, 電子郵件 = v.Email, 教育程度 = v.Education, 職業 = v.Job, 聯絡地址 = v.Address, 申請日期 = Convert.ToDateTime(v.Sign_up_date).ToShortDateString(), 審核日期 = Convert.ToDateTime(v.Approval_date).ToShortDateString(), 身分類別 = v.Sign_up_type });
                }
            }
            else //未進任何頁面時點選查詢
            {
               var q = (from v in Myentity.Sign_up
                        join s in Myentity.Stages on v.Stage equals s.Stage_ID
                        where (D_Start_date.Text == "" ? true : v.Sign_up_date >= D_Start_date.SelectedDate) && (D_End_date.Text == "" ? true : v.Sign_up_date <= D_End_date.SelectedDate)
                        select new { Sign_up_no = v.Sign_up_no, Stage = s.Stage1, Stagetype = s.Stage_type, SignupName = v.Chinese_name + " " + v.English_name, Sex = v.Sex, Birthday = v.Birthday, Personality_scale = v.Personality_scale, Phone = v.Phone, Mobile = v.Mobile, Email = v.Email, Education = v.Education, Job = v.Job, Address = v.Address, Sign_up_date = v.Sign_up_date, Approval_date = v.Approval_date, Sign_up_type = v.Sign_up_type }).ToList();
                foreach (var v in q)
                {  
                    volunteer.Add(new Volunteer_Applicant() { 申請編號 = v.Sign_up_no, 申請階段 = v.Stage, 申請人姓名 = v.SignupName, 性別 = v.Sex, 生日 = Convert.ToDateTime(v.Birthday).ToShortDateString(), 人格量表結果 = v.Personality_scale, 電話號碼 = v.Phone, 手機號碼 = v.Mobile, 電子郵件 = v.Email, 教育程度 = v.Education, 職業 = v.Job, 聯絡地址 = v.Address, 申請日期 = Convert.ToDateTime(v.Sign_up_date).ToShortDateString(), 審核日期 = Convert.ToDateTime(v.Approval_date).ToShortDateString(), 身分類別 = v.Sign_up_type });
                }
            }
            this.D_Start_date.Text = "";   //查詢後將值設為預設
            this.D_End_date.Text = "";     //查詢後將值設為預設 
            return volunteer;
        }

        //寄送電子郵件的方法1▼
        private void sending_email(string email,string name, bool whattype)
        {
            int n = email == null ? 1 : 0;  //判斷郵件地址是否為null
            if (n != 1)
            {
                if (email.Contains("@"))
                {
                    sending(email, whattype);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("姓名" + name + "郵件發送失敗\n可能原因為無效地址!!", "發送失敗提醒!!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
            }
        }
        //寄送電子郵件的方法2▼
        private void sending(string email,bool whattype)
        {
            SmtpClient SC = new SmtpClient("smtp.gmail.com", 587);   //設定伺服器主機
            SC.Credentials = new System.Net.NetworkCredential("yhkcjo@gmail.com", "y hk4cjo4");  //設定帳密
            SC.EnableSsl = true;                                                                 //Google要使用加密連線   
            MailMessage MM = new MailMessage();                      //寫信的內容用
            if (whattype)
            {
                if (first)//初次審核階段
                {
                    MM.Subject ="志工審核面試通知";             //信件主旨
                    MM.Body = "面試";                                            //信件內容
                    MM.IsBodyHtml = false;                                       //內容是否使用Html
                    MM.From = new MailAddress("yhkcjo@gmail.com", "志工督導");   //寄件地址，顯示名稱
                    MM.To.Add(email);                                            //設定收件地址
                }
                else if (interview)//面試階段
                {
                    MM.Subject = "志工審核通過通知";                             //信件主旨
                    MM.Body = "審核通過";                                        //信件內容
                    MM.IsBodyHtml = false;                                       //內容是否使用Html
                    MM.From = new MailAddress("yhkcjo@gmail.com", "志工督導");   //寄件地址，顯示名稱
                    MM.To.Add(email);                                            //設定收件地址
                }
                else
                {
                    MM.Subject = "志工審核通過通知";                             //信件主旨
                    MM.Body = "審核通過";                                        //信件內容
                    MM.IsBodyHtml = false;                                       //內容是否使用Html
                    MM.From = new MailAddress("yhkcjo@gmail.com", "志工督導");   //寄件地址，顯示名稱
                    MM.To.Add(email);
                }
            }
            else
            {
                string body;
                string subject;
                if (first || interview)
                {
                    body = "志工申請駁回";
                    subject = "志工申請駁回通知";
                }
                else
                {
                    body = "志工資料移除";
                    subject = "志工資料移除通知";
                }
                MM.Subject = subject;                                  //信件主旨
                MM.Body = body;                                                   //信件內容
                MM.IsBodyHtml = false;                                            //內容是否使用Html
                MM.From = new MailAddress("yhkcjo@gmail.com", "志工督導");        //寄件地址，顯示名稱
                MM.To.Add(email);                                                 //設定收件地址
            }
                SC.Send(MM);                                           //寄出信件                       
        }
        //顯示下方詳細資訊的方法▼
        public void getsignup_Data(int signup_no)
        {
            Signup_data_View signup_Data_View = new Signup_data_View(signup_no);
            wondow_show.Children.Clear();
            object content = signup_Data_View.Content;
            signup_Data_View.Content = null;
            wondow_show.Children.Add(content as UIElement);
        }
        //取的階段資料表的階段代表ID▼
        private int getstageID(string stage)
        {//取的階段資料表的階段代表ID
            int res = 10;
            var q = from s in Myentity.Stages
                    where s.Stage1 == stage
                    select s;
            foreach (var r in q)
            {
                res = r.Stage_ID;
            }
            return res;
        }
        //匯出ExcelStart▼
        private void btn_getExcel_Click(object sender, RoutedEventArgs e)
        {//判斷欄位數，並進行項目選擇
            if (this.DataGrid_1.Columns.Count >= 3)   //判斷dataGrid有沒有欄位
            {                 
                select_use.Clear();                   //清空集合內項目
                select_use.AddRange(select_everytime);//加入常用項目值
                select_canuse.Clear();                //清空集合內項目
                sp.Children.Clear();                  //清空以建立的控制項目
                w = new Window();                     //建立新的視窗控制項
                w.Height = 500;
                w.Width = 150;
                w.Title = "勾選匯出項目";
                sp.Orientation = Orientation.Vertical;//建立容器
                sp.Margin = new Thickness(5);
                sp.CanVerticallyScroll = true;

                for (int i = 0; i < this.DataGrid_1.Columns.Count; i += 1) //判斷dataGrid的欄位數
                {
                    if (DataGrid_1.Columns[i].Header != null)              //dataGrid欄位不為空時
                    {
                        select_canuse.Add(DataGrid_1.Columns[i].Header.ToString()); //加入可供選擇用的集合
                    }
                }
                for (int j = 0; j < select_canuse.Count; j += 1)           //依可供選擇用的集合數建立checkbox控制項
                {
                    CheckBox Checklist = new CheckBox();
                    Checklist.Content = select_canuse[j];
                    if (select_use.Contains(Checklist.Content))            //判斷名稱是否與常用的項目相同
                    {
                        Checklist.IsChecked = true;                         //相同就預設勾起來
                    }
                    Checklist.Width = 140;
                    Checklist.Margin = new Thickness(5);
                    sp.Children.Add(Checklist);
                    Checklist.Checked += Checklist_Checked;
                    Checklist.Unchecked += Checklist_Unchecked;
                }
                Button btn_select = new Button();                          //確認選取用按鈕
                btn_select.Width = 100;
                btn_select.Height = 30;
                btn_select.Click += btn_select_Click;
                btn_select.Content = "選擇";
                sp.Children.Add(btn_select);
                w.Content = sp;
                w.WindowStyle = WindowStyle.SingleBorderWindow;
                w.ResizeMode = ResizeMode.NoResize;
                w.Show();
            }
            else    //如果沒有欄位就不執行動作                              
            {
                return;
            }
        }

        //勾選項目後按的選擇鍵▼
        private void btn_select_Click(object sender, RoutedEventArgs e)
        {//勾選項目後按的選擇鍵
            string select = "";

            w.Close();                            //關閉選擇視窗
            if (select_use.Count != 0)
            {
                foreach (string s in select_use)  //將選取項目蒐集
                {
                    select += s + "\n";
                }
                System.Windows.Forms.MessageBox.Show($"選擇了:\n{select}等項目", "欲匯出的項目");  //顯示選取了哪些項目
                System.Windows.Forms.SaveFileDialog SF = new System.Windows.Forms.SaveFileDialog();//顯示儲存視窗
                SF.Filter = "(*.xlsx)|*.xlsx";                                                     //設定輸出檔案格式
                SF.AddExtension = true;
                if (SF.ShowDialog() == System.Windows.Forms.DialogResult.OK)                       //設定儲存路徑用視窗
                {
                    System.IO.FileStream f = new FileStream(SF.FileName, FileMode.Create);
                    getExcel(this.DataGrid_1, f);
                   // System.IO.FileInfo Fileinfo = new System.IO.FileInfo(SF.FileName);
                   // getExcel(this.DataGrid_1, Fileinfo);                                           //呼叫匯出Excel方法
                }
            }
            else
            {
                return;
            }
        }
        //private void getExcel(DataGrid dataGrid, FileInfo fS)
        //產生Excel的方法▼
        private void getExcel(DataGrid dataGrid, FileStream fS)
        {//產生Excel的方法
            int ListCount = 0;
            int ColumnsHeard = 0;
            int Rowscount = 0;
            //if (!File.Exists(fS.ToString()))
            //{
                ExcelPackage EP = new ExcelPackage(fS);
                ExcelWorksheet ws;
                if (dataGrid.Items.Count != 0)//設定工作表單名稱
                {
                    ws = EP.Workbook.Worksheets.Add(DateTime.Now.ToShortDateString() + "資料");
                }
                else
                {
                    MessageBox.Show("查詢無資料!!");
                    return;
                }

                for (int j = 0; j < dataGrid.Columns.Count; j++)
                {
                    if (ListCount != select_use.Count)//限制欄位數用
                    {
                        for (int i = 0; i < select_use.Count; i += 1)
                        {
                            if (dataGrid.Columns[j].Header != null && dataGrid.Columns[j].Header.ToString() == select_use[i])
                            {
                                Rowscount = 0;
                                ws.Cells[1, ColumnsHeard + 1].Value = dataGrid.Columns[j].Header;          //新增Excel的column名稱
                                ws.Cells.Style.Font.Size = 12;                                             //設定Excel欄位基本設定
                                ws.Column(ColumnsHeard + 1).Width += 20;
                                ws.Cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                                for (int k = 0; k < dataGrid.Items.Count; k += 1)                          //新增Excel的row值
                                {
                                    TextBlock x = dataGrid.Columns[j].GetCellContent(dataGrid.Items[k]) as TextBlock;
                                    if (x != null)
                                    {
                                        ws.Cells[Rowscount + 2, ColumnsHeard + 1].Value = x.Text; 
                                        Rowscount += 1;
                                    }
                                }
                                ColumnsHeard += 1;
                                ListCount += 1;
                            }
                        }
                    }
                    else
                    { break; }
                }
                EP.SaveAs(fS);
                System.Windows.Forms.MessageBox.Show("匯出成功", "Excel產生成功");
            }
        //}
        //判斷勾選項目加入集合▼
        private void Checklist_Checked(object sender, RoutedEventArgs e)
        {//判斷勾選項目加入集合
            if (((CheckBox)sender).IsChecked == true)
            {
                if (select_use.Count == 0)
                {
                    select_use.Add(((CheckBox)sender).Content.ToString());
                }
                else
                {
                    for (int i = 0; i < select_use.Count; i += 1)
                    {
                        if (select_use[i] == ((CheckBox)sender).Content.ToString())
                        {
                            return;
                        }
                    }
                    select_use.Add(((CheckBox)sender).Content.ToString());
                }
            }
        }
        //取消勾選▼
        private void Checklist_Unchecked(object sender, RoutedEventArgs e)
        {//取消勾選
            select_use.Remove(((CheckBox)sender).Content.ToString());
        }
        //身分分類選擇
        private void rbtn_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = e.Source as RadioButton;
            if (rb != null)
            {
                sign_up_type_use = rb.Content.ToString();
            }
        }
        //判斷勾跟X的方法
        private void getcheck_delete(List<int> A, List<int> D, Label AL, Label DL)
        {
            Volunteer_Applicant v = this.DataGrid_1.SelectedItem as Volunteer_Applicant;
            if (A.Count == 0)                                  
            {
                A.Add(v.申請編號);                           
                AL.Content = A.Count.ToString();        
                for (int i = 0; i < A.Count; i += 1)          
                {
                    for (int j = 0; j < D.Count; j += 1)
                    {
                        if (A[i] == D[j])        
                        {
                            D.Remove(D[j]);                            
                            DL.Content = D.Count.ToString();
                        }
                    }
                }
            }
            else                                                       
            {
                bool ok_in = false;                                    
                for (int i = 0; i < A.Count; i += 1)
                {
                    ok_in = true;                                     
                    if (A[i] == v.申請編號)                   
                    {
                        A.Remove(A[i]);              
                        ok_in = false;                                
                        AL.Content = A.Count.ToString();        
                        DL.Content = D.Count.ToString();
                        break;                                        
                    }
                }
                if (ok_in)                                         
                {
                    A.Add(v.申請編號);                   
                    D.Remove(v.申請編號);           
                    AL.Content = A.Count.ToString();       
                    DL.Content = D.Count.ToString();
                }
            }
        }
        //按下確認的方法
        private void getclick(List<int> AD, List<int> DL, string message, string state)
        {
            if (AD.Count != 0)                       //如果勾勾集合內有值的話進入
            {                                                //秀出一個確認視窗
                if (System.Windows.Forms.MessageBox.Show("確認通過" + AD.Count + "筆審核?如果電子郵件欄位為空，就不會發送信件", message, System.Windows.Forms.MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    var q = (from v in Myentity.Sign_up
                             select v).ToList();
                    for (int i = 0; i < AD.Count; i += 1)
                    {
                        for (int j = 0; j < q.Count; j += 1)
                        {
                            if (AD[i] == q[j].Sign_up_no)  //判斷集合內的值跟資料庫內的值是否相同
                            {
                                q[j].Stage = getstageID(state); //將狀態改成1，代表為要求面試
                                q[j].Approval_date = DateTime.Now; //新增審核日期為今天
                                q[j].supervision_ID = supervisionID;

                                // -wlc-
                                if (message == "面試審核")
                                {
                                    Volunteer volunteer = new Volunteer();
                                    volunteer.Chinese_name = q[j].Chinese_name;
                                    volunteer.English_name = q[j].English_name;
                                    volunteer.sex = q[j].Sex;
                                    volunteer.birthday = q[j].Birthday;
                                    volunteer.IDcrad_no = "A123456789";
                                    if (q[j].Sign_up_type == "學生志工")
                                    {
                                        volunteer.Identity_type = 2;
                                    }
                                    else
                                    {
                                        volunteer.Identity_type = 1;
                                    }
                                    volunteer.Join_date = q[j].Sign_up_date;
                                    if (int.TryParse(q[j].Phone, out int phone_no))
                                    {
                                        volunteer.Phone_no = phone_no;
                                    }
                                    if (int.TryParse(q[j].Mobile, out int mobile_no))
                                    {
                                        volunteer.Mobile_no = mobile_no;
                                    }
                                    volunteer.Address = q[j].Address;
                                    volunteer.Education = q[j].Education;
                                    volunteer.Lssuing_unit_no = 1;                                    

                                    Myentity.Volunteer.Add(volunteer);
                                }                                
                                // -wlc-

                                ThreadStart TS = new ThreadStart(
                                    delegate ()
                                    {
                                        sending_email(q[j].Email, q[i].Chinese_name + q[i].English_name, Issend_pass);//寄送郵件
                                        System.Windows.Forms.Application.DoEvents();
                                    });
                                send_mail = new Thread(TS);
                                send_mail.IsBackground = true;
                                send_mail.Start();
                                break;                             //判斷完一筆後離開
                            }
                        }
                    }
                }
            }
            if (DL.Count != 0)                         //如果XX集合內有值的話進入
            {                                                      //秀出一個確認視窗
                if (System.Windows.Forms.MessageBox.Show("確認駁回" + DL.Count + "筆審核?如果電子郵件欄位為空，就不會發送信件", "駁回確認", System.Windows.Forms.MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    var q = (from v in Myentity.Sign_up
                             select v).ToList();
                    for (int i = 0; i < DL.Count; i += 1)
                    {
                        for (int j = 0; j < q.Count; j += 1)
                        {
                            if (DL[i] == q[j].Sign_up_no)   //判斷集合內的值跟資料庫內的值是否相同
                            {
                                //this.Myentity.Sign_up.Remove(q[j]);  //執行刪除的動作   
                                q[j].Stage = getstageID(stage_reject); //代表駁回
                                q[j].Approval_date = DateTime.Now; //新增審核日期為今天
                                q[j].supervision_ID = supervisionID;
                                ThreadStart TS = new ThreadStart(
                                    delegate ()
                                    {
                                        sending_email(q[j].Email, q[i].Chinese_name + q[i].English_name, Issend_delete);//寄送郵件
                                        System.Windows.Forms.Application.DoEvents();
                                    });
                                send_mail = new Thread(TS);
                                send_mail.IsBackground = true;
                                send_mail.Start();
                                break;
                            }
                        }
                    }
                }
            }
        }

        public void Inertgroup_andexpertise(List<int> AD)
        {
            var Sign_up_list = (from n in Myentity.Sign_up
                     select n).ToList();
            var Sign_up_expertise_list = (from n in Myentity.Sign_up_expertise
                                          select n).ToList();

            var Sign_up_Service_period_list = (from n in Myentity.Sign_up_Service_period select n).ToList();

            var stage_id = (from n in Myentity.Stages where n.Stage1 == "未排班" && n.Stage_type == "排班意願" select n.Stage_ID).First();

            foreach (var row in AD)
            {
                Sign_up sign_Up = Sign_up_list.Where(p => p.Sign_up_no == row).First();

                var q = (from n in Myentity.Volunteer
                         where n.Chinese_name == sign_Up.Chinese_name &&
                              n.Join_date == sign_Up.Sign_up_date
                         select n).First();

                Service_Group1 service_Group1 = new Service_Group1();
                service_Group1.Volunteer_no = q.Volunteer_no;
                service_Group1.Group_no = 1;

                Myentity.Service_Group1.Add(service_Group1);

                var expertise_list = Sign_up_expertise_list.Where(p => p.Sign_up_no == row).ToList();
                foreach (var expertise in expertise_list)
                {
                    Expertise2 expertise2 = new Expertise2();
                    expertise2.Volunteer_no = q.Volunteer_no;
                    expertise2.Expertise_no = (int)expertise.Expertise;

                    Myentity.Expertise2.Add(expertise2);
                }

                var Service_period_list = Sign_up_Service_period_list.Where(p => p.Sign_up_no == row).ToList();
                foreach (var _period in Service_period_list)
                {
                    Service_period2 service_Period2 = new Service_period2();
                    service_Period2.Volunteer_no = q.Volunteer_no;
                    service_Period2.Service_period_no = _period.Service_period_no;
                    service_Period2.Stage = stage_id;
                    service_Period2.Wish_order = _period.Wish_order;

                    Myentity.Service_period2.Add(service_Period2);
                }
            }            

            this.Myentity.SaveChanges();
        }
    }
}
