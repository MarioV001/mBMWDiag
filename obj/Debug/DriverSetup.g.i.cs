﻿#pragma checksum "..\..\DriverSetup.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "0879EE9A7919D39F213D6229F86A7977D8846F7BA6F829DAE6451D28B30AB8B5"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
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
using mBMWDiagn;


namespace mBMWDiagn {
    
    
    /// <summary>
    /// DriverSetup
    /// </summary>
    public partial class DriverSetup : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\DriverSetup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button InstallBtn;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\DriverSetup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button UninStallBTN;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\DriverSetup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RefreshDevices;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\DriverSetup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label USBCountLabel;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\DriverSetup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView USBListView;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\DriverSetup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SetTraceBTN;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\DriverSetup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SetPrimaryBTNs;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\DriverSetup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GroupBox WarningBox;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\DriverSetup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label WarningBoxLabel;
        
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
            System.Uri resourceLocater = new System.Uri("/mBMWDiagn;component/driversetup.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\DriverSetup.xaml"
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
            
            #line 8 "..\..\DriverSetup.xaml"
            ((mBMWDiagn.DriverSetup)(target)).Initialized += new System.EventHandler(this.Window_Initialized);
            
            #line default
            #line hidden
            return;
            case 2:
            this.InstallBtn = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\DriverSetup.xaml"
            this.InstallBtn.Click += new System.Windows.RoutedEventHandler(this.InstallBtn_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.UninStallBTN = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\DriverSetup.xaml"
            this.UninStallBTN.Click += new System.Windows.RoutedEventHandler(this.UninStallBTN_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.RefreshDevices = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\DriverSetup.xaml"
            this.RefreshDevices.Click += new System.Windows.RoutedEventHandler(this.RefreshDevices_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.USBCountLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.USBListView = ((System.Windows.Controls.ListView)(target));
            
            #line 42 "..\..\DriverSetup.xaml"
            this.USBListView.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.USBListView_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.SetTraceBTN = ((System.Windows.Controls.Button)(target));
            
            #line 51 "..\..\DriverSetup.xaml"
            this.SetTraceBTN.Click += new System.Windows.RoutedEventHandler(this.SetTraceBTNBTN_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.SetPrimaryBTNs = ((System.Windows.Controls.Button)(target));
            
            #line 52 "..\..\DriverSetup.xaml"
            this.SetPrimaryBTNs.Click += new System.Windows.RoutedEventHandler(this.SetPrimaryBTN_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.WarningBox = ((System.Windows.Controls.GroupBox)(target));
            return;
            case 10:
            this.WarningBoxLabel = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
