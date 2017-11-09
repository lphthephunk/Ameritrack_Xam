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
            if (string.IsNullOrEmpty(ClientName.Text) || string.IsNullOrWhiteSpace(ClientName.Text))
            {
                ClientName.Placeholder = "*Customer Name Required";
                ClientName.PlaceholderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(ClientAddress.Text) || string.IsNullOrWhiteSpace(ClientAddress.Text))
            {
                ClientAddress.Placeholder = "*Customer Address Required";
                ClientAddress.PlaceholderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(ClientContactName.Text) || string.IsNullOrWhiteSpace(ClientContactName.Text))
            {
                ClientContactName.Placeholder = "*Customer Contact Name Required";
                ClientContactName.PlaceholderColor = Color.Red;
            }
           
            else if ((!string.IsNullOrEmpty(ClientName.Text) && !string.IsNullOrWhiteSpace(ClientName.Text)) && (!string.IsNullOrEmpty(ClientAddress.Text)
                && !string.IsNullOrWhiteSpace(ClientAddress.Text)) && (!string.IsNullOrEmpty(ClientContactName.Text) && !string.IsNullOrWhiteSpace(ClientContactName.Text)))
            {
                // cache our current report data
                await ViewModel.InsertReportData(ClientName.Text, ClientAddress.Text, ClientContactName.Text);

                InspectionDataCache.IsReportStarted = true; // set this to true so we can access it globally
                                                            // accessing this globally will allow us to know when to populate the map with pre-existing pins

                MessagingCenter.Send<InspectionHeaderPopupPage>(this, "started");

                await PopupNavigation.PopAsync(true);
            }
        }

        private async void OnCloseButtonTapped(object sender, EventArgs e)
        {
            await PopupNavigation.PopAsync();
        }
    }
}
