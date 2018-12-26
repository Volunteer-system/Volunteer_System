using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer_WPF.Model;

namespace Volunteer_WPF.View_Model
{
    class Shift_schedule_data_ViewModel
    {
        public IEnumerable<Shift_schedule_data_VM> Shift_schedule_data_VMs { get; set; }
        public Dictionary<string, string> Repeat_periods { get; set; }

        public void SelectShift_schedule(int unit_id)
        {
            Shift_schedule_unit_Model shift_Schedule_Unit_Model = new Shift_schedule_unit_Model();
            List<Shift_schedule_data_VM> shift_schedule_data_VMs = new List<Shift_schedule_data_VM>();
            foreach (var row in shift_Schedule_Unit_Model.Selectshift_schedule_unit_byunit_id(unit_id))
            {
                Shift_schedule_data_VM shift_Schedule_Data_VM = new Shift_schedule_data_VM();
                shift_Schedule_Data_VM.Unit_no = row.Unit_no;
                shift_Schedule_Data_VM.Service_period = row.Service_period;
                shift_Schedule_Data_VM.Time = row.Time;
                shift_Schedule_Data_VM.Actual_number = row.Actual_number;
                shift_Schedule_Data_VM.Volunteer_number = row.Volunteer_number;

                shift_schedule_data_VMs.Add(shift_Schedule_Data_VM);
            }

            Shift_schedule_data_VMs = shift_schedule_data_VMs;

            Repeat_period(Shift_schedule_data_VMs);
        }

        public void Repeat_period(IEnumerable<Shift_schedule_data_VM> schedule_datas)
        {
            Dictionary<string, string> repeat_periods = new Dictionary<string, string>();            
            foreach (var row in schedule_datas)
            {
                if (repeat_periods.Keys.Where(p => p == row.Service_period.Substring(2).Trim()).Count() == 0)
                {
                    repeat_periods.Add(row.Service_period.Substring(2).Trim(), row.Time.Trim());
                }
            }
            Repeat_periods = repeat_periods;
        }
    }

    class Shift_schedule_data_VM
    {
        public int Unit_no { get; set; }
        public string Service_period { get; set; }
        public string Time { get; set; }
        public int Actual_number { get; set; }
        public int Volunteer_number { get; set; }
    }
}
