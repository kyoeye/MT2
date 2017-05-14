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
        int appOpennum;
        public Rootpage()
        {
            this.InitializeComponent();

            try
            {
                if (localsettings.Values["_AppOpenNum"] == null)
                {
                    appOpennum++;
                    localsettings.Values["_AppOpenNum"] = appOpennum;
                    localsettings.Values["_FileAllOpen"] = "false"; //默认关闭：每次保存文件都询问保存地址
                }
                else
                {
                    appOpennum = int.Parse(localsettings.Values["_AppOpenNum"].ToString());
                    appOpennum++;
                    localsettings.Values["_AppOpenNum"] = appOpennum;
                }
                if (localsettings.Values["_AppOpenNum"].ToString() == "1")
                {
                    var thisDevice = Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily;

                    if (thisDevice == "Windows.Desktop")
                    {
                        localsettings.Values["_ThisDeviceis"] = "Desktop";
                    }
                    else if (thisDevice == "Windows.Mobile")
                    {
                        localsettings.Values["_ThisDeviceis"] = "Mobile";

                    }
                }

            }
            catch
            {

            }
         
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //this.InitialBackButton();
            try
            {
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
            }
            catch
            {

            }
            base.OnNavigatedTo(e);
        }


    }

    
}
