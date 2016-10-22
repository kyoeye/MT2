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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace MT2.Control
{
    public sealed partial class TextDialog : UserControl
    {
        public TextDialog()
        {
            this.InitializeComponent();
        }

        public void TopTextblocktext ( string a) //标题
        {
            TopTextblock.Text = a;
        }

        public void Contenttextblocktext(string b) //内容
        {
            ContentTextblock.Text = b;
        }
    }
}
