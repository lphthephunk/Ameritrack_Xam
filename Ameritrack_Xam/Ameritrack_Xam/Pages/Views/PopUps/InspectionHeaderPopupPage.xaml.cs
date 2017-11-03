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
            if (string.IsNullOrEmpty(CustomerName.Text) || string.IsNullOrWhiteSpace(CustomerName.Text))
            {
                CustomerName.Placeholder = "*Customer Name Required";
                CustomerName.PlaceholderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(CustomerAddress.Text) || string.IsNullOrWhiteSpace(CustomerAddress.Text))
            {
                CustomerAddress.Placeholder = "*Customer Address Required";
                CustomerAddress.PlaceholderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(CustomerContactName.Text) || string.IsNullOrWhiteSpace(CustomerContactName.Text))
            {
                CustomerContactName.Placeholder = "*Customer Contact Name Required";
                CustomerContactName.PlaceholderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(InspectorName.Text) || string.IsNullOrWhiteSpace(InspectorName.Text))
            {
                InspectorName.Placeholder = "*Inspector Name Required";
                InspectorName.PlaceholderColor = Color.Red;
            }
            else if ((!string.IsNullOrEmpty(CustomerName.Text) && !string.IsNullOrWhiteSpace(CustomerName.Text)) && (!string.IsNullOrEmpty(CustomerAddress.Text)
                && !string.IsNullOrWhiteSpace(CustomerAddress.Text)) && (!string.IsNullOrEmpty(CustomerContactName.Text) && !string.IsNullOrWhiteSpace(CustomerContactName.Text))
                && (!string.IsNullOrEmpty(InspectorName.Text) && !string.IsNullOrWhiteSpace(InspectorName.Text)))
            {
                // cache our current report data
                await ViewModel.InsertReportData(CustomerName.Text, CustomerAddress.Text, CustomerContactName.Text);

                InspectionDataCache.IsReportStarted = true; // set this to true so we can access it globally
                                                            // accessing this globally will allow us to know when to populate the map with pre-existing pins

                MessagingCenter.Send<InspectionHeaderPopupPage>(this, "started");

                await PopupNavigation.PopAsync(true);
            }
        }
    }
}
