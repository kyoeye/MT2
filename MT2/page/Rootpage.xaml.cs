using MT2.CS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
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

            //Setting2Page set = new Setting2Page();
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
                    ONETimeAsync();
                }
                else
                {
                    try
                    {
                        var ss = (int)localsettings.Values["_listslider"];

                        if ((int)localsettings.Values["_listslider"]==0)
                        {
                            localsettings.Values["_listslider"] = 25;
                            localsettings.Values["_TackToJS"] = true;
                        }
                    }
                    catch
                    {
                        //利用catch报错来初始化
                        localsettings.Values["_listslider"] = 25;
                        localsettings.Values["_TackToJS"] = true;

                    }
                }

                //利用trycatch判断键值是否存在
                try
                {
                    var h = localsettings.Values["_H"];
                }
                catch
                {
                    localsettings.Values["_H"] = false;
                }

            }
            catch 
            {
            }
        }

        #region 初始化操作
        private async void ONETimeAsync()
        {
            try
            {
                #region 判断系统平台
                var thisDevice = Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily;

                if (thisDevice == "Windows.Desktop")
                {
                    localsettings.Values["_ThisDeviceis"] = "Desktop";
                }
                else if (thisDevice == "Windows.Mobile")
                {
                    localsettings.Values["_ThisDeviceis"] = "Mobile";

                }
                #endregion
                localsettings.Values["_listslider"] = 25;
                localsettings.Values["_TackToJS"] = true;
                localsettings.Values["_EggVisble"] = false;
                localsettings.Values["_H"] = false;
                localsettings.Values ["_package"] =  package.Id.Version.Major + "." + package.Id.Version.Minor + "." + package.Id.Version.Revision + "." + package.Id.Version.Build;
            }
            catch (Exception ex)
            {
                await new MessageDialog("初始化异常" + ex).ShowAsync();
            }
        }
        #endregion
        #region 应用更新的配置
        Windows.ApplicationModel.Package package = Windows.ApplicationModel.Package.Current;

        //private async void UpTimeAsync()
        //{
        //    try
        //    {
        //        var pac = package.Id.Version.Major + "." + package.Id.Version.Minor + "." + package.Id.Version.Revision + "." + package.Id.Version.Build;
        //      if (localsettings.Values["_package"].ToString () != pac  )
        //        {
        //            //通过判断版本号改变来配置新的功能
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        await new MessageDialog("包更新验证异常" + ex).ShowAsync();
        //    }
        //}
        #endregion
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
