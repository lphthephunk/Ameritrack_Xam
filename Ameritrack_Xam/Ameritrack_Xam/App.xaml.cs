using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Ameritrack_Xam.Pages.Views;
using Ameritrack_Xam.PCL.Helpers;

namespace Ameritrack_Xam
{
    public partial class App : Application
    {
        public static MasterDetailPage MasterDetail { get; set; }

        public App()
        {
            InitializeComponent();

            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            //CommonDefectsCache.GetDefectsFromServer();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
