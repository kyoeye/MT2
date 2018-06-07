using MT2.Control;
using MT2.CS;
using MT2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace MT2.page
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class StarPage : Page
    {
        ApplicationDataContainer localsettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public StarPage()
        {
            this.InitializeComponent();
            #region 判断api是否支持
            if (VersionHelper.Windows10Build15063 == true)
            {
                MyTitleBar.Style = (Style)Application.Current.Resources["GridBackgroud"];
                StarBackground.Style = (Style)Application.Current.Resources["GridBackgroud"];
            }
            else
            {
                MyTitleBar.Background = new SolidColorBrush(Color.FromArgb(100, 244, 244, 244));
                StarBackground.Background = new SolidColorBrush(Color.FromArgb(100, 244, 244, 244));
            }
            #endregion
            NavigationCacheMode = NavigationCacheMode.Enabled;

            if (localsettings.Values["_ThisDeviceis"].ToString() == "Mobile")
            {
                MyTitleBarVB.Visibility = Visibility.Collapsed;
            }
            GetStar(0);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Window.Current.SetTitleBar(MyTitleBar);
            //  await GetStar(0);
        }
        private async void GetStar(int index)
        {
            try
            {
                switch (index)
                {
                    case 0:
                            await getStar(CS.apiset.apiurisave.YandeHost, SettingHelper.Username_Yande);
                        break;
                }
            }
            catch { await new MessageDialog("您没有登陆").ShowAsync(); }
        }
        private async void TostButton_Click(object sender, RoutedEventArgs e)
        {
            await new MessageDialog("测试中，碰到问题请主人反馈给咱的邮箱喵~", "关于收藏").ShowAsync();

        }

        private void GobackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
        }

        GetAPIstring getresponse = new GetAPIstring();
        GetJson getjson = new GetJson();
        public ObservableCollection<Yande_post_json> Source_Yande { get; set; }
        int page = 1;
        private async Task getStar(string urihost, string name)
        {
            if (name != null&name!="")
            {
                //tags=vote%3A3%3Aluojiwei+order%3Avote
                string uri = $"{urihost}post.json?tags=vote%3A3%3A{name}+order%3Avote";
                string response = await getresponse.GetWebString(uri);
                Source_Yande = getjson.SaveJson(response);
                Mygridview.ItemsSource = Source_Yande;
            }
            else
            {
                await new MessageDialog("您没有登陆").ShowAsync();
            }

        }
        private async void Loadinglist(string urihost, string name, int page)
        {
            if (name != null)
            {
                string uri = $"{urihost}post.json?page={page}&tags=vote%3A3%3A{name}+order%3Avote";
                string response = await getresponse.GetWebString(uri);
                if (response.Length > 5)
                {
                      getjson.Loadingitem(response, 40);
                    getjson.NoH();
                    Mygridview.ItemsSource = Source_Yande;
                }
                else
                    await new MessageDialog("没有更多了").ShowAsync();

            }
        }

        //点击查看
        private void gridstackpanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var boxs = sender as StackPanel;
            var box = boxs.DataContext as Yande_post_json;
            Frame.Navigate(typeof(LookImg), box);

        }

        private void Loadingbutton_Click(object sender, RoutedEventArgs e)
        {
            //到时候可能用pivot
            page++;
            Loadinglist(CS.apiset.apiurisave.YandeHost, SettingHelper.Username_Yande, page);

        }


        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            page = 1;
            GetStar(0);
        }
    }
}
