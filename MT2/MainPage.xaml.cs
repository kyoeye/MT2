﻿using MT2.CS;
using MT2.page;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Xml.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace MT2
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        string hotapiuri = "https://yande.re/post.xml?limit=100";
        
        public MainPage()
        {
            
            this.InitializeComponent();
         
            NavigationCacheMode = NavigationCacheMode.Enabled;
         
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
           
        }
        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            Mymenu.IsPaneOpen = !Mymenu.IsPaneOpen;
        }

       

        //private void SeachButton_Click(object sender, RoutedEventArgs e)
        //{
        //    Frame.Navigate(typeof(SearchPage));
        //}
        //private void gridstackpanel_Tapped(object sender, TappedRoutedEventArgs e)
        //{
        //    var boxs = sender as StackPanel;
        //    var box = boxs.DataContext as Listapiset;
        //    lookit.lookimguri = box.sample_url;
        //    lookit.b = box._a;
        //    Frame.Navigate(typeof(LookImg), lookit);
        //}

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Setting2Page));
            Mymenu.IsPaneOpen = false;

        }

        private void MenuListboxitem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in e.AddedItems)
            {
                if (item == homepage)
                {
                    Frame.Navigate(typeof(MainPage));
                    Mymenu.IsPaneOpen = false;
                }
                else if (item == hotitem)
                {
                    Frame.Navigate(typeof(hotitempage));
                    Mymenu.IsPaneOpen = false;

                }
            }
        }
    }
}