using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Volunteer_WPF.Model;
using Volunteer_WPF.User_Control;

namespace Volunteer_WPF.View_Model
{
    public class Activity_AddAction_ViewModel : INotifyPropertyChanged,IDataErrorInfo
    {
        List<string> list_photo = new List<string>();
        List<string> list_photo_save = new List<string>();
        List<int> list_photo_click = new List<int>();
        List<int> delete_use = new List<int>();
        int photo_name = 1000;

        VolunteerEntities Myentity = new VolunteerEntities();
        Activity_AddAction_Model AA_Model = new Activity_AddAction_Model(); //實體化Model
        TypeModel TM_Model = new TypeModel();                        //活動類別Model
        GroupModel GM_Model = new GroupModel();                      //活動組別Model

        private WrapPanelModel wrapPanelmodel;                       //產生WrapPanel用
        public WrapPanelModel WrapPanelModel
        {
            get { return wrapPanelmodel; }
            set { wrapPanelmodel = value; }
        }
        public ICommand addcommand { get; set; }                     //附加子項目於WrapPanel用

        public ObservableCollection<TypeModel> TM { get; set; }      //Combobox ItemsSource  用的屬性
        public ObservableCollection<GroupModel> GM { get; set; }
       TypeModel TM_SelectedItem { get; set; }                      //Combobox SelectedItem 用的屬性
       GroupModel GM_SelectedItem { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;   //屬性值變更時產生的事件 繼承介面INotifyPropertyChanged實作的方法

        public void RaisePropertyChanged(string v="" )             //屬性值變更事件判斷
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }
        private bool addact;
        private bool amendact;
        //int supersionID = 2;                            //假設登錄者ID
        //public Activity_AddAction_ViewModel()           //建構子把Model實體化
        //{
        //    whologin(supersionID);
        //    getcombo();
        //}
        //public Activity_AddAction_ViewModel(WrapPanel vm_wrapPanel)           //建構子把Model實體化
        //{
        //    wrapPanelmodel = new WrapPanelModel();
        //    wrapPanelmodel.wrapPanel = vm_wrapPanel;                          //將傳入的WrapPanel，傳給wrapPanelmodel屬性
        //    addcommand = new Delegatecommand(btn_amend_addimage_Click);       //執行附加的方法
        //    whologin(supersionID);
        //    getcombo();
        //}
        public Activity_AddAction_ViewModel(int supersionID, WrapPanel vm_wrapPanel)
        {
            wrapPanelmodel = new WrapPanelModel();
            wrapPanelmodel.wrapPanel = vm_wrapPanel;                          //將傳入的WrapPanel，傳給wrapPanelmodel屬性
            addcommand = new Delegatecommand(btn_amend_addimage_Click);       //執行附加的方法
            whologin(supersionID);//將登錄人員的資料帶入的方法
            getcombo();
            VM_Activity_type_ID = TM[0].Activity_type_ID - 1;
            VM_Activity_type = TM[0].Activity_type;
            VM_Group_no = GM[0].Group_no - 1;
            VM_Group_name = GM[0].Group_name;
            getdefaultimage();
            addact = true;
            amendact = false;
        }
        ////查詢的事件▼
        public Activity_AddAction_ViewModel(string name, DateTime startdate, WrapPanel vm_wrapPanel)
        {
            addact = false;
            amendact = true;
            wrapPanelmodel = new WrapPanelModel();
            wrapPanelmodel.wrapPanel = vm_wrapPanel;                          //將傳入的WrapPanel，傳給wrapPanelmodel屬性
            addcommand = new Delegatecommand(btn_amend_addimage_Click);       //執行附加的方法
            var q = (from find_act in Myentity.Activities
                     where find_act.Activity_name == name && find_act.Activity_startdate == startdate
                     select find_act).Single();
            var q1 = (from find_supersion in Myentity.Volunteer_supervision
                      where find_supersion.supervision_ID == q.Undertaker
                      select find_supersion).Single();
            var q2 = (from find_photo in Myentity.Activity_photo
                      where find_photo.Activity_id == q.Activity_no
                      select find_photo).ToList();
            var q3 = (from t in Myentity.Activity_type
                      where t.Activity_type_ID == q.Activity_type_ID
                      select t).Single();
            var q4 = (from g in Myentity.Service_group
                      where g.Group_no == q.Group_no
                      select g).Single();
            for (int i = 0; i < q2.Count; i += 1)
            {
                if (i == 0)
                {
                    byte[] img = q2[i].Activity_photo1;
                    BitmapImage Bi = new BitmapImage();
                    Bi.BeginInit();
                    Bi.StreamSource = new System.IO.MemoryStream(img);
                    Bi.EndInit();
                    VM_Activity_Photo_id = q2[i].Activity_photo_id;
                    VM_Activity_photos = Bi;
                }
                else
                {
                    //int name_end = (list_photo[i].Length - list_photo[i].LastIndexOf('\\')) - (list_photo[i].Length - list_photo[i].IndexOf('.'));
                    byte[] img = q2[i].Activity_photo1;
                    BitmapImage Bi = new BitmapImage();
                    Bi.BeginInit();
                    Bi.StreamSource = new System.IO.MemoryStream(img);
                    Bi.EndInit();
                    Activity_Addimage_use UserControl = new Activity_Addimage_use();
                    // UserControl.Title = list_photo[i].Substring(list_photo[i].LastIndexOf('\\') + 1, name_end - 1);
                    UserControl.imagesource = Bi;//new BitmapImage(new Uri(list_photo[i]));
                    UserControl.Height = 150;//210
                    UserControl.Width = 200;//200
                    UserControl.Margin = new Thickness(5);
                    WrapPanelModel.wrapPanel.Children.Add(UserControl);
                    UserControl.Name = "a" + q2[i].Activity_photo_id;
                    photo_name += 1;
                    UserControl.click += UserControl_click;
                    UserControl.A_MouseleftbuttonDown += UserControl_A_MouseleftbuttonDown;
                }
            }

            VM_Activity_no = q.Activity_no;
            VM_Activity_name = q.Activity_name;
            VM_lecturer = q.lecturer;
            if (q.Activity_type_ID == 1)
            { VM_Activity_type_ID = 0; }
            else
            { VM_Activity_type_ID = q.Activity_type_ID; }
            if (Convert.ToInt32(q.Group_no) == 1)
            {
                VM_Group_no = 0;
            }
            else
            { VM_Group_no = Convert.ToInt32(q.Group_no); }
            VM_Activity_type = q3.Activity_type1;
            VM_Group_name = q4.Group_name;
            VM_Place = q.Place;
            VM_Activity_startdate = q.Activity_startdate.Value.ToShortDateString();
            VM_Activity_enddate = q.Activity_enddate.Value.ToShortDateString();
            VM_supervision_ID = Convert.ToInt32(q.Undertaker);
            VM_supervision_Name = q1.supervision_Name;
            VM_supervision_phone = int.Parse(q.Undertake_phone);
            VM_supervision_email = q.Undertake_email;
            VM_Member = Convert.ToInt32(q.Member);
            VM_Spare = Convert.ToInt32(q.Spare);
            VM_Summary = q.Summary;
            getcombo();


        }
        //抓活動類型、活動組別資料的程式▼
        void getcombo()
        {
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
        }

