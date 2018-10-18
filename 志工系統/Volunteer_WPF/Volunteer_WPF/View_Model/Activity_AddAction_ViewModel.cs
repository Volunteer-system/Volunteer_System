using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Volunteer_WPF.Model;

namespace Volunteer_WPF.View_Model
{
    public class Activity_AddAction_ViewModel : INotifyPropertyChanged
    {
        Activity_AddAction_Model AA_Model = new Activity_AddAction_Model(); //實體化Model
        VolunteerEntities Myentity = new VolunteerEntities();

        TypeModel TM_Model = new TypeModel();                        //活動類別Model
        GroupModel GM_Model = new GroupModel();                      //活動組別Model

        public ObservableCollection<TypeModel> TM { get; set; }      //Combobox ItemsSource  用的屬性
        public ObservableCollection<GroupModel> GM { get; set; }
        TypeModel TM_SelectedItem { get; set; }                      //Combobox SelectedItem 用的屬性
        GroupModel GM_SelectedItem { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;   //屬性值變更時產生的事件 繼承介面INotifyPropertyChanged實作的方法

        public void RaisePropertyChanged(string v = "")             //屬性值變更事件判斷
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }

        int supersionID = 2;                            //假設登錄者ID
        public Activity_AddAction_ViewModel()           //建構子把Model實體化
        {
            whologin(supersionID);                      //將登錄人員的資料帶入的方法
            //抓活動類型、活動組別資料的程式▼
            #region
            TM = new ObservableCollection<TypeModel>();
            var q = from t in Myentity.Activity_type
                    select t;

            foreach (var row in q)
            {
                TM.Add(new TypeModel { Activity_type_ID = row.Activity_type_ID, Activity_type = row.Activity_type1 });
            }
            GM = new ObservableCollection<GroupModel>();
            var q1 = from s in Myentity.Service_group
                     select s;

            foreach (var row in q1)
            {
                GM.Add(new GroupModel { Group_no = row.Group_no, Group_name = row.Group_name });
            }
            #endregion
        }

        private void whologin(int supersionID)          //將登錄人員的資料帶入
        {
            var q = (from s in Myentity.Volunteer_supervision
                    where s.supervision_ID == supersionID
                    select s).SingleOrDefault();
            VM_supervision_Name = q.supervision_Name;
            VM_supervision_phone =Convert.ToInt32( q.supervision_phone);
            VM_supervision_email = q.supervision_email;
        }

