using MT2.Control;
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
       public static  string thisDevice = Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily;
        int appOpennum;
        public static Frame myMainframe;
        public Rootpage()
        {
            this.InitializeComponent();
            myMainframe = Mainframe;
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
                            localsettings.Values["_listslider"] = 50;
                            localsettings.Values["_TackToJS"] = true;
                        }
                    }
                    catch
                    {
                        //利用catch报错来初始化
                        localsettings.Values["_listslider"] = 50;
                        localsettings.Values["_TackToJS"] = true;

                    }
                }
            
                if (localsettings.Values["_package"].ToString() != package.Id.Version.Major + "." + package.Id.Version.Minor + "." + package.Id.Version.Build + "." + package.Id.Version.Revision)
                {
                    localsettings.Values["_listslider"] = 50;

                    Show_ContentDialogAsync(
                        "更新日志",
                        package.Id.Version.Major + "." + package.Id.Version.Minor + "." + package.Id.Version.Build + "." + package.Id.Version.Revision+"更新",
                        "1.【新功能】搜索功能现已可用（虽然并不好用）\r\n2.【新功能】标签（Tags）现已加入，点击任一一个标签便可跳转到搜索界面进行搜索\r\n3.【修复】现已修复画中画功能无权打开其他磁盘文件的bug\r\n4.【修复】现已修复下载文件尺寸过小清晰度不佳的bug\r\n5.【修复】现已修复首页top图未完成加载的情况下“日”榜二次加载造成无法打开的bug\r\n6.【优化】版本号判断不正确的bug\r\n7.一些优化和修复……",
                        "",
                        ""
                        );

                }
                //利用trycatch判断键值是否存在
                try
                {
                    var h = localsettings.Values["_Fu_kMS"];
                }
                catch
                {
                    localsettings.Values["_Fu_kMS"] = false;
                }
                //获取系统平台
                localsettings.Values["_package"] = package.Id.Version.Major + "." + package.Id.Version.Minor + "." + package.Id.Version.Build + "." + package.Id.Version.Revision;

            }
            catch 
            {
            }

        }

        private async  void Show_ContentDialogAsync(string title,string title1 ,string content ,string Title2,string content2)
        {
            ContentDialog cd = new ContentDialog()
            {

                Title = title ,
                Content = new Content(null)
                {
                    Title = title1,
                    Context = content ,
                    Title2 = Title2 ,
                    Context2 = content2 
                    //Context = "嗯……虽然目标很多但是现在只有一个yande.re图源的。。。动漫图库？",
                    //Title2 = "为什么访问这么慢?"
                },
                SecondaryButtonText = "知道啦",
                FullSizeDesired = false,
            };
           await  cd.ShowAsync();
        }


        #region 初始化操作
        private async void ONETimeAsync()
        {
            try
            {
                #region 判断系统平台

                if (thisDevice == "Windows.Desktop")
                {
                    localsettings.Values["_ThisDeviceis"] = "Desktop";
                }
                else if (thisDevice == "Windows.Mobile")
                {
                    localsettings.Values["_ThisDeviceis"] = "Mobile";

                }
                #endregion
                localsettings.Values["_listslider"] = 50;
                localsettings.Values["_FuckSlider"] = 1;
                localsettings.Values["_TackToJS"] = true;
                localsettings.Values["_Fu_kMSvisble"] = false;
                localsettings.Values["_Fu_kMS"] = false;
                localsettings.Values ["_package"] = package.Id.Version.Major + "." + package.Id.Version.Minor + "." + package.Id.Version.Build + "." + package.Id.Version.Revision;
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
