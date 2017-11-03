using Ameritrack_Xam.Pages.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Plugin.Geolocator;
using System.Threading.Tasks;
using Ameritrack_Xam.PCL.Models;
using Ameritrack_Xam.Pages.Views.PopUps;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using System.Collections.Generic;
using Ameritrack_Xam.PCL.Helpers;

namespace Ameritrack_Xam.Pages.Views
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

            MainMap.Tap += MainMap_LongTouch;
            MainMap.PinTap += MainMap_PinTap;
        }

        /// <summary>
        /// Display the popup with data associated to the pin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void MainMap_PinTap(object sender, PinTapEventArgs e)
        {
            try
            {
                var faultAtThisPin = await ViewModel.FindFault(e.CurrentPin.Position.Latitude, e.CurrentPin.Position.Longitude);

                var pinPopup = new PinPopupPage(faultAtThisPin);

                if (pinPopup != null)
                {
                    await Navigation.PushPopupAsync(pinPopup, true);
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private async void MainMap_LongTouch(object sender, MapLongTouchEventArgs e)
        {
            if (InspectionDataCache.IsReportStarted)
            {
                var pin = new Pin()
                {
                    Label = "Placeholder",
                    Position = new Position(e.Position.Latitude, e.Position.Longitude),
                    Type = PinType.Place
                };

                // add the pin to the MapExtension List of pins
                MainMap.ListOfPins.Add(pin);
                //MainMap.Pins.Add(pin);

                // insert this pin coordinates into the local database for later use
                await ViewModel.InsertFault(pin);
            }
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
            await Navigation.PushPopupAsync(new InspectionHeaderPopupPage());

            MessagingCenter.Subscribe<InspectionHeaderPopupPage>(this, "started", async(messageSender) =>
            {
                await BuildPinsListByArea();
            });
        }

        /// <summary>
        /// Populates pins in area that user is doing inspection in
        /// </summary>
        /// <returns></returns>
        private async Task BuildPinsListByArea()
        {
            var faults = await ViewModel.GetAllFaultsByArea();

            var pinsList = await ViewModel.ConstructPinsFromFaults(faults);

            MainMap.ListOfPins = new List<Pin>(pinsList);

            foreach (var pin in pinsList)
            {
                MainMap.Pins.Add(pin);
            }
        }

        private async Task BuildPinsListByReport()
        {
            var faults = await ViewModel.GetAllFaultsByReport();

            var pinsList = await ViewModel.ConstructPinsFromFaults(faults);

            MainMap.ListOfPins = new List<Pin>(pinsList);

            foreach (var pin in pinsList)
            {
                MainMap.Pins.Add(pin);
            }
        }
    }
}
