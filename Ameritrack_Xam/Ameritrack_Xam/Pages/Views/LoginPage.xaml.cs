using Ameritrack_Xam.Pages.ViewModels;
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

			//InsertTestEmp();
            Title = "Login Page";
		}

		private async void InsertTestEmp()
		{
			await ViewModel.InsertMockEmployee();
		}

		private async void OnLoginButtonClicked(object sender, System.EventArgs e)
		{
            if (await ViewModel.IsValidID(employeeID.Text))
            {
                if (StayLoggedInSwitch.IsToggled)
                {
                    ViewModel.StoreCredentials(employeeID.Text);
                }

                await Navigation.PushModalAsync(new MainPage(), false);
                incorrectIDWarning.IsVisible = false;
            }
            else
            {
                incorrectIDWarning.IsVisible = true;
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
            }
        }
    }
}
