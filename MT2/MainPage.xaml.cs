using MT2.CS;
using MT2.CS.apiset;
using MT2.page;
using MT2.pubuliu;
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
using System.Collections;

using static MT2.CS.GetXml;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace MT2
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {



        string[] authorname = new string[100];
        string[] authorid = new string[100];
        string[] previewurl = new string[100];//瀑布流概览图


        public class Lookimgclass
        {
            public int b;
            public string lookimguri;//选中索引
            public int a = 0; // 数组索引
            public string[] sampleurl = new string[100];
            public string[] ratings = new string[100];
            public string[] jpegurl = new string[100];

        }
        Lookimgclass lookit = new Lookimgclass();

        public MainPage()
        {
            this.InitializeComponent();

            getimage();

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
            public string id { get; set; } //作者id
            public string name { get; set; } //作者名字
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
                        Listapiitems.Add(new Listapiset { name = "作者：" + authorname[lookit.a], rating = lookit.ratings[lookit.a], preview_url = previewurl[lookit.a], sample_url = lookit.sampleurl[lookit.a], _a = lookit.a });
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

            for (int i = count; i < count + 20; i++)
            {
                if (lookit.ratings[lookit.a] != "q")
                {
                    if (lookit.ratings[lookit.a] != "e")
                    {
                        Listapiitems.Add(new Listapiset { name = "作者：" + authorname[lookit.a], rating = lookit.ratings[lookit.a], preview_url = previewurl[lookit.a], sample_url = lookit.sampleurl[lookit.a], _a = lookit.a });
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
        public async void getimage()
        {
            httpclient = new HttpClient();
            cts = new CancellationTokenSource();
            string homeimguri = ("https://yande.re/post.xml?limit=50");
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
                                authorid[lookit.a] = (string)item;
                            }
                            else if (item.Name == "preview_url")
                            {
                                previewurl[lookit.a] = (string)item;
                            }
                            else if (item.Name == "author")
                            {
                                authorname[lookit.a] = (string)item;
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

                                //bool fc = (item.Value == "q");
                                //if (fc == true)
                                //{
                                //    continue;
                                //}
                                //break;
                            }
                        }
                        if (lookit.a < 50)
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
            lookit.lookimguri = box.sample_url;
            lookit.b = box._a;
            Frame.Navigate(typeof(LookImg), lookit);
        }
    }
}
