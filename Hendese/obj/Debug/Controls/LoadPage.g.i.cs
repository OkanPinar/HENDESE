﻿#pragma checksum "..\..\..\Controls\LoadPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "918761093440530F4B7096F2E7BA8286386718134F5EFF967CF494B44BE73399"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Hendese.Controls;
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


namespace Hendese.Controls {
    
    
    /// <summary>
    /// LoadPage
    /// </summary>
    public partial class LoadPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 29 "..\..\..\Controls\LoadPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SimpleSingleLoad;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\Controls\LoadPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SimpleDistributedLoad;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\Controls\LoadPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ConsolSingleLoad;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\Controls\LoadPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ConsolDistributedLoad;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\Controls\LoadPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button FixedSingleLoad;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\Controls\LoadPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button FixedDistributedLoad;
        
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
            System.Uri resourceLocater = new System.Uri("/Hendese;component/controls/loadpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Controls\LoadPage.xaml"
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
            this.SimpleSingleLoad = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\..\Controls\LoadPage.xaml"
            this.SimpleSingleLoad.Click += new System.Windows.RoutedEventHandler(this.Model_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.SimpleDistributedLoad = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\..\Controls\LoadPage.xaml"
            this.SimpleDistributedLoad.Click += new System.Windows.RoutedEventHandler(this.Model_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ConsolSingleLoad = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\..\Controls\LoadPage.xaml"
            this.ConsolSingleLoad.Click += new System.Windows.RoutedEventHandler(this.Model_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ConsolDistributedLoad = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\..\Controls\LoadPage.xaml"
            this.ConsolDistributedLoad.Click += new System.Windows.RoutedEventHandler(this.Model_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.FixedSingleLoad = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\..\Controls\LoadPage.xaml"
            this.FixedSingleLoad.Click += new System.Windows.RoutedEventHandler(this.Model_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.FixedDistributedLoad = ((System.Windows.Controls.Button)(target));
            
            #line 45 "..\..\..\Controls\LoadPage.xaml"
            this.FixedDistributedLoad.Click += new System.Windows.RoutedEventHandler(this.Model_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

