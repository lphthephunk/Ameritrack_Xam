using Ameritrack_Xam.Pages.Views.PopupViewModels;
using Ameritrack_Xam.PCL.Helpers;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using Rg.Plugins.Popup.Services;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using Ameritrack_Xam.PCL.Models;

namespace Ameritrack_Xam.Pages.Views.PopUps
{
	public partial class PinPopupPage : PopupPage
	{
		PinPopupVM ViewModel;
        Fault FaultContext;

        MapExtension MapContext;
        List<Pin> ListOfPins;

        private readonly double EPSILON = Double.Epsilon;   // for comparing floating point numbers

        public PinPopupPage(Fault fault, MapExtension map)
        {
            InitializeComponent();

            FaultContext = fault;
            MapContext = map;
            ListOfPins = map.ListOfPins;

            ViewModel = new PinPopupVM(fault);

            BindingContext = ViewModel; // BindingContext allows us to bind to objects from our ViewModel and display them on the UI
                                        // The real benefit of this is real-time updating and displaying data without having to do any extra code

            CameraBtn.Clicked += CameraBtn_Clicked;

            SubmitBtn.Clicked += SubmitBtn_Clicked;
            
            // temporary until Rg.Plugins finishes the tap issue
            // CloseBtn.Clicked += CloseBtn_Clicked;

            DeleteBtn.Clicked += DeleteBtn_Clicked;

            CloseWhenBackgroundIsClicked = true;
        }

