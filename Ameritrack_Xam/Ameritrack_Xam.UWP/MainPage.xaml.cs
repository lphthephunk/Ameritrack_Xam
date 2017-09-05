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

namespace Ameritrack_Xam.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            Xamarin.FormsMaps.Init("VzIHMTcM9WhHLhD0Tkk5~NsuwnZJVawhmEihq4hs-UQ~Av4cnDkRPzItHwXLqinNs8yek3KWfqxvJVBkdsHQIYmEJr3OCZ2PvGkZOvj8lAax");
            LoadApplication(new Ameritrack_Xam.App());
        }
    }
}
