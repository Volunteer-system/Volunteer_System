using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer_WPF.Model;

namespace Volunteer_WPF.View_Model
{
    class Shift_schedule_ViewModel
    {
        //運用單位編號
        public int Application_unit_no { get; set; }
        //運用單位
        public string Application_unit { get; set; }
        //組別
        public string Group { get; set; }
        //志工總人數
        public string Total_volunteers { get; set; }

        public List<Shift_schedule_ViewModel> SelectApplication_unit()
        {
            Application_unit_Model application_Unit_Model = new Application_unit_Model();
            List<Shift_schedule_ViewModel> Shift_schedules = new List<Shift_schedule_ViewModel>();

            foreach (var row in application_Unit_Model.SelectApplication_unit())
            {
                Shift_schedule_ViewModel shift_Schedule_ViewModel = new Shift_schedule_ViewModel();
                shift_Schedule_ViewModel.Application_unit_no = row.Application_unit_no;
                shift_Schedule_ViewModel.Application_unit = row.Application_unit;

                Shift_schedules.Add(shift_Schedule_ViewModel);
            }

            return Shift_schedules;
        }
    }
}
