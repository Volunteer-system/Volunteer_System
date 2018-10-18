using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteer_WPF.Model
{
   public class Activity_AddAction_Model
    {  //活動設定檔(Basic.Activity)
        public string Activity_name { get; set; }                 //活動名稱
        public string Undertake_unit { get; set; }                //承辦單位
        public int Undertaker { get; set; }                       //承辦人 int?
        public string Undertake_phone { get; set; }               //承辦單位電話
        public string Undertake_email { get; set; }               //承辦單位e-mail
        public int Member { get; set; }                           //課程人數
        public int Spare { get; set; }                            //備取人數
        public string Place { get; set; }                         //活動地點
        public string Summary { get; set; }                       //活動簡介(連接文字描述檔)?
        public int Activity_Photo_id { get; set; }                //活動照片ID -> 活動照片檔(Basic.Activity_photo)

        //志工督導 [supervision].[Volunteer_supervision]
        //public int supervision_ID { get; set; }                   //志工督導ID
        public string supervision_Name { get; set; }              //志工督導姓名
        public int supervision_phone { get; set; }                //志工督導電話
        public string supervision_address { get; set; }           //志工督導地址
        public string supervision_email { get; set; }             //志工督導Email

        //活動照片檔(Basic.Activity_photo)
        // public int Activity_Photo_id { get; set; }     //活動照片ID
        public int Activity_id { get; set; }              //活動ID -> 活動設定檔(Basic.Activity) Activity_no
        public byte[] Activity_photo { get; set; }                //照片
        //public BitmapImage VM_Activity_photos                  //照片   
        //{
        //    get;
        //    set;
        //}

        ////服務組別基本檔(Basic.Service_group)
        //public int Group_leader { get; set; }                     //組長
        //public int Group_viceleader { get; set; }                 //副組長        
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
}
