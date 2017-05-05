using MT2.CS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
        string hotapiuri = "https://yande.re/post/popular_recent.xml";
        string w_hotapiuri = "https://yande.re/post/popular_recent.xml?period=1w";
        string m_hotapiuri = "https://yande.re/post/popular_recent.xml?period=1m";
        string y_hotapiuri = "https://yande.re/post/popular_recent.xml?period=1y";
        #endregion

        string xmltext;
        public string  XmlText { get { return xmltext; }set { xmltext = value; } } 
        ItemGET itemget = new ItemGET();
        //string mystring;
        public  hotitempage()
        {
            this.InitializeComponent();
            progressrin.IsActive = true;
            getxmltext();
            
            NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Window.Current.SetTitleBar(MyTitleBar);
            base.OnNavigatedTo(e);
        }

        public async void getxmltext()
        {
            GetXml getxml = new GetXml();
            XmlText  = await getxml.GetWebString(hotapiuri );
           
            itemssoureGet();
        }
        public void itemssoureGet()
        {
            itemget.Toitem(xmltext);
            itemget.getlistitems(false);
            Mygridview.ItemsSource = itemget.Listapiitems;
            
            progressrin.IsActive = false;
        }
        private void gridstackpanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var boxs = sender as StackPanel;
            var box = boxs.DataContext as ItemGET.listsave;
            string lookuri = box.sample_url;
            Frame.Navigate(typeof(LookImg), box);
        }

        private void GobackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
        }
        #region json接口测试by一周

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
                    break;
                case 2:
                    B2.FontFamily = new FontFamily("Segoe UI Black");
                    B2.Opacity = 1;
                    break;
                case 3:
                    B3.FontFamily = new FontFamily("Segoe UI Black");
                    B3.Opacity = 1;
                    break;
            }
        }

        private void B0_Click(object sender, RoutedEventArgs e)
        {
            pivot.SelectedIndex = 0;
            pivot.SelectedItem = pivot.Items[0];
        }

        private void B1_Click(object sender, RoutedEventArgs e)
        {
            pivot.SelectedIndex = 1;
            pivot.SelectedItem = pivot.Items[1];
        }

        private void B2_Click(object sender, RoutedEventArgs e)
        {
            pivot.SelectedIndex = 2;
            pivot.SelectedItem = pivot.Items[2];
        }

        private void B3_Click(object sender, RoutedEventArgs e)
        {
            pivot.SelectedIndex = 3;
            pivot.SelectedItem = pivot.Items[3];
        }
        #endregion
    }
}
