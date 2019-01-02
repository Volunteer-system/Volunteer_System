using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Activity_volunteer_list_View.xaml 的互動邏輯
    /// </summary>
    public partial class Activity_volunteer_list_View : Window
    {
        ObservableCollection<Activity_Volunteer_List> activity_Volunteer_Lists = new ObservableCollection<Activity_Volunteer_List>();
        List<int> insert_list = new List<int>();
        List<int> delete_list = new List<int>();
        int activity_no;

        public Activity_volunteer_list_View(string activity_name, DateTime start_date)
        {
            InitializeComponent();            
                 
            Activity_volunteer_list_ViewModel activity_Volunteer_List_ViewModel = new Activity_volunteer_list_ViewModel();
            foreach (var row in activity_Volunteer_List_ViewModel.SelectVolunteer_list(activity_name, start_date))
            {
                activity_no = row.Activity_no;

                Activity_Volunteer_List activity_Volunteer_List = new Activity_Volunteer_List();
                activity_Volunteer_List.volunteer_no = row.Volunteer_no;
                activity_Volunteer_List.姓名 = row.Volunteer;
                activity_Volunteer_List.聯絡電話 = row.Phone;
                activity_Volunteer_List.素食 = row.Vegetarian;

                activity_Volunteer_Lists.Add(activity_Volunteer_List);
            }

            this.dg_Volunteer_list.ItemsSource = activity_Volunteer_Lists;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            this.txt_Addvolunteer.Visibility = Visibility.Collapsed;
            this.btn_Addvolunteer.Visibility = Visibility.Collapsed;

            this.dg_Volunteer_list.Columns[1].Visibility = Visibility.Collapsed;
        }

        private void CB_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.dg_Volunteer_list.SelectedItem != null)
            {
                if (delete_list.Where(p => p == (this.dg_Volunteer_list.SelectedItem as Activity_Volunteer_List).volunteer_no).Count() > 0)
                {
                    delete_list.Remove((this.dg_Volunteer_list.SelectedItem as Activity_Volunteer_List).volunteer_no);
                }

                if (insert_list.Where(p => p == (this.dg_Volunteer_list.SelectedItem as Activity_Volunteer_List).volunteer_no).Count() == 0)
                {
                    insert_list.Add((this.dg_Volunteer_list.SelectedItem as Activity_Volunteer_List).volunteer_no);
                }
            }

            int index = this.dg_Volunteer_list.SelectedIndex;
            DataGridRow Drow = (DataGridRow)dg_Volunteer_list.ItemContainerGenerator.ContainerFromIndex(index);
            Drow.Background = Brushes.White;
            Drow.Foreground = Brushes.Black;
        }

        private void CB_Checked(object sender, RoutedEventArgs e)
        {
            if (this.dg_Volunteer_list.SelectedItem != null)
            {
                if (delete_list.Where(p => p == (this.dg_Volunteer_list.SelectedItem as Activity_Volunteer_List).volunteer_no).Count() == 0)
                {
                    delete_list.Add((this.dg_Volunteer_list.SelectedItem as Activity_Volunteer_List).volunteer_no);
                }

                if (insert_list.Where(p => p == (this.dg_Volunteer_list.SelectedItem as Activity_Volunteer_List).volunteer_no).Count() > 0)
                {
                    insert_list.Remove((this.dg_Volunteer_list.SelectedItem as Activity_Volunteer_List).volunteer_no);
                }
            }            

            int index = this.dg_Volunteer_list.SelectedIndex;
            DataGridRow Drow = (DataGridRow)dg_Volunteer_list.ItemContainerGenerator.ContainerFromIndex(index);
            Drow.Background = Brushes.IndianRed;
            Drow.Foreground = Brushes.White;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Activity_volunteer_list_ViewModel activity_Volunteer_List_ViewModel = new Activity_volunteer_list_ViewModel();
            activity_Volunteer_List_ViewModel.InsertVoluteer_list(insert_list, activity_no);
            activity_Volunteer_List_ViewModel.DeleteVoluteer_list(delete_list, activity_no);

            MessageBox.Show("更新成功");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.txt_Addvolunteer.Visibility = Visibility.Visible;
            this.btn_Addvolunteer.Visibility = Visibility.Visible;
            
        }

        private void Btn_Addvolunteer_Click(object sender, RoutedEventArgs e)
        {
            string Volinteer_name = this.txt_Addvolunteer.Text;
            Activity_volunteer_list_ViewModel activity_Volunteer_List_ViewModel = new Activity_volunteer_list_ViewModel();
            activity_Volunteer_List_ViewModel.SelectVolunteer_byvolunteer_name(Volinteer_name);

            Activity_Volunteer_List activity_Volunteer_List = new Activity_Volunteer_List();
            activity_Volunteer_List.volunteer_no = activity_Volunteer_List_ViewModel.Volunteer_no;
            activity_Volunteer_List.姓名 = this.txt_Addvolunteer.Text;
            activity_Volunteer_List.聯絡電話 = activity_Volunteer_List_ViewModel.Phone;

            activity_Volunteer_Lists.Add(activity_Volunteer_List);
            insert_list.Add(activity_Volunteer_List_ViewModel.Volunteer_no);
            int index = activity_Volunteer_Lists.IndexOf(activity_Volunteer_List);

            this.txt_Addvolunteer.Visibility = Visibility.Collapsed;
            this.btn_Addvolunteer.Visibility = Visibility.Collapsed;
        }
    }

    class Activity_Volunteer_List
    {
        public int volunteer_no { get; set; }
        public string 姓名 { get; set; }
        public string 聯絡電話 { get; set; }
        public bool 素食 { get; set; }
    }
}
