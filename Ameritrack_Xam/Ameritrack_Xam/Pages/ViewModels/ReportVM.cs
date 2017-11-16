using System;
using System.Collections.ObjectModel;
using Ameritrack_Xam.PCL.Interfaces;
using Ameritrack_Xam.PCL.Models;
using Xamarin.Forms;

namespace Ameritrack_Xam.Pages.ViewModels
{
    public class FaultList : ObservableCollection<Fault> {
        public string Heading { get; set; }
        public ObservableCollection<Fault> Faults;
    }

    public class ReportVM : INotifyPropertyChanged
    {
        IDatabaseServices DatabaseService = DependencyService.Get<IDatabaseServices>();
        private ObservableCollection<FaultList> _listOfFaults { get; set; }

        public ObservableCollection<Fault> ListOfFaults 
        {
            set 
            {
                _listOfFaults = value;
                OnPropertyChanged(nameof(ListOfFaults))
            }
        }

        public ReportVM(Report report)
        {
            
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
