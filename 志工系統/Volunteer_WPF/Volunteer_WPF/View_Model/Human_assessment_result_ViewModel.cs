using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer_WPF.Model;

namespace Volunteer_WPF.View_Model
{
    class Human_assessment_result_ViewModel
    {
        public int Assessment_result_ID { get; set; }
        public string Assessment_result { get; set; }

        public List<Human_assessment_result_ViewModel> SelectHuman_assessment_result()
        {
            Human_assessment_result_Model human_Assessment_Result_Model = new Human_assessment_result_Model();
            List<Human_assessment_result_Model> Human_assessment_results = human_Assessment_Result_Model.SelectHuman_assessment_result();
            List<Human_assessment_result_ViewModel> human_assessment_results = new List<Human_assessment_result_ViewModel>();
            foreach (var row in Human_assessment_results)
            {
                Human_assessment_result_ViewModel human_Assessment_Result_ViewModel = new Human_assessment_result_ViewModel();
                human_Assessment_Result_ViewModel.Assessment_result_ID = row.Assessment_result_ID;
                human_Assessment_Result_ViewModel.Assessment_result = row.Assessment_result;
                human_assessment_results.Add(human_Assessment_Result_ViewModel);
            }

            return human_assessment_results;
        }
    }
}