        private void whologin(int supersionID)          //將登錄人員的資料帶入
        {
            var q = (from s in Myentity.Volunteer_supervision
                    where s.supervision_ID == supersionID
                    select s).SingleOrDefault();
            VM_supervision_ID = supersionID;
            VM_supervision_Name = q.supervision_Name;
            VM_supervision_phone =Convert.ToInt32( q.supervision_phone);
            VM_supervision_email = q.supervision_email;
        }

        public int VM_Activity_no                       //活動ID
        {
            get { return AA_Model.Activity_no; }
            set
            {
                AA_Model.Activity_no = value;
                RaisePropertyChanged();
            }
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
        public string VM_lecturer
        {
            get { return AA_Model.lecturer; }
            set
            {
                AA_Model.lecturer = value;
                RaisePropertyChanged();
            }
        }
        public bool Addact
        {
            get { return addact; }
            set { addact = value; }
        }   
        public bool Amendact
        {
            get { return amendact; }
            set { amendact = value; }
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
        public int VM_supervision_ID                       //志工督導ID
        { get { return AA_Model.supervision_ID; }
            set
            {
                AA_Model.supervision_ID = value;
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
        public byte[] VM_Activity_photo_byte             //照片、二進制
        {
            get { return AA_Model.Activity_photo; }   
            set
            {
                AA_Model.Activity_photo = value;
                RaisePropertyChanged();
            }
        }
        private BitmapImage im;
        public BitmapImage VM_Activity_photos           //照片、顯示用 
        {
            get { return im; }
            set
            {
                im = value;
                RaisePropertyChanged();
            }
        }
        public string VM_Activity_startdate                //活動起始日期
        { get { return AA_Model.Activity_startdate; }
            set
            {
                AA_Model.Activity_startdate = value;
                RaisePropertyChanged();
            }
        }            
        public string VM_Activity_enddate                 //活動結束日期
        {
            get { return AA_Model.Activity_enddate; }
            set
            {
               AA_Model.Activity_enddate = value;
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
            }   
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
        public string VM_Group_name                         //組別名稱
        {
            get { return GM_Model.Group_name; }
            set
            {
                GM_Model.Group_name = value;
                RaisePropertyChanged();
            }
        }       

        //開啟圖片用事件▼
        public void btn_openImage_Click(object sender, RoutedEventArgs e)
        {//開啟圖片用
            System.Windows.Forms.OpenFileDialog openImage = new System.Windows.Forms.OpenFileDialog();
            openImage.ShowHelp = true;
            openImage.Multiselect = false;
            openImage.Filter = "點陣圖檔案(*.bmp;*.dib)|*.bmp;*.dib|JPEG(*.jpg;*.jpeg;*.jpe;*.jfif)|*.jpg;*.jpeg;*.jpe;*.jfif|GIF(*.gif)|*.gif|TIFF(*.tif;*.tiff)|*.tif;*.tiff|PNG(*.pgn)|*.png|ICO(*.ico)|*.ico|所有檔案(*.*)|*.*";
            if (openImage.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                BitmapImage bi = new BitmapImage(new Uri(openImage.FileName));
                VM_Activity_photos = bi;
            }
        }
        //開啟檔案用事件▼
        public void btn_openFile_Click(object sender, RoutedEventArgs e)
        {//開啟檔案用事件
            System.Windows.Forms.OpenFileDialog openfile = new System.Windows.Forms.OpenFileDialog();
            openfile.ShowHelp = true;
            openfile.Filter = "文字文件(*.txt)|*.txt|所有檔案(*.*)|*.*";
            if (openfile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader SR = new System.IO.StreamReader(openfile.FileName, Encoding.GetEncoding(950));
                VM_Summary = SR.ReadToEnd();
                SR.Dispose();
            }
        }
        //拖入照片時的反應▼
        public void image_DragEnter(object sender, DragEventArgs e)
        {//拖入照片時的反應
            e.Effects = DragDropEffects.Link;
        }
        //拖入照片時▼
        public void image_Drop(object sender, DragEventArgs e)
        {//拖入照片時
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] file =(string[])e.Data.GetData(DataFormats.FileDrop);

                foreach (string s in file)
                {
                  BitmapImage bi = new BitmapImage(new Uri(s));
                  VM_Activity_photos = bi;
                }
                
            }           
        }       

        //活動類型選取時▼
        public void cmb_ActType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {//活動類型選取時
          TypeModel tm =  ((ComboBox)sender).SelectedItem as TypeModel;
            if (tm != null)
            {
                VM_Activity_type_ID = tm.Activity_type_ID;
                VM_Activity_type = tm.Activity_type;
            }            
        }
        //活動組別選取時▼
        public void cmb_ActGroupID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           GroupModel gm =  ((ComboBox)sender).SelectedItem as GroupModel;
            if (gm != null)
            {
                VM_Group_no = gm.Group_no;
                VM_Group_name = gm.Group_name;
            }
        }
        //起始日期選取事件▼
        public void datepicker_start_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
           DatePicker d =   e.Source as DatePicker;
            if (d != null)
            {
                if(d.SelectedDate+"" == string.Empty)
                {
                    ;
                }
            }
            else
            {
                VM_Activity_startdate = ((DatePicker)sender).SelectedDate.Value.ToShortDateString();
            }
        }
        //起始日期選取事件▼
        public void datepicker_end_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker d = e.Source as DatePicker;
            if (d != null)
            {
                if (d.SelectedDate + "" == string.Empty)
                {
                    ;
                }
            }
            else
            {
                VM_Activity_enddate = ((DatePicker)sender).SelectedDate.Value.ToShortDateString();
                if (DateTime.Parse(VM_Activity_enddate) < DateTime.Parse(VM_Activity_startdate))
                {
                    MessageBox.Show("結束日期不可早於起始日期!!");
                }
            }
        }
        //新增活動事件▼
        public void btn_addActivity_Click(object sender, RoutedEventArgs e)
        {//新增活動事件
            if (VM_Activity_name == null || VM_Activity_type == null || VM_Group_name == null)
            {
                System.Windows.Forms.MessageBox.Show("欄位有空格無法新增");
                return;
            }
            else if (VM_Activity_startdate == null)
            {
                System.Windows.Forms.MessageBox.Show("起始日期空白", "新增活動失敗");
                return;
            }
            else if (VM_Activity_enddate == null || Convert.ToDateTime( VM_Activity_enddate)< Convert.ToDateTime(VM_Activity_startdate))
            {
                System.Windows.Forms.MessageBox.Show("結束日期空白或短於起始日期","新增活動失敗");
                return;
            }
            System.IO.MemoryStream MS = new System.IO.MemoryStream(); //將圖轉成二進位
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(VM_Activity_photos));
            encoder.Save(MS);
            VM_Activity_photo_byte = MS.GetBuffer();
            //先新增一筆活動▼新增照片時才有活動編號可用
            if (VM_Activity_type_ID == 0)
            {
                VM_Activity_type_ID = 1;
            }
            if (VM_Group_no == 0)
            {
                VM_Group_no = 1;
            }
            Activity Ac = new Activity() { Activity_name = VM_Activity_name ,lecturer = VM_lecturer,Activity_type_ID = VM_Activity_type_ID, Group_no = VM_Group_no, Undertaker = VM_supervision_ID, Undertake_phone = VM_supervision_phone.ToString(), Undertake_email = VM_supervision_email, Member = VM_Member , Spare = VM_Spare, Place = VM_Place, Summary = VM_Summary, Activity_startdate = DateTime.Parse(VM_Activity_startdate), Activity_enddate = DateTime.Parse(VM_Activity_enddate)};
            Myentity.Activities.Add(Ac);          
            Myentity.SaveChanges();
            //再新增照片進資料庫▼
            var q = (from A in Myentity.Activities
                     where A.Activity_name == VM_Activity_name
                     select new { ActID = A.Activity_no }).Single();
            Activity_photo Ap = new Activity_photo() { Activity_id = q.ActID, Activity_photo1 = VM_Activity_photo_byte };
            Myentity.Activity_photo.Add(Ap);
            Myentity.SaveChanges();
            //再回頭修改剛新增的活動-活動照片ID
            var q1 = (from p in Myentity.Activity_photo
                      where p.Activity_id == q.ActID
                      select new { P_photoID = p.Activity_photo_id, P_activityID = p.Activity_id }).Single();
            var q2 = (from A2 in Myentity.Activities
                      where A2.Activity_no == q1.P_activityID
                      select A2).Single();
            q2.Activity_Photo_id = q1.P_photoID;
            Myentity.SaveChanges();
            if (wrapPanelmodel.wrapPanel != null && WrapPanelModel.wrapPanel.Children.Count > 0)
            {
                for (int i = 0; i < list_photo_save.Count; i += 1)
                {
                    BitmapImage bi = new BitmapImage(new Uri(list_photo_save[i]));
                    System.IO.MemoryStream MS2 = new System.IO.MemoryStream(); //將圖轉成二進位
                    JpegBitmapEncoder encoder2 = new JpegBitmapEncoder();
                    encoder2.Frames.Add(BitmapFrame.Create(bi));
                    encoder2.Save(MS2);
                    VM_Activity_photo_byte = MS2.GetBuffer();
                    Activity_photo ap = new Activity_photo() { Activity_id = q1.P_activityID, Activity_photo1 = VM_Activity_photo_byte };
                    Myentity.Activity_photo.Add(ap);

                }
            }
            Myentity.SaveChanges();
            MessageBox.Show("新增成功");
            System.Windows.Forms.Application.Exit();
        }
        //產生下方圖片的按鈕事件▼
        public void btn_amend_addimage_Click(object sender, RoutedEventArgs e)
        {
            list_photo.Clear();
            System.Windows.Forms.OpenFileDialog openImage = new System.Windows.Forms.OpenFileDialog();
            openImage.ShowHelp = true;
            openImage.Multiselect = true;
            openImage.Filter = "所有檔案(*.*)|*.*|點陣圖檔案(*.bmp;*.dib)|*.bmp;*.dib|JPEG(*.jpg;*.jpeg;*.jpe;*.jfif)|*.jpg;*.jpeg;*.jpe;*.jfif|GIF(*.gif)|*.gif|TIFF(*.tif;*.tiff)|*.tif;*.tiff|PNG(*.pgn)|*.png|ICO(*.ico)|*.ico";
            if (openImage.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                list_photo.AddRange(openImage.FileNames);
                list_photo_save.AddRange(openImage.FileNames);
                getWrapPanel();
            }
        }
        //判斷勾選了哪些照片▼
        private void UserControl_click(object sender, EventArgs e)
        {   int n = int.Parse(((Activity_Addimage_use)sender).Name.Trim('a'));
            bool can_add = true;
            if (list_photo_click.Count == 0)
            {
                list_photo_click.Add(n);
            }
            else
            {
                for (int i = 0; i < list_photo_click.Count; i += 1)
                {
                    if (n == list_photo_click[i])
                    {
                        list_photo_click.Remove(list_photo_click[i]);
                        can_add = false;
                        break;
                    }
                    break;
                }
                if (can_add)
                {
                    list_photo_click.Add(n);
                }
            }
        }
        //刪除勾選的照片▼
        public void btn_amend_delete_Click(object sender, RoutedEventArgs e)
        {
            if (list_photo_click.Count != 0)
            {
                for (int j = 0; j < list_photo_click.Count; j += 1)
                {
                    for (int i = 0; i < WrapPanelModel.wrapPanel.Children.Count; i += 1)
                    {
                        int n = 0;
                        Activity_Addimage_use a = WrapPanelModel.wrapPanel.Children[i] as Activity_Addimage_use;
                        if (a != null)
                        {
                            if (list_photo_click[j] == int.Parse(a.Name.Trim('a')))
                            {
                                n = list_photo_click[j];
                                WrapPanelModel.wrapPanel.Children.Remove(WrapPanelModel.wrapPanel.Children[i]);
                                if (a.Tag != null)
                                {
                                    list_photo.Remove(a.Tag.ToString());
                                    //list_photo_save.Remove(a.Tag.ToString());
                                }
                                
                                delete_use.Add(list_photo_click[j]);                                               
                                var q = (from p in Myentity.Activity_photo
                                         where p.Activity_photo_id == n
                                         select p).FirstOrDefault();
                                if (q != null)
                                {
                                    Myentity.Activity_photo.Remove(q);
                                   // Myentity.SaveChanges();
                                }
                            }
                        }
                    }
                }
                for (int i = 0; i < delete_use.Count; i += 1)
                {
                    for (int j = 0; j < list_photo_click.Count; j += 1)
                    {
                        if (list_photo_click[j] == delete_use[i])
                        {
                            list_photo_click.Remove(list_photo_click[j]);
                            break;
                        }
                    }
                }
            }            
            else
            {
                return;
            }
            list_photo_click.Clear();
        }
        //WrapPanel拖入照片時的反應▼
        public void WrapPanel_Viewphoto_DragEnter(object sender, DragEventArgs e)
        {            
            e.Effects = DragDropEffects.Link;
        }
        //WrapPanel拖入照片時▼
        public void WrapPanel_Viewphoto_Drop(object sender, DragEventArgs e)
        {
            list_photo.Clear();
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                list_photo.AddRange((string[])e.Data.GetData(DataFormats.FileDrop));
                list_photo_save.AddRange((string[])e.Data.GetData(DataFormats.FileDrop));
            }
            getWrapPanel();           
        }
        //按下修改時
        public void btn_amendact_Click(object sender, RoutedEventArgs e)
        {
            if (VM_Activity_name == null || VM_Activity_type == null || VM_Group_name == null)
            {
                System.Windows.Forms.MessageBox.Show("欄位有空格無法新增");
                return;
            }
            else if (VM_Activity_startdate == null)
            {
                System.Windows.Forms.MessageBox.Show("起始日期空白", "新增活動失敗");
                return;
            }
            else if (VM_Activity_enddate == null || Convert.ToDateTime(VM_Activity_enddate) < Convert.ToDateTime(VM_Activity_startdate))
            {
                System.Windows.Forms.MessageBox.Show("結束日期空白或短於起始日期", "新增活動失敗");
                return;
            }
            System.IO.MemoryStream MS = new System.IO.MemoryStream(); //將圖轉成二進位
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(VM_Activity_photos));
            encoder.Save(MS);
            VM_Activity_photo_byte = MS.GetBuffer();
            var q = (from o in Myentity.Activities
                     where o.Activity_no == VM_Activity_no
                     select o).Single();
            var q2 = (from p in Myentity.Activity_photo
                      where p.Activity_photo_id == VM_Activity_Photo_id
                      select p).Single();
            if (VM_Activity_type_ID == 0)
            {
                VM_Activity_type_ID = 1;
            }
            if (VM_Group_no == 0)
            {
                VM_Group_no = 1;
            }
            q.Activity_name = VM_Activity_name;
            q.lecturer = VM_lecturer;
            q.Activity_type_ID = VM_Activity_type_ID;
            q.Group_no = VM_Group_no;
            q.Place = VM_Place;
            q.Activity_startdate = DateTime.Parse(VM_Activity_startdate);
            q.Activity_enddate = DateTime.Parse(VM_Activity_enddate);
            q.Undertaker = VM_supervision_ID;
            q.Undertake_email = VM_supervision_email;
            q.Undertake_phone = VM_supervision_phone.ToString();
            q.Member = VM_Member;
            q.Spare = VM_Spare;
            q.Summary = VM_Summary;
            q.Activity_Photo_id = VM_Activity_Photo_id;
            q2.Activity_photo1 = VM_Activity_photo_byte;
            if (wrapPanelmodel.wrapPanel != null && WrapPanelModel.wrapPanel.Children.Count > 0)
            {
                for (int i = 0; i < list_photo_save.Count; i += 1)
                {
                    BitmapImage bi = new BitmapImage(new Uri(list_photo_save[i]));
                    System.IO.MemoryStream MS2 = new System.IO.MemoryStream(); //將圖轉成二進位
                        JpegBitmapEncoder encoder2 = new JpegBitmapEncoder();
                        encoder2.Frames.Add(BitmapFrame.Create(bi));
                        encoder2.Save(MS2);
                        VM_Activity_photo_byte = MS2.GetBuffer();
                        Activity_photo ap = new Activity_photo() { Activity_id = VM_Activity_no, Activity_photo1 = VM_Activity_photo_byte };                        
                        Myentity.Activity_photo.Add(ap);
                    
                }
            }
            Myentity.SaveChanges();
            MessageBox.Show("修改完成");
        }
        //初始的預設圖片▼
       public void getdefaultimage()
        {
            string path_default = System.IO.Path.GetFullPath("../../image/Noimage.png");
            BitmapImage bi = new BitmapImage(new Uri(path_default,UriKind.RelativeOrAbsolute));
            VM_Activity_photos = bi;
        }
        //驗證判斷▼
        public string Error
        {
            get
            {
                return "";
            }
        }
        public string this[string columnName]
        {
            get
            {
                if (columnName == "VM_Activity_name")
                {
                    if (VM_Activity_name == null || VM_Activity_name == string.Empty)
                    {
                        return "此為必填項目!!";
                    }
                }
                if (columnName == "VM_Activity_startdate")
                {
                    if (VM_Activity_startdate == null)
                    {
                        return "此為必填項目!!";
                    }
                }
                if (columnName == "VM_Activity_enddate")
                { string s = "";
                    if (VM_Activity_enddate == null)
                    {
                        s = "此為必填項目!!";

                    }
                    else
                    {
                        DateTime start;
                        DateTime end;
                        if (DateTime.TryParse(VM_Activity_enddate,out end) &&  DateTime.TryParse(VM_Activity_startdate, out start))
                        {
                            if (end < start)
                            {
                                s = "結束日期不能早於起始日期!!";
                            }                          
                        }                     
                    }
                    return s;
                }
                return string.Empty;
            }
        }
        //產生自訂控制項▼
        public void getWrapPanel()
        {
            for (int i = 0; i < list_photo.Count; i += 1)
            {
                int name_end = (list_photo[i].Length - list_photo[i].LastIndexOf('\\')) - (list_photo[i].Length - list_photo[i].IndexOf('.'));
                Activity_Addimage_use UserControl = new Activity_Addimage_use();
                UserControl.Title = list_photo[i].Substring(list_photo[i].LastIndexOf('\\') + 1, name_end - 1);
                UserControl.imagesource = new BitmapImage(new Uri(list_photo[i]));
                UserControl.Height = 150;//210
                UserControl.Width = 200;//200
                UserControl.Margin = new Thickness(5);
                WrapPanelModel.wrapPanel.Children.Add(UserControl);
                UserControl.Name = "a" + (photo_name + 1);
                UserControl.Tag = list_photo[i];
                photo_name += 1;
                UserControl.click += UserControl_click;
                UserControl.A_MouseleftbuttonDown += UserControl_A_MouseleftbuttonDown; //滑鼠左鍵按下時
            }
        }
        //自訂控制項被點選後▼
        private void UserControl_A_MouseleftbuttonDown(object sender, MouseButtonEventArgs e)
        {
            Activity_Addimage_use myaim = ((Activity_Addimage_use)sender);
            Viewbox v = new Viewbox();
            StackPanel sp = new StackPanel();
            Window w = new Window();
            TextBox TB = new TextBox();
            Button btn = new Button();
            Viewbox vb = new Viewbox();

            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.Height = 25;
            btn.Width = 100;
            btn.Content = "關閉";           

            w.Width = 400;
            w.Height = 500;
            w.Title = "顯示照片";                        
            
            TB.Height = 25;
            TB.FontSize = 16;
            TB.TextAlignment = TextAlignment.Center;
            TB.Text = myaim.Title;

            Image i = new Image();
            i.Source = myaim.imagesource;
            i.Height = 400;
            i.Stretch = System.Windows.Media.Stretch.Fill;

            sp.Orientation = Orientation.Vertical;
            sp.Children.Add(TB);
            sp.Children.Add(i);
            sp.Children.Add(btn);
            TB.TextChanged += delegate (object sender1, TextChangedEventArgs e1)
            {
                myaim.Title = ((TextBox)sender1).Text;
            };
            vb.Stretch = System.Windows.Media.Stretch.Fill;
            vb.Child = sp;
            w.Content = vb;
            w.Show();
            btn.Click += delegate (object sender1, RoutedEventArgs e1)
            {
                w.Close();
            };
        }
    }
    //執行附加wrapPanel子項目的方法▼
    internal class Delegatecommand : ICommand
    {
        private Action<object, RoutedEventArgs> btn_amend_addimage_Click;

        public Delegatecommand(Action<object, RoutedEventArgs> btn_amend_addimage_Click)
        {
            this.btn_amend_addimage_Click = btn_amend_addimage_Click;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            btn_amend_addimage_Click(parameter,null);
        }
    }   
}
