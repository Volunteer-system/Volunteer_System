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

namespace Volunteer_WPF.View
{
    /// <summary>
    /// Sign_up_View.xaml 的互動邏輯
    /// </summary>
    public partial class Sign_up_View : Window
    {
        public Sign_up_View()
        {
            InitializeComponent();
            this.DataGrid_1.IsReadOnly = true;
            this.DataGrid_1.Columns[0].Visibility = Visibility.Hidden;
            this.DataGrid_1.Columns[1].Visibility = Visibility.Hidden;
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
        int stage_first = 1;//1                        //審核階段的代表值變數
        int stage_interview = 2;//4
        int stage_pass_applicant = 3;//5
        int stage_reject = 4;                          //代表駁回
        int stage_NotUse = 9;                          //非審核階段的代表值

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
            //public int Sign_up_no { get; set; }
            //public string Stage { get; set; }
            //public string Name { get; set; }
            //public string Sex { get; set; }
            //public DateTime Birthday { get; set; }
            //public string Personality_scale { get; set; }
            //public string Phone { get; set; }
            //public string Mobile { get; set; }
            //public string Email { get; set; }
            //public string Education { get; set; }
            //public string Job { get; set; }
            //public string Address { get; set; }
            //public DateTime Approval_date { get; set; }
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
            Volunteer_Applicant v = this.DataGrid_1.SelectedItem as Volunteer_Applicant;          
            if (l_click_ok.Count == 0)                                  //沒點選任何勾勾時進入
            {
                l_click_ok.Add(v.申請編號);                             //點選的項目加入集合  
                lab_check.Content = l_click_ok.Count.ToString();        //計算勾選數
                for (int i = 0; i < l_click_ok.Count; i += 1)           //判斷使否存在於打XX集合內
                {
                    for (int j = 0; j < l_click_delete.Count; j += 1)
                    {
                        if (l_click_ok[i] == l_click_delete[j])         //如果有
                        {
                            l_click_delete.Remove(l_click_delete[j]);   //刪除XX集合內的值                          
                            lab_Dcheck.Content = l_click_delete.Count.ToString();//計算打X數
                        }
                    }
                }
            }
            else                                                       //集合內已經有值時進入
            {
                bool ok_in = false;                                    //判斷是否有相同號碼用
                for (int i = 0; i < l_click_ok.Count; i += 1)
                {
                    ok_in = true;                                      //狀態設成可以加入
                    if (l_click_ok[i] == v.申請編號)                   //如果已經存在同樣的號碼，代表同號碼點選2次，要取消點選
                    {
                        l_click_ok.Remove(l_click_ok[i]);              //就從集合內刪掉該號碼
                        ok_in = false;                                 //狀態設成不可加入
                        lab_check.Content = l_click_ok.Count.ToString();        //計算勾選數
                        lab_Dcheck.Content = l_click_delete.Count.ToString();//計算打X數
                        break;                                         //離開迴圈
                    }
                }
                   if(ok_in)                                           //如果狀態為可以加入
                {
                        l_click_ok.Add(v.申請編號);                    //該號碼加入集合
                        l_click_delete.Remove(v.申請編號);             //刪除XX集合內的值
                    lab_check.Content = l_click_ok.Count.ToString();        //計算勾選數
                    lab_Dcheck.Content = l_click_delete.Count.ToString();//計算打X數
                }               
            }
        }
        //儲存按下XX的人▼
        private void Check_interview_MU_Click_Delete(object sender, EventArgs e)
        {//儲存按下XX的人
            Volunteer_Applicant v = this.DataGrid_1.SelectedItem as Volunteer_Applicant;           
            if (l_click_delete.Count == 0)                        //沒點選任何XX時進入
            {
                l_click_delete.Add(v.申請編號);                   //點選的項目加入集合
                lab_Dcheck.Content = l_click_delete.Count.ToString();//計算打X數
                for (int i = 0; i < l_click_delete.Count; i += 1) //判斷使否存在於打勾勾集合內
                {
                    for (int j = 0; j < l_click_ok.Count; j += 1)
                    {
                        if (l_click_delete[i] == l_click_ok[j])   //如果有
                        {
                            l_click_ok.Remove(l_click_ok[j]);     //刪除勾勾集合內的值 
                            lab_check.Content = l_click_ok.Count.ToString(); //計算勾選數
                        }
                    }
                }
            }
            else                                                  //集合內已經有值時進入
            {
                bool ok_in = false;                               //判斷是否有相同號碼用
                for (int i = 0; i < l_click_delete.Count; i += 1)
                {
                    ok_in = true;                                 //狀態設成可以加入
                    if (l_click_delete[i] == v.申請編號)          //如果已經存在同樣的號碼，代表同號碼點選2次，要取消點選
                    {
                        l_click_delete.Remove(l_click_delete[i]); //就從集合內刪掉該號碼
                        ok_in = false;                            //狀態設成不可加入
                        lab_check.Content = l_click_ok.Count.ToString();     //計算勾選數
                        lab_Dcheck.Content = l_click_delete.Count.ToString();//計算打X數
                        break;                                    //離開迴圈
                    }
                }
                if (ok_in)                                        //如果狀態為可以加入
                {
                    l_click_delete.Add(v.申請編號);               //該號碼加入集合
                    l_click_ok.Remove(v.申請編號);                //刪除勾勾集合內的值
                    lab_check.Content = l_click_ok.Count.ToString();     //計算勾選數
                    lab_Dcheck.Content = l_click_delete.Count.ToString();//計算打X數
                }                                   
            }
        }
        //按下確認▼
        private void btn_check_Click(object sender, RoutedEventArgs e)
        {
            if (l_click_ok.Count != 0 || l_click_delete.Count != 0)  //如果勾勾或XX集合內有值的話進入
            {
                if (first)                                           //如果是在初次審核的頁面
                {
                    if (l_click_ok.Count != 0)                       //如果勾勾集合內有值的話進入
                    {                                                //秀出一個確認視窗
                        if (System.Windows.Forms.MessageBox.Show("確認通過" + l_click_ok.Count + "筆審核?如果電子郵件欄位為空，就不會發送信件", "初次審核", System.Windows.Forms.MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                        {
                            var q = (from v in Myentity.Sign_up
                                     select v).ToList();
                            for (int i = 0; i < l_click_ok.Count; i += 1)
                            {
                                for (int j = 0; j < q.Count; j += 1)
                                {
                                    if (l_click_ok[i] == q[j].Sign_up_no)  //判斷集合內的值跟資料庫內的值是否相同
                                    {
                                        q[j].Stage = stage_interview;      //將狀態改成1，代表為要求面試
                                        q[j].Approval_date = DateTime.Now; //新增審核日期為今天
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
                    if (l_click_delete.Count != 0)                         //如果XX集合內有值的話進入
                    {                                                      //秀出一個確認視窗
                        if (System.Windows.Forms.MessageBox.Show("確認駁回" + l_click_delete.Count + "筆審核?如果電子郵件欄位為空，就不會發送信件", "駁回確認", System.Windows.Forms.MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                        {
                            var q = (from v in Myentity.Sign_up
                                     select v).ToList();
                            for (int i = 0; i < l_click_delete.Count; i += 1)
                            {
                                for (int j = 0; j < q.Count; j += 1)
                                {
                                    if (l_click_delete[i] == q[j].Sign_up_no)   //判斷集合內的值跟資料庫內的值是否相同
                                    {
                                        //this.Myentity.Sign_up.Remove(q[j]);  //執行刪除的動作   
                                        q[j].Stage = stage_reject;             //代表駁回
                                        q[j].Approval_date = DateTime.Now; //新增審核日期為今天
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
                    this.Myentity.SaveChanges();                               //將修改存回資料庫
                    this.DataGrid_1.ItemsSource = null;                        //更新該頁面
                    this.DataGrid_1.ItemsSource = get_volunteer(stage_first);  //重新呼叫資料
                }
                else if (interview || selection)                                            //如果是在面試審核的頁面
                {
                    if (l_click_ok.Count != 0)                                 //如果勾勾集合內有值的話進入
                    {                                                          //秀出一個確認視窗
                        if (System.Windows.Forms.MessageBox.Show("確認通過" + l_click_ok.Count + "筆審核?如果電子郵件欄位為空，就不會發送信件", "面試審核", System.Windows.Forms.MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                        {
                            var q = (from v in Myentity.Sign_up
                                     select v).ToList();
                            for (int i = 0; i < l_click_ok.Count; i += 1)      //判斷集合內的值跟資料庫內的值是否相同
                            {
                                for (int j = 0; j < q.Count; j += 1)
                                {
                                    if (l_click_ok[i] == q[j].Sign_up_no)
                                    {
                                        q[j].Stage = stage_pass_applicant;     //將狀態改成2，代表為審核通過 
                                        q[j].Approval_date = DateTime.Now;     //新增審核日期為今天
                                        ThreadStart TS = new ThreadStart(
                                            delegate ()
                                            {
                                                sending_email(q[j].Email, q[i].Chinese_name + q[i].English_name, Issend_pass);//寄送郵件
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
                    if (l_click_delete.Count != 0)                             //如果XX集合內有值的話進入
                    {                                                          //秀出一個確認視窗
                        if (System.Windows.Forms.MessageBox.Show("確認駁回" + l_click_delete.Count + "筆審核?如果電子郵件欄位為空，就不會發送信件", "駁回確認", System.Windows.Forms.MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                        {
                            var q = (from v in Myentity.Sign_up
                                     select v).ToList();
                            for (int i = 0; i < l_click_delete.Count; i += 1) //判斷集合內的值跟資料庫內的值是否相同
                            {
                                for (int j = 0; j < q.Count; j += 1)
                                {
                                    if (l_click_delete[i] == q[j].Sign_up_no)
                                    {
                                        //this.Myentity.Sign_up.Remove(q[j]); //執行刪除的動作
                                        q[j].Stage = stage_reject;            //代表駁回
                                        q[j].Approval_date = DateTime.Now; //新增審核日期為今天
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
                    this.Myentity.SaveChanges();                              //將修改存回資料庫
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
        private IEnumerable get_volunteer(int n)
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
                        where v.Stage == n && (D_Start_date.Text == "" ? true : v.Sign_up_date >= D_Start_date.SelectedDate) &&( D_End_date.Text == "" ? true : v.Sign_up_date <= D_End_date.SelectedDate)
                     select new { Sign_up_no = v.Sign_up_no, Stage  = v.Stage,Stagetype = s.Stage_type,SignupName = v.Chinese_name + " " + v.English_name ,Sex = v.Sex, Birthday =v.Birthday, Personality_scale  = v.Personality_scale,Phone = v.Phone,Mobile = v.Mobile,Email = v.Email,Education=v.Education,Job = v.Job,Address=v.Address, Sign_up_date =v.Sign_up_date, Approval_date = v.Approval_date}).ToList();
                foreach (var v in q)
                {  //申請編號 = v.Sign_up_no    //"初次申請""已要求面試" "已通過審核""已駁回申請"
                    volunteer.Add(new Volunteer_Applicant() { 申請編號 = v.Sign_up_no,申請階段 = Convert.ToInt32(v.Stage) == stage_first ? v.Stagetype : Convert.ToInt32(v.Stage) == stage_interview ? v.Stagetype : Convert.ToInt32(v.Stage) == stage_pass_applicant ? v.Stagetype : Convert.ToInt32(v.Stage) == stage_reject ? v.Stagetype : "其他", 申請人姓名 =v.SignupName, 性別 = v.Sex, 生日 = Convert.ToDateTime(v.Birthday).ToShortDateString(), 人格量表結果 = v.Personality_scale, 電話號碼 = v.Phone, 手機號碼 = v.Mobile, 電子郵件 = v.Email, 教育程度 = v.Education, 職業 = v.Job, 聯絡地址 = v.Address, 申請日期 = Convert.ToDateTime(v.Sign_up_date).ToShortDateString(), 審核日期 = Convert.ToDateTime(v.Approval_date).ToShortDateString() });
                }
            }
            else //未進任何頁面時點選查詢
            {
               var q = (from v in Myentity.Sign_up
                        join s in Myentity.Stages on v.Stage equals s.Stage_ID
                        where (D_Start_date.Text == "" ? true : v.Sign_up_date >= D_Start_date.SelectedDate) && (D_End_date.Text == "" ? true : v.Sign_up_date <= D_End_date.SelectedDate)
                        select new { Sign_up_no = v.Sign_up_no, Stage = v.Stage, Stagetype = s.Stage_type, SignupName = v.Chinese_name + " " + v.English_name, Sex = v.Sex, Birthday = v.Birthday, Personality_scale = v.Personality_scale, Phone = v.Phone, Mobile = v.Mobile, Email = v.Email, Education = v.Education, Job = v.Job, Address = v.Address, Sign_up_date = v.Sign_up_date, Approval_date = v.Approval_date }).ToList();
                foreach (var v in q)
                {  //申請編號 = v.Sign_up_no    //"初次申請""已要求面試" "已通過審核""已駁回申請"
                    volunteer.Add(new Volunteer_Applicant() { 申請編號 = v.Sign_up_no, 申請階段 = Convert.ToInt32(v.Stage) == stage_first ? v.Stagetype : Convert.ToInt32(v.Stage) == stage_interview ? v.Stagetype : Convert.ToInt32(v.Stage) == stage_pass_applicant ? v.Stagetype : Convert.ToInt32(v.Stage) == stage_reject ? v.Stagetype : "其他", 申請人姓名 = v.SignupName, 性別 = v.Sex, 生日 = Convert.ToDateTime(v.Birthday).ToShortDateString(), 人格量表結果 = v.Personality_scale, 電話號碼 = v.Phone, 手機號碼 = v.Mobile, 電子郵件 = v.Email, 教育程度 = v.Education, 職業 = v.Job, 聯絡地址 = v.Address, 申請日期 = Convert.ToDateTime(v.Sign_up_date).ToShortDateString(), 審核日期 = Convert.ToDateTime(v.Approval_date).ToShortDateString() });
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
                if (first || interview)
                {
                    body = "志工申請駁回";
                }
                else
                {
                    body = "志工資料移除";
                }
                MM.Subject = "志工申請駁回通知";                                  //信件主旨
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
    }
}
