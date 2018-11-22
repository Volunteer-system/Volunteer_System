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
using System.Windows.Shapes;
using Volunteer_WPF.View_Model;
using System.Windows.Forms.DataVisualization.Charting;


namespace Volunteer_WPF.View
{
    /// <summary>
    /// Abnormal_event_analysis_chart_View.xaml 的互動邏輯
    /// </summary>
    public partial class Abnormal_event_analysis_chart_View : Window
    {
        public Abnormal_event_analysis_chart_View(object abnormal_Event_Analysis_ViewModel)
        {
            InitializeComponent();
            Abnormal_event_analysis_ViewModel Event_Analysis = abnormal_Event_Analysis_ViewModel as Abnormal_event_analysis_ViewModel;

            Chart chart1 = this.FindName("Chart1") as Chart;
            chart1.Titles.Add("月份數據分析");
            //chart1.DataSource = Event_Analysis.Compute_Eventtypes;
            List<string> xValues1 = new List<string>();
            List<int> yValues1 = new List<int>();
            foreach (var row in Event_Analysis.Compute_Eventmonths)
            {
                //chart1.Series[0].Points.AddXY(row.Month,row.event_count);
                xValues1.Add(row.Month.ToString());
                yValues1.Add(row.event_count);
            }
            chart1.Series[0].Points.DataBindXY(xValues1, yValues1);
            chart1.Series[0].Label = "#VAL";
            chart1.ChartAreas[0].AxisX.Title = "月份";
            chart1.ChartAreas[0].AxisY.Title = "數量";

            Chart chart2 = this.FindName("Chart2") as Chart;
            chart2.Titles.Add("運用單位數據分析");
            //chart2.DataSource = Event_Analysis.Compute_Applicationunits;
            List<string> xValues2 = new List<string>();
            List<int> yValues2 = new List<int>();
            foreach (var row in Event_Analysis.Compute_Applicationunits)
            {
                xValues2.Add(row.Application_unit);
                yValues2.Add(row.event_count);
            }
            chart2.Series[0].Points.DataBindXY(xValues2, yValues2);
            chart2.Series[0]["PieLabelStyle"] = "Inside";
            chart2.Series[0].Label = "#VAL";
            //chart2.Legends

            Chart chart3 = this.FindName("Chart3") as Chart;
            chart3.Titles.Add("事件類別數據分析");
            //chart3.DataSource = Event_Analysis.Compute_Eventtypes;
            chart3.ChartAreas[0].AxisX.Title = "類別";
            chart3.ChartAreas[0].AxisY.Title = "數量";
            chart3.Series[0].Label = "#VAL";
            List<string> xValues3 = new List<string>();
            List<int> yValues3 = new List<int>();
            foreach (var row in Event_Analysis.Compute_Eventtypes)
            {
                xValues3.Add(row.event_Category);
                yValues3.Add(row.event_count);
            }
            chart3.Series[0].Points.DataBindXY(xValues3, yValues3);

        }

    }
}
