﻿using MT2.CS;
using MT2.page;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Xml.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace MT2
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {

        //public class Uridh
        //{           
            public int shuzu = 100;//数组容量变量
            public int pageint = 1; //页码索引
        //}


        string[] previewurl = new string[100];//瀑布流概览图


        public class Lookimgclass
        {

            public string[] _id;
            public string[] authorname ;
            public int b;
            public string lookimguri;//选中索引
            public int a = 0; // 数组索引
            public string[] sampleurl;
            public string[] ratings ;
            public string[] jpegurl;
            public string[] thisname;
        }
        Lookimgclass lookit = new Lookimgclass();
        public void getsz() //数组用一个方法引用
        {
            lookit._id = new string[shuzu];
            lookit.ratings = new string[shuzu];
            lookit.authorname = new string[shuzu];
            lookit.sampleurl = new string[shuzu];
            lookit.jpegurl = new string[shuzu];
            lookit.thisname = new string[shuzu];
        }
        //Uridh uridh = new Uridh();
        public MainPage()
        {
            
            this.InitializeComponent();
            getsz();
            getimage(null);
            NavigationCacheMode = NavigationCacheMode.Enabled;
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
            public int _a { get; set; }
            public string id { get; set; } //作品id
            public string _name { get; set; } //作者名字
            public string imguri { get; set; } //图片uri

            public string preview_url { get; set; }//瀑布流预览图

            public string tags { get; set; } // 标签，这个实现的方式有点特殊
            public string created_at { get; set; }//创建者
            public string approver_id { get; set; }//审核人

            public string sample_url { get; set; }//二级预览
            public string rating { get; set; }//安全等级


        }
        
        public ObservableCollection<Listapiset> Listapiitems { get; set; }
        public void GetWaterfall()
        {
            lookit.a = 0;
            Listapiitems = new ObservableCollection<Listapiset>();

            for (int i = 0; i < 20; i++) // 50为一次瀑布流显示的所有数量
            {
                if (lookit.ratings[lookit.a] != "q")
                {
                    if (lookit.ratings[lookit.a] != "e")
                    {
                        Listapiitems.Add(new Listapiset { _name = "作者：" + lookit.authorname[lookit.a], rating = lookit.ratings[lookit.a], preview_url = previewurl[lookit.a], sample_url = lookit.sampleurl[lookit.a], _a = lookit.a, id = lookit._id[lookit.a] });
                    }
                    else
                    {
                        lookit.a++;
                        continue;
                    }
                }
                else
                {
                    lookit.a++;
                    continue;
                }
                lookit.a++;
            }
        }

        private void Loadingbutton_Click(object sender, RoutedEventArgs e)
        {
            int count = Listapiitems.Count;

            for (int i = count;  i < count + 20; i++)
            {
                if (lookit.ratings[lookit.a] != "q")
                {
                    if (lookit.ratings[lookit.a] != "e")
                    {
                        Listapiitems.Add(new Listapiset { _name = "作者：" + lookit.authorname[lookit.a], rating = lookit.ratings[lookit.a], preview_url = previewurl[lookit.a], sample_url = lookit.sampleurl[lookit.a], _a = lookit.a, id = lookit._id[lookit.a] });
                    }
                    else
                    {
                        lookit.a++;
                        continue;
                    }
                }
                else
                {
                    lookit.a++;
                    continue;
                }
                lookit.a++;
                //Listapiitems.Add(new Listapiset { name = "作者：" + authorname[count], rating = lookit.ratings[count], preview_url = previewurl[count], sample_url = lookit.sampleurl[count] });
            }
        }

        private HttpClient httpclient;
        private CancellationTokenSource cts;
        public async void getimage(int? a)
        {
            if (a != null)
            {
                //我要干啥了？
            }
            else
            {
                httpclient = new HttpClient();
                cts = new CancellationTokenSource();
                string homeimguri = ("https://yande.re/post.xml?limit=100" + "&page=" + pageint);
                var mystring = await GetXml.GetWebString(homeimguri, null);

                //string resuri = homeimguri;
                //const uint streamLength = 1000000;
                //HttpStreamContent streamContent = new HttpStreamContent(new SlowInputStream(streamLength));
                IProgress<HttpProgress> httpprogress = new Progress<HttpProgress>(ProgressHandler);

                //HttpRequestMessage response = await httpclient.PostAsync(new Uri(homeimguri)).AsTask(cts.Token, httpprogress);
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
                                    lookit._id[lookit.a] = (string)item;
                                }
                                else if (item.Name == "preview_url")
                                {
                                    previewurl[lookit.a] = (string)item;
                                }
                                else if (item.Name == "author")
                                {
                                    lookit.authorname[lookit.a] = (string)item;
                                }
                                else if (item.Name == "sample_url")
                                {
                                    lookit.sampleurl[lookit.a] = (string)item;
                                }
                                else if (item.Name == "jpeg_url")
                                {
                                    lookit.jpegurl[lookit.a] = (string)item;
                                }
                                else if (item.Name == "rating") // 这个判断需要重新写11.5留
                                {
                                    lookit.ratings[lookit.a] = (string)item;

                                }
                            }
                            if (lookit.a < 100)
                            {

                                lookit.a++;
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
        }

        private void ProgressHandler(HttpProgress obj)
        {
            throw new NotImplementedException();
        }

        public void GetNextPage()
        {
            pageint++;
        }


        private void SeachButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SearchPage));
        }

        private void gridstackpanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var boxs = sender as StackPanel;
            var box = boxs.DataContext as Listapiset;
            lookit.lookimguri = box.sample_url;
            lookit.b = box._a;
            Frame.Navigate(typeof(LookImg), lookit);
        }
    }
}
