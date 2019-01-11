using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Volunteer_WPF.View_Model;

namespace Volunteer_WPF.View
{
    /// <summary>
    /// Volunteer_supervision_View.xaml 的互動邏輯
    /// </summary>
    public partial class Volunteer_supervision_View : Window
    {
        public Volunteer_supervision_View()
        {
            InitializeComponent();
        }

        private void Btn_select_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<supervision_list> supervision_itemlist = new ObservableCollection<supervision_list>();

            Volunteer_supervision_ViewModel volunteer_Supervision_ViewModel = new Volunteer_supervision_ViewModel();
            foreach (var row in volunteer_Supervision_ViewModel.Selectsupervision())
            {
                supervision_list supervision_List = new supervision_list();
                supervision_List.姓名 = row.Name;
                supervision_List.電話 = row.Phone;
                supervision_List.地址 = row.Address;
                supervision_List.電子郵件 = row.email;
                supervision_itemlist.Add(supervision_List);
            }

            this.dg_supervision.ItemsSource = supervision_itemlist;
        }

        public class supervision_list
        {
            public string 姓名 { get; set; }
            public string 電話 { get; set; }
            public string 地址 { get; set; }
            public string 電子郵件 { get; set; }
            
        }
    }
}
