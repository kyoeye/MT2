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
        string hotapiuri = "https://yande.re/post.xml?popular_by_week";
        //string mystring;
        public  hotitempage()
        {
            this.InitializeComponent();
            xmlstring(hotapiuri);

            Mygridview.ItemsSource = _itemget .itemlist;
        }
        
        GetXml getxml = new GetXml();
        ItemGET _itemget = new ItemGET();

        public async  void  xmlstring (string homeimguri)
        {
            //string result;
            var mystring = await GetXml.GetWebString(homeimguri, null);
            _itemget.Toitem(mystring);

            //mystring = await GetXml.GetWebString(homeimguri, null);
            //HttpClient httpclient1 = new HttpClient();
            //HttpResponseMessage httpResponseMessage = await httpclient1.GetAsync(new Uri(homeimguri));
            //httpResponseMessage.EnsureSuccessStatusCode();
            //mystring  = await httpResponseMessage.Content.ReadAsStringAsync();

        }

    }
}
