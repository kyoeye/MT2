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

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace MT2.page
{


    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SearchPage : Page
    {
        public List<Seachitem> secitem;
        ApplicationDataContainer localsettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public SearchPage()
        {
            this.InitializeComponent();
            Window.Current.SetTitleBar(MyTitleBar);
            secitem = Seachitems.seachitem();
        }

        private async void SeachClick_Click(object sender, RoutedEventArgs e)
        {
            if ((int)localsettings.Values["_AppOpenNum"] >= 10)
            {
                if (mytextbox.Text.Contains("苟利国家"))
                {
                    await new MessageDialog("禁止养苟！").ShowAsync();
                    //showContentDialog();
                }
                else
                {
                    await new MessageDialog("正在搅拌混泥土，下个版本再来吧poi~").ShowAsync();

                }
            }
            else
            {
                await new MessageDialog("搬砖忙着呢，一边吵去，我们走了再来。").ShowAsync();
            }
        }
        //private async void showContentDialog()
        //{
        //    //try
        //    //{
        //    //    cd = new ContentDialog()
        //    //    {
        //    //        Title = "为什么会这样……",
        //    //        Content = new Content(null)
        //    //        {
        //    //            Title = "明明藏得这么好……",
        //    //            Context = "为什么会变成这样呢……\r\n第一次找到了藏彩蛋的地方\r\n第一次做到了自己都发现不了。\r\n这两件愉快的事情交织在了一起\r\n而这两份喜悦\r\n又会给我带来许许多多的喜悦。\r\n我本应该获得了这种如梦一般的幸福时光才对。\r\n可是，为什么\r\n会变成现在这样呢……",
        //    //            Title2 = "为什么你这么熟练……",
        //    //            Context2 = "你竟然能发现这里。。\r\n为什么你那么熟练。。\r\n那。。\r\n你是不是在期待什么\r\n我知道你在期待什么\r\n新世界的大门已经打开\r\n记得注意身体。。",
        //    //        },

        //    //        PrimaryButtonText = "打死",
        //    //        FullSizeDesired = true,
        //    //    };
        //    //    cd.PrimaryButtonClick += (_s, _e) => {
        //    //        Steins.Visibility = Visibility.Visible;
        //    //        localsettings.Values["_Fu_kMSvisble"] = true;
        //    //    };
        //    //    await cd.ShowAsync();
        //    //}
        //    //catch(Exception ex)
        //    //{
        //    //    await new MessageDialog(ex.ToString()).ShowAsync();
        //    //}
        //    await new MessageDialog("此诗甚吼，想必你便是石头门选中之人，看来你已经做好学习一番的准备了").ShowAsync();
        //    localsettings.Values["_Fu_kMSvisble"] = true;
        //    localsettings.Values["_OpenH"] = true;
        //}

        private void SettingGoback_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
        }
    }

    public class Seachitem
    {
        //public string Canseestring { get; set; }
        public string Seachstring { get; set; }
    }

    public class Seachitems
    {
        public static List<Seachitem> seachitem()
        {
            var seachitems = new List<Seachitem>();
            seachitems.Add(new page.Seachitem { Seachstring = "人渣的本愿" });
            seachitems.Add(new page.Seachitem { Seachstring = "小林家的龙女仆" });
            seachitems.Add(new page.Seachitem { Seachstring = "巨乳" });
           
            return seachitems;
        }
    }
}
