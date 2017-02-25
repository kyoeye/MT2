using MT2.CS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
        string hotapiuri = "https://yande.re/post/popular_recent.xml";
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
    }
}
