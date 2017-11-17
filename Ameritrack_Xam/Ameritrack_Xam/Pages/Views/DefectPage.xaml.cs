using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Ameritrack_Xam.PCL.Models;
using Xamarin.Forms;

namespace Ameritrack_Xam.Pages.Views
{
    public partial class DefectPage : ContentPage
    {
        DefectVM ViewModel;
        ObservableCollection<Fault> faultList;

        public DefectPage(ObservableCollection<Fault> faults, bool isUrgent)
        {
            InitializeComponent();

            faultList = faults;
            ViewModel = new DefectVM(faults, isUrgent);
            BindingContext = ViewModel;
            Title = faults[0].TrackName;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}
