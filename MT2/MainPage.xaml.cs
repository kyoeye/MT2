using Microsoft.Toolkit.Uwp;
using Microsoft.Toolkit.Uwp.UI.Animations;
using MT2.CS;
using MT2.page;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;
using static MT2.CS.ItemGET;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace MT2
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {

        string Mainapiuri = "https://yande.re/post.xml?limit=100";
        string xmltext;
        int page = 1;
        //string hotimg;
        public MainPage()
        {
            //需要判断运行环境是否为手机（如果是则需要隐藏MyTitleBar)，建议在启动的时候判断，以免影响首页加载速度
            this.InitializeComponent();
            #region 网络判断
            //switch (ConnectionHelper.ConnectionType)
            //{
            //    case ConnectionType.Ethernet:
            //        // Ethernet
            //        break;
            //    case ConnectionType.WiFi:
            //        // WiFi
            //        break;
            //    case ConnectionType.Data:
            //        // Data
            //        break;
            //    case ConnectionType.Unknown:
            //        // Unknown
            //        break;
            //}

            #endregion
            ApplicationDataContainer localsettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            #region 开发者模式
            //string ao = localsettings.Values["AdminIsOpen"].ToString(); //开发者模式，这个设置不能为空
            //if ( ao != "NO" )
            //{
            //    var ds = Window.Current;
            //    ds.SizeChanged += Ds_SizeChanged;
            //}

            #endregion

            if (  coreTitleBar.IsVisible == false )//失败，需要获取系统平台了
            {
                coreTitleBar.ExtendViewIntoTitleBar = true;
                UiLoading();

            }
            else
            {
                MyTitleBar.Visibility = Visibility.Collapsed;
            }

            Topprogress.Visibility = Visibility.Visible;
            getxmltext();
            NavigationCacheMode = NavigationCacheMode.Enabled;
        }
        #region 开发者模式
        //测量窗口大小
        private void Ds_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            //throw new NotImplementedException();
            string a = e.Size.ToString();

            betatext.Text = "已改变窗口" + "  大小：" + a;
        }

        #endregion

        ItemGET MainItemget = new CS.ItemGET();
        ItemGET Hotitemget = new ItemGET();
        GetXml getxml = new CS.GetXml(); // 拓展加载更多，getxml共用


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

        }
        #region 对ui元素的处理
        public async void UiLoading()
        {
            await MenuBlur.Blur(value: 10, duration: 3, delay: 0).StartAsync();
            await TopBlur.Blur(value: 10, duration: 3, delay: 0).StartAsync();
            var coreTileBarButton = ApplicationView.GetForCurrentView();
            var titlebar = coreTileBarButton.TitleBar;
            titlebar.ButtonBackgroundColor = Color.FromArgb(0, 0, 0, 0);
            titlebar.ButtonForegroundColor = Colors.Black;

        }
        #endregion

        public async void getxmltext()
        {

            Progresstext.Text = "正在下载瀑布流数据……";
            xmltext = await getxml.GetWebString(Mainapiuri);
            MainItemget.Toitem(xmltext);
            if (MainItemget.NetworkIsOK != false) //如果网络判断不为false则继续执行
            {
                MainItemget.getlistitems(true);
                Pictureada.ItemsSource = MainItemget.Listapiitems;
                await GetHotimage();
            }
            //progressrin.IsActive = false;
        }
        //HotimageHub hih = new HotimageHub();

        string homehoturl;
        public string Homehoturl { get { return homehoturl; } set { homehoturl = value; } }

        public async Task GetHotimage() //按照获取首页瀑布流的方法获取热榜瀑布流数据，热榜直接继承这个类
        {
            GetXml gethotxml = new GetXml();
            Progresstext.Text = "正在下载TOP数据……";

            try
            {
                string hotxmltext = await gethotxml.GetWebString(@"https://yande.re/post/popular_recent.xml");
                Hotitemget.Toitem(hotxmltext);
                Hotitemget.getlistitems(false);
                var HotitemList = Hotitemget.Listapiitems;
                Homehoturl = HotitemList[1].sample_url;
                BitmapImage bit = new BitmapImage(new Uri(Homehoturl));
                HomeHot.Source = bit;
                Topprogress.Visibility = Visibility.Collapsed;

            }
            catch
            {

            }

            //    try
            //   {
            //       //hih.Gethotxml();
            //       hih.Gethotimg();
            //       hotimg = hih.Tophotimg;
            //       BitmapImage bit = new BitmapImage(new Uri(hotimg));
            //       HomeHot.Source = bit;
            //   }
            //catch
            //   {

            //   }
        }


        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            Mymenu.IsPaneOpen = !Mymenu.IsPaneOpen;
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            //this.Frame.Navigate(typeof(Setting2Page));
            Mainframe.Navigate(typeof(Setting2Page));

            Mymenu.IsPaneOpen = false;

        }

        private void MenuListboxitem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in e.AddedItems)
            {
                if (item == homepage)
                {
                    Frame.Navigate(typeof(MainPage));
                    Mymenu.IsPaneOpen = false;
                }
                else if (item == hotitem)
                {
                    Frame.Navigate(typeof(hotitempage));
                    Mymenu.IsPaneOpen = false;

                }
            }
        }

        private void Picturegrid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var boxs = sender as Grid;
            var box = boxs.DataContext as ItemGET.listsave;
            Frame.Navigate(typeof(LookImg), box);
        }

        private void Searchbutton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SearchPage));
        }
        #region 加载更多
        private void LoadingButton_Click(object sender, RoutedEventArgs e)
        {
            LoadingfuctionAsync();
        }
        private async void LoadingfuctionAsync()
        {

            page++;

            xmltext = await getxml.GetWebString(Mainapiuri + "&page=" + page);
            MainItemget.Toitem(xmltext);
            MainItemget.Loadinglistitems();



        }
        #endregion
    }
}