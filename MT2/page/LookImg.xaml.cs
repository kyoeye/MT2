using MT2.CS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace MT2.page
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class LookImg : Page
    {
        public LookImg()
        {
            this.InitializeComponent();
            Getsuface();
            //betatext.Text = System.Windows.Forms.Screen.GetWorkingArea(this);
        }
        double wit;
        double hei;

        SETall setall = new SETall();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var value = (string)e.Parameter;
            setall.sample_url = value;
            BitmapImage bitmapimage = new BitmapImage(new Uri(value));
            SeeImage.Source = bitmapimage;
        }

        public void Getsuface()
        {
            var f = Window.Current.Bounds;
            wit = f.Width;
            hei = f.Height;
            //betatext.Text = "宽度" + wit + "--高度：" + hei;
            betaborder.Height = hei;
        }

        
    }
}
