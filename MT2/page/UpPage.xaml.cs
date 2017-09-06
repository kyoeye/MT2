using MT2.CS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

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

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selection = sender as ComboBox;
            switch (selection.SelectedIndex)
            {
                case 0:
                    localsettings.Values["Language"] = "China";
                    LanguageSelection.SettingLanguage(localsettings.Values["Language"].ToString());
                    Mycombobox.PlaceholderText = "中文";
                    SetText();
                    break;
                case 1:
                    localsettings.Values["Language"] = "USA";
                    LanguageSelection.SettingLanguage(localsettings.Values["Language"].ToString());
                    Mycombobox.PlaceholderText = "English";
                    SetText();
                    break;
                default:
                    localsettings.Values["Language"] = "China";
                    LanguageSelection.SettingLanguage(localsettings.Values["Language"].ToString());
                    Mycombobox.PlaceholderText = "中文";
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void SetText()
        {
            //英文版将不开启h
            //Steins.Visibility = Visibility.Collapsed;
            ResourceLoader rl = new ResourceLoader();
            UpPage_Title.Text = rl.GetString("String36");
            UpPage_Title2.Text = rl.GetString("String37");
            Mycombobox.Header = rl.GetString("String38");
       
        }

    }
    public class UpContents
    {
        public static List<Upcontenttext> GetContent()
        {
            List<Upcontenttext> Luc = new List<Upcontenttext>();
            Luc.Add(new Upcontenttext { text = "【新增】新的更新页面" });
            Luc.Add(new Upcontenttext { text = "【新增翻译】新增英语 (English) 支持（部分翻译未完成），可以在设置中自由更改  English support added (some translations unfinished), which you can change in the settings easily." });
            Luc.Add(new Upcontenttext { text = "【改进】移除了【网络优化】功能，用处不大" });
            Luc.Add(new Upcontenttext { text = "【改进】大部分的文本可以选择复制（照顾国外用户）" });
            Luc.Add(new Upcontenttext { text = "【改进】改进了一些逻辑" });
            Luc.Add(new Upcontenttext { text = "【注意】从这个版本开始，应用名变更为【萌豚MoeTon】这是来自Watson花生酱的点子~谢谢她啦。 萌豚MoeTon更多绅士用法可以加群531234373了解~" });

            Luc.Add(new Upcontenttext { text = "【公告】停更一段时间，开学了_(:з)∠)_要回黑蜥蜴星上学不出意外的话会在十月中旬恢复更新和加入一些其他图源等。。预留的账户管理无法点击属于正常现象，吾辈还没想好怎么处理多个图源的登陆问题。。设置里的关于页面能够找到邮箱反馈，大家碰到什么bug或者应用建议都可以投递啊~谢谢（＞人＜；）下个月见~" });
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
