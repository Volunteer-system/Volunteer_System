using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer_WPF.Model;

namespace Volunteer_WPF.View_Model
{
    class ExpertiseBasic_ViewModel
    {
        ExpertiseBasic_Model _Model = new ExpertiseBasic_Model();

        public int 專長編號
        {
            get { return _Model.Expertise_no; }
            set { _Model.Expertise_no = value; }
        }
        public string 專長
        {
            get { return _Model.Expertise_name; }
            set { _Model.Expertise_name = value; }
        }

        public List<ExpertiseBasic_ViewModel> GetExpertises()
        {
            List<ExpertiseBasic_Model> a=_Model.GetExpertises();


            List<ExpertiseBasic_ViewModel> vm = new List<ExpertiseBasic_ViewModel>();
            foreach (var modelitems in a)
            {
                ExpertiseBasic_ViewModel view = new ExpertiseBasic_ViewModel();
                view.專長 = modelitems.Expertise_name;
                view.專長編號 = modelitems.Expertise_no;
                vm.Add(view);
            }
            return vm;
        }
    }
}
