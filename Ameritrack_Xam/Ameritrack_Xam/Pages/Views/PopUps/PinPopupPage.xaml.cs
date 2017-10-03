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

namespace Ameritrack_Xam.Pages.Views.PopUps
{
	public partial class PinPopupPage : PopupPage
	{
		PinPopupVM ViewModel;

		Pin CurrentPinContext;


		public PinPopupPage(Pin TappedPin)
		{
			InitializeComponent();

			ViewModel = new PinPopupVM();

			BindingContext = ViewModel;

			CurrentPinContext = TappedPin;

            // Need a source for the defects 
            // CommonDefectsPicker.ItemsSource = Defects;

			//TestLabel.Text = "Lat: " + TappedPin.Position.Latitude + " Lng: " + TappedPin.Position.Longitude;
		}

		void Handle_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// Called when the user selects a common defect different from the one currently selected
			var picker = (Picker)sender;
			Debug.WriteLine("The item is " + picker.Items[picker.SelectedIndex]);
		}

		void Handle_Unfocused(object sender, Xamarin.Forms.FocusEventArgs e)
		{
			// This is called if the user opens the picker, but does not pick anything and rather taps outside of it to dismiss it.
			Debug.WriteLine("In unfocused");
		}

		private void OnClose(object sender, EventArgs e)
		{
			PopupNavigation.PopAsync();
		}
	}
}