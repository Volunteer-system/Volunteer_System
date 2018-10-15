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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Volunteer_WPF.Model;
using Volunteer_WPF.View_Model;

namespace Volunteer_WPF.View
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class Volunteer_View : Window
    {
        VolunteerEntities dbcontext = new VolunteerEntities();
        public Volunteer_View()
        {
            InitializeComponent();

            datagrid1.IsReadOnly = true;
            var q = from p in dbcontext.Expertise1
                    select p.Expertise;
            專長CB.Items.Add("(無)");
            foreach (var items in q)
            {
                專長CB.Items.Add(items);
            }
            專長CB.SelectedIndex = 0;

            var q1 = from p in dbcontext.Service_group
                    select p.Group_name;
            組別CB.Items.Add("(無)");

            foreach (var items in q1)
            {
                組別CB.Items.Add(items);
            }
            組別CB.SelectedIndex = 0;



        }


        string name;
        string group;
        string expertise;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            Volunteer_ViewModel model = new Volunteer_ViewModel();

             name = nametext.Text;
             group = 組別CB.Text;
             expertise = 專長CB.Text;
            var q =model.Search_Volunteer(name,group,expertise);

            this.datagrid1.ItemsSource = q.ToList();

        }

        

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string a = (this.datagrid1.SelectedItem as Volunteer_ViewModel).Volunteer_no;
            int volunteernum = Convert.ToInt32(a);



            Window window = new Volunteer_data_View(volunteernum);
            window.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Window window = new Volunteer_data_View();
            window.ShowDialog();
        }

        private void datagrid1_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (this.datagrid1.SelectedItem != null)
            {            string a = (this.datagrid1.SelectedItem as Volunteer_ViewModel).Volunteer_no;
            int volunteernum = Convert.ToInt32(a);
            Volunteer_detail v = new Volunteer_detail(volunteernum);
            grid4detail.Children.Clear();
            object content = v.Content;
            v.Content = null;
            grid4detail.Children.Add(content as UIElement);
            }





        }
    }
}
