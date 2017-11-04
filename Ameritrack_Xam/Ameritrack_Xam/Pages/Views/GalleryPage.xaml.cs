using Ameritrack_Xam.Pages.ViewModels;
using Ameritrack_Xam.PCL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public GalleryPage(Fault fault)
        {
            InitializeComponent();

            ViewModel = new GalleryVM(fault);

            FaultContext = fault;

            BindingContext = ViewModel;
        }

        protected async override void OnAppearing()
        {
            await PopulateGallery();

            base.OnAppearing();
        }

        private async Task PopulateGallery()
        {



            var pictures = await ViewModel.GetAllPictures(FaultContext.FaultId);

            int rowNum = 0;
            int colNum = 0;
            try
            {
                for (int i = 0; i < pictures.Count(); i++) {
                    // Gallery.Children.RemoveAt(i);
                }

                System.Diagnostics.Debug.WriteLine("PICTURES.COUNT: " + pictures.Count());
                for (int i = 0; i < pictures.Count; i++)
                {
                    System.Diagnostics.Debug.WriteLine("row.def.count: " + Gallery.RowDefinitions.Count());
                    var image = new Image
                    {
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        Source = pictures[i],
                        WidthRequest = Application.Current.MainPage.Width / 3,
                        HeightRequest = Application.Current.MainPage.Width / 3,
                        Aspect = Aspect.AspectFill
                                                   
                    };

                    System.Diagnostics.Debug.WriteLine("--i is " + i);
                    System.Diagnostics.Debug.WriteLine("--row is " + rowNum);
                    System.Diagnostics.Debug.WriteLine("--col is " + colNum);

                    Gallery.Children.Add(image, colNum, rowNum);
                    colNum++;

                    if (colNum == 3)
                    {
                        Gallery.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

                        ++rowNum;
                        colNum = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private async Task CameraBtn_Clicked(object sender, EventArgs e)
        {
            await ViewModel.TakePicture();

            await PopulateGallery();
        }
    }
}