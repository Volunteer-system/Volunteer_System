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
    /// Application_unit_expertise_View.xaml 的互動邏輯
    /// </summary>
    public partial class Application_unit_expertise_View : Window
    {
        public List<string> insert_list { get; set; }
        public List<string> delete_list { get; set; }

        List<string> insert_Expertise = new List<string>();
        List<string> delete_Expertise = new List<string>();
        ObservableCollection<expertise_list> Unit_Expertise_lists = new ObservableCollection<expertise_list>();

        public Application_unit_expertise_View()
        {
            InitializeComponent();

            Application_unit_expertise_ViewModel application_Unit_Expertise_ViewModel = new Application_unit_expertise_ViewModel();
            List<Application_unit_expertise_ViewModel> Expertises = application_Unit_Expertise_ViewModel.Getexpertises();            
            foreach (var row in Expertises)
            {
                Unit_Expertise_lists.Add(new expertise_list()
                {
                    專長名稱 = row.Expertise_name
                });
            }

            this.dg_Expertise.ItemsSource = Unit_Expertise_lists;
        }

        public Application_unit_expertise_View(int unit_no)
        {
            InitializeComponent();

            Application_unit_expertise_ViewModel application_Unit_Expertise_ViewModel = new Application_unit_expertise_ViewModel();
            List<Application_unit_expertise_ViewModel> Expertises = application_Unit_Expertise_ViewModel.Getexpertises();
            List<string> Unit_Expertises = application_Unit_Expertise_ViewModel.Selectexpertises_byUnit_no(unit_no);            
            foreach (var row1 in Expertises)
            {
                expertise_list unit_Expertise_List = new expertise_list();
                foreach (var row2 in Unit_Expertises)
                {
                    if (row1.Expertise_name == row2)
                    {
                        unit_Expertise_List.Checked = true;
                        insert_Expertise.Add(row1.Expertise_name);
                    }
                }                
                unit_Expertise_List.專長名稱 = row1.Expertise_name;
                Unit_Expertise_lists.Add(unit_Expertise_List);
            }
            
            this.dg_Expertise.ItemsSource = Unit_Expertise_lists;
        }        

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            this.dg_Expertise.Columns[1].Visibility = Visibility.Collapsed;
        }

        private void CB_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.dg_Expertise.SelectedItem != null)
            {
                delete_Expertise.Add((this.dg_Expertise.SelectedItem as expertise_list).專長名稱);
                insert_Expertise.Remove((this.dg_Expertise.SelectedItem as expertise_list).專長名稱);

                foreach (var row in Unit_Expertise_lists.Where(p => p.專長名稱 == (this.dg_Expertise.SelectedItem as expertise_list).專長名稱))
                {
                    row.Checked = false;
                }

                int index = this.dg_Expertise.SelectedIndex;
                DataGridRow Drow = (DataGridRow)dg_Expertise.ItemContainerGenerator.ContainerFromIndex(index);
                Drow.Background = Brushes.IndianRed;
                Drow.Foreground = Brushes.White;
            }
        }

        private void CB_Checked(object sender, RoutedEventArgs e)
        {
            if (this.dg_Expertise.SelectedItem != null)
            {
                insert_Expertise.Add((this.dg_Expertise.SelectedItem as expertise_list).專長名稱);
                if (delete_Expertise.Where(p => p == (this.dg_Expertise.SelectedItem as expertise_list).專長名稱).Count() > 0)
                {
                    delete_Expertise.Remove((this.dg_Expertise.SelectedItem as expertise_list).專長名稱);
                }

                foreach (var row in Unit_Expertise_lists.Where(p => p.專長名稱 == (this.dg_Expertise.SelectedItem as expertise_list).專長名稱))
                {
                    row.Checked = false;
                }

                int index = this.dg_Expertise.SelectedIndex;
                DataGridRow Drow = (DataGridRow)dg_Expertise.ItemContainerGenerator.ContainerFromIndex(index);
                Drow.Background = Brushes.LimeGreen;
                Drow.Foreground = Brushes.White;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            insert_list = insert_Expertise;
            delete_list = delete_Expertise;
            this.Close();
        }
    }    
}
