using Edi.UWP.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.Email;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.Storage;
using Windows.UI.Popups;
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
    public sealed partial class AboutPage : Page
    {
        Windows.ApplicationModel.Package package = Windows.ApplicationModel.Package.Current;

        public AboutPage()
        {
            this.InitializeComponent();
            Window.Current.SetTitleBar(MyTitleBar);
            buildtext.Text ="版本号:"+package.Id.Version.Major + "." + package.Id.Version.Minor + "." + package.Id.Version.Build + "." + package.Id.Version.Revision;
            if (localsettings.Values["_ThisDeviceis"].ToString() == "Mobile")
            {
                MyTitleBarVB.Visibility = Visibility.Collapsed;
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            SetText();
        }
        private void Gobackbutton_Click(object sender, RoutedEventArgs e)
        {
             if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }
        ApplicationDataContainer localsettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        private void Objectadmin_Click(object sender, RoutedEventArgs e)
        {
        
            localsettings.Values["AdminIsOpen"]=  "YES";
            
        }

        private async void email_i_Click(object sender, RoutedEventArgs e)
        {
      
            try
            {
                //em = new EmailMessage();
               string Subject = "MoeTon反馈";
                string Body = "您礼貌的反馈和建议是吾辈前进的动力poi~ \r\n";
                await Tasks.OpenEmailComposeAsync("ghhhgvchbvc55667@outlook.com", Subject, Body + Readyourdevice());
            }
            catch(Exception ex)
            {
                await new MessageDialog("邮箱发送错误："+ex).ShowAsync();
            }
        
        }

        public string  Readyourdevice()
        {
            var deviceInfo = new EasClientDeviceInformation();
            //var package = Windows.ApplicationModel.Package.Current;
            string _content = $", 设备名：{deviceInfo.FriendlyName}，" +
                  $"操作系统：{deviceInfo.OperatingSystem}，" +
                  $"产品名称：{deviceInfo.SystemProductName}，" +
                  $"制造商：{deviceInfo.SystemManufacturer}，" +
                  $"固件版本：{deviceInfo.SystemFirmwareVersion}， " +
                  $"硬件版本：{deviceInfo.SystemHardwareVersion}，）" +
                  $"DispalyName: " + package.DisplayName +"，"+
                  $"build版本：" + package.Id.Version.Build+ "包版本："+ package.Id.Version.Major + "." + package.Id.Version.Minor + "." + package.Id.Version.Build + "." + package.Id.Version.Revision;
                  
            //原来还有这种操作.jpg
            return _content;

        }

        private void QQ_Click(object sender, RoutedEventArgs e)
        {
            string str = "531234373";
            DataPackage cp = new DataPackage();
            cp.SetText(str);
            Clipboard.SetContent(cp);
            QQ.Content = "已复制，请前往qq粘贴搜索群(微页UWP交流群)";
        }
        #region 显示文字
        private void SetText()
        {
            ResourceLoader rl = new ResourceLoader();
            Feedback_text.Text  = rl.GetString("String31");
            Feedback_tishi.Text = rl.GetString("String32");
        }
        #endregion

    }
}
