﻿using MT2.CS;
using MT2.CS.apiset;
using MT2.page;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;
using static MT2.CS.GetXml;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace MT2
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        int a = 0; // 数组索引
        

        string[] authorname = new string[100];
        string[] authorid = new string[100];
        string[] previewurl = new string[100];//瀑布流概览图

        public class Lookimgclass
        {
            public string[] sampleurl = new string[100];
        }
        Lookimgclass lookit = new Lookimgclass();

        public MainPage()
        {
            this.InitializeComponent();

            getimage();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //Mygridview.ItemsSource = Listapiitems;
            //getimage();
            //下面是判断瀑布流列
            //var f = Window.Current.Bounds;
            //var wit = (int)f.Width;
            //if (wit < 500)
            //{

            //}
            //GetWaterfall();

        }
        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            Mymenu.IsPaneOpen = !Mymenu.IsPaneOpen;
        }

        private void SettingButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingPage));
        }

        public class Listapiset
        {
            public string id { get; set; } //作者id
            public string name { get; set; } //作者名字
            public string imguri { get; set; } //图片uri

            public string preview_url { get; set; }//瀑布流预览图

            public string tags { get; set; } // 标签，这个实现的方式有点特殊
            public string created_at { get; set; }//创建者
            public string approver_id { get; set; }//审核人

            public string sample_url { get; set; }//二级预览

        }

        public ObservableCollection<Listapiset> Listapiitems { get; set; }
        public void GetWaterfall()
        {
            a = 0;
            Listapiitems = new ObservableCollection<Listapiset>();

            for (int i = 0; i < 20; i++) // 50为一次瀑布流显示的所有数量
            {
                Listapiitems.Add(new Listapiset { name = "作者：" + authorname[a] + a, id = authorid[a], preview_url = previewurl[a], sample_url = lookit.sampleurl[a] });
                a++;
            }
        }

        private void Loadingbutton_Click(object sender, RoutedEventArgs e)
        {
            int count = Listapiitems.Count;

            for (int i = count; i < count + 1; i++)
            {
                Listapiitems.Add(new Listapiset { name = "作者：" + authorname[count], id = authorid[count], preview_url = previewurl[count], sample_url = lookit.sampleurl[a] });
            }
        }

        private HttpClient httpclient;
        private CancellationTokenSource cts;
        public async void getimage()
        {
            httpclient = new HttpClient ();
            cts = new CancellationTokenSource();
            string homeimguri = ("https://yande.re/post.xml?limit=50");
            var mystring = await GetXml.GetWebString(homeimguri, null);
          
            //string resuri = homeimguri;
            //const uint streamLength = 1000000;
            //HttpStreamContent streamContent = new HttpStreamContent(new SlowInputStream(streamLength));
            IProgress<HttpProgress> httpprogress = new Progress<HttpProgress>(ProgressHandler);
   
            HttpRequestMessage response = await httpclient.PostAsync(new Uri(homeimguri)).AsTask(cts.Token ,httpprogress);
            //依旧没有实现进度条
            if (mystring != null)
            {
                XElement root = XElement.Parse(mystring);
                IEnumerable<XElement> elements = root.Elements();
                foreach (var element in elements)
                {
                    if (element.Name == "post")
                    {
                        IEnumerable<XAttribute> Items = element.Attributes();
                        foreach (var item in Items)
                        {
                            if (item.Name == "id")
                            {
                                authorid[a] = (string)item;
                            }
                            else if (item.Name == "preview_url")
                            {
                                previewurl[a] = (string)item;
                            }
                            else if (item.Name == "author")
                            {
                                authorname[a] = (string)item;
                            }
                            else if (item.Name == "sample_url")
                            {
                                lookit.sampleurl[a] = (string)item;
                            }
                            else if (item.Name == "rating") // 这个判断需要重新写11.5留
                            {
                                bool fc = (item.Value == "e");
                                if (fc == true)
                                {
                                    continue;
                                }
                                //break;
                            }
                        }
                        if (a < 50)
                        {

                            a++;
                        }
                        else
                        {
                            //  GetWaterfall();
                            return;
                        }
                    }

                }
                //break;

                GetWaterfall();
                Mygridview.ItemsSource = Listapiitems;
            }
            else
            {
                NoNetworld.Visibility = Visibility.Visible;
            }
        }

        private void ProgressHandler(HttpProgress obj)
        {
            throw new NotImplementedException();
        }

        public void getwitch()
        {

        }


        private void SeachButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SearchPage));

        }

        private void gridstackpanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var boxs = sender as StackPanel;
            var box = boxs.DataContext as Listapiset;
            var lookimguri = box.sample_url;
            Frame.Navigate(typeof(LookImg), lookimguri);
        }
    }
}