        /// <summary>
        /// Stops the popup from being closed by the hardware back button
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        protected override async void OnAppearing()
        {
            try
            {
                if (FaultContext.FaultId != null)
                {
                    await ViewModel.PopulatePopup(FaultContext.Latitude, FaultContext.Longitude);
                    SetupBindings();
                }
                else
                {
                    SetupTrackName();
                    SetupCommonDefectsPicker();
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            base.OnAppearing();
        }

        private async void CameraBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                //bool success = await SaveData();
                // if (success)
                this.IsVisible = false;
                await App.MasterDetail.Detail.Navigation.PushAsync(new GalleryPage(FaultContext));
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Delete button event to delete this fault data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void DeleteBtn_Clicked(object sender, EventArgs e)
        {
            // there is a defect stored in the db
            if (FaultContext.FaultId != null)
            {
                var displayTitle = "Remove Pin";
                var displayMessage = "Remove this defect from the report?";
                var result = await DisplayAlert(displayTitle, displayMessage, "Delete", "Cancel");
                if (result)
                {
                    await ViewModel.DeleteFault(FaultContext);

                    ListOfPins.Remove(ListOfPins.FirstOrDefault((Pin arg) => Math.Abs(arg.Position.Latitude - FaultContext.Latitude) < EPSILON && Math.Abs(arg.Position.Longitude - FaultContext.Longitude) < EPSILON));

                    UpdatePins();

                    await PopupNavigation.PopAsync();
                }
            }
            // no defect stored in the db, so simply pop the popup 
            else
            {
                await PopupNavigation.PopAsync();
            }
        }

        /// <summary>
        /// Close button event to pop the popup page off of the stack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void CloseBtn_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.PopAsync();
        }

        /// <summary>
        /// Handles business logic to submit a fault to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SubmitBtn_Clicked(object sender, EventArgs e)
        {
            PlacePin();

            await SaveData();
        }

        private async Task<bool> SaveData() {
            if ((string.IsNullOrEmpty(TrackName.Text) || string.IsNullOrWhiteSpace(TrackName.Text)) && CommonDefectsPicker.SelectedIndex == -1) {
                TrackName.Placeholder = "*Track Name required";
                TrackName.PlaceholderColor = Color.Red;
                CommonDefectsPicker.BackgroundColor = Color.MistyRose;

                return false;
            }
            else if ((string.IsNullOrEmpty(TrackName.Text) || string.IsNullOrWhiteSpace(TrackName.Text)) && CommonDefectsPicker.SelectedIndex != -1)
            {
                TrackName.Placeholder = "*Track Name Required";
                TrackName.PlaceholderColor = Color.Red;
                CommonDefectsPicker.BackgroundColor = Color.Transparent;

                return false;
            }
            else if (CommonDefectsPicker.SelectedIndex == -1)
            {
                CommonDefectsPicker.BackgroundColor = Color.MistyRose;

                return false;
            }
            else {
                CommonDefectsPicker.BackgroundColor = Color.Transparent;

                TrackNameDataCache.CurrentTrackName = TrackName.Text;
                Debug.WriteLine(TrackNameDataCache.CurrentTrackName);

                var faultToSubmit = new Fault()
                {
                    TrackName = TrackName.Text,
                    Employee = UserDataCache.CurrentEmployeeData.EmployeeCredentials,
                    AreaAddress = InspectionDataCache.CurrentReportData.Address,
                    FaultComments = NotesEditor.Text,
                    FaultType = CommonDefectsPicker.Items[CommonDefectsPicker.SelectedIndex],
                    IsUrgent = IsUrgentSwitch.IsToggled,
                    Latitude = FaultContext.Latitude,
                    Longitude = FaultContext.Longitude,
                    ReportId = InspectionDataCache.CurrentReportData.ReportId
                };

                if (FaultContext.FaultId != null)
                {
                    faultToSubmit.FaultId = FaultContext.FaultId;
                }

                // submit to database
                await ViewModel.SubmitFaultToDb(faultToSubmit);
            
                // close popup
                await PopupNavigation.PopAsync();

                return true;
            }
        }

        /// <summary>
        /// Sets up the data bindings for the picker on the popup page
        /// </summary>
        private void SetupBindings()
        {
            // disable the delete button if the current user wasn't the one that created that fault initially
            if (ViewModel.FaultData.FirstOrDefault().Employee != UserDataCache.CurrentEmployeeData.EmployeeCredentials)
            {
                DeleteBtn.IsEnabled = false;
            }

            // track name binding
            TrackName.Text = ViewModel.FaultData.FirstOrDefault().TrackName;

            // common defects bindings
            SetupCommonDefectsPicker();

            var faultType = ViewModel.FaultData.FirstOrDefault().FaultType;
            CommonDefectsPicker.Title = faultType ?? "Select a Common Defect";
            CommonDefectsPicker.SelectedIndex = CommonDefectsCache.UpdatedDefectsList.FindIndex(defect => defect.DefectName == faultType);
            // notes binding
            NotesEditor.Text = ViewModel.FaultData.FirstOrDefault().FaultComments;

            // urgency toggle binding
            IsUrgentSwitch.IsToggled = ViewModel.FaultData.FirstOrDefault().IsUrgent;
        }

        private void SetupCommonDefectsPicker()
        {
            CommonDefectsPicker.SetBinding(Picker.ItemsSourceProperty, "ListOfDefects");
            CommonDefectsPicker.ItemDisplayBinding = new Binding("DefectName");
        }

        private void SetupTrackName()
        {
            TrackName.Text = TrackNameDataCache.CurrentTrackName;
        }

        private void UpdatePins()
        {
            MapContext.Pins.Clear();

            foreach (var pin in ListOfPins)
            {
                MapContext.Pins.Add(pin);
            }
        }

        private void PlacePin()
        {
            var pin = new Pin()
            {
                Label = "Placeholder",
                Position = new Position(FaultContext.Latitude, FaultContext.Longitude),
                Type = PinType.Place
            };

            // add the pin to the MapExtension list of pins
            MapContext.ListOfPins.Add(pin);
            MapContext.Pins.Add(pin);
        }

        private async void OnCloseButtonTapped(object sender, EventArgs e)
        {
            await PopupNavigation.PopAsync();
        }
    }
}