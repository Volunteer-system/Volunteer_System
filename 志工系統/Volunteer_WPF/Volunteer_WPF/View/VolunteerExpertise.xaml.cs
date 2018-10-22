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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Volunteer_WPF.Model;

namespace Volunteer_WPF.View
{
    /// <summary>
    /// VolunteerExpertise.xaml 的互動邏輯
    /// </summary>
    public partial class VolunteerExpertise : Window
    {
        public VolunteerExpertise(int _volunteerNo) //加入一個多載 這個多載用來傳遞志工編號
        {
            InitializeComponent();



            //global::Volunteer_WPF.Model.VolunteerEntities entities = new VolunteerEntities();
            VolunteerEntities entities = new VolunteerEntities();

            //var q3 = entities.Expertise2.Where(x => x.Volunteer_no == _volunteerNo);
            var q2 = from e2 in entities.Expertise2
                     where e2.Volunteer_no == _volunteerNo
                     select  new { Expertise = e2.Expertise1.Expertise };


            //Expertise1 就是Expertise(Basic) 裡面有Expertise_no,Expertise
            var q = from e in entities.Expertise1
                    select new
                    {
                        //專長名稱
                        Expertise = e.Expertise

                    };
            List<expertise_list> ListIWantShow = new List<expertise_list>();
            

            //foreach (var e in q)
            //{
            //    expertise_list a = new expertise_list();
            //    a.專長名稱 = e.Expertise;
            //    ListIWantShow.Add(a);
            //}

            //mydatagrid1.ItemsSource = ListIWantShow;


            foreach (var row1 in q)
            {
                expertise_list expertise_List = new expertise_list();
                foreach (var row2 in q2)                {
                    
                    if (row1.Expertise == row2.Expertise)
                    {
                        //CheckBox tb = mydatagrid1.Columns[0].GetCellContent(mydatagrid1.Items[i]) as CheckBox;


                        //DataGridCheckBoxColumn checkBoxColumn = (DataGridCheckBoxColumn)mydatagrid1.Columns[0];


                        //CheckBox c = mydatagrid1.Columns[0];
                        //Expertise2 e = mydatagrid1.SelectedItem as Expertise2;
                        //((CheckBox)mydatagrid1.Columns[0].GetCellContent(e1)).IsChecked = true;

                        //if (c != null)
                        //{

                        // c.IsChecked = true;
                        //}

                        
                        expertise_List.Checked = true;

                    }
                }
                expertise_List.專長名稱 = row1.Expertise;
                ListIWantShow.Add(expertise_List);

            }

            mydatagrid1.ItemsSource = ListIWantShow;            
        }


        public VolunteerExpertise()
        {
            InitializeComponent();
            //global::Volunteer_WPF.Model.VolunteerEntities entities = new VolunteerEntities();
            VolunteerEntities entities = new VolunteerEntities();
            
            //Expertise1 就是Expertise(Basic) 裡面有Expertise_no,Expertise
            var q = from e in entities.Expertise1
                    select new
                    {
                        //專長名稱
                        e.Expertise

                    };
            //var q2 = from e1 in entities.Expertise2
            //         where 
            //         select 
            List<expertise_list> ListIWantShow = new List<expertise_list>();

            foreach (var e in q)
            {
                expertise_list a = new expertise_list();
                a.專長名稱 = e.Expertise;
                ListIWantShow.Add(a);
            }

            mydatagrid1.ItemsSource = ListIWantShow;

            
            
            //foreach(var v in q)
            //{
            //    mydatagrid1.Items.Add(v.Expertise_no);
            //}
            //var q = from e in entities.Expertise1
            //        select e;

            //mydatagrid1.ItemsSource = q.ToList();
        }
        ObservableCollection<expertise_list> expertise_itemlist = new ObservableCollection<expertise_list>();


        //宣告一個字串陣列 Str_Expertise 用來存放 字串
        //當在 按下 "選專長" 按鈕後, 在 VolunteerExpertise視窗裡
        //有選擇到的專長項目(字串) 都存放到 List<string>Str_Expertise 
        List<string> Str_Expertise = new List<string>();
        List<int> no_Expertise = new List<int>();

        

        //List<checkBoxIte> item = new List<checkBoxIte>();
        //public class checkBoxIte
        //{
        //    public string expe { get; set; }
        //    public CheckBox expe_check { get; set; }
        //}

    
        public List<int> STR()
        {
            return no_Expertise;
        }

        //按 "選專長" 按鈕後, 在 VolunteerExpertise視窗裡的 "確認" 鈕
        //當在 按下 "選專長" 按鈕後, 在 VolunteerExpertise視窗裡
        //有選擇到的專長項目(字串) 都存放到 List<string>Str_Expertise 
        private void btnpExpechecked_Click(object sender, RoutedEventArgs e)
        {
            VolunteerEntities entities = new VolunteerEntities();  //new整個entities實體資料模型

            foreach(var n in Str_Expertise)
            {

               var q = from v in entities.Expertise1.AsEnumerable()
                       where v.Expertise.Trim() == n
                       select new { Expertise_no = v.Expertise_no };
               foreach (var v in q)
               {
                    //MessageBox.Show(v.Expertise_no.ToString());
                    no_Expertise.Add(v.Expertise_no);
               }
            }

            this.Close();
        
            //this.mydatagrid1.datagridch
        }
        expertise_list E;
        private void ExpertiseChk(object sender, RoutedEventArgs e)
        {
            if (mydatagrid1.SelectedItem != null)
            {
                E = mydatagrid1.SelectedItem as expertise_list;
                //DataGrid.
                //Str_Expertise.Clear();
                //MessageBox.Show(E.專長名稱);
                Str_Expertise.Add(E.專長名稱);
            }            
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (mydatagrid1.SelectedItem != null)
            {
                //expertise_list E = mydatagrid1.SelectedItem as expertise_list;
                E = mydatagrid1.SelectedItem as expertise_list;
                Str_Expertise.Remove(E.專長名稱);
            }
            
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            mydatagrid1.Columns[1].Visibility = Visibility.Collapsed;
        }




        //public List<checkBoxIte> ExpChk()
        //{

        //    checkBoxIte ite = new checkBoxIte();
        //    ite.vol_no = 
        //}
    }

    public class expertise_list
    {
        public bool Checked { get; set; }
        public string 專長名稱 { get; set; }
    }
}
