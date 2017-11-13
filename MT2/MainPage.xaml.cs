﻿using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Toolkit.Uwp.UI.Animations;
using MT2.Control;
using MT2.CS;
using MT2.Model;
using MT2.page;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.Resources;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace MT2
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ApplicationDataContainer localsettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        #region 存了点变量poi~
        int appOpennum;
        string Mainapiuri = "https://yande.re/post"; // Mainapiuri+ ".xml?limit=" + limit 和 Mainapiuri+ ".json?limit=" + limit
        string hotapiuri = "https://yande.re/post/popular_recent.json";
        //string apitype;
        int limit;//列表总数
        int page = 1;

        string xmltext;
        string jsontext;

        #endregion

        //string hotimg;
        public MainPage()
        {
            //需要判断运行环境是否为手机（如果是则需要隐藏MyTitleBar)，建议在启动的时候判断，以免影响首页加载速度
            this.InitializeComponent();
            #region 设备判断
            if (localsettings.Values["_ThisDeviceis"].ToString() == "Mobile")
            {
                //TopBlur.Visibility = Visibility.Collapsed;
                TitleBar2.Visibility = Visibility.Collapsed;
                MyTitleBar.Visibility = Visibility.Collapsed;
                picinpciitem.Visibility = Visibility.Collapsed;

            }
            #endregion
            //开始计算启动次数
            //TheAppOpenNum();
            BlurGlass(BlurListBox);
            BlurGlass(TopBlur);
            #region 设置应用语言
            try
            {
                if (localsettings.Values["Language"].ToString() == "China")
                {
                    LanguageSelection.SettingLanguage("China");
                }
                else if (localsettings.Values["Language"].ToString() == "USA")
                {
                    LanguageSelection.SettingLanguage("USA");
                }

            }
            catch
            {

            }
            #endregion

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
            var f = Window.Current.Bounds;
            int wit = (int)f.Width;
            sizechanged(wit);

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

            limit = (int)localsettings.Values["_listslider"];
            GetimgvalueAsync();

            NavigationCacheMode = NavigationCacheMode.Enabled;
            //订阅窗口大小变化
            Window.Current.SizeChanged += Ds_SizeChanged;
            Myscrollviewer.ViewChanged += Loadingit;

            try
            {
                //不再显示第一次启动弹窗
                if ((bool)localsettings.Values["_BuzaixianshiOnetost"] != true)
                {
                    Show_OneTextDialogAsync();
                }
            }
            catch
            {
                Show_OneTextDialogAsync();
            }

        }

        private void Loadingit(object sender, ScrollViewerViewChangedEventArgs e)
        {
            ScrollViewer_SizeChanged();
        }

        //扔异步处理下载瀑布流数据
        private async void GetimgvalueAsync()
        {
            await Getimgvalue();
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
            sizechanged(e);
            //betatext.Text = "已改变窗口" + "  大小：" + a;
        }

        private void sizechanged(Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            if (e.Size.Width < 600)
            {
                BlurListBox.Visibility = Visibility.Visible;
                MenuBlurGrid.Visibility = Visibility.Collapsed;
            }
            if (e.Size.Width > 600)
            {
                BlurListBox.Visibility = Visibility.Collapsed;
                MenuBlurGrid.Visibility = Visibility.Visible;
            }

        }
        private void sizechanged(int e)
        {
            if (e < 600)
            {
                BlurListBox.Visibility = Visibility.Visible;
                MenuBlurGrid.Visibility = Visibility.Collapsed;
            }
            if (e > 600)
            {
                BlurListBox.Visibility = Visibility.Collapsed;
                MenuBlurGrid.Visibility = Visibility.Visible;
            }

        }

        #endregion

        ItemGET MainItemget = new CS.ItemGET();
        ItemGET Hotitemget = new ItemGET();
        GetAPIstring getapistring = new CS.GetAPIstring(); // 拓展加载更多，getxml共用


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Window.Current.SetTitleBar(TitleBar2);
            base.OnNavigatedTo(e);
            try
            {
                SetText();
            }
            catch
            {
                //新用户会catch，不会对语言进行操作，升级用户会被调到UpPage进行配置
            }
            limit = (int)localsettings.Values["_listslider"];

        }


        #region 判断应用打开次数以管理整个应用&符合第一次打开应用执行的方法
        public void TheAppOpenNum()
        {
            if (localsettings.Values["_AppOpenNum"] == null)
            {
                appOpennum++;
                localsettings.Values["_AppOpenNum"] = appOpennum;
                //第一次启动调用弹窗
                localsettings.Values["_FileAllOpen"] = "false"; //默认关闭：每次保存文件都询问保存地址
            }
            else
            {
                appOpennum = int.Parse(localsettings.Values["_AppOpenNum"].ToString());
                appOpennum++;
                localsettings.Values["_AppOpenNum"] = appOpennum;
            }

        }

        private async void Show_OneTextDialogAsync()
        {
            try
            {
                string uri = "ms-appx-web:///page/FristOpen.html";
                //string uri2 = "http://www.baidu.com";

                ContentDialog cd = new ContentDialog()
                {

                    Title = "哇，竟然有人来惹……",
                    Content = new Content(uri)
                    {
                        Title = "吾辈好不容易写的确定不看下吗？哇哇哇",

                        //Context = "嗯……虽然目标很多但是现在只有一个yande.re图源的。。。动漫图库？",
                        //Title2 = "为什么访问这么慢?"
                    },
                    PrimaryButtonText = "不再提示",
                    SecondaryButtonText = "知道啦",
                    FullSizeDesired = true,
                };
                cd.PrimaryButtonClick += (_s, _e) =>
                {
                    localsettings.Values["_BuzaixianshiOnetost"] = true;
                };
                cd.SecondaryButtonClick += (_s, _e) =>
                {

                };
                await cd.ShowAsync();
            }
            catch
            {

            }
        }

        //如果应用第一次启动，保存路径将指向系统默认相册
        private void one_SaveFileUri()
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

            await MenuBlur.Blur(value: 10, duration: 1076, delay: 0).StartAsync();
            //await TopBlur.Blur(value: 10, duration: 1076, delay: 0).StartAsync();         
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
                    MultiplyAmount = 0,
                    Source1Amount = 0.5f,
                    Source2Amount = 0.5f,
                    Source1 = new CompositionEffectSourceParameter("backdropBrush"),
                    Source2 = new ColorSourceEffect { Color = Color.FromArgb(255, 245, 245, 245) }

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

        public async Task Getimgvalue()
        {
            Progresstext.Text = "少女迷茫中……";    
                GetAPIstring getjson = new GetAPIstring();
                if (getjson != null)
                {
                    string jsontext = await getjson.GetWebString(Mainapiuri + ".json?limit=" + limit);
                    SetjsonstringAsync(jsontext);
                }
        }

        GetJson getjson = new GetJson();
        private async void SetjsonstringAsync(string jsontext)
        {
            try
            {
                Progresstext.Text = "正在排列一些奇怪的东西……";
                var source = getjson.SaveJson(jsontext);
                Pictureada.ItemsSource = source;
                await newGetHotimageAsync();
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.ToString()).ShowAsync();
            }
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
        }
        public async Task newGetHotimageAsync()
        {
            getapistring = new GetAPIstring();
            Progresstext.Text = "正在下载TOP数据……";
            var Hotjsonvalue = await getapistring.GetWebString(hotapiuri);
            GetJson gethotjson = new GetJson();
            var savejsonreturn = gethotjson.SaveJson(Hotjsonvalue);
            Homehoturl = savejsonreturn.First().sample_url;
            BitmapImage bit = new BitmapImage(new Uri(Homehoturl));
            HomeHot.Source = bit;
            Topprogress.Visibility = Visibility.Collapsed;
            //江+1s热榜瀑布流传递给热榜页面
            MTHub.Hotitemvalue = savejsonreturn;
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
                    MenuListhoxitem.SelectedItem = null;

                }
                else if (item == hotitem)
                {
                    Frame.Navigate(typeof(hotitempage));
                    MenuListhoxitem.SelectedItem = null;
                    Mymenu.IsPaneOpen = false;
                }
                else if (item == downitem)
                {
                    Frame.Navigate(typeof(DownloadPage));
                    MenuListhoxitem.SelectedItem = null;
                    Mymenu.IsPaneOpen = false;

                }
                else if (item == picinpciitem)
                {
                    Frame.Navigate(typeof(PicinPicPage));
                    MenuListhoxitem.SelectedItem = null;
                    Mymenu.IsPaneOpen = false;
                }
                else if (item == collects)
                {
                    Frame.Navigate(typeof(StarPage));
                    MenuListhoxitem.SelectedItem = null;
                    Mymenu.IsPaneOpen = false;
                }
            }
        }

        #endregion

        private void Picturegrid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if ((bool)localsettings.Values["_TackToJS"] == true)
            {
                var boxs = sender as Grid;
                var box = boxs.DataContext as Yande_post_json;
                Frame.Navigate(typeof(LookImg), box);
            }
            //else
            //{
            //    var boxs = sender as Grid;
            //    var box = boxs.DataContext as ItemGET.listsave;
            //    Frame.Navigate(typeof(LookImg), box);
            //}

        }

        private void Searchbutton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Seach2Page));
        }
        #region 加载更多
        private async void LoadingButton_Click(object sender, RoutedEventArgs e)
        {
            await LoadingfuctionAsync();
        }
        private async Task LoadingfuctionAsync()
        {
            //当数据更改后暂时让用户强退应用以免出现问题
            page++;
            if ((bool)localsettings.Values["_TackToJS"] == true)
            {
                jsontext = await getapistring.GetWebString(Mainapiuri + ".json?limit=" + limit + "&page=" + page);
                getjson.Loadingitem(jsontext, limit);

            }
            //else
            //{
            //    xmltext = await getapistring.GetWebString(Mainapiuri + ".xml?limit=" + limit + "&page=" + page);
            //    MainItemget.Toitem(xmltext);
            //    MainItemget.Loadinglistitems();
            //}


            return;

        }
        #endregion
        #region pixiv
        private async void get_Pixivjson()
        {
            try
            {
                getjson = new GetJson();
                var pixiv_retunJsonstring = await getjson.GetWebJsonStringAsync(@" https://app-api.pixiv.net/v1/illust/recommended-nologin?include_ranking_illusts=false&offset=30");
                GetPixivJson gpj = new GetPixivJson();
                var ret = gpj.SaveJson(pixiv_retunJsonstring);
                //Pictureada_Pixiv.ItemsSource = ret; //pivot控件会让瀑布流失效，所以暂时回退注释掉关于瀑布流的更改
            }
            catch
            {
                await new MessageDialog("还没准备好").ShowAsync();
            }

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



        private void HotGridTap_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(hotitempage));
            MenuListhoxitem.SelectedItem = null;
            Mymenu.IsPaneOpen = false;
        }

        private async void ScrollViewer_SizeChanged()
        {

            if (Myscrollviewer.ScrollableHeight > 100)
            {
                if (Myscrollviewer.VerticalOffset == Myscrollviewer.ScrollableHeight)
                {
                    //AppName.Text = "True";
                    await LoadingfuctionAsync();
                }
            }
        }

        //private void HomePage_Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    switch (HomePage_Pivot.SelectedIndex)
        //    {
        //        case 0:
        //            HomePage_Pivot.SelectedItem = 0;
        //            break;
        //        case 1:
        //            HomePage_Pivot.SelectedItem = 1;
        //            //get_Pixivjson();
        //            break;
        //    }
        //}
        #region 显示文字
        private void SetText()
        {
            ResourceLoader rl = new ResourceLoader();
            Mainpage_Title.Text = rl.GetString("Mainpage_Shouye");
            Mainpage_Shouye.Text = rl.GetString("Mainpage_Shouye");
            Mainpage_Hot.Text = rl.GetString("Mainpage_Hot");
            Mainpage_User.Text = rl.GetString("Mainpage_User");
            Mainpage_PicinPic.Text = rl.GetString("Mainpage_PicinPic");
            Mainpage_Download.Text = rl.GetString("Mainpage_Download");
            Mainpage_setting.Text = rl.GetString("Setting_Title");
        }
        #endregion
    }
}