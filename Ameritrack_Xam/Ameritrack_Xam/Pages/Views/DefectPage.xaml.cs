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

        public DefectPage(ObservableCollection<Fault> faults)
        {
            InitializeComponent();

            faultList = faults;
            ViewModel = new DefectVM(faults);

            BindingContext = ViewModel;
        }

        protected override void OnAppearing()
        {
            
            base.OnAppearing();
        }
    }
}
