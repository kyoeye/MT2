using MT2.CS;
using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using MT2.Control;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace MT2.page
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Setting2Page : Page
    {
        ApplicationDataContainer localsettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        public List<ThemeColors> themeColors;
        Fallsclass falclass = new Fallsclass();
        public bool TackToJS_bool;
        StorageFolder a = KnownFolders.SavedPictures;//获取图片保存目录
        //public int fallshub { get { return falclass.FallsHub; } set { falclass.FallsHub = value; } }
        //MainPage mainpage;
        CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
        //彩蛋
        private TranslateTransform tt;
        public Setting2Page()
        {
            //Logobackground.Source =  mainpage.Homehoturl;

            this.InitializeComponent();
            tt = new TranslateTransform();
            logo.RenderTransform = tt;
            Window.Current.SetTitleBar(MyTitleBar);
            Listslider = (int)listslider.Value;
            //coreTitleBar.ExtendViewIntoTitleBar = false;
            //DefualtFilebutton.Content = @"默认保存至系统目录的”保存的图片“";

            themeColors = ThemeColorsAdd.GetThemeColors(); //返回主题数据

            falclass.FallsHub = (int)listslider.Value;
            //按下默认下载位置的按钮   

            opennum.Text = localsettings.Values["_AppOpenNum"].ToString();

            if (localsettings.Values["_Fileuri"].ToString() != a.Path)
            {
                DefualtFilebutton.IsChecked = true;

            }

            if (localsettings.Values["_password"] != null)
            {
                Nowpassword.Text = "当前密码是：" + localsettings.Values["_password"].ToString();
                Nowpassword.Visibility = Visibility.Visible;
            }
            else
            {
                Nowpassword.Visibility = Visibility.Collapsed;
            }


        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            try
            {
                //修改保存路径
                if (localsettings.Values["_Fileuri"].ToString() == a.Path)
                {
                    DefualtFilebutton.Content = "勾选以启用自定义路径";
                    Picksacefile.IsEnabled = false;
                    FileUri.Text = @"系统“保存的图片”文件夹" + localsettings.Values["_Fileuri"].ToString();
                }
                else
                {
                    DefualtFilebutton.Content = "取消勾选以恢复默认保存路径";
                    Picksacefile.IsEnabled = true;
                    //DefualtFilebutton.Content  = localsettings.Values["_Fileuri"].ToString();
                    FileUri.Text = "当前保存的文件夹" + localsettings.Values["_Fileuri"].ToString();
                }

                //是否使用js获取数据
                if ((bool)localsettings.Values["_TackToJS"] == true)
                {
                    TackToJS_bool = true ;
                    TackToJS.IsOn = true;
                }
                else
                {
                    TackToJS_bool = false;
                    TackToJS.IsOn = false;
                    //localsettings.Values["_TackToJS"] = false;
                }

                //总是拉取文件选取器
                if (localsettings.Values["_FileAllOpen"].ToString() == "true")
                {
                    FileAllOpen.IsChecked = true;
                }
                else
                {
                    FileAllOpen.IsChecked = false;
                }
                //是否显示里区开关
                if ((bool)localsettings.Values ["_EggVisble"] == true)
                {
                    EggGrid.Visibility = Visibility.Visible;
                }
                else
                {
                    EggGrid.Visibility = Visibility.Collapsed;

                }
            }
            catch
            {

            }
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            localsettings.Values["_listslider"] = (int)listslider.Value;
           
        }
        #region 访问器们\(￣︶￣*\))
        int _listslider;

        public int Listslider { get => _listslider; set => _listslider = value; }

        #endregion
        #region Ui状态

        #endregion

        //protected override void OnNavigatedFrom(NavigationEventArgs e)
        //{
        //    coreTitleBar.ExtendViewIntoTitleBar = true;
        //}

        private void App_BackRequested(object sender, BackRequestedEventArgs e)
        {
            Frame.GoBack();
        }

        private void PasswordClick_Click(object sender, RoutedEventArgs e)
        {

            if (loagingpassword.Password.Length != 0)
            {
                localsettings.Values["_password"] = loagingpassword.Password;
                Nowpassword.Text = "当前密码是：" + localsettings.Values["_password"].ToString();
                Nowpassword.Visibility = Visibility.Visible;

            }
            else
            {
                localsettings.Values["_password"] = null;
                Nowpassword.Visibility = Visibility.Collapsed;
            }
        }

        private void Abutopagebutton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AboutPage));
        }

        //private void loagingpassword_PasswordChanged(object sender, RoutedEventArgs e)
        //{

        //}

        private void SettingGoback_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private async void beta1button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var file = await a.CreateFileAsync("萌豚保存目录（测试）");
            }
            catch
            {
                await new MessageDialog("文件已存在").ShowAsync();
            }



        }
        private async void beta2button_Click(object sender, RoutedEventArgs e)
        {
            FolderPicker fop = new FolderPicker();
            fop.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            fop.ViewMode = PickerViewMode.List;
            fop.FileTypeFilter.Add("*");
            var f = await fop.PickSingleFolderAsync();

            beta2button.Content = f.Path;

        }


        private async void Picksacefile_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                FolderPicker fop = new FolderPicker();
                fop.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
                fop.ViewMode = PickerViewMode.List;
                fop.FileTypeFilter.Add("*");
                var f = await fop.PickSingleFolderAsync();
                localsettings.Values["_Fileuri"] = f.Path;
                DefualtFilebutton.Content = "取消勾选以恢复默认保存路径";
                FileUri.Text = "当前保存的文件夹" + localsettings.Values["_Fileuri"].ToString();
                //DefualtFilebutton.Content = localsettings.Values["_Fileuri"].ToString();
            }
            //catch  (Exception ex)
            //{
            //    await new MessageDialog("抓到异常~"+ex).ShowAsync();

            //}
            catch
            {
                await new MessageDialog("您没有选择文件路径哦~").ShowAsync();
                if (localsettings.Values["_Fileuri"].ToString() == a.Path)
                {
                    DefualtFilebutton.IsChecked = false;

                }
            }
            #region 备份
            ////private async void Picksacefile_Click(object sender, RoutedEventArgs e)
            ////{
            ////    try
            ////    {
            ////        Savefile.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            ////        Savefile.FileTypeChoices.Add("保存地址选择", new List<string>() { ".jpg" });
            ////        Savefile.SuggestedFileName = "01";
            ////        var storagefile = await Savefile.PickSaveFileAsync();
            ////        var c = storagefile.GetParentAsync();
            ////        localsettings.Values["_Fileuri"] = storagefile.GetParentAsync().ToString();
            ////        //storagefile.GetParentAsync().Close();
            ////        if (storagefile.GetParentAsync().Status == Windows.Foundation.AsyncStatus.Completed)
            ////        {
            ////            await storagefile.DeleteAsync();
            ////            //storagefile.DeleteAsync().Close();
            ////        }
            ////        FileUri.Text = localsettings.Values["_Fileuri"].ToString();
            ////    }
            ////    catch (Exception ex)
            ////    {
            ////        await new MessageDialog("异常" + ex).ShowAsync();
            ////    }
            ////    //catch (Exception)
            ////    //{
            ////    //    await new MessageDialog("您没有选择文件路径哦~").ShowAsync();
            ////    //}

            ////}

            #endregion
        }

        #region 处理保存地址
        private void DefualtFilebutton_Checked(object sender, RoutedEventArgs e)
        {
            if (localsettings.Values["_Fileuri"].ToString() == a.Path)
            {
                DefualtFilebutton.Content = "取消勾选以恢复默认保存路径";
                Picksacefile.IsEnabled = true;
                Picksacefile_Click(null, null);
            }

            //Picksacefile.IsEnabled = false;
            //DefualtFilebutton.Content = @"图片默认保存在”保存图片“";
        }
        #endregion

        private void DefualtFilebutton_Unchecked(object sender, RoutedEventArgs e)
        {
            Picksacefile.IsEnabled = false;
            localsettings.Values["_Fileuri"] = a.Path;
            FileUri.Text = @"系统“保存的图片”文件夹" + localsettings.Values["_Fileuri"].ToString();
            DefualtFilebutton.Content = "勾选以启用自定义路径";
        }
        #region 是否每次保存都询问保存地址
        private void FileAllOpen_Checked(object sender, RoutedEventArgs e)
        {
            localsettings.Values["_FileAllOpen"] = "true";
        }

        private void FileAllOpen_Unchecked(object sender, RoutedEventArgs e)
        {
            localsettings.Values["_FileAllOpen"] = "false";
        }

        #endregion

        private void TackToJS_Toggled(object sender, RoutedEventArgs e)
        {
           
           
            if (TackToJS.IsOn == true)
            {
                localsettings.Values["_TackToJS"] = true;
            }
            else
            {
                localsettings.Values["_TackToJS"] = false;

            }
            if ((bool)localsettings.Values["_TackToJS"] != TackToJS_bool)
            {
                showmessAsync();

            }


        }
        private async void  showmessAsync()
        {
            
            try
            {
                var messagedialog = new MessageDialog("数据发生更改，您需要手动重启下应用");
                messagedialog.Commands.Add(new UICommand("关闭", cmd => { }, commandId: 0));
                messagedialog.DefaultCommandIndex = 0;
                var a = await messagedialog.ShowAsync();

                a.Invoked += await chonshi();
            }
            catch
            {

            }
        }

        private Task<UICommandInvokedHandler> chonshi()
        {
            App.Current.Exit();
            return null;
        }

        ContentDialog cd;
        int c = 1;
        private void Image_ManipulationDelta(object sender, Windows.UI.Xaml.Input.ManipulationDeltaRoutedEventArgs e)
        {
            tt.X += e.Delta.Translation.X;
            tt.Y += e.Delta.Translation.Y;
            if (Math.Abs(tt.X) > Window.Current.Bounds.Width  )
            {
                LUXUN.Visibility = Visibility.Visible;
                try
                {
                            if (c ==1)
                    {
                        c++;
                        showContentDialog();
                        
                    }

                }
                catch
                {

                }
            }

        }

        private async void showContentDialog()
        {         
            try
            {
                cd = new ContentDialog()
                {
                    Title = "为什么会这样……",
                    Content = new Content() {
                        Title = "明明藏得这么好……",
                        Context = "为什么会变成这样呢……\r\n第一次找到了藏彩蛋的地方\r\n第一次做到了自己都发现不了。\r\n这两件愉快的事情交织在了一起\r\n而这两份喜悦\r\n又会给我带来许许多多的喜悦。\r\n我本应该获得了这种如梦一般的幸福时光才对。\r\n可是，为什么\r\n会变成现在这样呢……",
                        Title2 = "为什么你这么熟练……",
                        Context2 ="你竟然能发现这里。。\r\n为什么你那么熟练。。\r\n那。。\r\n你是不是在期待什么\r\n我知道你在期待什么\r\n新世界的大门已经打开\r\n记得注意身体。。"
                       
                    },                
                    PrimaryButtonText = "打死",
                    FullSizeDesired = true,
                };
                cd.PrimaryButtonClick += (_s, _e) => {
                    EggGrid.Visibility = Visibility.Visible;
                    localsettings.Values["_EggVisble"] = true;
                };
                await cd.ShowAsync();
            }
            catch
            {

            }
        }

        private void NoH_Check_Toggled(object sender, RoutedEventArgs e)
        {
            if (NoH_Check.IsOn)
            {
                localsettings.Values["_H"] = true;
            }
            else 
            {
                localsettings.Values["_H"] = false;
            }
        }
    }
}
