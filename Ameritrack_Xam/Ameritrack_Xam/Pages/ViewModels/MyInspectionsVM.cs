using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ameritrack_Xam.PCL.Interfaces;
using Ameritrack_Xam.PCL.Models;
using Ameritrack_Xam.PCL.Helpers;
using Xamarin.Forms;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;

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
                    await GetReports();
                    IsBusy = false;
                });
            }
        }

        public MyInspectionsVM()
        {
            ReportList = new ObservableCollection<Report>();
        }

        private async Task<ObservableCollection<Report>> GetReportsByEmployee(Employee employee)
        {
            var reports = new ObservableCollection<Report>();

            try 
            {
                
                List<Report> list = await DatabaseService.GetReportsByEmployee(employee);
                foreach (var report in list)
                    reports.Add(report);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Couldn't retrieve reports for employee " + ex.Message);
            }

            return reports;
        }

        public async Task GetReports()
        {
            ReportList.Clear();
            var reports = await GetReportsByEmployee(UserDataCache.CurrentEmployeeData);

            foreach (var report in reports)
            {
                ReportList.Add(report);
            }
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