        public string VM_Activity_name                  //活動名稱
        {
            get { return AA_Model.Activity_name; }
            set
            {
                AA_Model.Activity_name = value;
                RaisePropertyChanged();
            }
        }
        public string VM_Undertake_unit                 //承辦單位
        {
            get { return AA_Model.Undertake_unit; }
            set
            {
                AA_Model.Undertake_unit = value;
                RaisePropertyChanged();
            }
        }
        public int VM_Undertaker                        //承辦人 int?  
        {
            get { return AA_Model.Undertaker; }
            set
            {
                AA_Model.Undertaker = value;
                RaisePropertyChanged();
            }
        }
        public string VM_Undertake_phone                //承辦單位電話
        {
            get { return AA_Model.Undertake_phone; }
            set
            {
                AA_Model.Undertake_phone = value;
                RaisePropertyChanged();
            }
        }
        public string VM_Undertake_email                //承辦單位e-mail
        {
            get { return AA_Model.Undertake_email; }
            set
            {
                AA_Model.Undertake_email = value;
                RaisePropertyChanged();
            }
        }
        public int VM_Member                            //課程人數
        {
            get { return AA_Model.Member; }
            set
            {
                AA_Model.Member = value;
                RaisePropertyChanged();
            }
        }
        public int VM_Spare                             //備取人數
        {
            get { return AA_Model.Spare; }
            set
            {
                AA_Model.Spare = value;
                RaisePropertyChanged();
            }
        }
        public string VM_Place                          //活動地點
        {
            get { return AA_Model.Place; }
            set
            {
                AA_Model.Place = value;
                RaisePropertyChanged();
            }
        }
        public string VM_Summary                        //活動簡介(連接文字描述檔)?
        {
            get { return AA_Model.Summary; }
            set
            {
                AA_Model.Summary = value;
                RaisePropertyChanged();
            }
        }
        public int VM_Activity_Photo_id                 //活動照片ID -> 活動照片檔(Basic.Activity_photo) 識別規格
        {
            get { return AA_Model.Activity_Photo_id; }
            set
            {
                AA_Model.Activity_Photo_id = value;
                RaisePropertyChanged();
            }
        }
        public int VM_Activity_id                       //活動ID -> 活動設定檔(Basic.Activity) Activity_no
        {
            get { return AA_Model.Activity_id; }
            set
            {
                AA_Model.Activity_id = value;
                RaisePropertyChanged();
            }
        }
        public string VM_supervision_Name               //志工督導姓名
        {
            get { return AA_Model.supervision_Name; }
            set
            {
                AA_Model.supervision_Name = value;
                RaisePropertyChanged();
            }
        }
        public int VM_supervision_phone                 //志工督導電話
        {
            get { return AA_Model.supervision_phone; }
            set
            {
                AA_Model.supervision_phone = value;
                RaisePropertyChanged();
            }
        }
        public string VM_supervision_address            //志工督導地址
        {
            get { return AA_Model.supervision_address; }
            set
            {
                AA_Model.supervision_address = value;
                RaisePropertyChanged();
            }
        }
        public string VM_supervision_email              //志工督導Email
        {
            get { return AA_Model.supervision_email; }
            set
            {
                AA_Model.supervision_email = value;
                RaisePropertyChanged();
            }
        }
        public byte[] VM_Activity_photo                 //照片   
        {
            get { return AA_Model.Activity_photo; }      //二進制
            set
            {
                AA_Model.Activity_photo = value;
                RaisePropertyChanged();
            }
        }
        private BitmapImage im;
        public BitmapImage VM_Activity_photos           //照片   
        {
            //get { return AA_Model.VM_Activity_photos; }
            //set
            //{
            //    AA_Model.VM_Activity_photos = value;
            //    RaisePropertyChanged();
            //}
            get { return im; }
            set
            {
                im = value;
                RaisePropertyChanged();
            }
        }
        //TypeModel
        public int VM_Activity_type_ID
        {
            get { return TM_Model.Activity_type_ID; }    //活動類別ID
            set
            {
                TM_Model.Activity_type_ID = value;
                RaisePropertyChanged();
            }   //活動類別
        }
        public string VM_Activity_type
        {
            get { return TM_Model.Activity_type; }       //活動類別
            set
            {
                TM_Model.Activity_type = value;
                RaisePropertyChanged();
            }
        }
        //GroupModel
        public int VM_Group_no                           //組別
        {
            get { return GM_Model.Group_no; }
            set
            {
                GM_Model.Group_no = value;
                RaisePropertyChanged();
            }
        }
        public string Group_name                         //組別名稱
        {
            get { return GM_Model.Group_name; }
            set
            {
                GM_Model.Group_name = value;
                RaisePropertyChanged();
            }
        }

        
        //Command用▼
        void TESTFUN()
        {
            //System.Windows.Forms.MessageBox.Show(VM_Activity_name);
            //System.Windows.Forms.OpenFileDialog openfile = new System.Windows.Forms.OpenFileDialog();
            //openfile.ShowHelp = true;
            //openfile.Filter = "文字文件(*.txt)|*.txt|所有檔案(*.*)|*.*";
            //if (openfile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    System.IO.StreamReader SR = new System.IO.StreamReader(openfile.FileName,Encoding.GetEncoding(950));
            //    VM_Summary = SR.ReadToEnd();
            //    SR.Dispose();
            //}
            System.Windows.Forms.OpenFileDialog openImage = new System.Windows.Forms.OpenFileDialog();
            openImage.ShowHelp = true;
            openImage.Filter = "點陣圖檔案(*.bmp;*.dib)|*.bmp;*.dib|JPEG(*.jpg;*.jpeg;*.jpe;*.jfif)|*.jpg;*.jpeg;*.jpe;*.jfif|GIF(*.gif)|*.gif|TIFF(*.tif;*.tiff)|*.tif;*.tiff|PNG(*.pgn)|*.png|ICO(*.ico)|*.ico|所有檔案(*.*)|*.*";
            if (openImage.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                BitmapImage bi = new BitmapImage(new Uri(openImage.FileName));
                VM_Activity_photos = bi;
            }             
        }
        bool CanShow()
        {
            return true;
        }
        public ICommand ShowTEST { get { return new RelayCommand(TESTFUN, CanShow); } }
    }
}
