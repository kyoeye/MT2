using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
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
    public sealed partial class SettingPage : Page
    {
        public SettingPage()
        {
            this.InitializeComponent();
        }

        private void qqclick_Click(object sender, RoutedEventArgs e)
        {

            string text = (string)qqclick.Content;
            /*用了数据绑定的呀  先找控件  找到控件就找到内容了*/
            DataPackage dp = new DataPackage();
            dp.SetText(text);
            Clipboard.SetContent(dp);
            qqclick.Content = "已复制到剪贴板，请转至qq粘贴搜索";

        }

        private async void betaclick_Click(object sender, RoutedEventArgs e)
        {
            //Frame.Navigate(typeof(LookImg));

            var applicationdata = ApplicationData.Current.TemporaryFolder;
            var Content = applicationdata.Provider;
            if (Content != null)
            {
                await applicationdata.DeleteAsync();//不可用
            }
        }
    }
}
