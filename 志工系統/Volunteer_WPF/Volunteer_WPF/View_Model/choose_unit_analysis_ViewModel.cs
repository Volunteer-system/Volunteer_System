using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer_WPF.Model;

namespace Volunteer_WPF.View_Model
{
    class choose_unit_analysis_ViewModel
    {
        public string Application_unit { get; set; }
        public string event_category { get; set; }

        public List<string> SelectApplication_unit_Name()
        {
            Application_unit_Model application_Unit_Model = new Application_unit_Model();
            List<string> Application_units = new List<string>();
            foreach (var row in application_Unit_Model.SelectApplication_unit_name())
            {
                Application_units.Add(row);
            }

            return Application_units;
        }

        public List<string> Selectevent_category()
        {
            List<string> event_categorys = new List<string>();
            Abnormal_event_ViewModel abnormal_Event_ViewModel = new Abnormal_event_ViewModel();
            foreach (var row in abnormal_Event_ViewModel.Selectevent_category())
            {
                event_categorys.Add(row);
            }

            return event_categorys;
        }
    }
}
