﻿#pragma checksum "..\..\..\Find.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "041F6E2CB382253022991703E82E02F76C433CB3"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Notepad;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace Notepad {
    
    
    /// <summary>
    /// Find
    /// </summary>
    public partial class Find : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\..\Find.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FindTextBox;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\Find.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ReplaceTextBox;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\Find.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox CaseSensitiveCheckBox;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\Find.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox RegExCheckBox;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\Find.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button FindButton;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\Find.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ReplaceButton;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\Find.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label NumberOfOccurancesLabel;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.8.1.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/NotepadCore;component/find.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Find.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.8.1.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 8 "..\..\..\Find.xaml"
            ((Notepad.Find)(target)).Closed += new System.EventHandler(this.Window_Closed);
            
            #line default
            #line hidden
            return;
            case 2:
            this.FindTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 21 "..\..\..\Find.xaml"
            this.FindTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.FindTextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ReplaceTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.CaseSensitiveCheckBox = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 5:
            this.RegExCheckBox = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 6:
            this.FindButton = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\..\Find.xaml"
            this.FindButton.Click += new System.Windows.RoutedEventHandler(this.FindButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ReplaceButton = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\..\Find.xaml"
            this.ReplaceButton.Click += new System.Windows.RoutedEventHandler(this.ReplaceButton_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.NumberOfOccurancesLabel = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

