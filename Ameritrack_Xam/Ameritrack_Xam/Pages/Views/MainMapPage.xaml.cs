using Ameritrack_Xam.Pages.ViewModels;
using Ameritrack_Xam.Pages.Views;
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

            // hide nav-bar
            NavigationPage.SetHasNavigationBar(this, false);
        }

        /// <summary>
        /// This method will later be replaced by the user selecting a pin on the map
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void GoToFormPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FormPage());
        }
    }
}
