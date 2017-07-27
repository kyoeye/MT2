using Edi.UWP.Helpers;
using Microsoft.Toolkit.Uwp.UI.Animations;
using MT2.CS;
using MT2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.DataTransfer;
using Windows.Graphics.Display;
using Windows.Graphics.Imaging;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.System.UserProfile;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace MT2.page
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class LookImg : Page
    {
        //TosatModel tosalmodel = new TosatModel();


        public LookImg()
        {
            this.InitializeComponent();
            if (localsettings.Values["_ThisDeviceis"].ToString() == "Mobile")
            {
                MyTitleBarVB.Visibility = Visibility.Collapsed;
            }

            Getsuface();
            my_Image = SeeImage;
            //分享——订阅
            dataTransferManager.DataRequested += DataTransferManger_DataRequestedAsync;
            TagModes = new ObservableCollection<TagMode>();

            //Toastpopup.DataContext = tosalmodel;
            //betatext.Text = System.Windows.Forms.Screen.GetWorkingArea(this);
        }

        #region 将图片下载然后再显示
        public void downloadimg()
        {
           var tempfolder = ApplicationData.Current.TemporaryFolder;

        }
        #endregion

        double wit;
        double hei;
        ApplicationDataContainer localsettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        SETall setall = new SETall();
        public int a;
        public int imgid;
        public string imguri;
        public string img_sample_url;
        public string imgname;
        private string imgLocalpath;
        public static Image my_Image;
        BitmapImage bitmapimage;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Window.Current.SetTitleBar(MyTitleBar);
            base.OnNavigatedTo(e);
         
            #region new
            var type = e.Parameter.GetType();
            if (type.Name == "Yande_post_json")
            {
                var gg = (Yande_post_json)e.Parameter;
                img_sample_url = gg.sample_url;
                BitmapImage bitmapimage = new BitmapImage(new Uri(gg.sample_url));
                SeeImage.Source = bitmapimage;
                bitmapimage.DownloadProgress += Bitmapimage_DownloadProgress;
                imguri = gg.file_url;
                ImageID.Text = gg.id.ToString();
                imgid = int.Parse(gg.id.ToString());
                //获取到一个奇怪的不完整路径
                var bitf = bitmapimage.UriSource.AbsolutePath;
               imgLocalpath = bitmapimage.UriSource.LocalPath;
                // 处理tag
                TagFuntion(gg.tags);
                //   "/sample/78cd441063dd0dab7e88f94ab7ab8cd6/yande.re 395326 sample hatsune_miku lepoule_(kmjh90) vocaloid.jpg"
            }
          else
            {
                #region 旧的
                ItemGET.listsave lookit2 = (ItemGET.listsave)e.Parameter;
                 bitmapimage = new BitmapImage(new Uri(lookit2.sample_url));
                SeeImage.Source = bitmapimage;
                bitmapimage.DownloadProgress += Bitmapimage_DownloadProgress;
                imguri = lookit2.imguri;
                ImageID.Text = lookit2.id;
                imgid = int.Parse(lookit2.id);
                #endregion
            }
            #endregion
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
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            dataTransferManager.DataRequested -= DataTransferManger_DataRequestedAsync;
        }
        #region 对UI绘制
        public async void BlurUiAsync()
        {
            await Backgroundimg.Blur(value: 10, duration: 1076, delay: 1).StartAsync();
        }
        #endregion
        private void Bitmapimage_DownloadProgress(object sender, DownloadProgressEventArgs e)
        {
            Myprogressbar.Value = e.Progress;
            if (Myprogressbar.Value == 100)
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

        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            //下载
            if (   localsettings.Values["_FileAllOpen"].ToString() == "true")
            {
                Savefile();
            }
            else
            {
                Savefile2(imguri);
            }   

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
                await showtast();
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
                //tosalmodel.Info = new Entity() { name = "正在后台下载……" };

                //a.Label = "下载完成";
                //await a.Show();
                //Mypopup.IsOpen = true;
            }
        }//总是调起资源选择器

        private async Task showtast()
        {
            Toast.Visibility = Visibility.Visible;
            this.StoryboardShowPopup.Begin();
            //内容提示停留1.2s后开始隐藏
            await Task.Delay(3000);
            this.StoryboardHiddenPopup.Begin();
            Toast.Visibility = Visibility.Collapsed;
        }

        public async void Savefile2(string jpguri)//不调用自愿选择器
        {
            //Savefile();
            try
            {
                string fileuri = localsettings.Values["_Fileuri"].ToString();
                StorageFolder storagefolder = await StorageFolder.GetFolderFromPathAsync(fileuri);
                //StorageFolder fd = await ApplicationData.Current.LocalFolder.CreateFolderAsync("string", CreationCollisionOption.OpenIfExists);
                //StorageFolder fd2 = await fd.CreateFolderAsync("dd");
                storagefile = await storagefolder.CreateFileAsync(imgname + "ID" + imgid+".jpg", CreationCollisionOption.OpenIfExists);
             
                if (storagefile != null)
                {
                    CachedFileManager.DeferUpdates(storagefile);
                     await  showtast();

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
            catch
            {

            }

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

        private void Copybutton_Click(object sender, RoutedEventArgs e)  //暂时用复制uri替代收藏 //废弃
        {
            DataPackage dp = new DataPackage();
            dp.SetText(imguri);
            Clipboard.SetContent(dp);

        }
        public int b = 0;

        private void Setit_Click(object sender, RoutedEventArgs e)
        {

            if (b % 2 == 0)//抽屉展开
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
        StorageFolder sfd = ApplicationData.Current.TemporaryFolder;
        StorageFile sf;
        //public StorageFile Storagefile { get => sf; set=>sf = value; }

        private async void StartBackground_Click(object sender, RoutedEventArgs e)
        {
            sf = await sfd.CreateFileAsync(imgid + ".jpg", CreationCollisionOption.ReplaceExisting);
            await ImgTransfromAsync();
            //sf = await StorageFile.GetFileFromPathAsync(imgLocalpath);
            UserProfilePersonalizationSettings startsetting = UserProfilePersonalizationSettings.Current;
            bool b = await startsetting.TrySetWallpaperImageAsync (sf);

        }

        private void LockBackground_Click(object sender, RoutedEventArgs e)
        {

            

            //StorageFile file = await StorageFile.GetFileFromApplicationUriAsync();//需要将下载的目录拿到

            UserProfilePersonalizationSettings locksetting = UserProfilePersonalizationSettings.Current;
            

            //bool b =  await locksetting.TrySetLockScreenImageAsync(file);
            // LockBackground.Label = b.ToString ();
        }
        //img转化
        public async Task ImgTransfromAsync()
        {
            if (sf != null )
            {
                CachedFileManager.DeferUpdates(sf);
                RenderTargetBitmap rtb = new RenderTargetBitmap();
           
                await rtb.RenderAsync(SeeImage);
                var pixelBuffer = await rtb.GetPixelsAsync();
                //好想要存个临时文件不然报错
                using (var fileStream = await sf.OpenAsync(FileAccessMode.ReadWrite))
                {
                    var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, fileStream);
                    encoder.SetPixelData
                    (
                    BitmapPixelFormat.Bgra8,
                    BitmapAlphaMode.Ignore,
                    (uint)rtb.PixelWidth,
                    (uint)rtb.PixelHeight,
                    DisplayInformation.GetForCurrentView().LogicalDpi,
                    DisplayInformation.GetForCurrentView().LogicalDpi,
                    pixelBuffer.ToArray()
                    );
                    //刷新图像  
                    await encoder.FlushAsync();
                }
            }
            
        }
        #endregion
        #region 分享


        private StorageFile _tempExportFile;
        DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();

        private async void Share_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            sf = await sfd.CreateFileAsync(imgid + ".jpg", CreationCollisionOption.ReplaceExisting);
            await  ImgTransfromAsync();
            //var rmbp = await LoadWriteableBitmap (@"img\XH(2]5G215ZT4J8X`5XSYHN.jpg");//需要替换成本地图片路径
            var rmbp = await LoadWriteableBitmap(sf.Path);//需要替换成本地图片路径
            StorageFile tempFile = await ApplicationData.Current.TemporaryFolder.CreateFileAsync("TemId" + imgid+".jpg", CreationCollisionOption.ReplaceExisting);
            await rmbp.SaveStorageFile(tempFile);
            _tempExportFile = tempFile;
            DataTransferManager.ShowShareUI();
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.ToString()).ShowAsync();
            }
        }

        private async void DataTransferManger_DataRequestedAsync(DataTransferManager sender, DataRequestedEventArgs args)
        {
            try
            {
                DataPackage requestData = args.Request.Data;
                requestData.Properties.Title = "分享一张心仪的图片";
                requestData.Properties.Description = imguri;//在能调用pixiv之前先传原图链接
              
                requestData.SetText("这是我要共享的图片");//传到QQ这句没效果。。
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

        public static async Task<WriteableBitmap> LoadWriteableBitmap(string relativePath)

        {
            StorageFile storageFile = await StorageFile.GetFileFromPathAsync(relativePath);
            //var storageFile = await Package.Current.InstalledLocation.GetFileAsync(relativePath.Replace('/', '\\'));

            var stream = await storageFile.OpenReadAsync();

            var wb = new WriteableBitmap(1, 1);

            wb.SetSource(stream);

            return wb;

        }
        #endregion
        #region 画中画
        private void Compact_Click(object sender, RoutedEventArgs e)
        {
            //bool modeSwitched = await ApplicationView.GetForCurrentView().TryEnterViewModeAsync(ApplicationViewMode.CompactOverlay);
            //if (modeSwitched = true)
            //{

            //}
            ShowCompactView();

        }
        int compactViewId;
        private async void ShowCompactView()
        {
            await CoreApplication.CreateNewView().Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                var frame = new Frame();
                compactViewId = ApplicationView.GetForCurrentView().Id;
                frame.Navigate(typeof(ShowCompactPage), img_sample_url);
                Window.Current.Content = frame;
                Window.Current.Activate();
                ApplicationView.GetForCurrentView().Title = "CompactOverlay Window";
            });
            bool viewShown = await ApplicationViewSwitcher.TryShowAsViewModeAsync(compactViewId, ApplicationViewMode.CompactOverlay);
        }
        #endregion
        #region Tags
        public ObservableCollection<TagMode> TagModes { get; set; }
        private void TagFuntion(string Tags_all)
        {
            string[] TagArray = Tags_all.Split(' ');
          for( int a=0; a<TagArray.Count() ; a++ )
            {
                TagModes.Add(new TagMode { Tag = TagArray[a] });
            }
            TagTitle.Text = "标签" + "  " + "（共"+TagModes.Count+"个）";
        }
        #endregion
        private void GobackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
        }

        private void TagBorder_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var boxs = sender as Border;
            var box = boxs.DataContext as TagMode;
            Frame.Navigate(typeof(Seach2Page),box);
        }

        private void TastButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(DownloadPage));
        }
    }
    //弹窗
    //public class TosatModel : INotifyPropertyChanged
    //{
    //    public event PropertyChangedEventHandler PropertyChanged;
    //    private Entity _info;
    //    public Entity Info
    //    {
    //        get { return _info; }
    //        set { _info = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Info")); }
    //    }
    //}

}
