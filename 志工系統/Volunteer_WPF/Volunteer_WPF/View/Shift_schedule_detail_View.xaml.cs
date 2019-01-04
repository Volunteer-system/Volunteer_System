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
using Volunteer_WPF.User_Control;
using Volunteer_WPF.View_Model;

namespace Volunteer_WPF.View
{
    /// <summary>
    /// Shift_schedule_detail_View.xaml 的互動邏輯
    /// </summary>
    public partial class Shift_schedule_detail_View : Window
    {
        public int Volunteer_count { get; set; }

        private int _unit_no;
        private int _service_period_no;
        private List<ListBoxItem> selectedItems = new List<ListBoxItem>();
        List<Volunteer_card> stay_Volunteer_cards = new List<Volunteer_card>();
        List<Volunteer_card> new_Volunteer_cards = new List<Volunteer_card>();
        List<Volunteer_card> leave_Volunteer_cards = new List<Volunteer_card>();
        List<Volunteer_card> select_Volunteer_cards = new List<Volunteer_card>();

        public Shift_schedule_detail_View(int application_unit_no, int service_period_no)
        {
            InitializeComponent();

            _unit_no = application_unit_no;
            _service_period_no = service_period_no;

            Shift_schedule_detail_ViewModel Schedule_Detail_ViewModel = new Shift_schedule_detail_ViewModel();

            List<Shift_schedule_detail_ViewModel> Stay_Volunteers = Schedule_Detail_ViewModel.SelectStay_Volunteer_byService_Period(application_unit_no, service_period_no);
            foreach (var row in Stay_Volunteers)
            {
                Volunteer_card volunteer_Card = new Volunteer_card();
                volunteer_Card.Volunteer_no = row.Volunteer_no;
                volunteer_Card.Volunteer_name = row.Chinese_name;
                volunteer_Card.Type = row.Identity_type_name;
                volunteer_Card.Order = "續留";
                volunteer_Card.photo = row.Photo;

                stay_Volunteer_cards.Add(volunteer_Card);
                this.pan_stay.Items.Add(volunteer_Card);
            }

            List<Shift_schedule_detail_ViewModel> New_Volunteers = Schedule_Detail_ViewModel.SelectNew_Volunteer_byService_Period(application_unit_no, service_period_no);
            foreach (var row in New_Volunteers)
            {
                Volunteer_card volunteer_Card = new Volunteer_card();
                volunteer_Card.Volunteer_no = row.Volunteer_no;
                volunteer_Card.Volunteer_name = row.Chinese_name;
                volunteer_Card.Type = row.Identity_type_name;
                volunteer_Card.Order = "新申請";
                volunteer_Card.photo = row.Photo;

                new_Volunteer_cards.Add(volunteer_Card);
                this.new_stay.Items.Add(volunteer_Card);
            }

            List<Shift_schedule_detail_ViewModel> Leave_Volunteers = Schedule_Detail_ViewModel.SelectLeave_Volunteer_byService_Period(application_unit_no, service_period_no);
            foreach (var row in Leave_Volunteers)
            {
                Volunteer_card volunteer_Card = new Volunteer_card();
                volunteer_Card.Volunteer_no = row.Volunteer_no;
                volunteer_Card.Volunteer_name = row.Chinese_name;
                volunteer_Card.Type = row.Identity_type_name;
                volunteer_Card.Order = "移除";
                volunteer_Card.photo = row.Photo;

                leave_Volunteer_cards.Add(volunteer_Card);
                this.leave_stay.Items.Add(volunteer_Card);
            }

            Style itemContainerStyle1 = new Style(typeof(ListBoxItem));
            itemContainerStyle1.Setters.Add(new Setter(ListBoxItem.AllowDropProperty, true));
            itemContainerStyle1.Setters.Add(new EventSetter(ListBoxItem.PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler(s_PreviewMouseLeftButtonDown)));
            itemContainerStyle1.Setters.Add(new EventSetter(ListBoxItem.DropEvent, new DragEventHandler(pan_stay_Drop)));
            this.pan_stay.ItemContainerStyle = itemContainerStyle1;

            Style itemContainerStyle2 = new Style(typeof(ListBoxItem));
            itemContainerStyle2.Setters.Add(new Setter(ListBoxItem.AllowDropProperty, true));
            itemContainerStyle2.Setters.Add(new EventSetter(ListBoxItem.PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler(s_PreviewMouseLeftButtonDown)));
            itemContainerStyle2.Setters.Add(new EventSetter(ListBoxItem.DropEvent, new DragEventHandler(new_stay_Drop)));
            this.new_stay.ItemContainerStyle = itemContainerStyle2;

            Style itemContainerStyle3 = new Style(typeof(ListBoxItem));
            itemContainerStyle3.Setters.Add(new Setter(ListBoxItem.AllowDropProperty, true));
            itemContainerStyle3.Setters.Add(new EventSetter(ListBoxItem.PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler(s_PreviewMouseLeftButtonDown)));
            itemContainerStyle3.Setters.Add(new EventSetter(ListBoxItem.DropEvent, new DragEventHandler(leave_stay_Drop)));
            this.leave_stay.ItemContainerStyle = itemContainerStyle3;

            Style itemContainerStyle4 = new Style(typeof(ListBoxItem));
            itemContainerStyle4.Setters.Add(new Setter(ListBoxItem.AllowDropProperty, true));
            itemContainerStyle4.Setters.Add(new EventSetter(ListBoxItem.PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler(s_PreviewMouseLeftButtonDown)));
            //itemContainerStyle4.Setters.Add(new EventSetter(ListBoxItem.DropEvent, new DragEventHandler(select_stay_Drop)));
            this.select_stay.ItemContainerStyle = itemContainerStyle4;
        }

       

