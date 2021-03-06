﻿using System;
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
using Volunteer_WPF.Model;

namespace Volunteer_WPF.View
{
    /// <summary>
    /// VolunteerService_group.xaml 的互動邏輯
    /// </summary>
    public partial class VolunteerService_group : Window
    {
        public VolunteerService_group(int _volunteerNo)
        {
            InitializeComponent();
            VolunteerEntities entities = new VolunteerEntities();


            var q2 = from e2 in entities.Service_Group1
                     where e2.Volunteer_no == _volunteerNo
                     select new { group_name = e2.Service_group.Group_name };

            var q = from n in entities.Service_group
                    select new
                    {
                       group_name= n.Group_name

                    };

            

            //foreach(var e in q)
            //{
            //    Service_group_list a1 = new Service_group_list();
            //    a1.服務類別名稱 = e.Group_name;
            //    ListIWantShow1.Add(a1);
            //}
            //mydatagridVolunteerService_group.ItemsSource = ListIWantShow1;
            foreach(var row1 in q)
            {
                Service_group_list service_group_list = new Service_group_list();
                foreach(var row2 in q2)
                {
                    if(row1.group_name==row2.group_name)
                    {
                        service_group_list.Checked = true;
                    }
                }
                service_group_list.服務類別名稱 = row1.group_name;
                ListIWantShow1.Add(service_group_list);
            }
            mydatagridVolunteerService_group.ItemsSource = ListIWantShow1;
        }
        List<Service_group_list> ListIWantShow1 = new List<Service_group_list>();
        ObservableCollection<Service_group_list> Service_group_itemlist = new ObservableCollection<Service_group_list>();

        //宣告一個字串陣列 Str_group_name 用來存放 字串
        //當在 按下 "選服務類別" 按鈕後, 在 VolunteerService_group視窗裡
        //有選擇到的服務類別項目(字串) 都存放到 List<string> Str_group_name
        List<string> Str_group_name = new List<string>();
        List<int> Int_group_no = new List<int>();
        List<string> delete_str1 = new List<string>();
        List<int> delete_no1 = new List<int>();
        

        public List<int> GetVs()
        {
            return Int_group_no;
        }
        public List<int> GetVs1()
        {
            return delete_no1;
        }

        //按 "選服務類別" 按鈕後, 在 VolunteerService_group視窗裡的 "確認" 鈕
        //當在 按下 "選服務類別" 按鈕後, 在 VolunteerService_group視窗裡
        //有選擇到的專長項目(字串) 都存放到 List<string> Str_group_name
        //最後按下"確定"
        private void btnpExpechecked_Click(object sender, RoutedEventArgs e)
        {
            VolunteerEntities entities = new VolunteerEntities();  //new整個entities實體資料模型

            foreach(var n in Str_group_name)
            {
                var q = from f in entities.Service_group.AsEnumerable()
                        where f.Group_name == n
                        select new { group_no =f.Group_no};

                foreach(var v in q)
                {
                    Int_group_no.Add(v.group_no);
                }
            }

            //delete_str1 就是要刪除的List<string>
            foreach (var n1 in delete_str1)
            {
                var qv = from v1 in entities.Service_group.AsEnumerable()
                         where v1.Group_name.Trim() == n1
                         select new { GN_no = v1.Group_no };
                foreach(var v in qv)
                {
                    delete_no1.Add(v.GN_no);
                }
            }
            this.Close();
        }
        Service_group_list SG;
        private void Service_groupChk(object sender, RoutedEventArgs e)
        {
            if (mydatagridVolunteerService_group.SelectedItem != null)
            {
                SG = mydatagridVolunteerService_group.SelectedItem as Service_group_list;
                Str_group_name.Add(SG.服務類別名稱);
                foreach(var b in ListIWantShow1)
                {
                    if(SG==b)
                    {
                        b.Checked = true;
                    }
                }
                delete_str1.Remove(SG.服務類別名稱);
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (mydatagridVolunteerService_group.SelectedItem != null)
            {
                SG = mydatagridVolunteerService_group.SelectedItem as Service_group_list;
                Str_group_name.Remove(SG.服務類別名稱);
                foreach(var b in ListIWantShow1)
                {
                    if(SG == b)
                    {
                        b.Checked = false;
                    }
                }
                delete_str1.Add(SG.服務類別名稱);
            }
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            mydatagridVolunteerService_group.Columns[1].Visibility = Visibility.Collapsed;
        }
    }



    public class Service_group_list
    {
        public bool Checked { get; set; }
        public string 服務類別名稱 { get; set; }
    }
}
