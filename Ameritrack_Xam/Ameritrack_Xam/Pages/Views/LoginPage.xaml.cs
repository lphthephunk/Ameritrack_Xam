using Ameritrack_Xam.Pages.ViewModels;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ameritrack_Xam.Pages.Views
{
	public partial class LoginPage : ContentPage
	{
		LoginVM ViewModel;

		public LoginPage()
		{
			InitializeComponent();

			ViewModel = new LoginVM();

            Title = "Login Page";
		}

		private async void OnLoginButtonClicked(object sender, System.EventArgs e)
		{
            if (CrossConnectivity.IsSupported && CrossConnectivity.Current.IsConnected)
            {
                if (await ViewModel.IsValidID(employeeID.Text))
                {
                    if (StayLoggedInSwitch.IsToggled)
                    {
                        ViewModel.StoreCredentials(employeeID.Text);
                    }

                    ViewModel.SetUserData();

                    await Navigation.PushModalAsync(new MainPage(), false);
                    incorrectIDWarning.IsVisible = false;
                }
                else
                {
                    incorrectIDWarning.IsVisible = true;
                }
            }
            else
            {
                noInternetWarning.IsVisible = true;
            }
		}

        private void StayLoggedInSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            if (!StayLoggedInSwitch.IsToggled)
            {
                ViewModel.RemoveCredentials();
            }
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            if (ViewModel.IsStored())
            {
                employeeID.Text = ViewModel.GetCredentials();
                StayLoggedInSwitch.IsToggled = true;
            }
            else
            {
                StayLoggedInSwitch.IsToggled = false;
            }
        }
    }
}