        private void s_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListBoxItem)
            {                
                ListBoxItem draggedItem = sender as ListBoxItem;
                //DragDrop.DoDragDrop(draggedItem, draggedItem.DataContext, DragDropEffects.Move);
                DragDrop.DoDragDrop(draggedItem, draggedItem.DataContext, DragDropEffects.Copy);
                draggedItem.IsSelected = true;
            }
        }

        private void pan_stay_Drop(object sender, DragEventArgs e)
        {
            Volunteer_card droppedData = e.Data.GetData(typeof(Volunteer_card)) as Volunteer_card;
            Volunteer_card target = ((ListBoxItem)(sender)).DataContext as Volunteer_card;

            Volunteer_card volunteer_Card = new Volunteer_card();
            volunteer_Card.Volunteer_no = droppedData.Volunteer_no;
            volunteer_Card.Volunteer_name = droppedData.Volunteer_name;
            volunteer_Card.Type = droppedData.Type;
            volunteer_Card.Order = droppedData.Order;
            volunteer_Card.photo = droppedData.photo;

            int removedIdx = stay_Volunteer_cards.Where(p => p.Volunteer_no == droppedData.Volunteer_no).Count();

            if (removedIdx == 0)
            {
                stay_Volunteer_cards.Add(volunteer_Card);
                this.pan_stay.Items.Add(volunteer_Card);

                int new_index = new_Volunteer_cards.FindIndex(p => p.Volunteer_no == volunteer_Card.Volunteer_no);
                if (new_index >= 0)
                {
                    this.new_stay.Items.RemoveAt(new_index);
                    new_Volunteer_cards.RemoveAt(new_index);
                }

                int leave_index = leave_Volunteer_cards.FindIndex(p => p.Volunteer_no == volunteer_Card.Volunteer_no);
                if (leave_index >= 0)
                {
                    this.leave_stay.Items.RemoveAt(leave_index);
                    leave_Volunteer_cards.RemoveAt(leave_index);
                }
            }

        }

        private void new_stay_Drop(object sender, DragEventArgs e)
        {
            Volunteer_card droppedData = e.Data.GetData(typeof(Volunteer_card)) as Volunteer_card;
            Volunteer_card target = ((ListBoxItem)(sender)).DataContext as Volunteer_card;

            Volunteer_card volunteer_Card = new Volunteer_card();
            volunteer_Card.Volunteer_no = droppedData.Volunteer_no;
            volunteer_Card.Volunteer_name = droppedData.Volunteer_name;
            volunteer_Card.Type = droppedData.Type;
            volunteer_Card.Order = droppedData.Order;
            volunteer_Card.photo = droppedData.photo;

            int removedIdx = new_Volunteer_cards.Where(p => p.Volunteer_no == droppedData.Volunteer_no).Count();

            if (removedIdx == 0)
            {
                new_Volunteer_cards.Add(volunteer_Card);
                this.new_stay.Items.Add(volunteer_Card);

                int stay_index = stay_Volunteer_cards.FindIndex(p => p.Volunteer_no == volunteer_Card.Volunteer_no);
                if (stay_index >= 0)
                {
                    this.pan_stay.Items.RemoveAt(stay_index);
                    stay_Volunteer_cards.RemoveAt(stay_index);
                }

                int leave_index = leave_Volunteer_cards.FindIndex(p => p.Volunteer_no == volunteer_Card.Volunteer_no);
                if (leave_index >= 0)
                {
                    this.leave_stay.Items.RemoveAt(leave_index);
                    leave_Volunteer_cards.RemoveAt(leave_index);
                }
            }
            
        }

        private void leave_stay_Drop(object sender, DragEventArgs e)
        {
            Volunteer_card droppedData = e.Data.GetData(typeof(Volunteer_card)) as Volunteer_card;
            Volunteer_card target = ((ListBoxItem)(sender)).DataContext as Volunteer_card;

            Volunteer_card volunteer_Card = new Volunteer_card();
            volunteer_Card.Volunteer_no = droppedData.Volunteer_no;
            volunteer_Card.Volunteer_name = droppedData.Volunteer_name;
            volunteer_Card.Type = droppedData.Type;
            volunteer_Card.Order = droppedData.Order;
            volunteer_Card.photo = droppedData.photo;

            int removedIdx = leave_Volunteer_cards.Where(p => p.Volunteer_no == droppedData.Volunteer_no).Count();

            if (removedIdx == 0)
            {
                leave_Volunteer_cards.Add(volunteer_Card);
                this.leave_stay.Items.Add(volunteer_Card);

                int new_index = new_Volunteer_cards.FindIndex(p => p.Volunteer_no == volunteer_Card.Volunteer_no);
                if (new_index >= 0)
                {
                    this.new_stay.Items.RemoveAt(new_index);
                    new_Volunteer_cards.RemoveAt(new_index);
                }

                int stay_index = stay_Volunteer_cards.FindIndex(p => p.Volunteer_no == volunteer_Card.Volunteer_no);
                if (stay_index >= 0)
                {
                    this.pan_stay.Items.RemoveAt(stay_index);
                    stay_Volunteer_cards.RemoveAt(stay_index);
                }
            }
        }

        private void Btn_volunteer_Click(object sender, RoutedEventArgs e)
        {
            this.select_stay.Items.Clear();

            Button Btn = sender as Button;

            Shift_schedule_detail_ViewModel Schedule_Detail_ViewModel = new Shift_schedule_detail_ViewModel();            
            foreach (var row in Schedule_Detail_ViewModel.SelectVolunteer_byIdentity_type("社會志工", _unit_no, _service_period_no))
            {
                Volunteer_card volunteer_Card = new Volunteer_card();
                volunteer_Card.Volunteer_no = row.Volunteer_no;
                volunteer_Card.Volunteer_name = row.Chinese_name;
                volunteer_Card.Type = row.Identity_type_name;
                if (row.Order > 0)
                {
                    volunteer_Card.Order = row.Order.ToString();
                }
                else
                {
                    volunteer_Card.Order = "非志願";
                }
                volunteer_Card.photo = row.Photo;
                this.select_stay.Items.Add(volunteer_Card);
                select_Volunteer_cards.Add(volunteer_Card);
            }
        }

        private void Btn_student_Click(object sender, RoutedEventArgs e)
        {
            this.select_stay.Items.Clear();

            Shift_schedule_detail_ViewModel Schedule_Detail_ViewModel = new Shift_schedule_detail_ViewModel();
            foreach (var row in Schedule_Detail_ViewModel.SelectVolunteer_byIdentity_type("學生志工", _unit_no, _service_period_no))
            {
                Volunteer_card volunteer_Card = new Volunteer_card();
                volunteer_Card.Volunteer_no = row.Volunteer_no;
                volunteer_Card.Volunteer_name = row.Chinese_name;
                volunteer_Card.Type = row.Identity_type_name;
                volunteer_Card.photo = row.Photo;
                if (row.Order > 0)
                {
                    volunteer_Card.Order = row.Order.ToString();
                }
                else
                {
                    volunteer_Card.Order = "非志願";
                }
                this.select_stay.Items.Add(volunteer_Card);
                select_Volunteer_cards.Add(volunteer_Card);
            }
        }

        private void Btn_select_Click(object sender, RoutedEventArgs e)
        {
            string name = this.txt_name.Text;
            if (string.IsNullOrEmpty(name))
            {
                return;
            }

            this.select_stay.Items.Clear();

            Shift_schedule_detail_ViewModel Schedule_Detail_ViewModel = new Shift_schedule_detail_ViewModel();            
            foreach (var row in Schedule_Detail_ViewModel.SelectVolunteer_byName(name, _unit_no, _service_period_no))
            {
                Volunteer_card volunteer_Card = new Volunteer_card();
                volunteer_Card.Volunteer_no = row.Volunteer_no;
                volunteer_Card.Volunteer_name = row.Chinese_name;
                volunteer_Card.Type = row.Identity_type_name;
                volunteer_Card.photo = row.Photo;
                if (row.Order > 0)
                {
                    volunteer_Card.Order = row.Order.ToString();
                }
                else
                {
                    volunteer_Card.Order = "非志願";
                }
                this.select_stay.Items.Add(volunteer_Card);
                select_Volunteer_cards.Add(volunteer_Card);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Volunteer_count = stay_Volunteer_cards.Count() + new_Volunteer_cards.Count();

            this.Close();
        }

        private void Btn_save_Click(object sender, RoutedEventArgs e)
        {
            Shift_schedule_detail_ViewModel shift_Schedule_Detail_ViewModel = new Shift_schedule_detail_ViewModel();

            List<Shift_schedule_detail_ViewModel> stay_list = new List<Shift_schedule_detail_ViewModel>();
            foreach (var row in stay_Volunteer_cards)
            {
                Shift_schedule_detail_ViewModel Schedule_Detail = new Shift_schedule_detail_ViewModel();
                Schedule_Detail.Volunteer_no = row.Volunteer_no;
                Schedule_Detail.Chinese_name = row.Volunteer_name;
                Schedule_Detail.Identity_type_name = row.Type;

                stay_list.Add(Schedule_Detail);
            }

            List<Shift_schedule_detail_ViewModel> new_list = new List<Shift_schedule_detail_ViewModel>();
            foreach (var row in new_Volunteer_cards)
            {
                Shift_schedule_detail_ViewModel Schedule_Detail = new Shift_schedule_detail_ViewModel();
                Schedule_Detail.Volunteer_no = row.Volunteer_no;
                Schedule_Detail.Chinese_name = row.Volunteer_name;
                Schedule_Detail.Identity_type_name = row.Type;

                new_list.Add(Schedule_Detail);
            }

            List<Shift_schedule_detail_ViewModel> leave_list = new List<Shift_schedule_detail_ViewModel>();
            foreach (var row in leave_Volunteer_cards)
            {
                Shift_schedule_detail_ViewModel Schedule_Detail = new Shift_schedule_detail_ViewModel();
                Schedule_Detail.Volunteer_no = row.Volunteer_no;
                Schedule_Detail.Chinese_name = row.Volunteer_name;
                Schedule_Detail.Identity_type_name = row.Type;

                leave_list.Add(Schedule_Detail);
            }

            shift_Schedule_Detail_ViewModel.UpdateSchedule_volunteer(stay_list, new_list, leave_list, _unit_no, _service_period_no);
            MessageBox.Show("更新成功");

            Volunteer_count = stay_Volunteer_cards.Count() + new_Volunteer_cards.Count();
        }
    }
}
