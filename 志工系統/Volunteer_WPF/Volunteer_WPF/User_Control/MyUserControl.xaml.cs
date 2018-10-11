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

namespace Volunteer_WPF
{
    /// <summary>
    /// MyUserControl.xaml 的互動邏輯
    /// </summary>
    public partial class MyUserControl : UserControl
    {
        bool isok;
        bool isdelete;
        public MyUserControl()
        {
            InitializeComponent();
        }
        public event EventHandler MU_Click_OK;               //控制項事件
        public event EventHandler MU_Click_Delete;
        private void btn_ok_Click(object sender, RoutedEventArgs e)
        {  //使用的圖片刷子，註解起來的為家用

            ImageBrush brush_Ok = new ImageBrush();
            brush_Ok.ImageSource = new BitmapImage(new Uri(@"C:\Users\peter.wu\Documents\GitHub\Volunteer_System\志工系統\Volunteer_WPF\Volunteer_WPF\image\check-green.png"));
            //brush_Ok.ImageSource = new BitmapImage(new Uri(@"C:\Users\user\Desktop\專案用\專案1081009\Volunteer_System-master\Volunteer_System-master\志工系統\Volunteer_WPF\Volunteer_WPF\image\ok.jpg"));
            ImageBrush brush_NOk = new ImageBrush();
            brush_NOk.ImageSource = new BitmapImage(new Uri(@"C:\Users\peter.wu\Documents\GitHub\Volunteer_System\志工系統\Volunteer_WPF\Volunteer_WPF\image\check.png"));
            //brush_NOk.ImageSource = new BitmapImage(new Uri(@"C:\Users\user\Desktop\專案用\專案1081009\Volunteer_System-master\Volunteer_System-master\志工系統\Volunteer_WPF\Volunteer_WPF\image\Nok.jpg"));

            ImageBrush brush_Ndelete = new ImageBrush();
            brush_Ndelete.ImageSource = new BitmapImage(new Uri(@"C:\Users\peter.wu\Documents\GitHub\Volunteer_System\志工系統\Volunteer_WPF\Volunteer_WPF\image\delete.png"));
            //brush_Ndelete.ImageSource = new BitmapImage(new Uri(@"C:\Users\user\Desktop\專案用\專案1081009\Volunteer_System-master\Volunteer_System-master\志工系統\Volunteer_WPF\Volunteer_WPF\image\Ncancel.jpg"));
            ImageBrush brush_delete = new ImageBrush();
            brush_delete.ImageSource = new BitmapImage(new Uri(@"C:\Users\peter.wu\Documents\GitHub\Volunteer_System\志工系統\Volunteer_WPF\Volunteer_WPF\image\delete-red.png"));
            //brush_delete.ImageSource = new BitmapImage(new Uri(@"C:\Users\user\Desktop\專案用\專案1081009\Volunteer_System-master\Volunteer_System-master\志工系統\Volunteer_WPF\Volunteer_WPF\image\cancel.jpg"));
            if (((Button)e.Source).Name == "btn_ok")             //如果按下打勾的按鈕
            {
                isok = !isok;                                    //判斷使否為亮起
                if (isok)
                {
                    this.btn_ok.Background = brush_Ok;           //改變打勾鈕的背景圖
                    this.btn_delete.Background = brush_Ndelete;  //改變打X鈕的背景圖
                    isdelete = false;                            //設定X為未按的狀態
                    if (MU_Click_OK != null)                     //按下打勾的事件
                    {
                        MU_Click_OK(this, EventArgs.Empty);
                    }
                }
                else
                {                                               //判斷是否為亮起，否
                    this.btn_ok.Background = brush_NOk;         //改變打勾鈕的背景圖
                    if (MU_Click_OK != null)
                    {
                        MU_Click_OK(this, EventArgs.Empty);
                    }
                }
            }
            if (((Button)e.Source).Name == "btn_delete")         //如果按下打X的按鈕
            {
                isdelete = !isdelete;                            //判斷使否為亮起
                if (isdelete)                                    
                {
                    isok = false;                                //設定打勾為未按的狀態
                    this.btn_ok.Background = brush_NOk;          //改變打勾鈕的背景圖
                    this.btn_delete.Background = brush_delete;   //改變打X鈕的背景圖
                    if (MU_Click_Delete != null)                 //按下打X的事件
                    {
                        MU_Click_Delete(this, EventArgs.Empty);
                    }

                }
                else
                {                                                //判斷是否為亮起，否
                    this.btn_delete.Background = brush_Ndelete;  //改變打X鈕的背景圖
                    if (MU_Click_Delete != null)
                    {
                        MU_Click_Delete(this, EventArgs.Empty);
                    }
                }
            }
        }
    }
}
