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
                this.username.Content = Account_ViewModel.User;
            }
            else
            {
                this.Close();
            }
        }
                
        List<string> CheckList = new List<string>();
        bool Repeat = false;

        private void AddTabItem(string s)  //按Button時，新增一個同名的TabItem
        {
            TabItem NewTab = new TabItem() { Header = s, Name = s, Width = 84.4, Height = 30, FontSize = 9 };

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

        private void AddTabItem(string s,object content)  //按Button時，新增一個同名的TabItem
        {
            TabItem NewTab = new TabItem() { Header = s, Name = s, Width = 84.4, Height = 30, FontSize = 9 };

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
        }

        private void ActivityButton_Click(object sender, RoutedEventArgs e)  //活動維護按鈕
        {
            AddTabItem(ActivityLabel.Content.ToString());
            CheckList.Add(ActivityLabel.Content.ToString());
        }

        private void EventButton_Click(object sender, RoutedEventArgs e)  //異常通報事件按鈕
        {
            AddTabItem(EventLabel.Content.ToString());
            CheckList.Add(EventLabel.Content.ToString());
        }

        private void ManpowerButton_Click(object sender, RoutedEventArgs e)  //人力資源申請按鈕
        {
            AddTabItem(ManpowerLabel.Content.ToString());
            CheckList.Add(ManpowerLabel.Content.ToString());
        }

        private void AssessmentButton_Click(object sender, RoutedEventArgs e)  //考核按鈕
        {
            AddTabItem(AssessmentLabel.Content.ToString());
            CheckList.Add(AssessmentLabel.Content.ToString());
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
            AddTabItem(ClassItem.Header.ToString());
            CheckList.Add(ClassItem.Header.ToString());
        }

        private void VolDataItem_Click(object sender, RoutedEventArgs e)  //志工基本資料維護按鈕
        {
            AddTabItem(VolDataItem.Header.ToString());
            CheckList.Add(VolDataItem.Header.ToString());
        }

        private void AplDataItem_Click(object sender, RoutedEventArgs e)  //運用單位基本資料維護按鈕
        {
            AddTabItem(AplDataItem.Header.ToString());
            CheckList.Add(AplDataItem.Header.ToString());
        }

        private void VolPowerItem_Click(object sender, RoutedEventArgs e)  //志工人力評估項目設定按鈕
        {
            AddTabItem(VolPowerItem.Header.ToString());
            CheckList.Add(VolPowerItem.Header.ToString());
        }

        private void BasicItem_Click(object sender, RoutedEventArgs e)  //基本設定檔維護按鈕
        {
            AddTabItem(BasicItem.Header.ToString());
            CheckList.Add(BasicItem.Header.ToString());
        }

        private void VolSuperItem_Click(object sender, RoutedEventArgs e)  //志工督導維護按鈕
        {
            AddTabItem(VolSuperItem.Header.ToString());
            CheckList.Add(VolSuperItem.Header.ToString());
        }

        
    }
}
