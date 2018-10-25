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
    /// Application_unit_detail_View.xaml 的互動邏輯
    /// </summary>
    public partial class Application_unit_detail_View : Window
    {
        public Application_unit_detail_View(string Application_unit)
        {
            InitializeComponent();

            Application_unit_ViewModel application_Unit_ViewModel = new Application_unit_ViewModel();
            application_Unit_ViewModel.SelectApplication_unit_byApplication_unit(Application_unit);
            this.Lab_application_unit.Content = application_Unit_ViewModel.Application_unit;
            this.Lab_application_unit_phone.Content = application_Unit_ViewModel.Application_phone_no;
            this.Lab_group.Content = application_Unit_ViewModel.Group;
            this.Lab_principal.Content = application_Unit_ViewModel.Principal;
            this.Lab_principal_phone.Content = application_Unit_ViewModel.Principal_phone_no;
            this.Lab_application_address.Content = application_Unit_ViewModel.Application_address;
            this.Lab_work_content.Content = application_Unit_ViewModel.Work_content;
            this.Lab_total_volunteers.Content = application_Unit_ViewModel.Total_volunteers;

            string str_expertise = null;
            foreach (var row in application_Unit_ViewModel.Expertises)
            {
                str_expertise += row + ", ";
            }
            this.Lab_expertise.Content = str_expertise;

            foreach (var row in application_Unit_ViewModel.Service_Periods)
            {
                if (row.Contains("週一"))
                {
                    if (row.Contains("上午"))
                    {
                        this.Mon_am.Content = "V";
                    }
                    else if (row.Contains("下午"))
                    {
                        this.Mon_pm.Content = "V";
                    }
                    else
                    {
                        this.Mon_night.Content = "V";
                    }
                }

                if (row.Contains("週二"))
                {
                    if (row.Contains("上午"))
                    {
                        this.Tue_am.Content = "V";
                    }
                    else if (row.Contains("下午"))
                    {
                        this.Tue_pm.Content = "V";
                    }
                    else
                    {
                        this.Tue_night.Content = "V";
                    }
                }

                if (row.Contains("週三"))
                {
                    if (row.Contains("上午"))
                    {
                        this.Wed_am.Content = "V";
                    }
                    else if (row.Contains("下午"))
                    {
                        this.Wed_pm.Content = "V";
                    }
                    else
                    {
                        this.Wed_night.Content = "V";
                    }
                }

                if (row.Contains("週四"))
                {
                    if (row.Contains("上午"))
                    {
                        this.Thu_am.Content = "V";
                    }
                    else if (row.Contains("下午"))
                    {
                        this.Thu_pm.Content = "V";
                    }
                    else
                    {
                        this.Thu_night.Content = "V";
                    }
                }

                if (row.Contains("週五"))
                {
                    if (row.Contains("上午"))
                    {
                        this.Fri_am.Content = "V";
                    }
                    else if (row.Contains("下午"))
                    {
                        this.Fri_pm.Content = "V";
                    }
                    else
                    {
                        this.Fri_night.Content = "V";
                    }
                }

                if (row.Contains("週六"))
                {
                    if (row.Contains("上午"))
                    {
                        this.Sat_am.Content = "V";
                    }
                    else
                    {
                        this.Sat_pm.Content = "V";
                    }
                }

                if (row.Contains("週日"))
                {
                    if (row.Contains("上午"))
                    {
                        this.Sun_am.Content = "V";
                    }
                    else
                    {
                        this.Sun_pm.Content = "V";
                    }
                }
            }

            ObservableCollection<Unit_volunteer_list> Unit_volunteer_lists = new ObservableCollection<Unit_volunteer_list>();
            foreach (var row in application_Unit_ViewModel.Volunteer_Lists)
            {
                Unit_volunteer_lists.Add(new Unit_volunteer_list() {
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

            this.Activity_datagrid.ItemsSource = Unit_volunteer_lists;
        }
    }

    class Unit_volunteer_list
    {
        public string 中文姓名 { get; set; }
        public string 英文姓名 { get; set; }
        public string 性別 { get; set; }
        public string 生日 { get; set; }
        public string 身分證字號 { get; set; }
        public string 病歷號 { get; set; }
        public string 身分類別 { get; set; }
        public string 年資 { get; set; }
        public string 志工背心號碼 { get; set; }
        public string 學歷 { get; set; }
    }
}
