using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Ameritrack_Xam.Pages.ViewModels;
using Plugin.Media;

namespace Ameritrack_Xam.Pages.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormPage : ContentPage
    {
        private FaultFormVM ViewModel;

        public FormPage()
        {
            InitializeComponent();

            ViewModel = new FaultFormVM();

            BindingContext = ViewModel;
        }

        private async void GoToCameraBtn_Clicked(object sender, EventArgs e)
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