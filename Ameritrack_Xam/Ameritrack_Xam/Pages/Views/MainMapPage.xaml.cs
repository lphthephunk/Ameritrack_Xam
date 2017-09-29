using Ameritrack_Xam.Pages.ViewModels;
using Ameritrack_Xam.Pages.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Plugin.Geolocator;
using System.Threading.Tasks;
using Ameritrack_Xam.PCL.Models;
using Ameritrack_Xam.Pages.Views.PopUps;
using Rg.Plugins.Popup.Extensions;

namespace Ameritrack_Xam
{
    public partial class MainMapPage : ContentPage
    {
        private MapPageVM ViewModel;
        

        public MainMapPage()
        {
            InitializeComponent();

            ViewModel = new MapPageVM();

            BindingContext = ViewModel;

            GetUserLocation();

            // hide nav-bar
            // NavigationPage.SetHasNavigationBar(this, false);

            MainMap.Tap += MainMap_Tap;
            MainMap.PinTap += MainMap_PinTap;
        }

        /// <summary>
        /// Display the popup with data associated to the pin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void MainMap_PinTap(object sender, PinTapEventArgs e)
        {
            var pinPopup = new PinPopupPage(e.CurrentPin);

            await Navigation.PushPopupAsync(pinPopup, true);
        }

        private void MainMap_Tap(object sender, MapTapEventArgs e)
        {
            // below code is just for a sample pin
            // TODO: prompt the user to input this information
            var customPin = new CustomPin
            {
                Pin = new Pin()
                {
                    Label = "Test",
                    Position = new Position(e.Position.Latitude, e.Position.Longitude),
                    Type = PinType.Place,
                },
            };

            // add the pin to the MapExtension List of pins
            MainMap.ListOfPins.Add(customPin.Pin);
            MainMap.Pins.Add(customPin.Pin);
        }

        private async void GetUserLocation()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 100.0;
            locator.PositionChanged += (sender, e) =>
            {

                var newPosition = e.Position;
                Position pos = new Position(newPosition.Latitude, newPosition.Longitude);

                // 
                // Allow user to scroll outside of region they are in, without centering them back 
                // By default, let user scroll out of region, but have a button that if they tap it will center them
                // The button will be tied to an event:
                //
                //  MainMap.MoveToRegion(MapSpan.FromCenterAndRadius(pos, MainMap.VisibleRegion.Radius));
                // 
                //  Whenever the button is tapped, it always centers unless they scroll away
                //

                if (MainMap.VisibleRegion != null)
                {
                    MainMap.MoveToRegion(MapSpan.FromCenterAndRadius(MainMap.VisibleRegion.Center, MainMap.VisibleRegion.Radius));
                }
            };

            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(20), null, true);

            MainMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromMiles(0.10)));

            if (!locator.IsListening)
            {
                await locator.StartListeningAsync(TimeSpan.FromSeconds(5.0), 0);
            }
        }
    }
}
