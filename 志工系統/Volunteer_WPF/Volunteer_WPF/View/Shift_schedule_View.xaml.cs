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
using Volunteer_WPF.View_Model;

namespace Volunteer_WPF.View
{
    /// <summary>
    /// Shift_schedule_View.xaml 的互動邏輯
    /// </summary>
    public partial class Shift_schedule_View : Window
    {
        List<string> CheckList = new List<string> { "0" };
        bool Repeat = false;

        public Shift_schedule_View()
        {
            InitializeComponent();

            Shift_schedule_ViewModel shift_Schedule_ViewModel = new Shift_schedule_ViewModel();
            List<Button> btn_units = new List<Button>();
            foreach(var row in shift_Schedule_ViewModel.SelectApplication_unit())
            {
                Button btn = new Button();
                btn.Name = "btn_" + row.Application_unit;
                btn.Tag = row.Application_unit_no;
                btn.Content = row.Application_unit;
                btn.Height = 35;
                btn.Width = 120;
                btn.Margin = new Thickness(2);
                btn.FontSize = 14;
                btn.Background = new SolidColorBrush(Color.FromArgb(100, 120, 230, 200));
                btn.BorderBrush = null;
                btn.Foreground = Brushes.Gray;
                //Style style = this.FindResource("ButtonTemplate") as Style;
                //btn.Style = style;
                btn.Click += Btn_Unit_Click;

                btn_units.Add(btn);
            }
            this.list_units.ItemsSource = btn_units;
        }

        private void Btn_Unit_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Shift_schedule_data_View shift_Schedule_Data_View = new Shift_schedule_data_View((int)btn.Tag);
            object content = shift_Schedule_Data_View.Content;
            shift_Schedule_Data_View.Content = null;

            AddTabItem(btn.Content.ToString(), content);
            CheckList.Add(btn.Content.ToString());
            Selected(btn.Content.ToString());
        }

        private void AddTabItem(string s, object content)  //按Button時，新增一個同名的TabItem
        {
            UCTabItemWithClose NewTab = new UCTabItemWithClose() { Header = s, Name = s, Height = 30, FontSize = 12 };
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
    }
}
