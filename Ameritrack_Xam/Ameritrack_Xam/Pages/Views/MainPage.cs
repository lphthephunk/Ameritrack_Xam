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
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();

            var mainMapNavigationPage = new NavigationPage(new MainMapPage());
            mainMapNavigationPage.Popped += async (sender, e) => {
                var navPage = (NavigationPage)sender;
                var navStack = navPage.Navigation.NavigationStack;
                var current = navStack[navStack.Count - 1].GetType();
                var mmp = navStack[0].GetType();

                var mapPage = (MainMapPage)navStack[navStack.Count - 1];
                var popup = mapPage.GetSelectedPinPopup();

                popup.Opacity = 0.0;
                popup.IsVisible = true;
                await popup.FadeTo(1, 300);
            };

            // MasterBehavior = MasterBehavior.Popover;
            this.Master = new NavigationDrawerPage();
            this.Detail = mainMapNavigationPage;
            App.MasterDetail = this;
        }

        private void PreviousFaultsBtn_Clicked(object sender, EventArgs e)
        {

        }

        private void ProfileBtn_Clicked(object sender, EventArgs e)
        {

        }

        private void LogoutBtn_Clicked(object sender, EventArgs e)
        {

        }
    }
}