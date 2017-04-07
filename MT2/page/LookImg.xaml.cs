using MT2.Control;
using MT2.CS;
using Microsoft.Toolkit.Uwp.UI.Animations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Windows.System.UserProfile;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using static MT2.Control.MyToastControl;
using static MT2.MainPage;
using Windows.UI.Popups;
using Windows.Storage.Streams;
using Edi.UWP.Helpers;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace MT2.page
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class LookImg : Page
    {
        TosatModel tosalmodel = new TosatModel();


        public LookImg()
        {
            this.InitializeComponent();
            Getsuface();
            //Toastpopup.DataContext = tosalmodel;
            //betatext.Text = System.Windows.Forms.Screen.GetWorkingArea(this);
        }
        double wit;
        double hei;

        SETall setall = new SETall();
        public int a;
        public int imgid;
        public string imguri;
        public string imgname;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ItemGET.listsave lookit2 = (ItemGET.listsave)e.Parameter;
            BitmapImage bitmapimage = new BitmapImage(new Uri(lookit2.sample_url));
            SeeImage.Source = bitmapimage;
            bitmapimage.DownloadProgress += Bitmapimage_DownloadProgress;
            imguri = lookit2.sample_url ;
             ImageID.Text  =  lookit2.id;
            imgid =int.Parse ( lookit2.id);
            #region 旧的
            //try
            //{
            //    base.OnNavigatedTo(e);
            //    Lookimgclass lookit = (Lookimgclass)e.Parameter;
            //    var sample_url = lookit.lookimguri;

            //    //var value = (string)e.Parameter;
            //    //setall.sample_url = value;
            //    BitmapImage bitmapimage = new BitmapImage(new Uri(sample_url));
            //    bitmapimage.DownloadProgress += Bitmapimage_DownloadProgress;
            //    SeeImage.Source = bitmapimage;
            //    a = lookit.b;             
            //    imguri = lookit.jpegurl[a];
            //    imgname = lookit.thisname[a];
            //    imgid = int.Parse(lookit._id[a]);
            //}
            //catch
            //{

            //}
#endregion
        }
 
        #region 对UI绘制
        public async void BlurUiAsync()
        {
            await Backgroundimg.Blur(value: 10, duration:1076, delay: 1).StartAsync();
        }
        #endregion
        private void Bitmapimage_DownloadProgress(object sender, DownloadProgressEventArgs e)
        {
           Myprogressbar .Value  = e.Progress;
            if (Myprogressbar .Value == 100)
            {
                Myprogressbar.Visibility = Visibility.Collapsed;
                BlurUiAsync();

            }
        }

        public void Getsuface()
        {
            var f = Window.Current.Bounds;
            wit = f.Width;
            hei = f.Height;
            //betatext.Text = "宽度" + wit + "--高度：" + hei;
            betaborder.Height = hei;
        }

        private void textbutoon_Click(object sender, RoutedEventArgs e)
        {
            textbutoon.Content = imguri;
            string text = (string)imguri;
            DataPackage dp = new DataPackage();
            dp.SetText(text);
            Clipboard.SetContent(dp);

        }

        #region 后台下载方法
        private string DownloadToastText { get; set; }

        private    void DownloadButton_Click(object sender, RoutedEventArgs e)
        {         
            //下载
            Savefile();

        }
        public StorageFile storagefile;
        public async void Savefile()
        {
            FileSavePicker savefile = new FileSavePicker();

            

            savefile.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            string f = "保存文件类型";
            savefile.FileTypeChoices.Add(f, new List<string>() { ".jpg", ".png", ".bmp" });
            savefile.SuggestedFileName = imgname + "ID" + imgid;
            storagefile = await savefile.PickSaveFileAsync();
            //var a = new ToastDialog();

            if (storagefile != null)
            {
                //DownloadToastText = "正在后台下载……";
                //a.Label = DownloadToastText;

                //await a.Show();
              

                CachedFileManager.DeferUpdates(storagefile);

                string Filename = imgname + imgid;
                string _transferUri = imguri;
                Uri transferUri;
                try
                {
                    transferUri = new Uri(Uri.EscapeUriString(_transferUri), UriKind.RelativeOrAbsolute);
                }
                catch
                {
                    return;
                }

                BackgroundDownloader backgrounddownloader = new BackgroundDownloader();//后台下载
                DownloadOperation downloader = backgrounddownloader.CreateDownload(transferUri, storagefile);
                await downloader.StartAsync();
                tosalmodel.Info = new Entity() { name = "正在后台下载……" };
                //a.Label = "下载完成";
                //await a.Show();
                //Mypopup.IsOpen = true;
            }
        }
        public async void getjpg(string jpguri)
        {
            Savefile();
            if (storagefile != null)
            {
                CachedFileManager.DeferUpdates(storagefile);

                string Filename = imgname + imgid;
                string _transferUri = jpguri;
                Uri transferUri;
                try
                {
                    transferUri = new Uri(Uri.EscapeUriString(_transferUri), UriKind.RelativeOrAbsolute);
                }
                catch
                {
                    return;
                }

                BackgroundDownloader backgrounddownloader = new BackgroundDownloader();//后台下载
                DownloadOperation downloader = backgrounddownloader.CreateDownload(transferUri, storagefile);
                await downloader.StartAsync();
            }
            //HttpClient httpclient = new HttpClient();
            //HttpResponseMessage httpResponseMessage = await httpclient.GetAsync(new Uri(jpguri));

        }
        #endregion
        #region 动画事件
        private void AppBarButton_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            storyboard1.Begin();
        }
        private void AppBarButton_PointerExited_1(object sender, PointerRoutedEventArgs e)
        {
            storyboard2.Begin();
        }

        #endregion

        private void Copybutton_Click(object sender, RoutedEventArgs e)  //暂时用复制uri替代收藏
        {
            DataPackage dp = new DataPackage();
            dp.SetText(imguri);
            Clipboard.SetContent(dp);

        }
       public int b = 0;

        private void Setit_Click(object sender, RoutedEventArgs e)
        {            

            if (b%2 == 0)//抽屉展开
            {
                b++;
                OpenSit.Begin();
            }
            else
            {
                b++;

                OpenSitEnd.Begin();

            }
        }
        #region 设置壁纸
        private void StartBackground_Click(object sender, RoutedEventArgs e)
        {
            UserProfilePersonalizationSettings startsetting = UserProfilePersonalizationSettings.Current;

        }
        private  void LockBackground_Click(object sender, RoutedEventArgs e)
        {
            //StorageFile file = await StorageFile.GetFileFromApplicationUriAsync();//需要将下载的目录拿到

            UserProfilePersonalizationSettings locksetting = UserProfilePersonalizationSettings.Current;

          
           //bool b =  await locksetting.TrySetLockScreenImageAsync(file);
           // LockBackground.Label = b.ToString ();
        }

        #endregion
        #region 分享

    
        private StorageFile _tempExportFile;

        private async  void Share_Click(object sender, RoutedEventArgs e)
        {
            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += DataTransferManger_DataRequestedAsync;
            var rmbp = await Utils.LoadWriteableBitmap(imguri);//需要替换成本地图片路径
            StorageFile tempFile = await ApplicationData.Current.TemporaryFolder.CreateFileAsync("TemId"+imgid, CreationCollisionOption.ReplaceExisting);
            await rmbp.SaveStorageFile(tempFile);

        }

        private async void DataTransferManger_DataRequestedAsync(DataTransferManager sender, DataRequestedEventArgs args)
        {
            try
            {
                DataPackage requestData = args.Request.Data;
                requestData.Properties.Title = "分享一张心仪的图片";
                requestData.Properties.Description = imguri;//在能调用pixiv之前先传原图链接

                List<IStorageItem> imageItems = new List<IStorageItem> { _tempExportFile };
                requestData.SetStorageItems(imageItems);

                RandomAccessStreamReference imageStreamRef = RandomAccessStreamReference.CreateFromFile(_tempExportFile);
                requestData.Properties.Thumbnail = imageStreamRef;
                requestData.SetBitmap(imageStreamRef);

            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message, @"啊勒Σ(っ °Д °;)っ").ShowAsync();
            }

        }

        #endregion
    }
    //弹窗
    public class TosatModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Entity _info;
        public Entity Info
        {
            get { return _info; }
            set { _info = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Info")); }
        }
    }

}
