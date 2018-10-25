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

        //Activity_AddAction_ViewModel AAVM;
        public Activity_AddActionDetail_View()
        {
            InitializeComponent();
            //AAVM = new Activity_AddAction_ViewModel(2,WrapPanel_Viewphoto);  //資料綁定的方式!!
            //this.DataContext = AAVM;                    //很重要!!   
        }

        public WrapPanel getWrapPanel()
        {
            return this.WrapPanel_Viewphoto;
        }
    }
}
