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
    /// Shift_schedule_data_View.xaml 的互動邏輯
    /// </summary>
    public partial class Shift_schedule_data_View : Window
    {
        public Shift_schedule_data_View(int unit_id)
        {
            InitializeComponent();

            Shift_schedule_data_ViewModel shift_Schedule_Data_ViewModel = new Shift_schedule_data_ViewModel();
            shift_Schedule_Data_ViewModel.SelectShift_schedule(unit_id);
            int grid_row = 1;

            //時段顯示
            var repeat_periods = shift_Schedule_Data_ViewModel.Repeat_periods;
            var list = repeat_periods.Values.ToList();
            list.Sort();
            
            foreach (var row in list) 
            {
                Label label = new Label();
                label.Content = row;
                label.HorizontalAlignment = HorizontalAlignment.Center;
                label.VerticalAlignment = VerticalAlignment.Center;
                label.FontSize = 14;
                Grid.SetRow(label, grid_row);

                this.grid_schedule.Children.Add(label);
                grid_row++;

            }

            foreach (var row in shift_Schedule_Data_ViewModel.Shift_schedule_data_VMs)
            {
                int schedule_column = 0;
                switch (row.Service_period.Substring(0, 2))
                {                   
                    case "週一" :
                        schedule_column = 1;                        
                        break;
                    case "週二":
                        schedule_column = 2;
                        break;
                    case "週三":
                        schedule_column = 3;
                        break;
                    case "週四":
                        schedule_column = 4;
                        break;
                    case "週五":
                        schedule_column = 5;
                        break;
                    case "週六":
                        schedule_column = 6;
                        break;
                    case "週日":
                        schedule_column = 7;
                        break;
                }

                int schedule_row = list.FindIndex(p => p == row.Time) + 1;
                string str = row.Actual_number + "/" + row.Volunteer_number;

                Label label = new Label();
                label.Content = str;
                label.HorizontalAlignment = HorizontalAlignment.Right;
                label.VerticalAlignment = VerticalAlignment.Top;
                label.FontSize = 16;
                Grid.SetRow(label, schedule_row);
                Grid.SetColumn(label, schedule_column);
                this.grid_schedule.Children.Add(label);

                Button button = new Button();
                button.Content = "編輯";
                button.Height = 30;
                button.Width = 100;
                button.HorizontalAlignment = HorizontalAlignment.Center;
                button.VerticalAlignment = VerticalAlignment.Bottom;
                button.Background = new SolidColorBrush(Color.FromArgb(100, 120, 230, 200));
                button.BorderBrush = null;
                button.Foreground = Brushes.Gray;
                Grid.SetRow(button, schedule_row);
                Grid.SetColumn(button, schedule_column);
                this.grid_schedule.Children.Add(button);
            }
        }
    }
}
