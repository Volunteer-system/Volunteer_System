using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer_WPF.Model;

namespace Volunteer_WPF.View_Model
{
    class Service_group_ViewModel
    {
        public string Group_name { get; set; }

        public List<string> SelectService_group()
        {
            Service_group_Model service_Group_Model = new Service_group_Model();
            List<string> service_Groups = service_Group_Model.SelectService_group();
            return service_Groups;
        }
    }
}
