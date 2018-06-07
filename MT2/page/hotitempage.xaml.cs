using MT2.Control;
using MT2.CS;
using MT2.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
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
using MT2.CS.apiset;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace MT2.page
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class hotitempage : Page
    {
        #region apis
        string hotapiuri = "https://yande.re/post/popular_recent.json";

        #endregion
        ApplicationDataContainer localsettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        string xmltext;
        public string  XmlText { get { return xmltext; }set { xmltext = value; } } 
        ItemGET itemget = new ItemGET();
        //string mystring;
        public  hotitempage()
        {
            this.InitializeComponent();
            #region 判断api是否支持
            if (VersionHelper.Windows10Build15063 == true)
            {
                Windows.UI.Xaml.Media.AcrylicBrush acrylic = new Windows.UI.Xaml.Media.AcrylicBrush();
                acrylic.TintOpacity = 0.5;
                acrylic.TintColor =  Colors.White;
                acrylic.BackgroundSource = AcrylicBackgroundSource.HostBackdrop;           
                HotGrid.Background = acrylic;
            }
            else
            {
                HotGrid.Background = new SolidColorBrush(  Color.FromArgb (100,215,215,215));
            }
            #endregion

            progressrin.IsActive = true;
            if (localsettings.Values["_ThisDeviceis"].ToString() == "Mobile")
            {
                MyTitleBarVB.Visibility = Visibility.Collapsed;
            }
            if (MTHub.Hotitemvalue != null )
            {
                Mygridview.ItemsSource = MTHub.Hotitemvalue;
                progressrin.IsActive = false;
            }
            else
            {
                Getjsonstring(hotapiuri,0);
            }
            NavigationCacheMode = NavigationCacheMode.Enabled;

            #region 理论上依次加载日，周，月，年
              //Getjsonstring();
            #endregion
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Window.Current.SetTitleBar(MyTitleBar);
            SetText();
            base.OnNavigatedTo(e);
        }

        //public async void getjsontext()
        //{
        
        //    GetAPIstring getxml = new GetAPIstring();
        //    XmlText  = await getxml.GetWebString(hotapiuri );
           
        //    itemssoureGet();
        //}
        //public void itemssoureGet()
        //{
        //    itemget.Toitem(xmltext);
        //    itemget.getlistitems(false);
        //    Mygridview.ItemsSource = itemget.Listapiitems;
            
        //    progressrin.IsActive = false;
        //}
        private void gridstackpanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
                var boxs = sender as StackPanel;
                switch (pivot.SelectedIndex) //目前暂时用pivot排序的方式来做区分，如果以后实现了自定义接口排序，这里要改
                {
                    case 0:
                        var box = boxs.DataContext as Yande_post_json;
                        Frame.Navigate(typeof(LookImg), box);
                        break;
                    case 1:
                        var box2 = boxs.DataContext as Konachan_post_json;
                        Frame.Navigate(typeof(LookImg), box2);
                    break;
                }
        }

        private void GobackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
        }
        #region json接口测试by一周
        //string jsontext;
        GetAPIstring getresponse = new GetAPIstring();

        private async void Getjsonstring(string thehotapiuri ,int pivotindexs)
        {
           string  jsontext = await getresponse.GetWebString(thehotapiuri);
            Setjsonstring(jsontext,pivotindexs );            
        }
        GetJson getjson = new GetJson();
        private void Setjsonstring(string jsontext , int sourceindex)
        {
            //使用Savejson方法将json数据反序列化到存储
            //       getjson.NoH();
            //   Mygridview.ItemsSource = source;

            switch (sourceindex)
            {
                case 0:
                    var source = getjson.SaveJson(jsontext);
                    Mygridview.ItemsSource = source;
                    progressrin.IsActive = false;
                    break;
                case 1:
                    var source2 = getjson.SaveJson_konachan(jsontext);
                    Mygridview2.ItemsSource = source2;
                    break;
                //case 2:
                //    Mygridview3.ItemsSource = source;
                //    break;
                //case 3:
                //    Mygridview4.ItemsSource = source;
                //    break;
            }

        }
        #endregion

        #region Pivot导航

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
 
            Yande.FontSize = 15;
            Konachan.FontSize = 15;
            Yande.Opacity = 0.5;
            Konachan.Opacity = 0.5;
            Yande.FontFamily = new FontFamily("Segoe UI");
            Konachan.FontFamily = new FontFamily("Segoe UI");
 
            switch (pivot.SelectedIndex)
            {
                case 0 :
                    Yande.Opacity = 1;
                    Yande.FontFamily = new FontFamily("Segoe UI Black");
                    break;
                case 1:
                    Konachan.FontFamily = new FontFamily("Segoe UI Black");
                    Konachan.Opacity = 1;
                    if (p1==0)
                    {
                        p1++;
                        Getjsonstring($"{apiurisave.KonachanHotHost}", 1);
                    }
                    break;
    
            }
        }
        #region 记录pivotitem的展示次数
        int p1;
        //int p2;
        //int p3;
        #endregion
        #region 更改日榜周榜月榜等
        private void DanbooruDWMY(int index)
        {   
            switch (index)
            {
                case 0:
                    if (pivot.SelectedIndex == 0)
                    {
                        // Getjsonstring($"{apiurisave.YandeHotHost}", 0);
                        if (MTHub.Hotitemvalue != null)
                        {
                            Mygridview.ItemsSource = MTHub.Hotitemvalue;
                            progressrin.IsActive = false;
                        }
                    }
                        
                     else if (pivot.SelectedIndex == 1)
                    {
                        //  Getjsonstring($"{apiurisave.KonachanHotHost}", 1);
                        if (MTHub.Hotitemvalue != null)
                        {
                            Mygridview2.ItemsSource = MTHub.Hotitemvalue_Konachan;
                            progressrin.IsActive = false;
                        }
                    }
                    
                    break;
                case 1:
                        p1++;
                        if(pivot.SelectedIndex==0)
                              Getjsonstring($"{apiurisave.YandeHotHost}?period=1w",0);
                        else if (pivot.SelectedIndex ==1)
                              Getjsonstring($"{apiurisave.KonachanHotHost}?period=1w", 1);  
                    break;
                case 2:
                        if (pivot.SelectedIndex == 0)
                            Getjsonstring($"{apiurisave.YandeHotHost}?period=1m", 0);
                        else if (pivot.SelectedIndex == 1)
                            Getjsonstring($"{apiurisave.KonachanHotHost}?period=1m", 1);
                    break;
                case 3:
                         if (pivot.SelectedIndex == 0)
                            Getjsonstring($"{apiurisave.YandeHotHost}?period=1y", 0);
                        else if (pivot.SelectedIndex == 1)
                            Getjsonstring($"{apiurisave.KonachanHotHost}?period=1y", 1);         
                    break;
            }

        }

        #endregion
        private void B0_Click(object sender, RoutedEventArgs e)
        {
            DanbooruDWMY(0);
   
        }

        private void B1_Click(object sender, RoutedEventArgs e)
        {
            DanbooruDWMY(1);
 
        }

        private void B2_Click(object sender, RoutedEventArgs e)
        {
            DanbooruDWMY(2);

         //   Getjsonstring(m_Hotapiuri,2 );

        }

        private void B3_Click(object sender, RoutedEventArgs e)
        {
            DanbooruDWMY(3);

            //pivot.SelectedIndex = 3;
            //pivot.SelectedItem = pivot.Items[3];
            //Getjsonstring(y_Hotapiuri,3);
        }
        #endregion
        #region 显示文字
        private void SetText()
        {
            ResourceLoader rl = new ResourceLoader();
            Hot_Title.Text = rl.GetString("Hot_Title");
        }
        #endregion

        private void Konachan_Click(object sender, RoutedEventArgs e)
        {
            pivot.SelectedIndex = 1;
            pivot.SelectedItem = pivot.Items[1];
            DanbooruDWMY(0);
          
        }

        private void Yande_Click(object sender, RoutedEventArgs e)
        {
            pivot.SelectedIndex = 0;
            pivot.SelectedItem = pivot.Items[0];
        //    DanbooruDWMY(0);        

        }
    }
}
