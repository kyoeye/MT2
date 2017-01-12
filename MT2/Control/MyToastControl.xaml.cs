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
    public sealed partial class MyToastControl : UserControl
    {
        public MyToastControl()
        {
            this.InitializeComponent();
           this.tbNotify.DataContext = this;
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("textData", typeof(Entity), typeof(MyToastControl), new PropertyMetadata(null));
        public Entity entity
        {
            get { return (Entity)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
    }
    public class Entity
    {
        public string name { get; set; }
    }

}
