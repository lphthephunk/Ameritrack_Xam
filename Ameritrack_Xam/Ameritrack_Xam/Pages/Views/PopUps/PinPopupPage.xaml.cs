using Ameritrack_Xam.Pages.Views.PopupViewModels;
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

        CustomPin CustomPinContext;

        public PinPopupPage(CustomPin TappedPin)
        {
            InitializeComponent();

            ViewModel = new PinPopupVM();

            BindingContext = ViewModel; // BindingContext allows us to bind to objects from our ViewModel and display them on the UI
                                        // The real benefit of this is real-time updating and displaying data without having to do any extra code
            SetupBindings();

            SubmitBtn.Clicked += SubmitBtn_Clicked;

            // temporary until Rg.Plugins finishes the tap issue
            CloseBtn.Clicked += CloseBtn_Clicked;

            CustomPinContext = TappedPin;

            CloseWhenBackgroundIsClicked = true;
        }

        protected override async void OnParentSet()
        {
            base.OnParentSet();
            try
            {
                var faultData = await ViewModel.PopulatePopup(CustomPinContext.Latitude, CustomPinContext.Longitude);

                if (faultData != null)
                {
                    TrackName.Text = faultData.TrackName;
                    CommonDefectsPicker.SelectedItem = faultData.FaultType;
                    NotesEditor.Text = faultData.FaultComments;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private async void CloseBtn_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.RemovePageAsync(this, true);
        }

        private async void SubmitBtn_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TrackName.Text) || string.IsNullOrWhiteSpace(TrackName.Text))
            {
                TrackName.Placeholder = "*Required";
                TrackName.PlaceholderColor = Color.Red;
            }
            if (CommonDefectsPicker.SelectedItem == null)
            {
                CommonDefectsPicker.BackgroundColor = Color.Red;
            }
            else if ((string.IsNullOrEmpty(NotesEditor.Text) || string.IsNullOrWhiteSpace(NotesEditor.Text))
                && (!string.IsNullOrEmpty(TrackName.Text) || !string.IsNullOrWhiteSpace(TrackName.Text)) && CommonDefectsPicker.SelectedItem != null)
            {
                var result = await DisplayAlert("", "Are you sure you don't want to add any notes?", "Yes", "No");
                if (result)
                {
                    // submit to database
                    await ViewModel.SubmitFaultToDb(CustomPinContext.PinId, TrackName.Text, NotesEditor.Text, CommonDefectsPicker.SelectedItem.ToString(), IsUrgentSwitch.IsToggled);

                    // close popup
                    await PopupNavigation.RemovePageAsync(this, true);
                }
            }
            else if ((!string.IsNullOrEmpty(NotesEditor.Text) || !string.IsNullOrWhiteSpace(NotesEditor.Text))
                && (!string.IsNullOrEmpty(TrackName.Text) || !string.IsNullOrWhiteSpace(TrackName.Text)) && CommonDefectsPicker.SelectedItem != null)
            {
                // submit to database
                await ViewModel.SubmitFaultToDb(CustomPinContext.PinId, TrackName.Text, NotesEditor.Text, CommonDefectsPicker.SelectedItem.ToString(), IsUrgentSwitch.IsToggled);

                // close popup
                await PopupNavigation.RemovePageAsync(this, true);
            }
        }

        private void SetupBindings()
        {
            CommonDefectsPicker.SetBinding(Picker.ItemsSourceProperty, "ListOfDefects");
            CommonDefectsPicker.ItemDisplayBinding = new Binding("DefectName");
        }

        void Handle_SelectedIndexChangedDefectPicker(object sender, System.EventArgs e)
		{
			// Called when the user selects a common defect different from the one currently selected
			var picker = (Picker)sender;
			Debug.WriteLine("The item is " + picker.Items[picker.SelectedIndex]);
		}

	    void Handle_UnfocusedDefectPicker(object sender, Xamarin.Forms.FocusEventArgs e)
		{
            // This is called if the user opens the picker, but does not pick anything and rather taps outside of it to dismiss it.
            Debug.WriteLine("In unfocused");
		}
    }
}