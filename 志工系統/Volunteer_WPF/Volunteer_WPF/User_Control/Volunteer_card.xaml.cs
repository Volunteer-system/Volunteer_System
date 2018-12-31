using System;
using System.Collections.Generic;
using System.IO;
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
    /// Volunteer_card.xaml 的互動邏輯
    /// </summary>
    public partial class Volunteer_card : UserControl
    {
        public int Volunteer_no { get; set; }
        public string Volunteer_name {
            get { return this.lab_name.Content.ToString(); }
            set { this.lab_name.Content = value; }
        }            
        public string Type {
            get { return this.lab_type.Content.ToString(); }
            set { this.lab_type.Content = value; }
        }
        private byte[] p_photo { get; set; }
        public byte[] photo {
            get { return p_photo; }
            set {
                BitmapImage image = new BitmapImage();
                if (value != null)
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(value);
                    image.BeginInit();
                    image.StreamSource = ms;
                    image.DecodePixelHeight = 55;
                    image.DecodePixelWidth = 55;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.EndInit();
                    this.img_photo.Source = image;

                    p_photo = value;
                }
            }
        }

        public Volunteer_card()
        {
            InitializeComponent();

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.DecodePixelHeight = 55;
            bitmap.DecodePixelWidth = 55;
            bitmap.UriSource = new Uri(@"C:\Users\peter.wu\Documents\GitHub\Volunteer_System\志工系統\Volunteer_WPF\Volunteer_WPF\image\Noimage.png");
            bitmap.EndInit();

            this.img_photo.Source = bitmap;           
            
        }        
    }
}
