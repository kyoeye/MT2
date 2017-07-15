using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Storage;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage.Pickers;
using System.Collections.ObjectModel;
using Windows.UI.Popups;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Media.Imaging;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace MT2.page
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class PicinPicPage : Page
    {
      
        public PicinPicPage()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;
            ImgitemOb = new ObservableCollection<Imgitem>();
        }

        private void GobackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
        }

        //private   List<Imgitems> Imgitem = new List<Imgitems>();
        public ObservableCollection<Imgitem> ImgitemOb { get; set; }
        StorageFile fopvalue;
        BitmapImage bi;
        private async void Addclick_ClickAsync(object sender, RoutedEventArgs e)
        {
            try
            {
                FileOpenPicker fop = new FileOpenPicker();
                fop.FileTypeFilter.Add(".jpg");
                fop.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
                fopvalue = await fop.PickSingleFileAsync();
                //拿到缩略图
                bi = new BitmapImage();
                var slt = await fopvalue.GetThumbnailAsync(Windows.Storage.FileProperties.ThumbnailMode.SingleItem, 190, Windows.Storage.FileProperties.ThumbnailOptions.ResizeThumbnail);
                if(slt !=null)
                {
                   await  bi.SetSourceAsync(slt);
                }
                NewMethod(fopvalue,bi);

                tosttextblock.Visibility = Visibility.Collapsed;
            }
            catch
            {
                await new MessageDialog("您没有选择文件，或者文件格式不正确").ShowAsync();
                tosttextblock.Visibility = Visibility.Visible;

            }


        }

        private void NewMethod(StorageFile fopvalue ,BitmapImage Bi )
        {
            ImgitemOb.Add(new Imgitem { Imgpath = fopvalue.Path, imgSamp = Bi , Filename = fopvalue.Name,SF=fopvalue });
        }

        private void Picturegrid_Tapped(object sender, TappedRoutedEventArgs e)
        {            
                var boxs = sender as StackPanel;
                var box = boxs.DataContext as Imgitem;
          
            ShowCompactView(box);
        }
        int compactViewId;

        private async void ShowCompactView(Imgitem imgitem)
        {
            await CoreApplication.CreateNewView().Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                var frame = new Frame();
                compactViewId = ApplicationView.GetForCurrentView().Id;
                frame.Navigate(typeof(ShowCompactPage), imgitem);
                Window.Current.Content = frame;
                Window.Current.Activate();
                ApplicationView.GetForCurrentView().Title = "CompactOverlay Window";
            });
            bool viewShown = await ApplicationViewSwitcher.TryShowAsViewModeAsync(compactViewId, ApplicationViewMode.CompactOverlay);
        }

    }
    public class Imgitem
    {
        public string Imgpath { get; set; }
        public string Filename { get; set; }
        public BitmapImage imgSamp { get; set; }
        public StorageFile SF { get; set; }
    }
}
