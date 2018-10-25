using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Volunteer_WPF.Model
{
   public class Activity_AddAction_Model
    {  //活動設定檔(Basic.Activity)
        public int Activity_no { get; set; }                      //活動ID
        public string Activity_name { get; set; }                 //活動名稱       
        public string lecturer { get; set; }                      //講師
        public int Member { get; set; }                           //課程人數
        public int Spare { get; set; }                            //備取人數
        public string Place { get; set; }                         //活動地點
        public string Summary { get; set; }                       //活動簡介(連接文字描述檔)?     
        public string Activity_startdate { get; set; }            //活動起始日期
        public string Activity_enddate { get; set; }              //活動結束日期

        //志工督導 [supervision].[Volunteer_supervision]
        public int supervision_ID { get; set; }                   //志工督導ID
        public string supervision_Name { get; set; }              //志工督導姓名
        public int supervision_phone { get; set; }                //志工督導電話
        public string supervision_address { get; set; }           //志工督導地址
        public string supervision_email { get; set; }             //志工督導Email

        //活動照片檔(Basic.Activity_photo)
        public int Activity_Photo_id { get; set; }                //活動照片ID      
        public byte[] Activity_photo { get; set; }                //照片      
    }
    public class TypeModel   //Combobox活動類別用           //活動類別(Basic.Activity_type)
    {
        public int Activity_type_ID { get; set; }           //活動類別ID
        public string Activity_type { get; set; }           //活動類別
    }
    public class GroupModel //Combobox活動組別用
    {
        public int Group_no { get; set; }                   //組別
        public string Group_name { get; set; }              //組別名稱
    }
    public class WrapPanelModel                             //WrapPanel_Model
    {
        private WrapPanel _wrappanel;
        public WrapPanel wrapPanel
        {
            get { return _wrappanel; }
            set {  _wrappanel = value; }
        }
    }
}
