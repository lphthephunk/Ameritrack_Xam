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

        public PinPopupPage(Fault fault)
        {
            InitializeComponent();

            FaultContext = fault;

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

        protected override async void OnAppearing()
        {
            try
            {
                await ViewModel.PopulatePopup(FaultContext.Latitude, FaultContext.Longitude);
                SetupBindings();
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
            await ViewModel.DeleteFault(FaultContext);
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
            await SaveData();
        }

        private async Task<bool> SaveData() {
            if ((string.IsNullOrEmpty(TrackName.Text) || string.IsNullOrWhiteSpace(TrackName.Text)) && CommonDefectsPicker.SelectedIndex == -1) {
                TrackName.Placeholder = "*Name of track...";
                TrackName.PlaceholderColor = Color.Red;
                CommonDefectsPicker.BackgroundColor = Color.MistyRose;

                return false;
            }
            else if ((string.IsNullOrEmpty(TrackName.Text) || string.IsNullOrWhiteSpace(TrackName.Text)) && CommonDefectsPicker.SelectedIndex != -1)
            {
                TrackName.Placeholder = "*Name of track...";
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

                // submit to database
                await ViewModel.SubmitFaultToDb((int)FaultContext.FaultId, TrackName.Text, NotesEditor.Text, CommonDefectsPicker.Items[CommonDefectsPicker.SelectedIndex], IsUrgentSwitch.IsToggled, FaultContext.Latitude, FaultContext.Longitude);
            
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
            // track name binding
            TrackName.Text = ViewModel.FaultData.FirstOrDefault().TrackName;

            // common defects bindings
            CommonDefectsPicker.SetBinding(Picker.ItemsSourceProperty, "ListOfDefects");
            CommonDefectsPicker.ItemDisplayBinding = new Binding("DefectName");

            var faultType = ViewModel.FaultData.FirstOrDefault().FaultType;
            CommonDefectsPicker.Title = faultType ?? "Select a Common Defect";
            CommonDefectsPicker.SelectedIndex = CommonDefectsCache.UpdatedDefectsList.FindIndex(defect => defect.DefectName == faultType);
            // notes binding
            NotesEditor.Text = ViewModel.FaultData.FirstOrDefault().FaultComments;

            // urgency toggle binding
            IsUrgentSwitch.IsToggled = ViewModel.FaultData.FirstOrDefault().IsUrgent;
        }
    }
}