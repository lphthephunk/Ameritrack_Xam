using Ameritrack_Xam.Pages.Views.PopupViewModels;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using Rg.Plugins.Popup.Services;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using Ameritrack_Xam.PCL.Helpers;

namespace Ameritrack_Xam.Pages.Views.PopUps
{
    public partial class InspectionHeaderPopupPage : PopupPage
    {
        InspectionHeaderPopupVM ViewModel;

        public InspectionHeaderPopupPage()
        {
            InitializeComponent();

            ViewModel = new InspectionHeaderPopupVM();

            BindingContext = ViewModel;
        }

        /// <summary>
        /// Handles the start of an inspection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Handle_Inspection_Start(object sender, System.EventArgs e)
        {
            // cache our current report data
            ViewModel.InsertReportData(CustomerName.Text, CustomerAddress.Text, CustomerContactName.Text);

            InspectionDataCache.IsReportStarted = true; // set this to true so we can access it globally
            // accessing this globally will allow us to know when to populate the map with pre-existing pins

            await PopupNavigation.PopAsync(true);
        }
    }
}
