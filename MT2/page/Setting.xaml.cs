using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace MT2.page
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Setting : Page
    {
        public Setting()
        {
            this.InitializeComponent();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().BackRequested += ddbacke;
        }
        private void ddbacke(object sender, BackRequestedEventArgs e)
        {
           if (settingframe .CanGoBack)
            {
                settingframe.GoBack();
                e.Handled = true;
                SystemNavigationManager.GetForCurrentView().BackRequested -= ddbacke;

            }
        }

        //private void aboutButton_Click(object sender, RoutedEventArgs e)
        //{
        //    storyboard1.Begin();        
        //}


        private void myListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
              foreach (var item in e.AddedItems )
            {
                if (item == aboutButton)
                {
                    storyboard1.Begin();
                }
                else if (item == allButton)
                {
                    settingframe.Navigate(typeof(Setting2));
                }
            }
        }
    }
}
