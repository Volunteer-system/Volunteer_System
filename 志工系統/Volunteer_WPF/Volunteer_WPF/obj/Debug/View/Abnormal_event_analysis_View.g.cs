﻿#pragma checksum "..\..\..\View\Abnormal_event_analysis_View.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9A8866EEB5D6861D6C6AD151BC2BC669EDF2A12E"
//------------------------------------------------------------------------------
// <auto-generated>
//     這段程式碼是由工具產生的。
//     執行階段版本:4.0.30319.42000
//
//     對這個檔案所做的變更可能會造成錯誤的行為，而且如果重新產生程式碼，
//     變更將會遺失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using Volunteer_WPF.View;


namespace Volunteer_WPF.View {
    
    
    /// <summary>
    /// Abnormal_event_analysis_View
    /// </summary>
    public partial class Abnormal_event_analysis_View : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\..\View\Abnormal_event_analysis_View.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DockPanel Dock_Frame;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\View\Abnormal_event_analysis_View.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_application_unit;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\View\Abnormal_event_analysis_View.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txt_unit;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\View\Abnormal_event_analysis_View.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbb_category;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\View\Abnormal_event_analysis_View.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dp_startdate;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\View\Abnormal_event_analysis_View.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dp_enddate;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\View\Abnormal_event_analysis_View.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_selectabnormal_event;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\View\Abnormal_event_analysis_View.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_eventlist_excel;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\View\Abnormal_event_analysis_View.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel wondow_show;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\View\Abnormal_event_analysis_View.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dg_abnormal_event;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Volunteer_WPF;component/view/abnormal_event_analysis_view.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\Abnormal_event_analysis_View.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Dock_Frame = ((System.Windows.Controls.DockPanel)(target));
            return;
            case 2:
            this.btn_application_unit = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\..\View\Abnormal_event_analysis_View.xaml"
            this.btn_application_unit.Click += new System.Windows.RoutedEventHandler(this.btn_application_unit_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.txt_unit = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.cbb_category = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.dp_startdate = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 6:
            this.dp_enddate = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 7:
            this.btn_selectabnormal_event = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\..\View\Abnormal_event_analysis_View.xaml"
            this.btn_selectabnormal_event.Click += new System.Windows.RoutedEventHandler(this.btn_selectabnormal_event_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btn_eventlist_excel = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\..\View\Abnormal_event_analysis_View.xaml"
            this.btn_eventlist_excel.Click += new System.Windows.RoutedEventHandler(this.btn_eventlist_excel_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.wondow_show = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 10:
            this.dg_abnormal_event = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

