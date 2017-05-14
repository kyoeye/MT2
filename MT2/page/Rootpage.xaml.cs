using MT2.CS;
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

namespace MT2.page
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Rootpage : Page
    {
        ApplicationDataContainer localsettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public Rootpage()
        {
            this.InitializeComponent();
           var thisDevice =  Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily;

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //this.InitialBackButton();
            if (e.NavigationMode == NavigationMode.New)
            {
                if (localsettings.Values["_password"] != null)
                {
                    Mainframe.Navigate(typeof(LockedPage));
                }
                else
                {
                    Mainframe.Navigate(typeof(MainPage));
                }

            }
            base.OnNavigatedTo(e);
        }


    }
}
