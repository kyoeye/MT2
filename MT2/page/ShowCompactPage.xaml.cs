using MT2.Control;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace MT2.page
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ShowCompactPage : Page
    {
        public ShowCompactPage()
        {
            this.InitializeComponent();
            CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            #region 判断api是否支持
            if (VersionHelper.Windows10Build15063 == true)
            {
                Windows.UI.Xaml.Media.AcrylicBrush acrylic = new Windows.UI.Xaml.Media.AcrylicBrush();
                acrylic.TintOpacity = 0.5;
                acrylic.TintColor = Colors.White;
                acrylic.BackgroundSource = AcrylicBackgroundSource.HostBackdrop;
                compactbackground.Background = acrylic;
            }
            else
            {
                compactbackground.Background = new SolidColorBrush(Colors.White);
            }
            #endregion

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if(e.Parameter.GetType().Name == "Imgitem")
            {
                showpathimg((Imgitem)e.Parameter);
            }
            else
            {
                var a = (string)e.Parameter;
                BitmapImage bitmapimage = new BitmapImage(new Uri(a));
                Myimage.Source = bitmapimage;
            }
        }

        private async void showpathimg(Imgitem  a)
        {
            BitmapImage bi = new BitmapImage();
            await bi.SetSourceAsync(await a.SF.OpenAsync(FileAccessMode.Read));
            Myimage.Source = bi;
        }

        public class SCPimguri
        {
            public static string scpimguri { get; set; }
        }
    }
}
