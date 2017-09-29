using Ameritrack_Xam.Pages.Views.PopupViewModels;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Ameritrack_Xam.Pages.Views.PopUps
{
    public partial class PinPopupPage : PopupPage
    {
        PinPopupVM ViewModel;

        Pin CurrentPinContext;

        public PinPopupPage(Pin TappedPin)
        {
            InitializeComponent();

            ViewModel = new PinPopupVM();

            BindingContext = ViewModel;

            CurrentPinContext = TappedPin;

            TestLabel.Text = "Lat: " + TappedPin.Position.Latitude + " Lng: " + TappedPin.Position.Longitude;
        }
    }
}