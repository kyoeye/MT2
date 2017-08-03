using MT2.CS;
using MT2.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
        string w_Hotapiuri = "https://yande.re/post/popular_recent.json?period=1w";
        string m_Hotapiuri = "https://yande.re/post/popular_recent.json?period=1m";
        string y_Hotapiuri = "https://yande.re/post/popular_recent.json?period=1y";
        #endregion
        ApplicationDataContainer localsettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        string xmltext;
        public string  XmlText { get { return xmltext; }set { xmltext = value; } } 
        ItemGET itemget = new ItemGET();
        //string mystring;
        public  hotitempage()
        {
            this.InitializeComponent();
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
            var boxx = boxs.DataContext as Yande_post_json;
            string lookurii = boxx.sample_url;
            Frame.Navigate(typeof(LookImg), boxx);
        }

        private void GobackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
        }
        #region json接口测试by一周
        //string jsontext;
        private async void Getjsonstring(string thehotapiuri ,int pivotindexs)
        {
            GetAPIstring getjson = new GetAPIstring();
           string  jsontext = await getjson.GetWebString(thehotapiuri);
            Setjsonstring(jsontext,pivotindexs );            
        }
        GetJson getjson = new GetJson();
        private void Setjsonstring(string jsontext , int pivotindex)
        {
            //使用Savejson方法将json数据反序列化到存储
           var source = getjson.SaveJson(jsontext);
            switch (pivotindex )
            {
                case 0:
                    Mygridview.ItemsSource = source;
                    progressrin.IsActive = false;
                    break;
                case 1:
                    Mygridview2.ItemsSource = source;
                    break;
                case 2:
                    Mygridview3.ItemsSource = source;
                    break;
                case 3:
                    Mygridview4.ItemsSource = source;
                    break;
            }

        }
        #endregion

        #region Pivot导航

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            B0.Content = "每日";
            B1.Content = "每周";
            B2.Content = "每月";
            B3.Content = "每年";
            B0.FontSize = 15;
            B1.FontSize = 15;
            B2.FontSize = 15;
            B3.FontSize = 15;
            B0.Opacity = 0.5;
            B1.Opacity = 0.5;
            B2.Opacity = 0.5;
            B3.Opacity = 0.5;
                    B0.FontFamily = new FontFamily("Segoe UI");
                    B1.FontFamily = new FontFamily("Segoe UI");
                    B2.FontFamily = new FontFamily("Segoe UI");
                    B3.FontFamily = new FontFamily("Segoe UI");
            //B0.Foreground = new SolidColorBrush(Color.FromArgb(225, 128, 128, 128));
            //B1.Foreground = new SolidColorBrush(Color.FromArgb(225, 128, 128, 128));
            //B2.Foreground = new SolidColorBrush(Color.FromArgb(225, 128, 128, 128));
            //B3.Foreground = new SolidColorBrush(Color.FromArgb(225, 128, 128, 128));

            switch (pivot.SelectedIndex)
            {
                case 0 :
                    B0.Opacity = 1;
                    B0.FontFamily = new FontFamily("Segoe UI Black");
                    break;
                case 1:
                    B1.FontFamily = new FontFamily("Segoe UI Black");
                    B1.Opacity = 1;
                    if (p1==0)
                    {
                        p1++;
                        Getjsonstring(w_Hotapiuri, 1);
                    }
                    break;
                case 2:
                    B2.FontFamily = new FontFamily("Segoe UI Black");
                    B2.Opacity = 1;
                    if (p2 ==0)
                    {
                        p2++;
                        Getjsonstring(m_Hotapiuri, 2);
                    }
                    break;
                case 3:
                    B3.FontFamily = new FontFamily("Segoe UI Black");
                    B3.Opacity = 1;
                    if(p3 ==0)
                    {
                        p3++;
                        Getjsonstring(y_Hotapiuri, 3);

                    }
                    break;
            }
        }
        #region 记录pivotitem的展示次数
        int p1;
        int p2;
        int p3;
        #endregion
        private void B0_Click(object sender, RoutedEventArgs e)
        {
            pivot.SelectedIndex = 0;
            pivot.SelectedItem = pivot.Items[0];
        }

        private void B1_Click(object sender, RoutedEventArgs e)
        {
            pivot.SelectedIndex = 1;
            pivot.SelectedItem = pivot.Items[1];
            Getjsonstring(w_Hotapiuri,1);
        }

        private void B2_Click(object sender, RoutedEventArgs e)
        {
            pivot.SelectedIndex = 2;
            pivot.SelectedItem = pivot.Items[2];
            Getjsonstring(m_Hotapiuri,2 );

        }

        private void B3_Click(object sender, RoutedEventArgs e)
        {
            pivot.SelectedIndex = 3;
            pivot.SelectedItem = pivot.Items[3];
            Getjsonstring(y_Hotapiuri,3);
        }
        #endregion
    }
}
