﻿using MT2.CS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using Windows.Storage.Pickers;
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
        public int imgid;
        public string imguri;
        public string imgname;
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
                bitmapimage.DownloadProgress += Bitmapimage_DownloadProgress;
                SeeImage.Source = bitmapimage;
                a = lookit.b;             
                imguri = lookit.jpegurl[a];
                imgname = lookit.thisname[a];
                imgid = int.Parse(lookit._id[a]);
            }
            catch
            {

            }
        }

        private void Bitmapimage_DownloadProgress(object sender, DownloadProgressEventArgs e)
        {
           Myprogressbar .Value  = e.Progress;
            if (Myprogressbar .Value == 100)
            {
                Myprogressbar.Visibility = Visibility.Collapsed;

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
            DataPackage dp = new DataPackage();
            dp.SetText(text);
            Clipboard.SetContent(dp);

        }

        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            //getjpg(imguri);
            //下载
            Savefile();
        }
        public StorageFile storagefile;
        public async void Savefile()
        {
            FileSavePicker savefile = new FileSavePicker();
            savefile.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            string f = "保存文件类型";
            savefile.FileTypeChoices.Add(f, new List<string>() { ".jpg", ".png", ".bmp" });
            savefile.SuggestedFileName = imgname + "ID" + imgid;
            storagefile = await savefile.PickSaveFileAsync();

            if (storagefile != null)
            {
                CachedFileManager.DeferUpdates(storagefile);

                string Filename = imgname + imgid;
                string _transferUri = imguri;
                Uri transferUri;
                try
                {
                    transferUri = new Uri(Uri.EscapeUriString(_transferUri), UriKind.RelativeOrAbsolute);
                }
                catch
                {
                    return;
                }

                BackgroundDownloader backgrounddownloader = new BackgroundDownloader();//后台下载
                DownloadOperation downloader = backgrounddownloader.CreateDownload(transferUri, storagefile);
                await downloader.StartAsync();
            }
        }
        public async void getjpg(string jpguri)
        {
            Savefile();
            if (storagefile != null)
            {
                CachedFileManager.DeferUpdates(storagefile);

                string Filename = imgname + imgid;
                string _transferUri = jpguri;
                Uri transferUri;
                try
                {
                    transferUri = new Uri(Uri.EscapeUriString(_transferUri), UriKind.RelativeOrAbsolute);
                }
                catch
                {
                    return;
                }

                BackgroundDownloader backgrounddownloader = new BackgroundDownloader();//后台下载
                DownloadOperation downloader = backgrounddownloader.CreateDownload(transferUri, storagefile);
                await downloader.StartAsync();
            }
            //HttpClient httpclient = new HttpClient();
            //HttpResponseMessage httpResponseMessage = await httpclient.GetAsync(new Uri(jpguri));

        }

        private void AppBarButton_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            storyboard1.Begin();
        }
    }
}
