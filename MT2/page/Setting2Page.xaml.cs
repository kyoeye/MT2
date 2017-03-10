using MT2.CS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace MT2.page
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Setting2Page : Page
    {
        ApplicationDataContainer localsettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        public List<ThemeColors> themeColors;
        Fallsclass falclass = new Fallsclass();
        //public int fallshub { get { return falclass.FallsHub; } set { falclass.FallsHub = value; } }
        MainPage mainpage;
        public Setting2Page()
        {
            //Logobackground.Source =  mainpage.Homehoturl;
            
            this.InitializeComponent();
            themeColors = ThemeColorsAdd.GetThemeColors(); //返回主题数据
            falclass.FallsHub = (int)listslider.Value;
            if (localsettings.Values["password"] != null)
            {
                Nowpassword.Text = "当前密码是：" + localsettings.Values["password"].ToString();
                Nowpassword.Visibility = Visibility.Visible;
            }
            else
            {
                Nowpassword.Visibility = Visibility.Collapsed;
            }

        }

        private void PasswordClick_Click(object sender, RoutedEventArgs e)
        {
            if (loagingpassword != null)
            {
                localsettings.Values["password"] = loagingpassword.Password;
                Nowpassword.Text = "当前密码是：" + localsettings.Values["password"].ToString();
                Nowpassword.Visibility = Visibility.Visible;

            }
            else
            {
                localsettings.Values["password"] = null;
                Nowpassword.Visibility = Visibility.Collapsed;

            }
        }


    }
}
