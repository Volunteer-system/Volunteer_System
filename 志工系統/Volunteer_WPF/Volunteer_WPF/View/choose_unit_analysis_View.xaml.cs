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
    /// choose_unit_analysis_View.xaml 的互動邏輯
    /// </summary>
    public partial class choose_unit_analysis_View : Window
    {
        public List<string> Application_unit_Names { get; set; }
        List<string> return_list = new List<string>();
        ObservableCollection<Application_unit_Name> Unit_Name_lists = new ObservableCollection<Application_unit_Name>();

        public choose_unit_analysis_View()
        {
            InitializeComponent();

            choose_unit_analysis_ViewModel choose_Unit_Analysis_ViewModel = new choose_unit_analysis_ViewModel();
            List<string> unit_Names = choose_Unit_Analysis_ViewModel.SelectApplication_unit_Name();
            foreach (var row in unit_Names)
            {
                Unit_Name_lists.Add(new Application_unit_Name()
                {
                    運用單位 = row
                });
            }

            this.dg_unit_analysis.ItemsSource = Unit_Name_lists;           
        }

        private void CB_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.dg_unit_analysis.SelectedItem != null)
            {
                return_list.Remove((this.dg_unit_analysis.SelectedItem as Application_unit_Name).運用單位);

                int index = this.dg_unit_analysis.SelectedIndex;
                DataGridRow Drow = (DataGridRow)dg_unit_analysis.ItemContainerGenerator.ContainerFromIndex(index);
                Drow.Background = Brushes.IndianRed;
                Drow.Foreground = Brushes.White;
            }            
        }

        private void CB_Checked(object sender, RoutedEventArgs e)
        {
            if (this.dg_unit_analysis.SelectedItem != null)
            {
                return_list.Add((this.dg_unit_analysis.SelectedItem as Application_unit_Name).運用單位);

                int index = this.dg_unit_analysis.SelectedIndex;
                DataGridRow Drow = (DataGridRow)dg_unit_analysis.ItemContainerGenerator.ContainerFromIndex(index);
                Drow.Background = Brushes.LimeGreen;
                Drow.Foreground = Brushes.White;
            }                
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application_unit_Names = return_list;
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {

        }
    }
}
