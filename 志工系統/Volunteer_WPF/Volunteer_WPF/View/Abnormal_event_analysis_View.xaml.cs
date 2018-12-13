using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Volunteer_WPF.View_Model;

namespace Volunteer_WPF.View
{
    /// <summary>
    /// Abnormal_event_analysis_View.xaml 的互動邏輯
    /// </summary>
    public partial class Abnormal_event_analysis_View : Window
    {
        List<string> unit_Names = new List<string>();
        List<string> select_use = new List<string>();   //已勾選的
        List<string> select_canuse = new List<string>();//可供勾選的欄位
        Window w;                                       //產生新視窗
        StackPanel sp = new StackPanel();               //新視窗內的容器
        string[] select_everytime = new string[] { "事件主旨", "志工姓名", "運用單位", "通報時間", "結案時間", "志工督導" }; //常用輸出項目

        public Abnormal_event_analysis_View()
        {
            InitializeComponent();

            choose_unit_analysis_ViewModel choose_Unit_Analysis_ViewModel = new choose_unit_analysis_ViewModel();
            List<string> event_categorys = choose_Unit_Analysis_ViewModel.Selectevent_category();
            this.cbb_category.ItemsSource = event_categorys;

            this.dp_startdate.SelectedDate = DateTime.Now.Date.AddMonths(-3);
            this.dp_enddate.SelectedDate = DateTime.Now.Date;
        }

        private void btn_selectabnormal_event_Click(object sender, RoutedEventArgs e)
        {            
            DateTime startdate = ((dp_startdate.SelectedDate == null) ? DateTime.MinValue : (DateTime)dp_startdate.SelectedDate);
            DateTime enddate = ((dp_enddate.SelectedDate == null) ? DateTime.MinValue : (DateTime)dp_enddate.SelectedDate);
            string event_category = this.cbb_category.Text;
            Abnormal_event_analysis_ViewModel abnormal_Event_Analysis_ViewModel = new Abnormal_event_analysis_ViewModel();
            List<Abnormal_event_analysis_ViewModel> event_analysiss = abnormal_Event_Analysis_ViewModel.SelectAbnormal_event_analysis(startdate, enddate, event_category, unit_Names);
            ObservableCollection<Abnormal_event_analysiss> abnormal_event_analysislist = new ObservableCollection<Abnormal_event_analysiss>();
            foreach (var row in event_analysiss)
            {
                abnormal_event_analysislist.Add(new Abnormal_event_analysiss {
                    事件案號 = row.Abnormal_event_ID,
                    事件主旨 = row.Abnormal_event,
                    志工姓名 = row.Volunteer_name,
                    運用單位 = row.Application_unit,
                    異常事件類別 = row.Event_category,
                    處理階段 = row.Stage,
                    通報時間 = row.Notification_date,
                    結案時間 = row.Closing_date,
                    志工督導 = row.Supervisor,
                    處理結果 = row.Result,
                    運用單位描述 = row.Unit_descrition,
                    志工自述 = row.Volunteer_description,
                    志工督導描述 = row.Supervisor_description,
                    志工幹部 = row.Volunteer_leader,
                    志工督導主管 = row.Supervisor_heads,
                    駁回原因 = row.Rejection_Reason
                });
            }

            this.dg_abnormal_event.ItemsSource = abnormal_event_analysislist;

            Abnormal_event_analysis_chart_View abnormal_Event_Analysis_Chart_View = new Abnormal_event_analysis_chart_View(abnormal_Event_Analysis_ViewModel);
            //abnormal_Event_Analysis_Chart_View.Show();
            wondow_show.Children.Clear();
            object content = abnormal_Event_Analysis_Chart_View.Content;
            abnormal_Event_Analysis_Chart_View.Content = null;
            wondow_show.Children.Add(content as UIElement);
        }

        private void btn_eventlist_excel_Click(object sender, RoutedEventArgs e)
        {
            
            //確認datagrid view是有資料的
            if (this.dg_abnormal_event.Columns.Count()>0)
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
                for (int i = 0; i < this.dg_abnormal_event.Columns.Count; i += 1) //判斷dataGrid的欄位數
                {
                    if (dg_abnormal_event.Columns[i].Header != null)              //dataGrid欄位不為空時
                    {
                        select_canuse.Add(dg_abnormal_event.Columns[i].Header.ToString()); //加入可供選擇用的集合
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
                    Checklist.Checked += Checklist_Checked; ;
                    Checklist.Unchecked += Checklist_Unchecked; ;
                }
                Button btn_select = new Button();                          //確認選取用按鈕
                btn_select.Width = 100;
                btn_select.Height = 30;
                btn_select.Click += Btn_select_Click; ;
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

        private void Btn_select_Click(object sender, RoutedEventArgs e)
        {
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
                    FileStream f = new FileStream(SF.FileName, FileMode.Create);
                    getExcel(this.dg_abnormal_event, f);
                    // System.IO.FileInfo Fileinfo = new System.IO.FileInfo(SF.FileName);
                    // getExcel(this.DataGrid_1, Fileinfo);                                           //呼叫匯出Excel方法
                }
            }
            else
            {
                return;
            }
        }

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

        private void Checklist_Unchecked(object sender, RoutedEventArgs e)
        {
            select_use.Remove(((CheckBox)sender).Content.ToString());
        }

        private void Checklist_Checked(object sender, RoutedEventArgs e)
        {
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

        private void dg_abnormal_event_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }

        private void btn_application_unit_Click(object sender, RoutedEventArgs e)
        {
            choose_unit_analysis_View windows = new choose_unit_analysis_View();
            windows.ShowDialog();
            unit_Names = windows.Application_unit_Names;
            string str = "";
            foreach (var row in unit_Names)
            {
                str += row + ", ";
            }

            this.txt_unit.Text = str;
        }
    }

    class Application_unit_Name
    {
        public string 運用單位 { get; set; }
    }

    class Abnormal_event_analysiss
    {
        public string 事件案號 { get; set; }
        public string 事件主旨 { get; set; }
        public string 志工姓名 { get; set; }
        public string 運用單位 { get; set; }
        public string 異常事件類別 { get; set; }
        public string 處理階段 { get; set; }
        public string 通報時間 { get; set; }
        public string 結案時間 { get; set; }
        public string 志工督導 { get; set; }
        public string 處理結果 { get; set; }
        public string 運用單位描述 { get; set; }
        public string 志工自述 { get; set; }
        public string 志工督導描述 { get; set; }
        public string 志工幹部 { get; set; }
        public string 志工督導主管 { get; set; }
        public string 駁回原因 { get; set; }
    }
}
