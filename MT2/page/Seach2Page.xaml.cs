using MT2.CS;
using MT2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace MT2.page
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Seach2Page : Page
    {
        ApplicationDataContainer localsettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public Seach2Page()
        {
            this.InitializeComponent();
            Window.Current.SetTitleBar(MyTitleBar);
            if (localsettings.Values["_ThisDeviceis"].ToString() == "Mobile")
            {
                MyTitleBarVB.Visibility = Visibility.Collapsed;
            }
            TagModes = new ObservableCollection<Yande_post_json>();
        }
        #region 变量
        string Mainapiuri = "https://yande.re/post"; // Mainapiuri+ ".xml?limit=" + limit 和 Mainapiuri+ ".json?limit=" + limit
        string Seach_text;

        #endregion
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            try
            {
                if (e.Parameter.GetType().Name == "TagMode")
                {
                    TagMode value = (TagMode)e.Parameter;
                    Seach_text = value.Tag;
                    SeachTextBox.Text = Seach_text;
                    GetSeachResult(Seach_text);
                }
            }
            catch
            {
                NoSeachContent_textblock.Text = "蓄势待发……\r\n（小建议：请使用罗马音或者英文，请尽量输入全称）";
                NoSeachContent_textblock.Visibility = Visibility.Visible;
            }
         
        }
        #region 瀑布流处理
        public ObservableCollection<Yande_post_json> TagModes { get; set; }
        //获取数据
        GetJson getjson = new GetJson();
        GetAPIstring getapistring = new GetAPIstring();
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            progressrin.IsActive = false;
        }
        private async  void GetSeachResult(string Seach_text)
        {
            progressrin.IsActive = true;
            NoSeachContent_textblock.Visibility = Visibility.Collapsed;
            string jsontext = await getapistring.GetWebString(Mainapiuri+ ".json?tags="+Seach_text);
                TagModes = getjson.SaveJson(jsontext);
            Mygridview.ItemsSource = TagModes;
            if (TagModes.Count ==0)
            {
                progressrin.IsActive = false ;

                NoSeachContent_textblock.Text = "当前关键字未搜索到内容，请更换关键字QAQ";
                NoSeachContent_textblock.Visibility = Visibility.Visible;
            }
            else
            {
                progressrin.IsActive = false;

                NoSeachContent_textblock.Visibility = Visibility.Collapsed;
            }
        }
        #endregion
        private void GobackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack) { Frame.GoBack(); }
        }

        private void Loadingbutton_Click(object sender, RoutedEventArgs e)
        {
                
        }

        private void gridstackpanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var boxs = sender as StackPanel;
            var box = boxs.DataContext as Yande_post_json;
            Frame.Navigate(typeof(LookImg), box);

        }

        private void SeachButton_Click(object sender, RoutedEventArgs e)
        {
            Seach_text = SeachTextBox.Text;
            GetSeachResult(Seach_text);
        }
    }
}
