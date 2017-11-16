using System; using System.Collections.Generic; using Ameritrack_Xam.Pages.ViewModels; using System.Threading.Tasks; using Xamarin.Forms; using System.Collections.ObjectModel; using Ameritrack_Xam.PCL.Models;   namespace Ameritrack_Xam.Pages.Views {     public partial class MyInspectionsPage : ContentPage     {         private MyInspectionsVM ViewModel;          public MyInspectionsPage()         {             InitializeComponent();              ViewModel = new MyInspectionsVM();             BindingContext = ViewModel;         }                   protected async override void OnAppearing()
        {             await ViewModel.GetReports(); 
            base.OnAppearing();
        }

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {             var list = (ListView)sender;             var report = (Report)list.SelectedItem;
            DisplayAlert("Captured Report", report.ClientName, "OK");
        }     } }