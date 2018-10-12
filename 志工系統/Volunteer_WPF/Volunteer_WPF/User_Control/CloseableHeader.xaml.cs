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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Volunteer_WPF.User_Control
{
    /// <summary>
    /// CloseableHeader.xaml 的互動邏輯
    /// </summary>
    public partial class CloseableHeader : UserControl
    {
        public CloseableHeader()
        {
            InitializeComponent();
        }

      public  class ClosableTab : TabItem
        {

            public ClosableTab()
            {
                CloseableHeader closableTabHeader = new CloseableHeader();
                this.Header = closableTabHeader;

                closableTabHeader.button_close.MouseEnter += Button_close_MouseEnter;
                closableTabHeader.button_close.MouseLeave += Button_close_MouseLeave;
                closableTabHeader.button_close.Click += Button_close_Click;
                closableTabHeader.label_TabTitle.SizeChanged += Label_TabTitle_SizeChanged;
            }

            private void Label_TabTitle_SizeChanged(object sender, SizeChangedEventArgs e)
            {
                ((CloseableHeader)this.Header).button_close.Margin = new Thickness(
                ((CloseableHeader)this.Header).label_TabTitle.ActualWidth + 5, 3, 4, 0);
            }

            private void Button_close_Click(object sender, RoutedEventArgs e)
            {
                ((TabControl)this.Parent).Items.Remove(this);
            }

            private void Button_close_MouseLeave(object sender, MouseEventArgs e)
            {
                ((CloseableHeader)this.Header).button_close.Foreground = Brushes.Black;
            }

            private void Button_close_MouseEnter(object sender, MouseEventArgs e)
            {
                ((CloseableHeader)this.Header).button_close.Foreground = Brushes.Red;
            }

            public string Title
            {
                set
                {
                    ((CloseableHeader)this.Header).label_TabTitle.Content = value;
                }
            }

            protected override void OnSelected(RoutedEventArgs e)
            {
                base.OnSelected(e);
                ((CloseableHeader)this.Header).button_close.Visibility = Visibility.Visible;
            }
            protected override void OnUnselected(RoutedEventArgs e)
            {
                base.OnUnselected(e);
                ((CloseableHeader)this.Header).button_close.Visibility = Visibility.Hidden;
            }
            protected override void OnMouseEnter(MouseEventArgs e)
            {
                base.OnMouseEnter(e);
                ((CloseableHeader)this.Header).button_close.Visibility = Visibility.Visible;
            }
            protected override void OnMouseLeave(MouseEventArgs e)
            {
                base.OnMouseLeave(e);
                if (!this.IsSelected)
                {
                    ((CloseableHeader)this.Header).button_close.Visibility = Visibility.Hidden;
                }
            }

        }

    }
}

