using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Ameritrack_Xam.Pages.Views
{
    public partial class NavigationDrawerPage : ContentPage
    {
        public NavigationDrawerPage()
        {
            InitializeComponent();
        }

		async void Handle_MyInspections_Clicked(object sender, System.EventArgs e)
		{
			System.Diagnostics.Debug.WriteLine("My inspections pressed");
            App.MasterDetail.IsPresented = false;
            await App.MasterDetail.Detail.Navigation.PushAsync(new MyInspectionsPage());
		}

        async void Handle_Logout_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
;       }
    }
}
