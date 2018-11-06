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
    /// Application_unit_data_View.xaml 的互動邏輯
    /// </summary>
    public partial class Application_unit_data_View : Window
    {
        public string Application_unit_name { get; set; }
        string g_type;//新增,修改
        int unit_num;
        List<string> insert_list = new List<string>();
        List<string> delete_list = new List<string>();
        List<Unit_service_period> Insert_Service_Periods = new List<Unit_service_period>();
        List<Unit_service_period> Delete_Service_Periods = new List<Unit_service_period>();

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

            Insert_Service_Periods.Clear();
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

            foreach (var row1 in Insert_Service_Periods)
            {
                foreach (var row2 in Delete_Service_Periods)
                {
                    if (row1.Service_period == row2.Service_period)
                    {
                        Delete_Service_Periods.Remove(row2);
                        if (Delete_Service_Periods.Count == 0)
                        {
                            break;
                        }
                    }
                }
                if (Delete_Service_Periods.Count == 0)
                {
                    break;
                }
            }

            switch (g_type)
            {
                case "新增":
                    application_Unit_Data_ViewModel.CommitApplication_unit("新增", application_Unit_Data_ViewModel, insert_list, delete_list, 
                                                                           Insert_Service_Periods, Delete_Service_Periods);
                    break;
                case "修改":
                    application_Unit_Data_ViewModel.CommitApplication_unit("修改",application_Unit_Data_ViewModel, insert_list, delete_list, 
                                                                           Insert_Service_Periods, Delete_Service_Periods);
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

        private void Mon_am_TextChanged(object sender, TextChangedEventArgs e)
        {
            Unit_service_period unit_Service_Period = new Unit_service_period();
            unit_Service_Period.Service_period = "週一上午";
            unit_Service_Period.Volunteer_number = this.Mon_am.Text;

            if (string.IsNullOrEmpty(this.Mon_am.Text))
            {
                Delete_Service_Periods.Add(unit_Service_Period);                
            }
            else
            {
                Insert_Service_Periods.Add(unit_Service_Period);
            }            
        }

        private void Tue_am_TextChanged(object sender, TextChangedEventArgs e)
        {
            Unit_service_period unit_Service_Period = new Unit_service_period();
            unit_Service_Period.Service_period = "週二上午";
            unit_Service_Period.Volunteer_number = this.Tue_am.Text;

            if (string.IsNullOrEmpty(this.Tue_am.Text))
            {
                Delete_Service_Periods.Add(unit_Service_Period); 
            }
            else
            {
                Insert_Service_Periods.Add(unit_Service_Period);
            }
        }

        private void Wed_am_TextChanged(object sender, TextChangedEventArgs e)
        {
            Unit_service_period unit_Service_Period = new Unit_service_period();
            unit_Service_Period.Service_period = "週三上午";
            unit_Service_Period.Volunteer_number = this.Wed_am.Text;

            if (string.IsNullOrEmpty(this.Wed_am.Text))
            {
                Delete_Service_Periods.Add(unit_Service_Period);
            }
            else
            {
                Insert_Service_Periods.Add(unit_Service_Period);
            }
        }

        private void Thu_am_TextChanged(object sender, TextChangedEventArgs e)
        {
            Unit_service_period unit_Service_Period = new Unit_service_period();
            unit_Service_Period.Service_period = "週四上午";
            unit_Service_Period.Volunteer_number = this.Thu_am.Text;

            if (string.IsNullOrEmpty(this.Thu_am.Text))
            {
                Delete_Service_Periods.Add(unit_Service_Period);
            }
            else
            {
                Insert_Service_Periods.Add(unit_Service_Period);
            }
        }

        private void Fri_am_TextChanged(object sender, TextChangedEventArgs e)
        {
            Unit_service_period unit_Service_Period = new Unit_service_period();
            unit_Service_Period.Service_period = "週五上午";
            unit_Service_Period.Volunteer_number = this.Fri_am.Text;

            if (string.IsNullOrEmpty(this.Fri_am.Text))
            {
                Delete_Service_Periods.Add(unit_Service_Period);
            }
            else
            {
                Insert_Service_Periods.Add(unit_Service_Period);
            }
        }

        private void Sat_am_TextChanged(object sender, TextChangedEventArgs e)
        {
            Unit_service_period unit_Service_Period = new Unit_service_period();
            unit_Service_Period.Service_period = "週六上午";
            unit_Service_Period.Volunteer_number = this.Sat_am.Text;

            if (string.IsNullOrEmpty(this.Sat_am.Text))
            {
                Delete_Service_Periods.Add(unit_Service_Period);
            }
            else
            {
                Insert_Service_Periods.Add(unit_Service_Period);
            }
        }

        private void Sun_am_TextChanged(object sender, TextChangedEventArgs e)
        {
            Unit_service_period unit_Service_Period = new Unit_service_period();
            unit_Service_Period.Service_period = "週日上午";
            unit_Service_Period.Volunteer_number = this.Sun_am.Text;

            if (string.IsNullOrEmpty(this.Sun_am.Text))
            {
                Delete_Service_Periods.Add(unit_Service_Period);
            }
            else
            {
                Insert_Service_Periods.Add(unit_Service_Period);
            }
        }

        private void Mon_pm_TextChanged(object sender, TextChangedEventArgs e)
        {
            Unit_service_period unit_Service_Period = new Unit_service_period();
            unit_Service_Period.Service_period = "週一下午";
            unit_Service_Period.Volunteer_number = this.Mon_pm.Text;

            if (string.IsNullOrEmpty(this.Mon_pm.Text))
            {
                Delete_Service_Periods.Add(unit_Service_Period);
            }
            else
            {
                Insert_Service_Periods.Add(unit_Service_Period);
            }
        }

        private void Tue_pm_TextChanged(object sender, TextChangedEventArgs e)
        {
            Unit_service_period unit_Service_Period = new Unit_service_period();
            unit_Service_Period.Service_period = "週二下午";
            unit_Service_Period.Volunteer_number = this.Tue_pm.Text;

            if (string.IsNullOrEmpty(this.Tue_pm.Text))
            {
                Delete_Service_Periods.Add(unit_Service_Period);
            }
            else
            {
                Insert_Service_Periods.Add(unit_Service_Period);
            }
        }

        private void Wed_pm_TextChanged(object sender, TextChangedEventArgs e)
        {
            Unit_service_period unit_Service_Period = new Unit_service_period();
            unit_Service_Period.Service_period = "週三下午";
            unit_Service_Period.Volunteer_number = this.Wed_pm.Text;

            if (string.IsNullOrEmpty(this.Wed_pm.Text))
            {
                Delete_Service_Periods.Add(unit_Service_Period);
            }
            else
            {
                Insert_Service_Periods.Add(unit_Service_Period);
            }
        }

        private void Thu_pm_TextChanged(object sender, TextChangedEventArgs e)
        {
            Unit_service_period unit_Service_Period = new Unit_service_period();
            unit_Service_Period.Service_period = "週四下午";
            unit_Service_Period.Volunteer_number = this.Thu_pm.Text;

            if (string.IsNullOrEmpty(this.Thu_pm.Text))
            {
                Delete_Service_Periods.Add(unit_Service_Period);
            }
            else
            {
                Insert_Service_Periods.Add(unit_Service_Period);
            }
        }

        private void Fri_pm_TextChanged(object sender, TextChangedEventArgs e)
        {
            Unit_service_period unit_Service_Period = new Unit_service_period();
            unit_Service_Period.Service_period = "週五下午";
            unit_Service_Period.Volunteer_number = this.Fri_pm.Text;

            if (string.IsNullOrEmpty(this.Fri_pm.Text))
            {
                Delete_Service_Periods.Add(unit_Service_Period);
            }
            else
            {
                Insert_Service_Periods.Add(unit_Service_Period);
            }
        }

        private void Sat_pm_TextChanged(object sender, TextChangedEventArgs e)
        {
            Unit_service_period unit_Service_Period = new Unit_service_period();
            unit_Service_Period.Service_period = "週六下午";
            unit_Service_Period.Volunteer_number = this.Sat_pm.Text;

            if (string.IsNullOrEmpty(this.Sat_pm.Text))
            {
                Delete_Service_Periods.Add(unit_Service_Period);
            }
            else
            {
                Insert_Service_Periods.Add(unit_Service_Period);
            }
        }

        private void Sun_pm_TextChanged(object sender, TextChangedEventArgs e)
        {
            Unit_service_period unit_Service_Period = new Unit_service_period();
            unit_Service_Period.Service_period = "週日下午";
            unit_Service_Period.Volunteer_number = this.Sun_pm.Text;

            if (string.IsNullOrEmpty(this.Sun_pm.Text))
            {
                Delete_Service_Periods.Add(unit_Service_Period);
            }
            else
            {
                Insert_Service_Periods.Add(unit_Service_Period);
            }
        }

        private void Mon_night_TextChanged(object sender, TextChangedEventArgs e)
        {
            Unit_service_period unit_Service_Period = new Unit_service_period();
            unit_Service_Period.Service_period = "週一夜間";
            unit_Service_Period.Volunteer_number = this.Mon_night.Text;

            if (string.IsNullOrEmpty(this.Mon_night.Text))
            {
                Delete_Service_Periods.Add(unit_Service_Period);
            }
            else
            {
                Insert_Service_Periods.Add(unit_Service_Period);
            }
        }

        private void Tue_night_TextChanged(object sender, TextChangedEventArgs e)
        {
            Unit_service_period unit_Service_Period = new Unit_service_period();
            unit_Service_Period.Service_period = "週二夜間";
            unit_Service_Period.Volunteer_number = this.Tue_night.Text;

            if (string.IsNullOrEmpty(this.Tue_night.Text))
            {
                Delete_Service_Periods.Add(unit_Service_Period);
            }
            else
            {
                Insert_Service_Periods.Add(unit_Service_Period);
            }
        }

        private void Wed_night_TextChanged(object sender, TextChangedEventArgs e)
        {
            Unit_service_period unit_Service_Period = new Unit_service_period();
            unit_Service_Period.Service_period = "週三夜間";
            unit_Service_Period.Volunteer_number = this.Wed_night.Text;

            if (string.IsNullOrEmpty(this.Wed_night.Text))
            {
                Delete_Service_Periods.Add(unit_Service_Period);
            }
            else
            {
                Insert_Service_Periods.Add(unit_Service_Period);
            }
        }

        private void Thu_night_TextChanged(object sender, TextChangedEventArgs e)
        {
            Unit_service_period unit_Service_Period = new Unit_service_period();
            unit_Service_Period.Service_period = "週四夜間";
            unit_Service_Period.Volunteer_number = this.Thu_night.Text;

            if (string.IsNullOrEmpty(this.Thu_night.Text))
            {
                Delete_Service_Periods.Add(unit_Service_Period);
            }
            else
            {
                Insert_Service_Periods.Add(unit_Service_Period);
            }
        }

        private void Fri_night_TextChanged(object sender, TextChangedEventArgs e)
        {
            Unit_service_period unit_Service_Period = new Unit_service_period();
            unit_Service_Period.Service_period = "週五夜間";
            unit_Service_Period.Volunteer_number = this.Fri_night.Text;

            if (string.IsNullOrEmpty(this.Fri_night.Text))
            {
                Delete_Service_Periods.Add(unit_Service_Period);
            }
            else
            {
                Insert_Service_Periods.Add(unit_Service_Period);
            }
        }

        private void Sat_night_TextChanged(object sender, TextChangedEventArgs e)
        {
            Unit_service_period unit_Service_Period = new Unit_service_period();
            unit_Service_Period.Service_period = "週六夜間";
            unit_Service_Period.Volunteer_number = this.Sat_night.Text;

            if (string.IsNullOrEmpty(this.Sat_night.Text))
            {
                Delete_Service_Periods.Add(unit_Service_Period);
            }
            else
            {
                Insert_Service_Periods.Add(unit_Service_Period);
            }
        }

        private void Sun_night_TextChanged(object sender, TextChangedEventArgs e)
        {
            Unit_service_period unit_Service_Period = new Unit_service_period();
            unit_Service_Period.Service_period = "週日夜間";
            unit_Service_Period.Volunteer_number = this.Sun_night.Text;

            if (string.IsNullOrEmpty(this.Sun_night.Text))
            {
                Delete_Service_Periods.Add(unit_Service_Period);
            }
            else
            {
                Insert_Service_Periods.Add(unit_Service_Period);
            }
        }
    }
}
