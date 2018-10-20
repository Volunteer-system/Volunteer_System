using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Volunteer_WPF.Model;

namespace Volunteer_WPF.View_Model
{
    class Activity_ViewModel
    {
        //活動編號
        public string Activity_no { get; set; }
        //活動名稱
        public string Activity_name { get; set; }
        //活動類別
        public string Activity_type { get; set; }
        //組別
        public string Group { get; set; }
        //活動起始時間
        public string Activity_startdate { get; set; }
        //活動結束時間
        public string Activity_enddate { get; set; }
        //承辦單位
        public string Undertake_unit { get; set; }
        //承辦人
        public string Undertaker { get; set; }
        //承辦單位電話
        public string Undertake_phone { get; set; }
        //承辦單位e-mail
        public string Undertake_email { get; set; }
        //講師
        public string Lecturer { get; set; }
        //課程人數
        public string Member { get; set; }
        //備取人數
        public string Spare { get; set; }
        //活動地點
        public string Place { get; set; }
        //活動簡介
        public string Summary { get; set; }
        //活動首頁照片
        public BitmapImage Home_image { get; set; }

        public List<BitmapImage> SelectActivity_byActivity_no(string activity_name, DateTime startdate)
        {
            Activity_Model activity_Model = new Activity_Model();
            List<BitmapImage> Activity_model_photos = activity_Model.SelectActivity_byActivity_no(activity_name, startdate);

            Activity_no = activity_Model.Activity_no.ToString();
            Activity_name = activity_Model.Activity_name;
            Activity_type = activity_Model.Activity_type;
            Group = activity_Model.Group;
            Activity_startdate = activity_Model.Activity_startdate;
            Activity_enddate = activity_Model.Activity_enddate;
            Undertake_unit = activity_Model.Undertake_unit;
            Undertaker = activity_Model.Undertaker;
            Undertake_phone = activity_Model.Undertake_phone;
            Undertake_email = activity_Model.Undertake_email;
            Lecturer = activity_Model.Lecturer;
            Member = activity_Model.Member;
            Spare = activity_Model.Spare;
            Place = activity_Model.Place;
            Summary = activity_Model.Summary;
            Home_image = activity_Model.Home_image;

            List<BitmapImage> Activity_photos = new List<BitmapImage>();
            foreach (var row in Activity_model_photos)
            {                
                Activity_photos.Add(row);
            }

            return Activity_photos;
        }

        public List<Activity_ViewModel> SelectActivity_byActivity_no(DateTime Startdate, DateTime Enddate, string Type, string Group)
        {
            Activity_Model activity_Model = new Activity_Model();
            List< Activity_Model > activity_models = activity_Model.SelectActivity_byActivity_no(Startdate, Enddate, Type, Group);
            List<Activity_ViewModel> activity_viewmodels = new List<Activity_ViewModel>();
            foreach (var row in activity_models)
            {
                Activity_ViewModel activity_ViewModel = new Activity_ViewModel();
                activity_ViewModel.Activity_no = row.Activity_no.ToString();
                activity_ViewModel.Activity_name = row.Activity_name;
                activity_ViewModel.Activity_type = row.Activity_type;
                activity_ViewModel.Group = row.Group;
                activity_ViewModel.Activity_startdate = row.Activity_startdate;
                activity_ViewModel.Activity_enddate = row.Activity_enddate;
                activity_ViewModel.Undertake_unit = row.Undertake_unit;
                activity_ViewModel.Undertaker = row.Undertaker;
                activity_ViewModel.Undertake_phone = row.Undertake_phone;
                activity_ViewModel.Undertake_email = row.Undertake_email;
                activity_ViewModel.Member = row.Member;
                activity_ViewModel.Spare = row.Spare;
                activity_ViewModel.Place = row.Place;
                activity_ViewModel.Summary = row.Summary;

                activity_viewmodels.Add(activity_ViewModel);
            }            

            return activity_viewmodels;
        }

        public List<Activity_ViewModel> SelectVolunteer_ActivitybyVolunteerno(int Volunteer_no)
        {
            Activity_Model activity_Model = new Activity_Model();
            List<Activity_ViewModel> activity_list = new List<Activity_ViewModel>();
            List<Activity_Model> activity_model_list = activity_Model.SelectVolunteer_ActivitybyVolunteerno(Volunteer_no);

            foreach (var row in activity_model_list)
            {
                Activity_ViewModel activity_ViewModel = new Activity_ViewModel();
                activity_ViewModel.Activity_no = row.Activity_no.ToString();
                activity_ViewModel.Activity_name = row.Activity_name;
                activity_ViewModel.Undertaker = row.Undertaker;

                activity_list.Add(activity_ViewModel);
            }

            return activity_list;
        }
       
    }
}
