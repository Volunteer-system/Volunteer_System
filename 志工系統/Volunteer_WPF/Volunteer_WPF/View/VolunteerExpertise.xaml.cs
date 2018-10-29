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
        int volun_no;
        public VolunteerExpertise(int _volunteerNo) //加入一個多載 這個多載用來傳遞志工編號
        {
            InitializeComponent();

            volun_no = _volunteerNo;
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
            
            foreach (var row1 in q)
            {
                expertise_list expertise_List = new expertise_list();
                foreach (var row2 in q2)
                {  
                    if (row1.Expertise == row2.Expertise)
                    {
                        expertise_List.Checked = true;
                    }
                }
                expertise_List.專長名稱 = row1.Expertise;
                ListIWantShow.Add(expertise_List);
            }
            mydatagrid1.ItemsSource = ListIWantShow;            
        }
        List<expertise_list> ListIWantShow = new List<expertise_list>();
        ObservableCollection<expertise_list> expertise_itemlist = new ObservableCollection<expertise_list>();

        //宣告一個字串陣列 Str_Expertise 用來存放 字串
        //當在 按下 "選專長" 按鈕後, 在 VolunteerExpertise視窗裡
        //有選擇到的專長項目(字串) 都存放到 List<string>Str_Expertise 
        List<string> Str_Expertise = new List<string>();
        List<int> no_Expertise = new List<int>();
        List<string> delete_str = new List<string>();
        List<int> delete_no = new List<int>();

        public List<int> STR()
        {
            return no_Expertise;
        }
        public List<int> STRA()
        {
            return delete_no;
        }

        //按 "選專長" 按鈕後, 在 VolunteerExpertise視窗裡的 "確認" 鈕
        //當在 按下 "選專長" 按鈕後, 在 VolunteerExpertise視窗裡
        //有選擇到的專長項目(字串) 都存放到 List<string>Str_Expertise 
        //最後按下"確定"
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
            //delete_str 就是要刪除的List<string>
            foreach(var n1 in delete_str)
            {
                var qv = from v1 in entities.Expertise1.AsEnumerable()
                         where v1.Expertise.Trim() == n1
                         select new { Expertise_no = v1.Expertise_no };
                foreach(var v in qv)
                {
                    delete_no.Add(v.Expertise_no);
                }
            }
            this.Close();
        }
        expertise_list E;
        private void ExpertiseChk(object sender, RoutedEventArgs e)
        {
            if (mydatagrid1.SelectedItem != null)
            {
                E = mydatagrid1.SelectedItem as expertise_list;
                Str_Expertise.Add(E.專長名稱);
                foreach(var b in ListIWantShow)
                {
                    if(E==b)
                    {
                        b.Checked = true;
                    }
                }
                delete_str.Remove(E.專長名稱);
            }            
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (mydatagrid1.SelectedItem != null)
            {
                E = mydatagrid1.SelectedItem as expertise_list;
                Str_Expertise.Remove(E.專長名稱);
                foreach (var b in ListIWantShow)
                {
                    if (E == b)
                    {
                        b.Checked = false;

                    }
                }
                delete_str.Add(E.專長名稱);
                
               
            }  
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            mydatagrid1.Columns[1].Visibility = Visibility.Collapsed;
        }

    }

    public class expertise_list
    {
        public bool Checked { get; set; }
        public string 專長名稱 { get; set; }
    }
}
