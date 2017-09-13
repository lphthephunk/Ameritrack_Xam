using System;
using System.Collections.Generic;
using System.Diagnostics;

using Xamarin.Forms;

namespace Ameritrack_Xam.Pages.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

		async void OnLoginButtonClicked(object sender, System.EventArgs e)
		{
			await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
		}
    }
}
