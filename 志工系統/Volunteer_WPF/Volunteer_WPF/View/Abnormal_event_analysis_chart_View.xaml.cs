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

            
            Dictionary<int, int>  value = new Dictionary<int, int>();
            for (int i = 0; i < 10; i++)
                value.Add(i, 10 + i);

            Chart chart1 = this.FindName("Chart1") as Chart;
            //chart1.DataSource = Event_Analysis.Compute_Eventtypes;
            for (int i = 0; i < 10; i++)
            {
                chart1.Series[0].Points.AddXY(i, i + 10);
            }                
            chart1.Series["series"].XValueMember = "類別";
            chart1.Series["series"].YValueMembers = "count";

            //Chart chart2 = this.FindName("Chart2") as Chart;
            //chart2.DataSource = Event_Analysis.Compute_Applicationunits;
            //chart2.Series["series"].XValueMember = "運用單位";
            //chart2.Series["series"].YValueMembers = "count";

            //Chart chart3 = this.FindName("Chart3") as Chart;
            //chart3.DataSource = Event_Analysis.Compute_Eventmonths;
            //chart3.Series["series"].XValueMember = "月份";
            //chart3.Series["series"].YValueMembers = "count";
        }

    }
}
