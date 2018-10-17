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
using Volunteer_WPF.View_Model;

namespace Volunteer_WPF.View
{
    /// <summary>
    /// ExpertiseBasic_View.xaml 的互動邏輯
    /// </summary>
    public partial class ExpertiseBasic_View : Window
    {
       
        public ExpertiseBasic_View()
        {
            InitializeComponent();
            rewrite();
            this.Title = "志工專長表";
        }
        
        

        private void rewrite()
        {
            //使用者不能刪除行
            datagrid1.CanUserDeleteRows = false;

            //創造物件以使用方法
           ExpertiseBasic_ViewModel basic_View = new ExpertiseBasic_ViewModel();
           List<ExpertiseBasic_ViewModel> vm=basic_View.GetExpertises();
            var q = from p in vm
                    select p;


            this.datagrid1.ItemsSource = q.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int a = this.datagrid1.SelectedIndex;
          //避免沒選到項目
            if (a != -1)
            {
                //取得row的方法
                DataGridRow row = (DataGridRow)datagrid1.ItemContainerGenerator
                                                   .ContainerFromIndex(a);
                row.Background = Brushes.IndianRed;
                row.Foreground = Brushes.White;
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            rewrite();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
           
        }

        private void datagrid1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                int a = this.datagrid1.SelectedIndex;
                //避免沒選到項目
                if (a != -1)
                {
                    DataGridRow row = (DataGridRow)datagrid1.ItemContainerGenerator
                                                       .ContainerFromIndex(a);
                    row.Background = Brushes.IndianRed;
                    row.Foreground = Brushes.White;
                }

            }
        }
    }
}
