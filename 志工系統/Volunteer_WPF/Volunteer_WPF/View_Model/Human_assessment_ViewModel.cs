using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer_WPF.Model;

namespace Volunteer_WPF.View_Model
{
    class Human_assessment_ViewModel
    {
        public int Assessment_ID { get; set; }
        public string Assessment_name { get; set; }

        public List<Human_assessment_ViewModel> SelectHuman_assessment()
        {
            Human_assessment_Model human_Assessment_Model = new Human_assessment_Model();
            List<Human_assessment_Model> Human_assessments = human_Assessment_Model.SelectHuman_assessment();
            List<Human_assessment_ViewModel> human_assessments = new List<Human_assessment_ViewModel>();

            foreach (var row in Human_assessments)
            {
                Human_assessment_ViewModel human_Assessment_ViewModel = new Human_assessment_ViewModel();
                human_Assessment_ViewModel.Assessment_ID = row.Assessment_ID;
                human_Assessment_ViewModel.Assessment_name = row.Assessment_name;
                human_assessments.Add(human_Assessment_ViewModel);
            }

            return human_assessments;
        }
    }
}
