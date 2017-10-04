using Ameritrack_Xam.Pages.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;

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

			InsertTestEmp();
            Title = "Login Page";
		}

		private void InsertTestEmp()
		{
			ViewModel.InsertMockEmployee();
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			var testEmpInfo = await ViewModel.PullEmployeeInfo();

			employeeID.Text = testEmpInfo.EmployeeFirstName + " " + testEmpInfo.EmployeeLastName;
		}

		private async void OnLoginButtonClicked(object sender, System.EventArgs e)
		{
            //if (await ViewModel.IsValidID(employeeID.Text))
            //{
            //	await Navigation.PushModalAsync(new MainMasterDetail(), false);
            //	incorrectIDWarning.IsVisible = false;
            //}
            //else
            //{
            //	incorrectIDWarning.IsVisible = true;
            //}
            await Navigation.PushModalAsync(new MainPage(), false);
		}

		public string entryText()
		{
			return employeeID.Text;
		}
	}
}
