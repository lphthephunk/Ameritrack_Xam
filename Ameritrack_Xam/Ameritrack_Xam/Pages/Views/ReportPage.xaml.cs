﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Ameritrack_Xam.Pages.ViewModels;
using Ameritrack_Xam.PCL.Models;
using Xamarin.Forms;
using Plugin.Connectivity;

namespace Ameritrack_Xam.Pages.Views
{
    public partial class ReportPage : ContentPage
    {
        ReportVM ViewModel;

        public ReportPage(Report report)
        {
            InitializeComponent();

            ViewModel = new ReportVM(report);
            BindingContext = ViewModel;
            Title = report.ClientName;

            ToolbarItems.Add(new ToolbarItem("Share", "share.png", async () => { 
                var page = new ContentPage(); 
                var result = await page.DisplayAlert("Share Report", "Would you like to share this report?", "Yes", "No");

                if (result)
                {
                    if (CrossConnectivity.IsSupported && CrossConnectivity.Current.IsConnected)
                    {
                        if (!await ViewModel.SendReportToServer(report))
                        {
                            await page.DisplayAlert("Oops!", "There was a problem sending the report to the server. Please try again", "OK");
                        }
                    }
                    else if (CrossConnectivity.IsSupported && !CrossConnectivity.Current.IsConnected)
                    {
                        await page.DisplayAlert("Connection Error", "It appears that you aren't connected to the internet. Please check your internet" +
                            " connection and try again", "OK");
                    }
                }
            }));
        }

        async void Handle_ItemTappedAsync(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var list = (ListView)sender;
            var trackName = (String)list.SelectedItem;
            var group = (TrackList)e.Group;

            await App.MasterDetail.Detail.Navigation.PushAsync(new DefectPage(ViewModel.trackDictionary[trackName], group.IsUrgent));
        }

        protected override async void OnAppearing()
        {
            await ViewModel.GetFaults();

            base.OnAppearing();
        }
    }
}
