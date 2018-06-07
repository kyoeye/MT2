using MT2.Control;
using MT2.CS;
using MT2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
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
    public sealed partial class Seach2Page : Page
    {
        ApplicationDataContainer localsettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public Seach2Page()
        {
            this.InitializeComponent();
            #region 判断api是否支持
            if (VersionHelper.Windows10Build15063 == true)
            {
                Windows.UI.Xaml.Media.AcrylicBrush acrylic = new Windows.UI.Xaml.Media.AcrylicBrush();
                acrylic.TintOpacity = 0.5;
                acrylic.TintColor = Colors.White;
                acrylic.BackgroundSource = AcrylicBackgroundSource.HostBackdrop;
                MyTitleBar.Style = (Style)Application.Current.Resources["GridBackgroud"];
                SuggestBackgroud.Style = (Style)Application.Current.Resources["GridBackgroud"];
            }
            else
            {
                MyTitleBar.Background = new SolidColorBrush(Color.FromArgb(100, 244, 244, 244));
                SuggestBackgroud.Background = new SolidColorBrush(Color.FromArgb(100, 244, 244, 244));
            }
            #endregion

            NavigationCacheMode = NavigationCacheMode.Enabled;
            SetText();
            if (localsettings.Values["_ThisDeviceis"].ToString() == "Mobile")
            {
                MyTitleBarVB.Visibility = Visibility.Collapsed;
            }
            TagModes = new ObservableCollection<Yande_post_json>();
            Tag_yande = new ObservableCollection<CS.Model.Yande_Tage>();

        }
        #region 变量
        int page = 1;
        string Seach_text;

        #endregion
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Window.Current.SetTitleBar(MyTitleBar);
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
                NoSeachContent_textblock.Text = "蓄势待发……\r\n（小建议：输入时等待一下会有关键字联想出现）";
                NoSeachContent_textblock.Visibility = Visibility.Visible;
            }
            if(TagModes!=null)
            {
                NoSeachContent_textblock.Visibility = Visibility.Collapsed;
            }
         
        }
        #region 获取搜索数据 处理
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
            string jsontext = await getapistring.GetWebString(CS.apiset.apiurisave.YandeHost+ "post.json?tags="+Seach_text);
                TagModes = getjson.SaveJson(jsontext);
       //     getjson.NoH();

            //if( getjson.list.Count == 40)
            //{

            //}
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
        //加载更多
        private async void Loadingbutton_Click_1(object sender, RoutedEventArgs e)
        {
            page++;
           await   LoadingAsync();

        }
        private async Task LoadingAsync()
        {
            var loadingresponse = await getapistring.GetWebString(CS.apiset.apiurisave.YandeHost + "post.json?tags=" + Seach_text + "&page=" + page);
            if (loadingresponse.Length > 3)
            {
                getjson.Loadingitem(loadingresponse, 40);
             //   getjson.NoH();

                Mygridview.ItemsSource = TagModes;
            }
            else
            {
                await new MessageDialog("没有更多了").ShowAsync();
            }

            return;
        }
        #endregion
        private void GobackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack) { Frame.GoBack(); }
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
            page = 1;
            GetSeachResult(Seach_text);
        }
        #region 显示文字
        private void SetText()
        {
            ResourceLoader rl = new ResourceLoader();
            Seach_Title.Text = rl.GetString("String21");
            SeachTextBox.PlaceholderText = rl.GetString("String22");
          
        }
        #endregion
        #region 搜索
        private ObservableCollection<CS.Model.Yande_Tage> Tag_yande { get ; set; }
       
        private void SeachTextBox2_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
           Seach_text = SeachTextBox.Text;
            GetTagAsync();
            SeachTextBox.ItemsSource = Tag_yande;
        }
        private void SeachTextBox2_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            page = 1;
            GetSeachResult(args.QueryText);
        }
        private async void GetTagAsync()
        {
            string jsontext = await getapistring.GetWebString($"{CS.apiset.apiurisave.YandeHost}/tag.json?name="+Seach_text);
              Tag_yande =  getjson.SaveJson_Tag_yande(jsontext);
        }
        private void SeachTextBox2_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
           var s = args.SelectedItem as CS.Model.Yande_Tage;
            SeachTextBox.Text = s.name.Replace ("#",null);
        }


        #endregion

    }
}
