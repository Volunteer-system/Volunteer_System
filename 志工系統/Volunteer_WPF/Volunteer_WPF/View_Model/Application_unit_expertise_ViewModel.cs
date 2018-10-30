using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer_WPF.Model;

namespace Volunteer_WPF.View_Model
{
    class Application_unit_expertise_ViewModel
    {
        public int Expertise_no { get; set; }
        public string Expertise_name { get; set; }

        public List<Application_unit_expertise_ViewModel> Getexpertises()
        {
            ExpertiseBasic_Model expertiseBasic_Model = new ExpertiseBasic_Model();
            List<ExpertiseBasic_Model> ExpertiseBasics = expertiseBasic_Model.GetExpertises();
            List<Application_unit_expertise_ViewModel> unit_expertises = new List<Application_unit_expertise_ViewModel>();
            foreach (var row in ExpertiseBasics)
            {
                Application_unit_expertise_ViewModel Unit_Expertise_ViewModel = new Application_unit_expertise_ViewModel();
                Unit_Expertise_ViewModel.Expertise_no = row.Expertise_no;
                Unit_Expertise_ViewModel.Expertise_name = row.Expertise_name;
                unit_expertises.Add(Unit_Expertise_ViewModel);
            }

            return unit_expertises;
        }

        public List<string> Selectexpertises_byUnit_no(int Unit_no)
        {
            Unit_expertise_Model unit_Expertise_Model = new Unit_expertise_Model();
            List<string> Unit_expertise_list = unit_Expertise_Model.SelectUnit_ExpertisebyApplication_unit_no(Unit_no);
            List<string> unit_expertises = new List<string>();
            foreach (var row in Unit_expertise_list)
            {
                unit_expertises.Add(row);
            }
            return unit_expertises;
        }
    }
}
