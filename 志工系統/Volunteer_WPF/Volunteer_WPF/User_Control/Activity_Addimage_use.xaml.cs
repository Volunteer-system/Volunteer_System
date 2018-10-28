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
            string path_ok = System.IO.Path.GetFullPath("../../image/check-green.png");
            brush_ok.ImageSource = new BitmapImage(new Uri(path_ok,UriKind.RelativeOrAbsolute));

            brush_nok = new ImageBrush();
            string path_nok = System.IO.Path.GetFullPath("../../image/check.png");
            brush_nok.ImageSource = new BitmapImage(new Uri(path_nok,UriKind.RelativeOrAbsolute));
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
