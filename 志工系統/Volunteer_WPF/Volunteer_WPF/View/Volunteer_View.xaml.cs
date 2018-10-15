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
            this.started();



        }
        //起始設置(combobox等)
        private void started()
        {
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

            Volunteer_ViewModel view_model = new Volunteer_ViewModel();

            //搜尋條件  
            name = nametext.Text;
            group = 組別CB.Text;
            expertise = 專長CB.Text;
            List<int> years = new List<int>();
            if (this.年資0到1.IsChecked == true) { years.Add(0); years.Add(1); }
            if (this.年資1到3.IsChecked == true) { years.Add(1); years.Add(2); years.Add(3); }
            if (this.年資3到5.IsChecked == true) { years.Add(3); years.Add(4); years.Add(5); }
            if (this.年資5到10.IsChecked == true) { years.Add(5); years.Add(6); years.Add(7); years.Add(8); years.Add(9); years.Add(10); }
            if (this.年資10年以上.IsChecked == true)
            {
                for (int i = 10; i <= 30; i++)
                {
                    years.Add(i);
                }

            }
            //將條件給後面使用
            var q = view_model.Search_Volunteer(name, group, expertise,years);

            //使用自訂類別控制欄位表現
            List<Volunteer_DataGirdView> q2 = new List<Volunteer_DataGirdView>();
            foreach (var p in q)
            {
                Volunteer_DataGirdView a = new Volunteer_DataGirdView();
                a.志工編號 = p.Volunteer_no;
                a.志工姓名 = p.Chinese_name;
                a.志工身分 = p.Identity_type;
                a.志工年資 = p.Seniority;
                a.志工專長 = p.getallexpetise();
                a.志工組別 = p.getallgroup();


                q2.Add(a);

            }
            
            this.datagrid1.ItemsSource = q2;
        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string a = (this.datagrid1.SelectedItem as Volunteer_DataGirdView).志工編號;
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
            {
                string a = (this.datagrid1.SelectedItem as Volunteer_DataGirdView).志工編號;
                int volunteernum = Convert.ToInt32(a);
                Volunteer_detail v = new Volunteer_detail(volunteernum);
                grid4detail.Children.Clear();
                object content = v.Content;
                v.Content = null;
                grid4detail.Children.Add(content as UIElement);
            }

        }
    }

    public class Volunteer_DataGirdView
    {
        public string 志工編號 { get; set; }
        public string 志工姓名 { get; set; }
        public string 志工身分 { get; set; }
        public string 志工年資 { get; set; }
        public string 志工專長 { get; set; }
        public string 志工組別 { get; set; }

    }
}
