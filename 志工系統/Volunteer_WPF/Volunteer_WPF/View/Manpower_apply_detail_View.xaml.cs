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
    /// Manpower_apply_detail_View.xaml 的互動邏輯
    /// </summary>
    public partial class Manpower_apply_detail_View : Window
    {
        public Manpower_apply_detail_View()
        {
            InitializeComponent();

            Manpower_apply_detail_ViewModel manpower_Apply_Detail_ViewModel = new Manpower_apply_detail_ViewModel();
            this.lab_Application_unit.Content = manpower_Apply_Detail_ViewModel.Application_unit;
            this.lab_Applicant.Content = manpower_Apply_Detail_ViewModel.Applicant;
            this.lab_Apply_date.Content = manpower_Apply_Detail_ViewModel.Apply_date;
            this.lab_Applicant_phone.Content = manpower_Apply_Detail_ViewModel.Applicant_phone;
            this.lab_unit_Supervisor.Content = manpower_Apply_Detail_ViewModel.unit_Supervisor;
            this.lab_Work_place.Content = manpower_Apply_Detail_ViewModel.Work_place;
            this.lab_unit_heads.Content = manpower_Apply_Detail_ViewModel.unit_heads;
            this.lab_Application_number.Content = manpower_Apply_Detail_ViewModel.Application_number;
            this.lab_application_unit.Content = manpower_Apply_Detail_ViewModel.Application_unit;
            this.txt_unit_descrition.Text = manpower_Apply_Detail_ViewModel.Apply_description;
            this.lab_Supervision.Content = manpower_Apply_Detail_ViewModel.Supervision;
            this.lab_Supervision_heads.Content = manpower_Apply_Detail_ViewModel.Supervision_heads;
            this.lab_Reply_date.Content = manpower_Apply_Detail_ViewModel.Reply_date;
            this.lab_Reply_number.Content = manpower_Apply_Detail_ViewModel.Reply_number;
            this.txt_Repply_description.Text = manpower_Apply_Detail_ViewModel.Repply_description;

            Human_assessment_ViewModel human_Assessment_ViewModel = new Human_assessment_ViewModel();
            List<Human_assessment_ViewModel> human_assessments = human_Assessment_ViewModel.SelectHuman_assessment();
            List<Apply_assessment> Apply_assessments = new List<Apply_assessment>();
            foreach (var row1 in human_assessments)
            {
                Apply_assessment apply_Assessment = new Apply_assessment();
                foreach (var row2 in manpower_Apply_Detail_ViewModel.Apply_assessments)
                {
                    if (row1.Assessment_name == row2)
                    {
                        apply_Assessment.check = true;
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
                foreach (var row2 in manpower_Apply_Detail_ViewModel.Apply_result)
                {
                    if (row1.Assessment_result == row2)
                    {
                        
                        apply_Result.check = true;
                    }
                }
                apply_Result.評估結果 = row1.Assessment_result;
                Apply_results.Add(apply_Result);
            }
            this.dg_Apply_result.ItemsSource = Apply_results;
        }
    }

    class Apply_assessment
    {
        public bool check { get; set; }
        public string 評估項目 { get; set; }
    }

    class Apply_result
    {
        public bool check { get; set; }
        public string 評估結果 { get; set; }
    }
}
