using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Toolkit.Uwp;
using Microsoft.Toolkit.Uwp.UI.Animations;
using MT2.CS;
using MT2.page;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
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
        ApplicationDataContainer localsettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            int appOpennum ;
        string Mainapiuri = "https://yande.re/post.xml?limit=100";
        int limit;//列表总数
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
            //开始计算启动次数
            //TheAppOpenNum();
            BlurGlass(BlurListBox);

            if (localsettings.Values["_AppOpenNum"].ToString() == "1")
            {
                one_SaveFileUri();
            }

            CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            #region 开发者模式
            //string ao = localsettings.Values["AdminIsOpen"].ToString(); //开发者模式，这个设置不能为空
            //if ( ao != "NO" )
            //{
            //    var ds = Window.Current;
            //    ds.SizeChanged += Ds_SizeChanged;
            //}

            #endregion

            if (coreTitleBar.IsVisible == false)//失败，需要获取系统平台了
            {
                coreTitleBar.ExtendViewIntoTitleBar = true;
               
                UiLoading();

            }
            else
            {
                MyTitleBar.Visibility = Visibility.Collapsed;
            }

            Topprogress.Visibility = Visibility.Visible;

            GetxmltextAsync();

            NavigationCacheMode = NavigationCacheMode.Enabled;
        }
        //扔异步处理下载瀑布流数据
        private async void GetxmltextAsync()
        {
            await Getxmltext();
      
        }
        #region 获取保存地址
        //public void SaveFileUri()
        //{
        //    var picuri = KnownFolderId.SavedPictures;
        //}
        #endregion
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
        GetAPIstring getxml = new CS.GetAPIstring(); // 拓展加载更多，getxml共用


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Window.Current.SetTitleBar(TitleBar2);
            base.OnNavigatedTo(e);
            limit = (int ) localsettings.Values["_listslider"];
        } 
       

        #region 判断应用打开次数以管理整个应用&符合第一次打开应用执行的方法
        public   void TheAppOpenNum()
        {
           if ( localsettings.Values["_AppOpenNum"] == null)
            {
                appOpennum++;
                localsettings.Values["_AppOpenNum"] = appOpennum;
                //第一次启动调用弹窗
                 Show_OneTextDialog();
                localsettings.Values["_FileAllOpen"] = "false"; //默认关闭：每次保存文件都询问保存地址
            }
           else
            {
                appOpennum = int.Parse(localsettings.Values["_AppOpenNum"].ToString ());
                appOpennum++;
                localsettings.Values["_AppOpenNum"] = appOpennum;
            }
        
        }

        private void  Show_OneTextDialog()
        {

        }

        //如果应用第一次启动，保存路径将指向系统默认相册
        private void one_SaveFileUri ()
        {
            StorageFolder picuri = KnownFolders.SavedPictures;
            localsettings.Values["_Fileuri"] = picuri.Path.ToString();
        }
     #endregion

        //#region 导航处理
        //// 每次完成导航 确定下是否显示系统后退按钮  
        //private void RootFrame_Navigated(object sender, NavigationEventArgs e)
        //{

        //    // ReSharper disable once PossibleNullReferenceException  
        //    SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
        //        (Window.Current.Content as Frame).BackStack.Any()
        //        ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        //}
        //#endregion

        #region 对ui元素的处理
        public async void UiLoading()
        {
            var coreTileBarButton = ApplicationView.GetForCurrentView();
            var titlebar = coreTileBarButton.TitleBar;
            titlebar.ButtonBackgroundColor = Color.FromArgb(0, 0, 0, 0);
            titlebar.ButtonForegroundColor = Colors.Black;
            
            //await MenuBlur.Blur(value: 10, duration: 1076, delay: 0).StartAsync();
            await TopBlur.Blur(value: 10, duration: 1076, delay: 0).StartAsync();         
        }
        private void BlurGlass(UIElement BlurUI)
        {
            Visual hostVisual = ElementCompositionPreview.GetElementVisual(BlurUI);
            Compositor compositor = hostVisual.Compositor;
            var glassEffect = new GaussianBlurEffect
            {
                BlurAmount = 15.0f,
                BorderMode = EffectBorderMode.Hard,
                Source = new ArithmeticCompositeEffect
                {
                    MultiplyAmount =0,
                    Source1Amount = 0.5f,
                    Source2Amount = 0.5f,
                    Source1 = new CompositionEffectSourceParameter ("backdropBrush"),
                    Source2 = new ColorSourceEffect { Color = Color.FromArgb(255,245,245,245)}

                }
            };

            var effectFactory = compositor.CreateEffectFactory(glassEffect);
            var backdropBrush = compositor.CreateBackdropBrush();
            var effectBrush = effectFactory.CreateBrush();

            effectBrush.SetSourceParameter("backdropBrush", backdropBrush);
            var glassVisual = compositor.CreateSpriteVisual();
            glassVisual.Brush = effectBrush;
            ElementCompositionPreview.SetElementChildVisual(BlurUI, glassVisual);
            var bindSizeAnimation = compositor.CreateExpressionAnimation("hostVisual.Size");
            bindSizeAnimation.SetReferenceParameter("hostVisual", hostVisual);
            glassVisual.StartAnimation("Size", bindSizeAnimation);
        }

        #endregion
        
        public async Task Getxmltext()
        {
            Progresstext.Text = "正在和绿坝娘达成交易……";
            xmltext = await getxml.GetWebString(Mainapiuri);
            MainItemget.Toitem(xmltext);
            if (MainItemget.NetworkIsOK != false) //如果网络判断不为false则继续执行
            {
                Progresstext.Text = "正在排列一些奇怪的东西……";
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
            GetAPIstring gethotxml = new GetAPIstring();
            Progresstext.Text = "正在下载TOP数据……";

            try
            {
                string hotxmltext = await gethotxml.GetWebString(@"https://yande.re/post/popular_recent.xml");
                Hotitemget.Toitem(hotxmltext);
                Hotitemget.getlistitems(false);
                var HotitemList = Hotitemget.Listapiitems;
                Progresstext.Text = "请坐和放宽……";
                Homehoturl = HotitemList.First().sample_url;
                //Homehoturl = HotitemList[1].sample_url;
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
            //Frame .Navigate(typeof(Setting2Page));
            Frame.Navigate(typeof(Setting2Page));
            Mymenu.IsPaneOpen = false;

        }
        #region 汉堡菜单
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
                else if (item == downitem)
                {
                    Frame.Navigate(typeof(DownloadPage));
                    Mymenu.IsPaneOpen = false;

                }
            }
            
        }

        #endregion

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

        private void GobackButton_Click(object sender, RoutedEventArgs e)
        {
            //if (Frame .CanGoBack)
            //{
            //    Frame .GoBack();
            //}
            if (Mymenu.IsPaneOpen)
                Mymenu.IsPaneOpen = false;
        }


    }
}