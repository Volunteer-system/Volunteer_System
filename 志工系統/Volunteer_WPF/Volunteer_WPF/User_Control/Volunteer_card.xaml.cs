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
        public byte[] photo { get; set; }

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

            if (photo != null)
            {
                this.img_photo.Source = ConvertByteArrayToBitmapImage(photo);
            }
            
        }

        public static BitmapImage ConvertByteArrayToBitmapImage(Byte[] bytes)
        {
            var stream = new MemoryStream(bytes);
            stream.Seek(0, SeekOrigin.Begin);
            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = stream;
            image.EndInit();
            return image;
        }
    }
}
