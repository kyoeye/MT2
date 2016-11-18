using MT2.CS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
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
using static MT2.MainPage;

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
        public int a;
        public string imguri;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                base.OnNavigatedTo(e);
                Lookimgclass lookit = (Lookimgclass)e.Parameter;
                var sample_url = lookit.lookimguri;
               
                    //var value = (string)e.Parameter;
                    //setall.sample_url = value;
                BitmapImage bitmapimage = new BitmapImage(new Uri(sample_url));
                SeeImage.Source = bitmapimage;
                a = lookit.b;
                textblock.Text = "是"+a;
                
                imguri = lookit.jpegurl[a];
            }
            catch
            {

            }
        }

        public void Getsuface()
        {
         

            var f = Window.Current.Bounds;
            wit = f.Width;
            hei = f.Height;
            //betatext.Text = "宽度" + wit + "--高度：" + hei;
            betaborder.Height = hei;
        }

        private void textbutoon_Click(object sender, RoutedEventArgs e)
        {
            textbutoon.Content = imguri;
            string text = (string)imguri;
            /*用了数据绑定的呀  先找控件  找到控件就找到内容了*/
            DataPackage dp = new DataPackage();
            dp.SetText(text);
            Clipboard.SetContent(dp);
            
        }

        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            getjpg(imguri);
            //下载
        }

        public async void getjpg(string jpguri)
        {
            HttpClient httpclient = new HttpClient();
            HttpResponseMessage httpResponseMessage = await httpclient.GetAsync(new Uri(jpguri));

        }
    }
}
