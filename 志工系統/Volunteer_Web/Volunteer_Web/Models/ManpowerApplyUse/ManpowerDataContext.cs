using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Volunteer_Web.Models
{
    public class ManpowerDataContext
    {
        //用來轉型cookie的方法 而且會把資料存進DB
        public static void  ConvertAndInsert(int applyID,string morning,string afternoon,string night,string assistance)
        {
            VolunteerEntities db = new VolunteerEntities();
            //用迴圈把資料讀到
            List<string> mo = new List<string>();
            List<string> af = new List<string>();
            List<string> ni = new List<string>();
            List<string> ast = new List<string>();
            mo = morning.Split(',').ToList();            
            af = afternoon.Split(',').ToList();
            ni = night.Split(',').ToList();
            ast = assistance.Split(',').ToList();//字串轉成字串集合

            List<int> Morning = new List<int>();
            List<int> Afternoon = new List<int>();
            List<int> Night = new List<int>();
            List<int> Assistance = new List<int>();

            foreach (var item in mo)
            {
                Morning.Add(Convert.ToInt16(item)); //字串集合變成int集合
            }
            foreach (var item in af)
            {
                Afternoon.Add(Convert.ToInt16(item));
            }
            foreach (var item in ni)
            {
                Night.Add(Convert.ToInt16(item));
            }
            foreach (var item in ast)
            {
                Assistance.Add(Convert.ToInt16(item));
            }

            for (int i=0; i<Morning.Count;i++) //迴圈大小為Morning集合的大小
            {
                if (Morning[i]>0) //有分配人數才存進資料庫
                {
                    Apply_Service_period apply_Service_Period = new Apply_Service_period(); //宣告一個Apply_Service_period物件
                    apply_Service_Period.Apply_ID = applyID;
                    apply_Service_Period.Volunteer_number = Morning[i];


                    string PeriodCategory = ""; //這個字串是用來記錄班別的中文字
                    switch (i) //目的是得到班別的字串
                    {
                        case 0:
                            PeriodCategory = "週一上午";
                            break;
                        case 1:
                            PeriodCategory = "週二上午";
                            break;
                        case 2:
                            PeriodCategory = "週三上午";
                            break;
                        case 3:
                            PeriodCategory = "週四上午";
                            break;
                        case 4:
                            PeriodCategory = "週五上午";
                            break;
                        case 5:
                            PeriodCategory = "週六上午";
                            break;
                        case 6:
                            PeriodCategory = "週日上午";
                            break;
                    }
                    
                    int service_period_no = db.Service_period1.Where(s => s.Service_period == PeriodCategory).First().Service_period_no;//根據中文班別找到service_period_no
                    apply_Service_Period.Service_period_no = service_period_no;
                    db.Apply_Service_period.Add(apply_Service_Period); //加入apply_Service_Period
                }

            }
            for (int i = 0; i < Afternoon.Count; i++)
            {
                if (Afternoon[i] > 0)
                {
                    Apply_Service_period apply_Service_Period = new Apply_Service_period();
                    apply_Service_Period.Apply_ID = applyID;
                    apply_Service_Period.Volunteer_number = Afternoon[i];


                    string PeriodCategory = "";
                    switch (i) //目的是得到班別的字串
                    {
                        case 0:
                            PeriodCategory = "週一下午";
                            break;
                        case 1:
                            PeriodCategory = "週二下午";
                            break;
                        case 2:
                            PeriodCategory = "週三下午";
                            break;
                        case 3:
                            PeriodCategory = "週四下午";
                            break;
                        case 4:
                            PeriodCategory = "週五下午";
                            break;
                        case 5:
                            PeriodCategory = "週六下午";
                            break;
                        case 6:
                            PeriodCategory = "週日下午";
                            break;
                    }

                    int service_period_no = db.Service_period1.Where(s => s.Service_period == PeriodCategory).First().Service_period_no;//根據中文班別找到service_period_no
                    apply_Service_Period.Service_period_no = service_period_no;
                    db.Apply_Service_period.Add(apply_Service_Period);
                }

            }
            for (int i = 0; i < Night.Count; i++)
            {
                if (Night[i] > 0)
                {
                    Apply_Service_period apply_Service_Period = new Apply_Service_period();
                    apply_Service_Period.Apply_ID = applyID;
                    apply_Service_Period.Volunteer_number = Night[i];


                    string PeriodCategory = "";
                    switch (i) //目的是得到班別的字串
                    {
                        case 0:
                            PeriodCategory = "週一夜間";
                            break;
                        case 1:
                            PeriodCategory = "週二夜間";
                            break;
                        case 2:
                            PeriodCategory = "週三夜間";
                            break;
                        case 3:
                            PeriodCategory = "週四夜間";
                            break;
                        case 4:
                            PeriodCategory = "週五夜間";
                            break;                        
                    }

                    int service_period_no = db.Service_period1.Where(s => s.Service_period == PeriodCategory).First().Service_period_no;//根據中文班別找到service_period_no
                    apply_Service_Period.Service_period_no = service_period_no;
                    db.Apply_Service_period.Add(apply_Service_Period);
                }

            }
            for (int i = 0; i < Assistance.Count; i++)
            {
                if (Assistance[i] > 0)
                {
                    Apply_Service_period apply_Service_Period = new Apply_Service_period();
                    apply_Service_Period.Apply_ID = applyID;
                    apply_Service_Period.Volunteer_number = Assistance[i];

                    string PeriodCategory = "";
                    switch (i) //目的是得到班別的字串
                    {
                        case 0:
                            PeriodCategory = "週一支援";
                            break;
                        case 1:
                            PeriodCategory = "週二支援";
                            break;
                        case 2:
                            PeriodCategory = "週三支援";
                            break;
                        case 3:
                            PeriodCategory = "週四支援";
                            break;
                        case 4:
                            PeriodCategory = "週五支援";
                            break;
                    }

                    int service_period_no = db.Service_period1.Where(s => s.Service_period == PeriodCategory).First().Service_period_no;//根據中文班別找到service_period_no
                    apply_Service_Period.Service_period_no = service_period_no;
                    db.Apply_Service_period.Add(apply_Service_Period);
                }
            }

            db.SaveChanges(); //更新資料庫
        }

        //這個方法用來回傳資料庫的班表
        public static Dictionary<string,int> ReturnDictionary(int apply_ID)
        {
            List<string> QueryStr = new List<string>(); //存放班別(中文)
            List<int> QueryNum = new List<int>(); //存各放班別人數


            VolunteerEntities db = new VolunteerEntities();
            var result= db.Apply_Service_period.Where(a => a.Apply_ID == apply_ID).ToList();
            
            foreach (var item in result)
            {
                string periodCategoryCn = item.Service_period1.Service_period;
                QueryStr.Add(periodCategoryCn);
                int periodCategoryNum = item.Volunteer_number.Value;
                QueryNum.Add(periodCategoryNum);
            }//加好資料

            //建立一個完整的Dionary用來存放周一~周日日間 周一~週日晚間
            Dictionary<string, int> completePeriod = new Dictionary<string, int>();
            completePeriod.Add("週一上午", 0);
            completePeriod.Add("週二上午", 0);
            completePeriod.Add("週三上午", 0);
            completePeriod.Add("週四上午", 0);
            completePeriod.Add("週五上午", 0);
            completePeriod.Add("週六上午", 0);
            completePeriod.Add("週日上午", 0);
            completePeriod.Add("週一下午", 0);
            completePeriod.Add("週二下午", 0);
            completePeriod.Add("週三下午", 0);
            completePeriod.Add("週四下午", 0);
            completePeriod.Add("週五下午", 0);
            completePeriod.Add("週六下午", 0);
            completePeriod.Add("週日下午", 0);
            completePeriod.Add("週一夜間", 0);
            completePeriod.Add("週二夜間", 0);
            completePeriod.Add("週三夜間", 0);
            completePeriod.Add("週四夜間", 0);
            completePeriod.Add("週五夜間", 0);
            completePeriod.Add("週一支援", 0);
            completePeriod.Add("週二支援", 0);
            completePeriod.Add("週三支援", 0);
            completePeriod.Add("週四支援", 0);
            completePeriod.Add("週五支援", 0);//字典到這邊為止建好了
            List<string> temp = new List<string> {"週一上午", "週二上午", "週三上午", "週四上午", "週五上午", "週六上午", "週日上午", "週一下午", "週二下午", "週三下午", "週四下午", "週五下午", "週六下午", "週日下午", "週一夜間", "週二夜間", "週三夜間", "週四夜間", "週五夜間", "週一支援", "週二支援", "週三支援", "週四支援", "週五支援" };

            for (int i = 0; i < temp.Count; i++)//長度根據temp
            {
                for (int j = 0; j < QueryStr.Count; j++)
                {
                    if (QueryStr[j]==temp[i])//跟temp內容比對
                    {
                        completePeriod[temp[i]] = QueryNum[j];//換掉字典的value (因為預設是0)
                        break;
                    }
                }
            }
            return completePeriod;
        }
        
        //這個方法用來修改
        public static void ModifiedPeriod(int applyId, string morning, string afternoon, string night, string assistance,int totalPeople)
        {
            VolunteerEntities db = new VolunteerEntities();
            List<Apply_Service_period> apply_Service_Periods = db.Apply_Service_period.Where(a => a.Apply_ID == applyId).ToList();
            foreach (var item in apply_Service_Periods)
            {
                db.Apply_Service_period.Remove(item);

            }//傳applyId近來先全部刪除 再全部新增

            Manpower_apply apply_Service_Period = db.Manpower_apply.Find(applyId);
            ////這邊寫一個foreach計算該applyId的人數
            //int totalPeople = 0;
            //List<int>temp= db.Apply_Service_period.Where(a => a.Apply_ID == applyId).Select(a => a.Volunteer_number.Value).ToList();
            //foreach (var item in temp)
            //{
            //    totalPeople += item;
            //}
            apply_Service_Period.Application_number = totalPeople;
            db.Entry(apply_Service_Period).State = System.Data.Entity.EntityState.Modified;
            
            db.SaveChanges();
            ConvertAndInsert(applyId, morning, afternoon, night, assistance);

            

        }
        
    }
}