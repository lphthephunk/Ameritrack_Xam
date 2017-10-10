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
using Rg.Plugins.Popup.Services;

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

            PopulateMapWithPins();

            MainMap.Tap += MainMap_Tap;
            MainMap.PinTap += MainMap_PinTap;
        }

        private async void PopulateMapWithPins()
        {
                var pins = await ViewModel.GetAllPins();
                if (pins != null && pins.Count > 0)
                {
                    foreach (var pin in pins)
                    {
                        // construct a new pin since we can't store that object in the sqlite db

                        MainMap.ListOfPins.Add(CreatePinFromCoords(pin.Latitude, pin.Longitude));
                        MainMap.Pins.Add(CreatePinFromCoords(pin.Latitude, pin.Longitude));
                    }
                }
        }

        private Pin CreatePinFromCoords(double lat, double lng)
        {
            var pin = new Pin()
            {
                Label = "Placeholder",
                Position = new Position(lat, lng),
                Type = PinType.Place
            };

            return pin;
        }

        /// <summary>
        /// Display the popup with data associated to the pin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void MainMap_PinTap(object sender, PinTapEventArgs e)
        {
            var pinPopup = new PinPopupPage(await ViewModel.FindCustomPin(e.CurrentPin));

            if (pinPopup != null)
            {
                await PopupNavigation.PushAsync(pinPopup, true);
            }
        }

        private async void MainMap_Tap(object sender, MapTapEventArgs e)
        {
            // below code is just for a sample pin
            // TODO: prompt the user to input this information
            var customPin = new CustomPin
            {
                Pin = new Pin()
                {
                    Label = "Placeholder",
                    Position = new Position(e.Position.Latitude, e.Position.Longitude),
                    Type = PinType.Place,
                },
                Latitude = e.Position.Latitude,
                Longitude = e.Position.Longitude,
            };

            // add the pin to the MapExtension List of pins
            MainMap.ListOfPins.Add(customPin.Pin);
            MainMap.Pins.Add(customPin.Pin);

            // insert this custom pin into the local database
            await ViewModel.InsertPin(customPin);
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

        private async void Handle_Start_Inspection_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushPopupAsync(new InspectionHeaderPopupPage(), true);
        }
    }
}
