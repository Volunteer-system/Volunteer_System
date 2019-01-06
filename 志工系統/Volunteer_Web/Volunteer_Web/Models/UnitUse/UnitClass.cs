using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Volunteer_Web.ViewModel;

namespace Volunteer_Web.Models.UnitUse
{
    public class UnitClass
    {
       //這個方法用來回傳Dictionary key為
       public static Dictionary<string,List<string>>GetPeriodNames(int application_unit_no)
        {
            int lastyear = DateTime.Now.AddYears(-1).Year;
            Dictionary<string, List<string>> QueryDict = new Dictionary<string, List<string>>();
            

            List<string> QueryStr = new List<string>(); //存放班別(中文)
            //List<string> QueryNames = new List<string>(); //存放各班別的所有人名

            VolunteerEntities db = new VolunteerEntities();
            var result = from ss in db.Shift_schedule
                    join sp1 in db.Service_period1 on ss.Service_period_no equals sp1.Service_period_no
                    join v in db.Volunteers on ss.Volunteer_no equals v.Volunteer_no
                    where ss.Application_unit_no == application_unit_no && ss.year == lastyear
                    select new Unit_Schedule { Schedule = ss, volunteer = v, period1 = sp1 };
            
            foreach (var item in result.ToList())
            {                
                string periodCategoryCn = item.period1.Service_period;
                QueryStr.Add(periodCategoryCn);
            }//加好資料庫的的紀錄
            QueryStr = QueryStr.Distinct().ToList();//讓他不重複
            
            foreach (var item in QueryStr)
            {
                var names= result.Where(s => s.period1.Service_period == item).Select(s=>s.volunteer.Chinese_name).ToList();
                QueryDict.Add(item,names);//資料庫裡面的個班別的各個名字
                
            }

            //建立一個完整的Dionary用來存放周一~周日日間 周一~週日晚間
            Dictionary<string, List<string>> completePeriod = new Dictionary<string,List<string>>();
            List<string> strL = new List<string>();
            strL.Add("");
            completePeriod.Add("週一上午", strL);
            completePeriod.Add("週二上午", strL);
            completePeriod.Add("週三上午", strL);
            completePeriod.Add("週四上午", strL);
            completePeriod.Add("週五上午", strL);
            completePeriod.Add("週六上午", strL);
            completePeriod.Add("週日上午", strL);
            completePeriod.Add("週一下午", strL);
            completePeriod.Add("週二下午", strL);
            completePeriod.Add("週三下午", strL);
            completePeriod.Add("週四下午", strL);
            completePeriod.Add("週五下午", strL);
            completePeriod.Add("週六下午", strL);
            completePeriod.Add("週日下午", strL);
            completePeriod.Add("週一夜間", strL);
            completePeriod.Add("週二夜間", strL);
            completePeriod.Add("週三夜間", strL);
            completePeriod.Add("週四夜間", strL);
            completePeriod.Add("週五夜間", strL);
            completePeriod.Add("週六夜間", strL);
            completePeriod.Add("週日夜間", strL);
            completePeriod.Add("週一支援", strL);
            completePeriod.Add("週二支援", strL);
            completePeriod.Add("週三支援", strL);
            completePeriod.Add("週四支援", strL);
            completePeriod.Add("週五支援", strL);
            completePeriod.Add("週六支援", strL);
            completePeriod.Add("週日支援", strL);//字典到這邊為止建好了
            List<string> temp = new List<string> { "週一上午", "週二上午", "週三上午", "週四上午", "週五上午", "週六上午", "週日上午", "週一下午", "週二下午", "週三下午", "週四下午", "週五下午", "週六下午", "週日下午", "週一夜間", "週二夜間", "週三夜間", "週四夜間", "週五夜間", "週六夜間", "週日夜間", "週一支援", "週二支援", "週三支援", "週四支援", "週五支援", "週六支援", "週日支援" };

            for (int i = 0; i < temp.Count; i++)//長度根據temp
            {
                foreach (var item in QueryDict)
                {
                    if (item.Key==temp[i])
                    {
                        completePeriod[item.Key] = QueryDict[item.Key];
                    }                   
                }
            }
           

            return completePeriod;
        }


    }
}