using Ameritrack_Xam.Pages.ViewModels;
using Ameritrack_Xam.PCL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ameritrack_Xam.Pages.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GalleryPage : ContentPage
    {
        GalleryVM ViewModel;
        Fault FaultContext;
        bool firstLoad = true;


        public GalleryPage(Fault fault)
        {
            InitializeComponent();

            ViewModel = new GalleryVM(fault);

            FaultContext = fault;

            BindingContext = ViewModel;
        }

        protected async override void OnAppearing()
        {
            if (firstLoad)
            {
                await PopulateGallery();
                firstLoad = false;
            }
            base.OnAppearing();
        }

        private void RandomizeIndicatorColor()
        {
            ViewModel.IsBusy = true;
            Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                Random random = new Random();
                spinner.Color = Color.FromRgb((random.Next(0, 255)), (random.Next(0, 255)), (random.Next(0, 255)));

                return ViewModel.IsBusy;
            });
        }

        private async Task PopulateGallery()
        {
            RandomizeIndicatorColor();

            var pictures = await ViewModel.GetAllPictures(FaultContext.FaultId);

            var noPicturesImage = new Image
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Source = ImageSource.FromFile("no_images.png"),
                WidthRequest = (Application.Current.MainPage.Width * 0.6),
                HeightRequest = Application.Current.MainPage.Height,
                Aspect = Aspect.AspectFit,
                StyleId = "noPictureImage"
            };

            if (pictures.Count() == 0 && Gallery.Children.Count() < 1)
            {
                Gallery.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                Gallery.ColumnDefinitions[1].Width = new GridLength(6, GridUnitType.Star);
                Gallery.Children.Add(noPicturesImage, 1, 0);

                ViewModel.IsBusy = false;
                return;
            }
            else if (Gallery.Children.Count() > 0 && Gallery.Children.First().StyleId == "noPictureImage")
            {
                Gallery.ColumnDefinitions.RemoveAt(1);
                Gallery.Children.RemoveAt(0);
            }

            int rowNum = 0;
            int colNum = 0;

            try
            {
                Image image = null;
                for (int i = 0; i < pictures.Count; i++)
                {
                    image = new Image
                    {
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        Source = pictures[i],
                        WidthRequest = Application.Current.MainPage.Width / 2.05,
                        HeightRequest = Application.Current.MainPage.Width / 2.05,
                        Aspect = Aspect.AspectFill
                    };

                    Gallery.Children.Add(image, colNum, rowNum);
                    colNum++;

                    if (colNum == 2)
                    {
                        Gallery.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

                        ++rowNum;
                        colNum = 0;
                    }
                }
                // set the spinner to finish when the last image finishes loading
                spinner.SetBinding(ActivityIndicator.IsRunningProperty, "IsLoading");
                spinner.BindingContext = image;
                ViewModel.IsBusy = false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private async Task CameraBtn_Clicked(object sender, EventArgs e)
        {
            spinner.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
            spinner.BindingContext = ViewModel;

            await ViewModel.TakePicture();

            await PopulateGallery();
        }
    }
}