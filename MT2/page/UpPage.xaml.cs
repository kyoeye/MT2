using MT2.CS;
using System.Collections.Generic;
using Windows.ApplicationModel.Resources;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace MT2.page
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class UpPage : Page
    {
        List<Upcontenttext> luc;
        ApplicationDataContainer localsettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        public UpPage()
        {
            this.InitializeComponent();
            Window.Current.SetTitleBar(MyTitleBar);
            Banbenhao.Text = localsettings.Values["_package"].ToString();
            luc = UpContents.GetContent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
        #region 新功能注册
        private void SetNewSetting()
        {
            SettingHelper.IsLoagin_Yande = "0";
            SettingHelper.Userid_Yande = "";
            SettingHelper.Username_Yande = "";
            SettingHelper.UserPass_Yande = "";
            SettingHelper.IsLoagin_Konachan = "0";
            SettingHelper.Userid_Konachan = "";
            SettingHelper.Username_Konachan  = "";
            SettingHelper.UserPass_Konachan = "";
        }
        #endregion
        private void SetText()
        {
            //英文版将不开启h
            //Steins.Visibility = Visibility.Collapsed;
            ResourceLoader rl = new ResourceLoader();
            UpPage_Title.Text = rl.GetString("String36");
        }

    }
    public class UpContents
    {
        public static List<Upcontenttext> GetContent()
        {
            List<Upcontenttext> Luc = new List<Upcontenttext>();
            Luc.Add(new Upcontenttext { text = "对不起。。。由于测试不仔细给大家造成了困扰。。。抽出了一些本该用来复习的时间，紧急修复了一些未登录造成的一系列闪退bug" });
            Luc.Add(new Upcontenttext { text = "【修复】因为未登录造成的闪退bug" });
            Luc.Add(new Upcontenttext { text = "【修复】忘记密码功能重置安全模式不起作用的问题已经修复，它现在可以正常工作了" });
            Luc.Add(new Upcontenttext { text = "【改进】搜索按钮将会在侧边菜单收起时显示" });
            Luc.Add(new Upcontenttext { text = "【改进】移除了安全模式下两个没啥用处的按钮" });
            return Luc;
        }
    }
    public class UpContent
    {
        public string Title { get; set; }
        public string TopPic { get; set; }
    }
    public class Upcontenttext
    {
        public string text { get; set; }
    }

}
