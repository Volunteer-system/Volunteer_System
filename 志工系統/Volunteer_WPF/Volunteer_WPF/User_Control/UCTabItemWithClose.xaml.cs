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

namespace TabControlWithClose
{

    public partial class UCTabItemWithClose : TabItem
    {
        public UCTabItemWithClose()
        {
            InitializeComponent();

        }

        public event EventHandler CheckClose; //關閉事件,可以讓外面使用
        private TabControl m_Parent; //父級TabControl
        private double m_ConventionWidth = 120; //約定寬度

        #region 事件        
        private void TabItem_Loaded(object sender, RoutedEventArgs e)
        {
            m_Parent = FindParentTabControl(this);   //找TabControl
            if (m_Parent != null)
            {
                SizeLoad();
            }
        }

        #region 關閉按鈕 
        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            if (m_Parent == null)
            {
                return;
            }

            if (CheckClose != null) //防止無事件時當掉
            {
                CheckClose.Invoke(this, e);
            }

            m_Parent.Items.Remove(this); //移除自己TabItem
            m_Parent.SizeChanged -= m_Parent_SizeChanged; //移除事件

            int criticalCount = (int)((m_Parent.ActualWidth - 5) / m_ConventionWidth); //調整剩餘項大小,保持约定寬度item的臨界個數           
            double perWidth = (m_Parent.ActualWidth - 5) / m_Parent.Items.Count;  //平均寬度
            foreach (UCTabItemWithClose item in m_Parent.Items)
            {
                if (m_Parent.Items.Count <= criticalCount)
                {
                    item.Width = m_ConventionWidth;
                }
                else
                {
                    item.Width = perWidth;
                }
            }
        }
        #endregion


        #region TabControl Size發生變化

        private void m_Parent_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            int criticalCount = (int)((m_Parent.ActualWidth - 5) / m_ConventionWidth); //調整Tabitem大小,保持約定寬度item的臨界個數
            if (m_Parent.Items.Count <= criticalCount)
            {
                this.Width = m_ConventionWidth; //小於等於臨界個數時 等於約定寬度
            }
            else
            {
                double perWidth = (m_Parent.ActualWidth - 5) / m_Parent.Items.Count; //大於臨界個數時 等於平均寬度
                this.Width = perWidth;
            }
        }
        #endregion
        #endregion

        #region 方法

        private void SizeLoad()
        {
            // double.TryParse(m_Parent.Tag.ToString(), out m_ConventionWidth);    //約定寬度轉成double     
            m_Parent.SizeChanged += m_Parent_SizeChanged; //註冊TabControl Size發生變化事件                      
            int criticalCount = (int)((m_Parent.ActualWidth - 5) / m_ConventionWidth); //保持约定宽度item的臨界個數

            if (m_Parent.Items.Count <= criticalCount)
            {
                this.Width = m_ConventionWidth; //小於等於臨界個數 等於約定寬度
            }
            else
            {
                double perWidth = (m_Parent.ActualWidth - 5) / m_Parent.Items.Count;//大於臨界個數 每項都設成平均寬度
                foreach (UCTabItemWithClose item in m_Parent.Items)
                {
                    item.Width = perWidth;
                }
                //this.Width = perWidth;
            }
        }

        #region 找TabControl

        private TabControl FindParentTabControl(DependencyObject reference) // 找TabControl
        {
            DependencyObject Depend = VisualTreeHelper.GetParent(reference);

            if (Depend == null)
            {
                return null;
            }
            if (Depend.GetType() == typeof(TabControl))
            {
                return Depend as TabControl;
            }
            else
                return FindParentTabControl(Depend);
        }
        #endregion
        #endregion
    }
}
