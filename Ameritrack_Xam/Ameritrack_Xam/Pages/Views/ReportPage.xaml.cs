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

        protected override async void OnAppearing()
        {
            await ViewModel.GetFaults();

            base.OnAppearing();
        }
    }
}
