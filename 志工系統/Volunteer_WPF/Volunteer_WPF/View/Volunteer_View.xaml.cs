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

            var q = from p in dbcontext.Expertise1
                    select p.Expertise;
            foreach (var p in q)
            {
                專長CB.ItemsSource = q.ToList();
            }

            var q1 = from p in dbcontext.Lssuing_unit
                    select p.Lssuing_unit1;
            foreach (var p in q)
            {
                組別CB.ItemsSource = q1.ToList();
            }

            
            

        }
        


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var q = from p in dbcontext.Volunteer
                                          //outer join   多條件left join:https://blog.darkthread.net/blog/linq-left-join/
                                      join p2 in dbcontext.Expertise2 on p.Volunteer_no equals p2.Volunteer_no into pp2
                                      from p2 in pp2.DefaultIfEmpty()//讓pp2能接受P2為Null值，且後面的where才能叫出P2(?)

                                      join p1 in dbcontext.Expertise1 on p2.Expertise_no equals p1.Expertise_no into pp3
                                      from p1 in pp3.DefaultIfEmpty()

                                      join p3 in dbcontext.Lssuing_unit on p.Lssuing_unit_no equals p3.Lssuing_unit_no into pp4
                                      from p3 in pp4.DefaultIfEmpty()

                                      join p4 in dbcontext.Identity_type on p.Identity_type equals p4.Identity_type1 into pp5
                                      from p4 in pp5.DefaultIfEmpty()
                                          //篩選，若有選，則where的內容=combobox內容

                                      where
                                      (!String.IsNullOrEmpty(專長CB.Text) ? p2.Expertise1.Expertise == 專長CB.Text : true) &&
                                      (!String.IsNullOrEmpty(組別CB.Text) ? p3.Lssuing_unit1 == 組別CB.Text : true) &&
                                      (!String.IsNullOrEmpty(textbox1.Text) ? p.Chinese_name.Contains(textbox1.Text) : true)
                                      select new { 姓名 = p.Chinese_name, 性別 = p.sex , 身分=p4.Identity_type_name , 背心號碼=p.Vest_no,電話號碼 =p.Phone_no,手機號碼=p.Mobile_no};


            this.datagrid1.ItemsSource=q.ToList();
           
        }

        private void datagrid1_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {


           
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //跳頁面
            MessageBox.Show("驚不驚喜，意不意外");
        }

        private void grid4detail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Volunteer_detail v = new Volunteer_detail();
            grid4detail.Children.Clear();
            object content = v.Content;
            v.Content = null;
            grid4detail.Children.Add(content as UIElement);
        }
    }
}
