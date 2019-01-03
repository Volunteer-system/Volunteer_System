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
    /// Manpower_applydata_View.xaml 的互動邏輯
    /// </summary>
    public partial class Manpower_apply_data_View : Window
    {
        int g_apply_id;
        List<string> insert_Assessment = new List<string>();
        List<string> delete_Assessment = new List<string>();
        List<string> insert_Result = new List<string>();
        List<string> delete_Result = new List<string>();

        public Manpower_apply_data_View(string type, int apply_id)
        {
            InitializeComponent();

            g_apply_id = apply_id;
            Manpower_apply_data_ViewModel manpower_Apply_data_ViewModel = new Manpower_apply_data_ViewModel();
            manpower_Apply_data_ViewModel.SelectManpower_apply_byApply_ID(type, apply_id);
            this.lab_Application_unit.Content = manpower_Apply_data_ViewModel.Application_unit;
            this.lab_Applicant.Content = manpower_Apply_data_ViewModel.Applicant;
            this.lab_Apply_date.Content = manpower_Apply_data_ViewModel.Apply_date;
            this.lab_Applicant_phone.Content = manpower_Apply_data_ViewModel.Applicant_phone;
            this.lab_unit_Supervisor.Content = manpower_Apply_data_ViewModel.unit_Supervisor;
            this.lab_Work_place.Content = manpower_Apply_data_ViewModel.Work_place;
            this.lab_unit_heads.Content = manpower_Apply_data_ViewModel.unit_heads;
            this.lab_Application_number.Content = manpower_Apply_data_ViewModel.Application_number;            
            this.txt_unit_descrition.Text = manpower_Apply_data_ViewModel.Apply_description;
            if (string.IsNullOrEmpty(manpower_Apply_data_ViewModel.Supervision))
            {
                this.lab_Supervision.Content = Account_ViewModel.User;
            }
            else
            {
                this.lab_Supervision.Content = manpower_Apply_data_ViewModel.Supervision;
            }
            
            this.lab_Supervision_heads.Content = manpower_Apply_data_ViewModel.Supervision_heads;
            this.lab_Reply_date.Content = manpower_Apply_data_ViewModel.Reply_date;
            this.txt_Reply_number.Text = manpower_Apply_data_ViewModel.Reply_number;
            this.txt_Repply_description.Text = manpower_Apply_data_ViewModel.Repply_description;

            Human_assessment_ViewModel human_Assessment_ViewModel = new Human_assessment_ViewModel();
            List<Human_assessment_ViewModel> human_assessments = human_Assessment_ViewModel.SelectHuman_assessment();
            List<Apply_assessment> Apply_assessments = new List<Apply_assessment>();
            foreach (var row1 in human_assessments)
            {
                Apply_assessment apply_Assessment = new Apply_assessment();
                if (type == "修改")
                {
                    foreach (var row2 in manpower_Apply_data_ViewModel.Apply_assessments)
                    {
                        if (row1.Assessment_name == row2)
                        {
                            apply_Assessment.assessment_check = true;
                        }
                    }
                }
                apply_Assessment.評估項目 = row1.Assessment_name;
                Apply_assessments.Add(apply_Assessment);
            }
            this.dg_Apply_Assessment.ItemsSource = Apply_assessments;

            Human_assessment_result_ViewModel human_Assessment_Result_ViewModel = new Human_assessment_result_ViewModel();
            List<Human_assessment_result_ViewModel> human_assessment_results = human_Assessment_Result_ViewModel.SelectHuman_assessment_result();
            List<Apply_result> Apply_results = new List<Apply_result>();
            foreach (var row1 in human_assessment_results)
            {
                Apply_result apply_Result = new Apply_result();
                if (type == "修改")
                {
                    foreach (var row2 in manpower_Apply_data_ViewModel.Apply_result)
                    {
                        if (row1.Assessment_result == row2)
                        {

                            apply_Result.Apply_result_check = true;
                        }
                    }
                }

                apply_Result.評估結果 = row1.Assessment_result;
                Apply_results.Add(apply_Result);
            }
            this.dg_Apply_result.ItemsSource = Apply_results;
        }

        private void cb_assessment_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.dg_Apply_Assessment.SelectedItem != null)
            {
                delete_Assessment.Add((this.dg_Apply_Assessment.SelectedItem as Apply_assessment).評估項目);
                if (insert_Assessment.Where(p => p == (this.dg_Apply_Assessment.SelectedItem as Apply_assessment).評估項目).Count() > 0)
                {
                    insert_Assessment.Remove((this.dg_Apply_Assessment.SelectedItem as Apply_assessment).評估項目);
                }

                int index = this.dg_Apply_Assessment.SelectedIndex;
                DataGridRow Drow = (DataGridRow)dg_Apply_Assessment.ItemContainerGenerator.ContainerFromIndex(index);
                Drow.Background = Brushes.IndianRed;
                Drow.Foreground = Brushes.White;
            }
        }

        private void cb_assessment_Checked(object sender, RoutedEventArgs e)
        {
            if (this.dg_Apply_Assessment.SelectedItem != null)
            {
                insert_Assessment.Add((this.dg_Apply_Assessment.SelectedItem as Apply_assessment).評估項目);
                if (delete_Assessment.Where(p => p == (this.dg_Apply_Assessment.SelectedItem as Apply_assessment).評估項目).Count() > 0)
                {
                    delete_Assessment.Remove((this.dg_Apply_Assessment.SelectedItem as Apply_assessment).評估項目);
                }

                int index = this.dg_Apply_Assessment.SelectedIndex;
                DataGridRow Drow = (DataGridRow)dg_Apply_Assessment.ItemContainerGenerator.ContainerFromIndex(index);
                Drow.Background = Brushes.LimeGreen;
                Drow.Foreground = Brushes.White;
            }
        }

        private void cb_apply_result_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.dg_Apply_result.SelectedItem != null)
            {
                delete_Result.Add((this.dg_Apply_result.SelectedItem as Apply_result).評估結果);
                if (insert_Assessment.Where(p => p == (this.dg_Apply_result.SelectedItem as Apply_result).評估結果).Count() > 0)
                {
                    insert_Assessment.Remove((this.dg_Apply_result.SelectedItem as Apply_result).評估結果);
                }

                int index = this.dg_Apply_result.SelectedIndex;
                DataGridRow Drow = (DataGridRow)dg_Apply_result.ItemContainerGenerator.ContainerFromIndex(index);
                Drow.Background = Brushes.IndianRed;
                Drow.Foreground = Brushes.White;
            }
        }

        private void cb_apply_result_Checked(object sender, RoutedEventArgs e)
        {
            if (this.dg_Apply_result.SelectedItem != null)
            {
                insert_Result.Add((this.dg_Apply_result.SelectedItem as Apply_result).評估結果);
                if (delete_Result.Where(p => p == (this.dg_Apply_result.SelectedItem as Apply_result).評估結果).Count() > 0)
                {
                    delete_Result.Remove((this.dg_Apply_result.SelectedItem as Apply_result).評估結果);
                }

                int index = this.dg_Apply_result.SelectedIndex;
                DataGridRow Drow = (DataGridRow)dg_Apply_result.ItemContainerGenerator.ContainerFromIndex(index);
                Drow.Background = Brushes.LimeGreen;
                Drow.Foreground = Brushes.White;
            }
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            this.dg_Apply_Assessment.Columns[1].Visibility = Visibility.Collapsed;
            this.dg_Apply_result.Columns[1].Visibility = Visibility.Collapsed;
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            Manpower_apply_data_ViewModel manpower_Apply_data_ViewModel = new Manpower_apply_data_ViewModel();
            manpower_Apply_data_ViewModel.Apply_ID = g_apply_id;
            manpower_Apply_data_ViewModel.Reply_number = this.txt_Reply_number.Text;
            manpower_Apply_data_ViewModel.Repply_description = this.txt_Repply_description.Text;
            manpower_Apply_data_ViewModel.UpdateManpower_apply(manpower_Apply_data_ViewModel, insert_Assessment, delete_Assessment, insert_Result, delete_Result);

            MessageBox.Show("存檔完成");
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_rejectapply_Click(object sender, RoutedEventArgs e)
        {
            Manpower_apply_data_ViewModel manpower_Apply_data_ViewModel = new Manpower_apply_data_ViewModel();
            manpower_Apply_data_ViewModel.UpdateManpower_apply_byreject(g_apply_id);
            MessageBox.Show("駁回完成");
        }
    
    }
}
