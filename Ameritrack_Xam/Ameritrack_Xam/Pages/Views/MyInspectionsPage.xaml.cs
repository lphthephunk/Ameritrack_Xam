using System;
using System.Collections.Generic;
using Ameritrack_Xam.Pages.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Ameritrack_Xam.PCL.Models;


namespace Ameritrack_Xam.Pages.Views
{
    public partial class MyInspectionsPage : ContentPage
    {
        private MyInspectionsVM ViewModel;

        public MyInspectionsPage()
        {
            InitializeComponent();

            ViewModel = new MyInspectionsVM();
            BindingContext = ViewModel;
        }
         
        protected async override void OnAppearing()
        {
            await ViewModel.GetReports();

            base.OnAppearing();
        }

        async void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var list = (ListView)sender;
            var report = (Report)list.SelectedItem;

            await App.MasterDetail.Detail.Navigation.PushAsync(new ReportPage(report));
        }
    }
}