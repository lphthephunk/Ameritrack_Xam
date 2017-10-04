using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ameritrack_Xam.Pages.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            // MasterBehavior = MasterBehavior.Popover;
            this.Master = new NavigationDrawerPage();
            this.Detail = new NavigationPage(new MainMapPage());
            App.MasterDetail = this;
        }

        private void PreviousFaultsBtn_Clicked(object sender, EventArgs e)
        {

        }

        private void ProfileBtn_Clicked(object sender, EventArgs e)
        {

        }

        private void LogoutBtn_Clicked(object sender, EventArgs e)
        {

        }
    }
}