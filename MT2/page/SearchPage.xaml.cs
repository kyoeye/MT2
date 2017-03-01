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
    public sealed partial class SearchPage : Page
    {
        public List<Seachitem> secitem;
        public SearchPage()
        {
            this.InitializeComponent();
            secitem = Seachitems.seachitem();
        }
    }

   public  class Seachitem
    {
        //public string Canseestring { get; set; }
       public   string Seachstring { get; set; }
    }

    public  class Seachitems
    {
        public static   List<Seachitem> seachitem ()
        {
            var seachitems = new List<Seachitem>();
            seachitems.Add(new page.Seachitem { Seachstring = "loli" });
            seachitems.Add(new page.Seachitem { Seachstring = "loli" });
            seachitems.Add(new page.Seachitem { Seachstring = "loli" });
            seachitems.Add(new page.Seachitem { Seachstring = "loli" });
            seachitems.Add(new page.Seachitem { Seachstring = "loli" });
            seachitems.Add(new page.Seachitem { Seachstring = "loli" });
            seachitems.Add(new page.Seachitem { Seachstring = "loli" });
            seachitems.Add(new page.Seachitem { Seachstring = "loli" });
            seachitems.Add(new page.Seachitem { Seachstring = "loli" });
            seachitems.Add(new page.Seachitem { Seachstring = "loli" });
            seachitems.Add(new page.Seachitem { Seachstring = "loli" });
            seachitems.Add(new page.Seachitem { Seachstring = "loli" });
            seachitems.Add(new page.Seachitem { Seachstring = "loli" });

            return seachitems;
        }
    }
}
