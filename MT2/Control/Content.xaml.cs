using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace MT2.Control
{
    public sealed partial class Content : UserControl
    {
        public Content(string webviewuri)
        {
            this.InitializeComponent();
            if (webviewuri != null)
            {
                ContentWebview.Visibility = Visibility.Visible;
                ContentWebview.Navigate(new Uri(webviewuri));
            }
            else
            {
                ContentWebview.Visibility = Visibility.Collapsed;
                ContentWebview.Source = new Uri("http://w.com");

            }
        }
        public string Title { get; set; }
        public string Title2 { get; set; }
        public string Context { get; set; }
        public string Context2 { get; set; }
        public string Imguri { get; set; }
        public string Webviewuri { get; set; }
    
    }
   
}
