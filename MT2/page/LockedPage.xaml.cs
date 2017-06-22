using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace MT2.CS
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class LockedPage : Page
    {
        ApplicationDataContainer localsettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public LockedPage()
        {
            this.InitializeComponent();
        }

        private void GoingButton_Click(object sender, RoutedEventArgs e)
        {
            if ( localsettings.Values["_password"].ToString() == UnlockPassword.Password)
                {
                    Frame.Navigate(typeof(MainPage));
                }
           else
               {
                   mytext.Text = "输入错误XD 输入错误XD 输入错误XD输入错误XD";
                }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);    
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            localsettings.Values["_password"] = null;
            localsettings.Values["_Fu_kMS"] = false;
            Frame.Navigate(typeof(MainPage));

        }
    }
}
