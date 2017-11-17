using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Ameritrack_Xam.Pages.ViewModels;
using Ameritrack_Xam.PCL.Models;
using Xamarin.Forms;

namespace Ameritrack_Xam.Pages.Views
{
    public partial class ReportPage : ContentPage
    {
        ReportVM ViewModel;

        public ReportPage(Report report)
        {
            InitializeComponent();

            ViewModel = new ReportVM(report);
            BindingContext = ViewModel;
            Title = report.ClientName;
        }

        async void Handle_ItemTappedAsync(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var list = (ListView)sender;
            var trackName = (String)list.SelectedItem;

            await App.MasterDetail.Detail.Navigation.PushAsync(new DefectPage(ViewModel.trackDictionary[trackName]));
        }

        protected override async void OnAppearing()
        {
            await ViewModel.GetFaults();

            base.OnAppearing();
        }
    }
}
