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
    public partial class MainMasterDetail : MasterDetailPage
    {
        public MainMasterDetail()
        {
            InitializeComponent();
            MasterBehavior = MasterBehavior.Popover;
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