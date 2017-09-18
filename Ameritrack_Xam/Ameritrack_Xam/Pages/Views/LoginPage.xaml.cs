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

        async void OnLoginButtonClicked(object sender, System.EventArgs e)
		{
			await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
		}
    }
}
