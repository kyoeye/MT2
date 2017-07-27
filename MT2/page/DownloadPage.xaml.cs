using MT2.CS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;
using Windows.UI.Popups;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace MT2.page
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class DownloadPage : Page
    {
        ApplicationDataContainer localsettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public DownloadPage()
        {
            this.InitializeComponent();
            if (localsettings.Values["_ThisDeviceis"].ToString() == "Mobile")
            {
                MyTitleBarVB.Visibility = Visibility.Collapsed;
            }
        }

        private  List<DownloadOperation> DownloadList; //活动的下载任务对象 
        private ObservableCollection<TransferModel> transfers = new ObservableCollection<TransferModel>(); // 下载任务的集合信息
        private CancellationTokenSource cancelToken = new CancellationTokenSource();//用于取消操作

        protected async  override void OnNavigatedTo(NavigationEventArgs e)
        {
            TransferList.ItemsSource = transfers;
            Window.Current.SetTitleBar(MyTitleBar);

            //StorageFolder sf1 = await ApplicationData.Current.LocalFolder.CreateFileAsync() //获取下载位置已读取已下载的信息
            await DiscoverActiveDownloadsAsync(); //获取正在下载的任务信息

        }

        private async  Task DiscoverActiveDownloadsAsync()
        {
            DownloadList = new List<DownloadOperation>();
            IReadOnlyList<DownloadOperation> downloads = null;
            //获取正在下载的任务
            downloads = await BackgroundDownloader.GetCurrentDownloadsAsync();
         
            if ( downloads .Count>0)
            {
                NoTask.Visibility = Visibility.Collapsed;
                List<Task> tasks = new List<Task>();
                foreach (DownloadOperation download in downloads)
                {
                    tasks.Add(HandleDownloadAsync(download ,false));
                }
                await Task.WhenAll(tasks);//等待所有任务完成
            }
            else 
            {
                NoTask.Visibility = Visibility.Visible;

            }
        }
        //处理正在下载的任务
        private async Task HandleDownloadAsync(DownloadOperation download, bool v)
        {
            try
            {
                TransferModel transfer = new TransferModel();
                transfer.DownloadOperation = download;
                transfer.Source = download.RequestedUri.ToString();
                transfer.Destination = download.ResultFile.Path;
                transfer.BytesReceived = download.Progress.BytesReceived;
                transfer.TotalBytesToReceive = download.Progress.TotalBytesToReceive;
                transfer.Progress = 0;
                transfers.Add(transfer);
                //当下载进度发生变化时的回调函数

                Progress<DownloadOperation> progressCallback = new Progress<DownloadOperation>(downloadprogressAsync);
                //监听已存在的后台下载任务
                await download.AttachAsync().AsTask(cancelToken.Token, progressCallback);
                ResponseInformation response = download.GetResponseInformation();

            }
            catch (TaskCanceledException)
            {
                await new MessageDialog("任务取消："  ).ShowAsync();

            }
            catch (Exception ex)
            {
                await new MessageDialog("处理下载任务失败：" + ex).ShowAsync();
            }
            finally
            {
                transfers.Remove(transfers.First(p => p.DownloadOperation == download));
                DownloadList.Remove(download);
                NoTask.Visibility = Visibility.Visible;
            }

        }

        private   void downloadprogressAsync(DownloadOperation download)
        {
            try
            {
                TransferModel transfer = transfers.First(p => p.DownloadOperation == download);
                transfer.Progress = (int)((download.Progress.BytesReceived * 100) / download.Progress.TotalBytesToReceive);
                transfer.BytesReceived = download.Progress.BytesReceived;
                transfer.TotalBytesToReceive = download.Progress.TotalBytesToReceive;
            }
            catch 
            {
                //await new MessageDialog("更新进度异常").ShowAsync();
            }
        }

        private void GobackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
        }
    }
}
