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
using Volunteer_WPF.Model;
using Volunteer_WPF.View_Model;
using System.Collections.ObjectModel;

namespace Volunteer_WPF.View
{
    /// <summary>
    /// Activity_AddActionDetail_View.xaml 的互動邏輯
    /// </summary>
    public partial class Activity_AddActionDetail_View : Window
    {
        List<Activity_AddActionDetail_View> AAD = new List<Activity_AddActionDetail_View>();
        Activity_AddAction_ViewModel AAVM = new Activity_AddAction_ViewModel();
        List<string> s = new List<string>();
        List<int> n = new List<int>();
        public Activity_AddActionDetail_View()
        {
            InitializeComponent();
            combin();
        }

        private void combin()
        {
           
        }
        public void getinfo()
        {
            s.Add(AAVM.VM_Activity_type);
            n.Add(AAVM.VM_Activity_type_ID);
        }
        public int V_Activity_type_ID
        { get { return AAVM.VM_Activity_type_ID; }
            set { AAVM.VM_Activity_type_ID = value; }
        }
        public string Activity_type
        { get { return AAVM.VM_Activity_type; }
            set { AAVM.VM_Activity_type = value; }
        }


    }
}
