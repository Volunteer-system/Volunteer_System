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
    /// Activity_Addimage_use.xaml 的互動邏輯
    /// </summary>
    public partial class Activity_Addimage_use : UserControl
    {
        bool is_ok;
        ImageBrush brush_ok;
        ImageBrush brush_nok;
        public event MouseButtonEventHandler A_MouseleftbuttonDown;
        public event EventHandler click;
        public Activity_Addimage_use()
        {
            InitializeComponent();
            brush_ok = new ImageBrush();
            brush_ok.ImageSource = new BitmapImage(new Uri(@"C:\Users\peter.wu\Documents\GitHub\Volunteer_System\志工系統\Volunteer_WPF\Volunteer_WPF\image\check-green.png"));
            brush_nok = new ImageBrush();
            brush_nok.ImageSource = new BitmapImage(new Uri(@"C:\Users\peter.wu\Documents\GitHub\Volunteer_System\志工系統\Volunteer_WPF\Volunteer_WPF\image\check.png"));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            is_ok = !is_ok;
            if (is_ok)
            {
                ((Button)e.Source).Background = brush_ok;
                if (click != null)
                {
                    click(this, EventArgs.Empty);
                }
            }
            else
            {
                ((Button)e.Source).Background = brush_nok;
                if (click != null)
                {
                    click(this, EventArgs.Empty);
                }
            }
        }
        public string Title
        {
            get { return this.lab_title.Content.ToString(); }
            set { this.lab_title.Content = value; }
        }

        public BitmapImage imagesource
        { get { return image1.Source as BitmapImage; }
          set
            {
                this.image1.Source = value;
            }
        }

        private void image1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (A_MouseleftbuttonDown != null)
            {
                A_MouseleftbuttonDown(this, null);
            }
        }
    }
}
