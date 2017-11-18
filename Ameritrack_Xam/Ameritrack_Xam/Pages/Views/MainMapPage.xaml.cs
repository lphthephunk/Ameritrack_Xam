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
        PinPopupPage selectedPinPopup;

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

                selectedPinPopup = new PinPopupPage(faultAtThisPin, MainMap);

                if (selectedPinPopup != null)
                {
                    await Navigation.PushPopupAsync(selectedPinPopup, true);
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
                MainMap.Pins.Add(pin);

                // insert this pin coordinates into the local database for later use
                await ViewModel.InsertFault(pin);

                var faultAtThisPin = await ViewModel.FindFault(e.Position.Latitude, e.Position.Longitude);

                selectedPinPopup = new PinPopupPage(faultAtThisPin, MainMap);

                await PopupNavigation.PushAsync(selectedPinPopup);
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
            if (!InspectionDataCache.IsReportStarted) {
                await Navigation.PushPopupAsync(new InspectionHeaderPopupPage());

                MessagingCenter.Subscribe<InspectionHeaderPopupPage>(this, "started", async (messageSender) =>
                {
                    InspectionStatusButton.Text = "COMPLETE INSPECTION";
                    await BuildPinsListByArea();
                });
            } else {
                // User is attempting to complete an inspection
                var title = "Complete Inspection";
                var message = "Have you completed the inspection?";
                var result = await DisplayAlert(title, message, "Yes", "No");

                if (result)
                {
                    // TODO: create a report to be sent off at a later time; faults will be sent to the remote server along with the report data

                    MainMap.Pins.Clear();
                    InspectionStatusButton.Text = "START INSPECTION";
                    InspectionDataCache.CurrentReportData = null;
                    InspectionDataCache.IsReportStarted = false;
                    TrackNameDataCache.CurrentTrackName = "";
                }
            }

        }

        /// <summary>
        /// Populates pins in area that user is doing inspection in
        /// </summary>
        /// <returns></returns>
        private async Task BuildPinsListByArea()
        {
            var faults = await ViewModel.GetAllFaultsByArea();

            bool boolIndex;
            if (faults.ContainsKey(false))
            {
                boolIndex = false;
            }
            else
            {
                boolIndex = true;
            }

            if (boolIndex == false)
            {
                // couldn't connect to network
                await DisplayAlert("Attention", "You are not connected to a network, so the pins that are presented on the map have been loaded" +
                    " from your local memory.", "OK");
            }
            else if (faults.Values == null)
            {
                await DisplayAlert("Oops!", "Theres some trouble trying to access the server. You'll be able to continue your inspection no problem." +
                    " Just send your report once you have a proper network connection and when our server is back up!", "OK");
                return;
            }

            var pinsList = await ViewModel.ConstructPinsFromFaults(faults);

            MainMap.ListOfPins = new List<Pin>(pinsList);

            foreach (var pin in pinsList)
            {
                MainMap.Pins.Add(pin);
            }
        }

        //private async Task BuildPinsListByReport()
        //{
        //    var faults = await ViewModel.GetAllFaultsByReport();

        //    var pinsList = await ViewModel.ConstructPinsFromFaults(faults);

        //    MainMap.ListOfPins = new List<Pin>(pinsList);

        //    foreach (var pin in pinsList)
        //    {
        //        MainMap.Pins.Add(pin);
        //    }
        //}

        public PinPopupPage GetSelectedPinPopup()
        {
            return selectedPinPopup;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}
