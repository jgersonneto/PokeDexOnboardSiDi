﻿#pragma checksum "C:\ProjectOnboard\PokedexSidi\PokedexUwp\Views\Pokedex.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4BB99DF66924E7A5DCD108FA54E2C6952AC957ABE4F67833DC8330E0923F1187"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PokedexUwp.Views
{
    partial class Pokedex : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1: // Views\Pokedex.xaml line 1
                {
                    this.PagePokedex = (global::Windows.UI.Xaml.Controls.Page)(target);
                }
                break;
            case 2: // Views\Pokedex.xaml line 14
                {
                    this.viewModels2 = (global::PokedexUwp.ViewModel.PokedexPageViewModel)(target);
                }
                break;
            case 3: // Views\Pokedex.xaml line 20
                {
                    this.ImageBackGround = (global::Windows.UI.Xaml.Media.Imaging.BitmapImage)(target);
                }
                break;
            case 4: // Views\Pokedex.xaml line 39
                {
                    this.listView = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 5: // Views\Pokedex.xaml line 106
                {
                    this.Loading = (global::Windows.UI.Xaml.Controls.ProgressRing)(target);
                }
                break;
            case 8: // Views\Pokedex.xaml line 52
                {
                    global::Windows.UI.Xaml.Controls.Button element8 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element8).Click += this.ButtonPageNavigation;
                }
                break;
            case 12: // Views\Pokedex.xaml line 29
                {
                    global::Windows.UI.Xaml.Controls.AutoSuggestBox element12 = (global::Windows.UI.Xaml.Controls.AutoSuggestBox)(target);
                    ((global::Windows.UI.Xaml.Controls.AutoSuggestBox)element12).TextChanged += this.AutoSuggestBox_TextChanged;
                    ((global::Windows.UI.Xaml.Controls.AutoSuggestBox)element12).SuggestionChosen += this.AutoSuggestBox_SuggestionChosen;
                    ((global::Windows.UI.Xaml.Controls.AutoSuggestBox)element12).QuerySubmitted += this.AutoSuggestBox_QuerySubmitted;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

