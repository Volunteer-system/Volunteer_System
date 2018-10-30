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
using Volunteer_WPF.View_Model;

namespace Volunteer_WPF.View
{
    /// <summary>
    /// Application_unit_data_View.xaml 的互動邏輯
    /// </summary>
    public partial class Application_unit_data_View : Window
    {
        public string Application_unit_name { get; set; }
        string g_type;//新增,修改
        int unit_num;
        List<string> insert_list = new List<string>();
        List<string> delete_list = new List<string>();

        public Application_unit_data_View(string type)
        {
            InitializeComponent();

            g_type = type;
            Application_unit_ViewModel application_Unit_ViewModel = new Application_unit_ViewModel();
            List<string> service_groups = application_Unit_ViewModel.Selectservice_group();
            this.ccb_group.ItemsSource = service_groups;
            this.lab_total_volunteers.Content = "0";
        }

        public Application_unit_data_View(string type, string application_unit)
        {
            InitializeComponent();

            g_type = type;
            Application_unit_ViewModel application_Unit_ViewModel = new Application_unit_ViewModel();
            List<string> service_groups = application_Unit_ViewModel.Selectservice_group();
            this.ccb_group.ItemsSource = service_groups;

            Application_unit_data_ViewModel application_Unit_Data_ViewModel = new Application_unit_data_ViewModel();
            application_Unit_Data_ViewModel.SelectApplication_unit_byApplication_unit(application_unit);
            unit_num = application_Unit_Data_ViewModel.Application_unit_no;
            this.txt_application_unit.Text = application_Unit_Data_ViewModel.Application_unit;
            this.ccb_group.SelectedValue = application_Unit_Data_ViewModel.Group.Trim();
            this.txt_application_unit_phone.Text = application_Unit_Data_ViewModel.Application_phone_no;
            this.txt_principal.Text = application_Unit_Data_ViewModel.Principal;
            this.txt_principal_phone.Text = application_Unit_Data_ViewModel.Principal_phone_no;
            this.txt_application_address.Text = application_Unit_Data_ViewModel.Application_address;
            this.lab_total_volunteers.Content = application_Unit_Data_ViewModel.Total_volunteers;
            this.txt_work_content.Text = application_Unit_Data_ViewModel.Work_content;

            if (application_Unit_Data_ViewModel.Service_Periods.Count > 0)
            {
                foreach (var row in application_Unit_Data_ViewModel.Service_Periods)
                {
                    if (row.Service_period.Contains("週一"))
                    {
                        if (row.Service_period.Contains("上午"))
                        {
                            this.Mon_am.Text = row.Volunteer_number;
                        }
                        else if (row.Service_period.Contains("下午"))
                        {
                            this.Mon_pm.Text = row.Volunteer_number;
                        }
                        else
                        {
                            this.Mon_night.Text = row.Volunteer_number;
                        }
                    }

                    if (row.Service_period.Contains("週二"))
                    {
                        if (row.Service_period.Contains("上午"))
                        {
                            this.Tue_am.Text = row.Volunteer_number;
                        }
                        else if (row.Service_period.Contains("下午"))
                        {
                            this.Tue_pm.Text = row.Volunteer_number;
                        }
                        else
                        {
                            this.Tue_night.Text = row.Volunteer_number;
                        }
                    }

                    if (row.Service_period.Contains("週三"))
                    {
                        if (row.Service_period.Contains("上午"))
                        {
                            this.Wed_am.Text = row.Volunteer_number;
                        }
                        else if (row.Service_period.Contains("下午"))
                        {
                            this.Wed_pm.Text = row.Volunteer_number;
                        }
                        else
                        {
                            this.Wed_night.Text = row.Volunteer_number;
                        }
                    }

                    if (row.Service_period.Contains("週四"))
                    {
                        if (row.Service_period.Contains("上午"))
                        {
                            this.Thu_am.Text = row.Volunteer_number;
                        }
                        else if (row.Service_period.Contains("下午"))
                        {
                            this.Thu_pm.Text = row.Volunteer_number;
                        }
                        else
                        {
                            this.Thu_night.Text = row.Volunteer_number;
                        }
                    }

                    if (row.Service_period.Contains("週五"))
                    {
                        if (row.Service_period.Contains("上午"))
                        {
                            this.Fri_am.Text = row.Volunteer_number;
                        }
                        else if (row.Service_period.Contains("下午"))
                        {
                            this.Fri_pm.Text = row.Volunteer_number;
                        }
                        else
                        {
                            this.Fri_night.Text = row.Volunteer_number;
                        }
                    }

                    if (row.Service_period.Contains("週六"))
                    {
                        if (row.Service_period.Contains("上午"))
                        {
                            this.Sat_am.Text = row.Volunteer_number;
                        }
                        else
                        {
                            this.Sat_pm.Text = row.Volunteer_number;
                        }
                    }

                    if (row.Service_period.Contains("週日"))
                    {
                        if (row.Service_period.Contains("上午"))
                        {
                            this.Sun_am.Text = row.Volunteer_number;
                        }
                        else
                        {
                            this.Sun_pm.Text = row.Volunteer_number;
                        }
                    }
                }
            }
            

            ObservableCollection<Unit_volunteer_list> Unit_volunteer_lists = new ObservableCollection<Unit_volunteer_list>();
            if (application_Unit_Data_ViewModel.Volunteer_Lists.Count > 0)
            {
                foreach (var row in application_Unit_Data_ViewModel.Volunteer_Lists)
                {
                    Unit_volunteer_lists.Add(new Unit_volunteer_list()
                    {
                        中文姓名 = row.Chinese_name,
                        英文姓名 = row.English_name,
                        性別 = row.Sex,
                        生日 = row.Birthday,
                        身分證字號 = row.IDcrad_no,
                        病歷號 = row.Medical_record_no,
                        身分類別 = row.Identity_type,
                        年資 = row.Seniority,
                        志工背心號碼 = row.Vest_no,
                        學歷 = row.Education
                    });
                }

                this.dg_volunteer_lists.ItemsSource = Unit_volunteer_lists;
            }           

        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            Application_unit_data_ViewModel application_Unit_Data_ViewModel = new Application_unit_data_ViewModel();
            application_Unit_Data_ViewModel.Application_unit_no = unit_num;
            application_Unit_Data_ViewModel.Application_unit = this.txt_application_unit.Text;
            application_Unit_Data_ViewModel.Group = this.ccb_group.Text;
            application_Unit_Data_ViewModel.Application_phone_no = this.txt_application_unit_phone.Text;
            application_Unit_Data_ViewModel.Principal = this.txt_principal.Text;
            application_Unit_Data_ViewModel.Principal_phone_no = this.txt_principal_phone.Text;
            application_Unit_Data_ViewModel.Application_address = this.txt_application_address.Text;            
            application_Unit_Data_ViewModel.Work_content = this.txt_work_content.Text;

            switch (g_type)
            {
                case "新增":
                    application_Unit_Data_ViewModel.CommitApplication_unit("新增", application_Unit_Data_ViewModel, insert_list, delete_list);
                    break;
                case "修改":
                    application_Unit_Data_ViewModel.CommitApplication_unit("修改",application_Unit_Data_ViewModel, insert_list, delete_list);
                    break;
            }

            Application_unit_name = this.txt_application_unit.Text;

            MessageBox.Show("存檔完成");
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_Expertises_Click(object sender, RoutedEventArgs e)
        {   
            switch (g_type)
            {
                case "新增":
                    Application_unit_expertise_View New_Unit_Expertise_View = new Application_unit_expertise_View();
                    New_Unit_Expertise_View.ShowDialog();
                    insert_list = New_Unit_Expertise_View.insert_list;
                    break;
                case "修改":
                    Application_unit_expertise_View application_Unit_Expertise_View = new Application_unit_expertise_View(unit_num);
                    application_Unit_Expertise_View.ShowDialog();
                    insert_list = application_Unit_Expertise_View.insert_list;
                    delete_list = application_Unit_Expertise_View.delete_list;
                    break;
            }
        }
    }
}
