using Ameritrack_Xam.Pages.ViewModels;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ameritrack_Xam
{
    public partial class MainPage : ContentPage
    {
        private MapPageVM ViewModel;

        public MainPage()
        {
            InitializeComponent();

            ViewModel = new MapPageVM();

            BindingContext = ViewModel;
        }

        private async void GoToCameraButton_Clicked(object sender, EventArgs e)
        {
            if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
            {
                // Supply media options for saving our photo after it's taken.
                var mediaOptions = new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Faults",
                    Name = $"{DateTime.UtcNow}.jpg"
                };

                var file = await CrossMedia.Current.TakePhotoAsync(mediaOptions);
            }
        }
    }
}
