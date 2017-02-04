using MT2.CS;
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

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace MT2.page
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Setting2Page : Page
    {
       
        public List<ThemeColors> themeColors;
        Fallsclass falclass = new Fallsclass();
        //public int fallshub { get { return falclass.FallsHub; } set { falclass.FallsHub = value; } }

        public Setting2Page()
        {

            this.InitializeComponent();       
            themeColors = ThemeColorsAdd.GetThemeColors(); //返回主题数据
          falclass .FallsHub  =(int)listslider.Value;
        }

    }
}
