using MT2.Control;
using MT2.CS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
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
    public sealed partial class UserManagePage : Page
    {
        ApplicationDataContainer localsettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        public UserManagePage()
        {
            this.InitializeComponent();
            #region 判断api是否支持
            if (VersionHelper.Windows10Build15063 == true)
            {
                MyTitleBar.Style = (Style)Application.Current.Resources["GridBackgroud"];
                TitleBackground.Style = (Style)Application.Current.Resources["GridBackgroud"];
            }
            else
            {
                MyTitleBar.Background = new SolidColorBrush(Color.FromArgb(100, 244, 244, 244));
                TitleBackground.Background = new SolidColorBrush(Color.FromArgb(100, 244, 244, 244));
            }
            #endregion
            NavigationCacheMode = NavigationCacheMode.Required;

            var device =   Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily;
            if( device == "Windows.Mobile")
            {
                MyTitleBarVB.Visibility = Visibility.Collapsed;
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Window.Current.SetTitleBar(MyTitleBar);
            if(SettingHelper.IsLoagin_Yande == "1")
            {
                YandeUse_Grid.Visibility = Visibility.Collapsed;
                YandeUse_Logined_Grid.Visibility = Visibility.Visible;
                UseName_Yande.Text = SettingHelper.Username_Yande;
                UseName_id_Yande.Text = SettingHelper.Userid_Yande;
            }
            else
            {
                YandeUse_Grid.Visibility = Visibility.Visible;
                YandeUse_Logined_Grid.Visibility = Visibility.Collapsed;
            }
            if (SettingHelper.IsLoagin_Konachan == "1")
            {
                KonachanUse_Grid.Visibility = Visibility.Collapsed;
                KonachanUse_Logined_Grid.Visibility = Visibility.Visible;
                UseName_Konachan.Text = SettingHelper.Username_Konachan;
                UseName_id_Konachan.Text = SettingHelper.Userid_Konachan;
            }
            else
            {
                KonachanUse_Grid.Visibility = Visibility.Visible;
                KonachanUse_Logined_Grid.Visibility = Visibility.Collapsed;
            }
        }
        HttpHelper httphelper = new HttpHelper();
        private async  void LoginButton_Yande_Click(object sender, RoutedEventArgs e)
        {
            if(Username_Yande.Text != null && Userpass_Yande.Password != null)
            {
                progress_login_yande.Visibility = Visibility.Visible;
                bool logined = await  httphelper.LoginClient(Username_Yande.Text, Userpass_Yande.Password,CS.apiset.apiurisave.YandeHost);
                progress_login_yande.Visibility = Visibility.Collapsed;
                if(logined==true)
                {
                    SettingHelper.IsLoagin_Yande = "1";
                    SettingHelper.UserPass_Yande = Userpass_Yande.Password;
                    YandeUse_Grid.Visibility = Visibility.Collapsed;
                    YandeUse_Logined_Grid.Visibility = Visibility.Visible;
                    UseName_Yande.Text = SettingHelper.Username_Yande;
                    UseName_id_Yande.Text = SettingHelper.Userid_Yande;
                }
                else
                {
                    SettingHelper.IsLoagin_Yande = "0";
                    progress_login_yande.Visibility = Visibility.Collapsed;
                }
            }
        }
        private async void LoginButton_Konachan_Click(object sender, RoutedEventArgs e)
        {
            if (Username_Konachan.Text != null && Userpass_Konachan.Password != null)
            {
                //区分yande和konachan的登录  3.11
                progress_login_konachan.Visibility = Visibility.Visible; 
                bool logined = await httphelper.LoginClient(Username_Konachan.Text, Userpass_Konachan.Password, CS.apiset.apiurisave.KonachanHost);
                progress_login_konachan.Visibility = Visibility.Collapsed; 
                if (logined == true)
                {
                    SettingHelper.IsLoagin_Konachan = "1";
                    SettingHelper.UserPass_Konachan = Userpass_Konachan.Password;
                    KonachanUse_Grid.Visibility = Visibility.Collapsed;
                    KonachanUse_Logined_Grid.Visibility = Visibility.Visible;
                    UseName_Konachan.Text = SettingHelper.Username_Konachan;
                    UseName_id_Konachan.Text = SettingHelper.Userid_Konachan;
                }
                else
                {
                    SettingHelper.IsLoagin_Konachan = "0";
                    progress_login_konachan.Visibility = Visibility.Collapsed;
                }
            }

        }

        private void GobackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
        }

        private void Logout_Yande_Click(object sender, RoutedEventArgs e)
        {
            //清空所有存储的id键值
            SettingHelper.Username_Yande = "";
            SettingHelper.UserPass_Yande = "";
            SettingHelper.IsLoagin_Yande = "0";
        }

        private void Logout_Konachan_Click(object sender, RoutedEventArgs e)
        {
            SettingHelper.Username_Konachan = "";
            SettingHelper.UserPass_Konachan = "";
            SettingHelper.IsLoagin_Konachan = "0";
        }
    }
}
