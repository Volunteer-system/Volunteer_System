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

namespace Volunteer_WPF.View
{
    /// <summary>
    /// Shift_schedule_detail_View.xaml 的互動邏輯
    /// </summary>
    public partial class Shift_schedule_detail_View : Window
    {
        private List<ListBoxItem> selectedItems = new List<ListBoxItem>();
        List<Volunteer_card> stay_Volunteer_cards = new List<Volunteer_card>();
        List<Volunteer_card> new_Volunteer_cards = new List<Volunteer_card>();
        List<Volunteer_card> leave_Volunteer_cards = new List<Volunteer_card>();

        public Shift_schedule_detail_View()
        {
            InitializeComponent();

            
            for (int i = 0; i < 3; i++)
            {
                Volunteer_card volunteer_Card = new Volunteer_card();
                volunteer_Card.Volunteer_name = "測試" + i;
                volunteer_Card.Type = "測試" + i;
                stay_Volunteer_cards.Add(volunteer_Card);
                
            }
            for (int i = 0; i < 3; i++)
            {
                Volunteer_card volunteer_Card = new Volunteer_card();
                volunteer_Card.Volunteer_name = "測試" + i;
                volunteer_Card.Type = "測試" + i;
                new_Volunteer_cards.Add(volunteer_Card);
            }
            for (int i = 0; i < 3; i++)
            {
                Volunteer_card volunteer_Card = new Volunteer_card();
                volunteer_Card.Volunteer_name = "測試" + i;
                volunteer_Card.Type = "測試" + i;
                leave_Volunteer_cards.Add(volunteer_Card);
            }

            this.pan_stay.ItemsSource = stay_Volunteer_cards;
            this.new_stay.ItemsSource = new_Volunteer_cards;
            this.leave_stay.ItemsSource = leave_Volunteer_cards;


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
        }

        private void s_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (sender is ListBoxItem)
            {
                ListBoxItem draggedItem = sender as ListBoxItem;
                DragDrop.DoDragDrop(draggedItem, draggedItem.DataContext, DragDropEffects.Move);
                draggedItem.IsSelected = true;
            }
        }

        private void pan_stay_Drop(object sender, DragEventArgs e)
        {
            Volunteer_card droppedData = e.Data.GetData(typeof(Volunteer_card)) as Volunteer_card;
            Volunteer_card target = ((ListBoxItem)(sender)).DataContext as Volunteer_card;

            var name = target.Volunteer_name;
            var type = target.Type;

            int removedIdx = pan_stay.Items.IndexOf(droppedData);
            int targetIdx = pan_stay.Items.IndexOf(target);

            if (removedIdx < targetIdx)
            {
                stay_Volunteer_cards.Insert(targetIdx + 1, droppedData);
                //stay_Volunteer_cards.RemoveAt(removedIdx);
            }
            else
            {
                int remIdx = removedIdx + 1;
                if (stay_Volunteer_cards.Count + 1 > remIdx)
                {
                    stay_Volunteer_cards.Insert(targetIdx, droppedData);
                    //stay_Volunteer_cards.RemoveAt(remIdx);
                }
            }
        }

        private void new_stay_Drop(object sender, DragEventArgs e)
        {
            Volunteer_card droppedData = e.Data.GetData(typeof(Volunteer_card)) as Volunteer_card;
            Volunteer_card target = ((ListBoxItem)(sender)).DataContext as Volunteer_card;

            var name = target.Volunteer_name;
            var type = target.Type;

            int removedIdx = new_stay.Items.IndexOf(droppedData);
            int targetIdx = new_stay.Items.IndexOf(target);

            if (removedIdx < targetIdx)
            {
                new_Volunteer_cards.Insert(targetIdx + 1, droppedData);
                //new_Volunteer_cards.RemoveAt(removedIdx);
            }
            else
            {
                int remIdx = removedIdx + 1;
                if (new_Volunteer_cards.Count + 1 > remIdx)
                {
                    new_Volunteer_cards.Insert(targetIdx, droppedData);
                    //new_Volunteer_cards.RemoveAt(remIdx);
                }
            }
        }

        private void leave_stay_Drop(object sender, DragEventArgs e)
        {
            Volunteer_card droppedData = e.Data.GetData(typeof(Volunteer_card)) as Volunteer_card;
            Volunteer_card target = ((ListBoxItem)(sender)).DataContext as Volunteer_card;

            var name = target.Volunteer_name;
            var type = target.Type;

            int removedIdx = leave_stay.Items.IndexOf(droppedData);
            int targetIdx = leave_stay.Items.IndexOf(target);

            if (removedIdx < targetIdx)
            {
                leave_Volunteer_cards.Insert(targetIdx + 1, droppedData);
                //leave_Volunteer_cards.RemoveAt(removedIdx);
            }
            else
            {
                int remIdx = removedIdx + 1;
                if (leave_Volunteer_cards.Count + 1 > remIdx)
                {
                    leave_Volunteer_cards.Insert(targetIdx, droppedData);
                    //leave_Volunteer_cards.RemoveAt(remIdx);
                }
            }
        }

        private void Btn_volunteer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_student_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_select_Click(object sender, RoutedEventArgs e)
        {

        }

        
    }
}
