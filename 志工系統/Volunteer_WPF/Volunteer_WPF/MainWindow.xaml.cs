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
using TabControlWithClose;
using Volunteer_WPF.View;
using Volunteer_WPF.View_Model;

namespace Volunteer_WPF
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();           
            
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            Login login = new Login();
            login.ShowDialog();

            if (login.login_ok)
            {
                string str = null;
                if (Account_ViewModel.Permission == "director")
                {
                    str =  "主任 " + Account_ViewModel.User;
                }
                if (Account_ViewModel.Permission == "Volunteer_supervision")
                {
                    str = "志工督導 " + Account_ViewModel.User;
                }
                
                this.username.Content = str;
            }
            else
            {
                this.Close();
            }

            UCTabItemWithClose NewTab = new UCTabItemWithClose() { Header = "首頁", Name = "首頁", Width = 84.4, Height = 30, FontSize = 12 };

            //NewTab.SetValue(UCTabItemWithClose.StyleProperty, Application.Current.Resources["TabItemStyle1"]);
            Style style = this.FindResource("TabItemStyle1") as Style;
            NewTab.Style = style;

            Home home = new Home();
            object content = home.Content;
            home.Content = null;
            NewTab.Content = content as UIElement;

            TabControl1.Items.Add(NewTab);
            TabControl1.SelectedItem = NewTab;
        }

        List<string> CheckList = new List<string> { "0" };
        
        bool Repeat = false;

        private void AddTabItem(string s)  //按Button時，新增一個同名的TabItem
        {
            UCTabItemWithClose NewTab = new UCTabItemWithClose() { Header = s, Name = s, Width = 84.4, Height = 30, FontSize = 12};
            NewTab.CheckClose += NewTab_CheckClose;         
            for (int i = 0; i < CheckList.Count(); i++)  //判斷有沒有重複的TabItem
            {

                if (NewTab.Name == CheckList[i])
                {
                    Repeat = true;
                    break;
                }
                else
                {
                    Repeat = false;
                    
                }
            }

            if (Repeat == false)  //沒有重複才新增TabItem
            {
                TabControl1.Items.Add(NewTab);
                TabControl1.SelectedItem = NewTab;
            }
                       
        }

        private void NewTab_CheckClose(object sender, EventArgs e)
        {
            UCTabItemWithClose SelectedTab = sender as UCTabItemWithClose;
            //刪除List裡面重複的
            for (int i = 0; i < CheckList.Count; i++)  //循環的次數
            {
                for (int j = CheckList.Count - 1; j > i; j--)  //比較的次數
                {
                    if (CheckList[i] == CheckList[j])
                    {
                        CheckList.RemoveAt(j);
                    }
                }
            }

            CheckList.Remove(SelectedTab.Name);
                     
        }

        private void Selected(string s)  //Tabitem已經產生後再次點擊自動跳轉之方法
        {
            foreach (TabItem item in TabControl1.Items)
            {
                if (item.Header.ToString() == s)
                {
                    TabControl1.SelectedItem = item;
                    break;
                }
            }
        }

        private void AddTabItem(string s, object content)  //按Button時，新增一個同名的TabItem
        {
            UCTabItemWithClose NewTab = new UCTabItemWithClose() {
                Header = s,
                Name = s,
                Height = 30
            };
            NewTab.CheckClose += NewTab_CheckClose;
            for (int i = 0; i < CheckList.Count(); i++)  //判斷有沒有重複的TabItem
            {

                if (NewTab.Name == CheckList[i])
                {
                    Repeat = true;
                    break;
                }
                else
                {
                    Repeat = false;

                }
            }

            if (Repeat == false)  //沒有重複才新增TabItem
            {
                NewTab.Content = content as UIElement;

                TabControl1.Items.Add(NewTab);
                TabControl1.SelectedItem = NewTab;
            }
        }

        private void NewVolunteerButton_Click(object sender, RoutedEventArgs e)  //新志工申請按鈕
        {
            Sign_up_View sign_Up_View = new Sign_up_View();
            object content = sign_Up_View.Content;
            sign_Up_View.Content = null;

            AddTabItem(NewVolunteerLabel.Content.ToString(), content);
            CheckList.Add(NewVolunteerLabel.Content.ToString());
            Selected(NewVolunteerLabel.Content.ToString());
        }

        private void ActivityButton_Click(object sender, RoutedEventArgs e)  //活動維護按鈕
        {
            Activity_View activity_View = new Activity_View();
            object content = activity_View.Content;
            activity_View.Content = null;

            AddTabItem(ActivityLabel.Content.ToString(), content);
            CheckList.Add(ActivityLabel.Content.ToString());
            Selected(ActivityLabel.Content.ToString());
        }

        private void EventButton_Click(object sender, RoutedEventArgs e)  //異常通報事件按鈕
        {
            Abnormal_event abnormal_Event = new Abnormal_event();
            object content = abnormal_Event.Content;
            abnormal_Event.Content = null;

            AddTabItem(EventLabel.Content.ToString(), content);
            CheckList.Add(EventLabel.Content.ToString());
            Selected(EventLabel.Content.ToString());
        }

        private void ManpowerButton_Click(object sender, RoutedEventArgs e)  //人力資源申請按鈕
        {
            Manpower_apply_View manpower_Apply_View = new Manpower_apply_View();
            object content = manpower_Apply_View.Content;
            manpower_Apply_View.Content = null;

            AddTabItem(ManpowerLabel.Content.ToString(), content);
            CheckList.Add(ManpowerLabel.Content.ToString());
            Selected(ManpowerLabel.Content.ToString());
        }

        private void AssessmentButton_Click(object sender, RoutedEventArgs e)  //考核按鈕
        {
            AddTabItem(AssessmentLabel.Content.ToString());
            CheckList.Add(AssessmentLabel.Content.ToString());
            Selected(AssessmentLabel.Content.ToString());
        }

        private void ReportButton_Click(object sender, RoutedEventArgs e)  //報表按鈕細項展開功能
        {
            this.ReportMenu.PlacementTarget = this.ReportButton;  //目標
            this.ReportMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom; //位置                       
            this.ReportMenu.IsOpen = true;  //顯示Menu 
        }

        private void MenuItem_MouseLeave(object sender, MouseEventArgs e)  //報表按鈕細項收起來功能
        {
            this.ReportMenu.IsOpen = false;
        }

        private void SetButton_Click(object sender, RoutedEventArgs e)  //設定按鈕細項展開功能
        {
            this.SetMenu.PlacementTarget = this.SetButton;  //目標
            this.SetMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;  //位置
            this.SetMenu.IsOpen = true;  //顯示Menu
        }

        private void MenuItem_MouseLeave_1(object sender, MouseEventArgs e)  //設定按鈕細項收起來功能
        {
            this.SetMenu.IsOpen = false;
        }

        private void SetMenu_PreviewMouseMove(object sender, MouseEventArgs e)  //滑鼠離開時關閉Menu
        {
            double x = e.GetPosition(SetMenu).X;
            double y = e.GetPosition(SetMenu).Y;

            if (x < 10 || y < -50 || x > SetMenu.ActualWidth || y > SetMenu.ActualHeight)
            {
                SetMenu.ReleaseMouseCapture();
            }
        }

        private void Class_Click(object sender, RoutedEventArgs e)  //班表按鈕
        {
            Shift_schedule_View shift_Schedule_View = new Shift_schedule_View();
            object content = shift_Schedule_View.Content;
            shift_Schedule_View.Content = null;

            AddTabItem(ClassItem.Header.ToString(), content);
            CheckList.Add(ClassItem.Header.ToString());
            Selected(ClassItem.Header.ToString());
        }

        private void VolDataItem_Click(object sender, RoutedEventArgs e)  //志工基本資料維護按鈕
        {
            Volunteer_View volunteer_View = new Volunteer_View();
            object content = volunteer_View.Content;
            volunteer_View.Content = null;

            AddTabItem(VolDataItem.Header.ToString(), content);
            CheckList.Add(VolDataItem.Header.ToString());
            Selected(VolDataItem.Header.ToString());
        }

        private void AplDataItem_Click(object sender, RoutedEventArgs e)  //運用單位基本資料維護按鈕
        {
            Application_unit_View application_Unit_View = new Application_unit_View();
            object content = application_Unit_View.Content;
            application_Unit_View.Content = null;

            AddTabItem(AplDataItem.Header.ToString(), content);
            CheckList.Add(AplDataItem.Header.ToString());
            Selected(AplDataItem.Header.ToString());
        }

        private void VolPowerItem_Click(object sender, RoutedEventArgs e)  //志工人力評估項目設定按鈕
        {
            
            AddTabItem(VolPowerItem.Header.ToString());
            CheckList.Add(VolPowerItem.Header.ToString());
            Selected(VolPowerItem.Header.ToString());
        }

        private void BasicItem_Click(object sender, RoutedEventArgs e)  //基本設定檔維護按鈕
        {
            BasicItem basicitem_View = new BasicItem();
            object content = basicitem_View.Content;
            basicitem_View.Content = null;

            AddTabItem(BasicItem.Header.ToString(), content);
            CheckList.Add(BasicItem.Header.ToString());
            Selected(BasicItem.Header.ToString());
        }

        private void VolSuperItem_Click(object sender, RoutedEventArgs e)  //志工督導維護按鈕
        {
            Volunteer_supervision_View volunteer_Supervision_View = new Volunteer_supervision_View();
            object content = volunteer_Supervision_View.Content;
            volunteer_Supervision_View.Content = null;

            AddTabItem(VolSuperItem.Header.ToString(), content);
            CheckList.Add(VolSuperItem.Header.ToString());
            Selected(VolSuperItem.Header.ToString());
        }

        private void Abnormaleventitem_Click(object sender, RoutedEventArgs e)//異常事件分析按鈕
        {
            Abnormal_event_analysis_View abnormal_Event_Analysis_View = new Abnormal_event_analysis_View();
            object content = abnormal_Event_Analysis_View.Content;
            abnormal_Event_Analysis_View.Content = null;

            AddTabItem(Abnormaleventitem.Header.ToString(),content);
            CheckList.Add(Abnormaleventitem.Header.ToString());
            Selected(Abnormaleventitem.Header.ToString());
        }
    }
}
