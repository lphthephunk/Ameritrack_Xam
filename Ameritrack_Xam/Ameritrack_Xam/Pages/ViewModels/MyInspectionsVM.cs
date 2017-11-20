﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ameritrack_Xam.PCL.Interfaces;
using Ameritrack_Xam.PCL.Models;
using Ameritrack_Xam.PCL.Helpers;
using Xamarin.Forms;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Plugin.Connectivity;

namespace Ameritrack_Xam.Pages.ViewModels
{
    public class MyInspectionsVM : INotifyPropertyChanged
    {
        IDatabaseServices DatabaseService = DependencyService.Get<IDatabaseServices>();
        public ObservableCollection<Report> ReportList { get; set; }
        public static bool loaded = false;

        private bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }

        public ICommand Refresh
        {
            get
            {
                return new Command(async () => 
                {
                    IsBusy = true;
                    await GetUpdatedReports();
                    IsBusy = false;
                });
            }
        }

        public MyInspectionsVM()
        {
            ReportList = new ObservableCollection<Report>();
        }

        private async Task<List<Report>> GetReportsByEmployee(Employee employee)
        {
            List<Report> list = new List<Report>();
            try 
            {
                list = await DatabaseService.GetReportsByEmployee(employee);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Couldn't retrieve reports for employee " + ex.Message);
            }

            return list;
        }

        public async Task GetReports()
        {
            ReportList.Clear();

            // check if the inspection list has already been fetched before in this session
            if (!InspectionDataCache.IsReportListFetched)
            {
                InspectionDataCache.CachedReportsList = await GetReportsByEmployee(UserDataCache.CurrentEmployeeData);
                InspectionDataCache.IsReportListFetched = true;
            }

            foreach (var cachedReport in InspectionDataCache.CachedReportsList)
            {
                ReportList.Add(cachedReport);
            }
        }

        /// <summary>
        /// Triggered when user refreshes the page
        /// </summary>
        /// <returns></returns>
        public async Task GetUpdatedReports()
        {
            ReportList.Clear();

            List<Report> reports = new List<Report>();

            if (CrossConnectivity.IsSupported && CrossConnectivity.Current.IsConnected)
            {
                // TODO: query the remote server if there is network access
                // TODO: insert the retrieved reports into the local sqlite database
                reports = await GetReportsByEmployee(UserDataCache.CurrentEmployeeData);
            }
            else if (!CrossConnectivity.IsSupported || !CrossConnectivity.Current.IsConnected)
            {
                // update from the local database
                reports = await GetReportsByEmployee(UserDataCache.CurrentEmployeeData);
            }

            foreach (var report in reports)
            {
                ReportList.Add(report);
            }

            // update the cached list
            InspectionDataCache.CachedReportsList = reports;
        }

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed(this, new PropertyChangedEventArgs(name));

        }

        #endregion
    }
}
