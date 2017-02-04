using MT2.CS;
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
        public int shuzu = 200;//数组容量变量
        public int buttonint;
        public int pageint; //页码索引
        //}


        //string[] previewurl = new string[100];//瀑布流概览图


        public class Lookimgclass
        {
            public List<string> previewurl;
            public List<string> _id;
            //public string[] _id;
            public List<string> authorname;
            public int b;
            public string lookimguri;//选中索引  
            public int a = 0; // 数组索引
            public List<string> sampleurl;
            public List<string> ratings;
            public List<string> jpegurl;
            public List<string> thisname;
        }
        Lookimgclass lookit = new Lookimgclass();
        public void getsz() //数组用一个方法引用
        {
            lookit.previewurl = new List<string>();
            lookit._id = new List<string>();
            lookit.ratings = new List<string>();
            lookit.authorname = new List<string>();
            lookit.sampleurl = new List<string>();
            lookit.jpegurl = new List<string>();
            lookit.thisname = new List<string>();
        }
        //Uridh uridh = new Uridh();
        public MainPage()
        {
            
            this.InitializeComponent();
            getsz();
            getimage(null);
            NavigationCacheMode = NavigationCacheMode.Enabled;
            Myprogressring.Visibility = Visibility.Visible;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
           
        }
        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            Mymenu.IsPaneOpen = !Mymenu.IsPaneOpen;
        }

        //private void SettingButton_Click(object sender, RoutedEventArgs e)
        //{
        //    Frame.Navigate(typeof(SettingPage));
        //}

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

            if (pageint == 1)
            {
                lookit.a = 0;
                Listapiitems = new ObservableCollection<Listapiset>();
            }
            for (int i = 0; i < 20; i++) // 50为一次瀑布流显示的所有数量
            {
                if (lookit.ratings[lookit.a] != "q")
                {
                    if (lookit.ratings[lookit.a] != "e")
                    {
                        Listapiitems.Add(new Listapiset
                        {
                            _name = "作者：" + lookit.authorname[lookit.a],
                            rating = lookit.ratings[lookit.a],
                            preview_url = lookit.previewurl[lookit.a],
                            sample_url = lookit.sampleurl[lookit.a],
                            _a = lookit.a,
                            id = lookit._id[lookit.a]
                        });
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
            Myprogressring.Visibility = Visibility.Collapsed;
            if (pageint ==1)
            {
                Mygridview.ItemsSource = Listapiitems;
            }
            

        }
        
        private void Loadingbutton_Click(object sender, RoutedEventArgs e)
        {                       
            int count = Listapiitems.Count;
            if (lookit.a == lookit._id.Count)
            {
                getimage(true);
            }
            else
            {
                for (int i = count; i < count + 20; i++)
                {
                    if (lookit.ratings[lookit.a] != "q")
                    {
                        if (lookit.ratings[lookit.a] != "e")
                        {
                            Listapiitems.Add(new Listapiset
                            {
                                _name = "作者：" + lookit.authorname[lookit.a],
                                rating = lookit.ratings[lookit.a],
                                preview_url = lookit.previewurl[lookit.a],
                                sample_url = lookit.sampleurl[lookit.a],
                                _a = lookit.a,
                                id = lookit._id[lookit.a]
                            });
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
        }

        private HttpClient httpclient = new HttpClient();
        //private CancellationTokenSource cts;
        public async void getimage(bool? a)
        {
            if (a == true)
            {
                pageint++;
                //页码增加1
            }
            else
            {
                pageint = 1;
            }
            //httpclient = new HttpClient();
            //cts = new CancellationTokenSource();
            string homeimguri = ("https://yande.re/post.xml?limit=100" + "&page=" + pageint);
            var mystring = await GetXml.GetWebString(homeimguri, null);
            //IProgress<HttpProgress> httpprogress = new Progress<HttpProgress>(ProgressHandler);
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
                            switch (item.Name.ToString())
                            {
                                case "id":
                                    lookit._id.Add((string)item);
                                    break;
                                case "preview_url":
                                    lookit.previewurl.Add((string)item);
                                    break;
                                case "author":
                                    lookit.authorname.Add((string)item);
                                    break;
                                case "sample_url":
                                    lookit.sampleurl.Add((string)item);
                                    break;
                                case "jpeg_url":
                                    lookit.jpegurl.Add((string)item);
                                    break;
                                case "rating":
                                    lookit.ratings.Add((string)item);
                                    break;
                            }
                        }
                        if (lookit.a < 100)
                        {
                            lookit.a++;
                        }
                    }
                }
               
                    GetWaterfall();
                
            }

            else
            {
                NoNetworld.Visibility = Visibility.Visible;
            }

        }

        //private void ProgressHandler(HttpProgress obj)
        //{
        //    throw new NotImplementedException();
        //}

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

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Setting2Page));
        }

        private void MenuListboxitem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in e.AddedItems)
            {
                if (item == homepage)
                {
                    Frame.Navigate(typeof(MainPage));
                }
                else if (item == hotitem)
                {
                    Frame.Navigate(typeof(hotitempage));
                }
            }
        }
    }
}