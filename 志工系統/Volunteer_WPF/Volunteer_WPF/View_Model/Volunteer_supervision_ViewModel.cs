using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer_WPF.Model;

namespace Volunteer_WPF.View_Model
{
    class Volunteer_supervision_ViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string email { get; set; }

        public List<Volunteer_supervision_ViewModel> Selectsupervision()
        {
            Volunteer_supervision_Model volunteer_Supervision_Model = new Volunteer_supervision_Model();
            List<Volunteer_supervision_ViewModel> ViewModels = new List<Volunteer_supervision_ViewModel>();
            foreach (var row in volunteer_Supervision_Model.Selectsupervision())
            {
                Volunteer_supervision_ViewModel volunteer_Supervision_ViewModel = new Volunteer_supervision_ViewModel();
                volunteer_Supervision_ViewModel.ID = row.ID;
                volunteer_Supervision_ViewModel.Name = row.Name;
                volunteer_Supervision_ViewModel.Phone = row.Phone;
                volunteer_Supervision_ViewModel.Address = row.Address;
                volunteer_Supervision_ViewModel.email = row.email;

                ViewModels.Add(volunteer_Supervision_ViewModel);
            }

            return ViewModels;
        }
    }
}
