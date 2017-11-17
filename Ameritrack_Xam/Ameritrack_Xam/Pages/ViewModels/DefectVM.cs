using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Ameritrack_Xam.PCL.Models;

namespace Ameritrack_Xam.Pages
{
    public class DefectVM
    {
        private ObservableCollection<Fault> _listOfFaults { get; set; }
        public ObservableCollection<Fault> ListOfFaults
        {
            get { return _listOfFaults; }
            set
            {
                _listOfFaults = value;
                OnPropertyChanged(nameof(ListOfFaults));
            }
        }

        public DefectVM(ObservableCollection<Fault> faults, bool isUrgent)
        {
            GetDefects(faults, isUrgent);
        }

        public void GetDefects(ObservableCollection<Fault> faults, bool isUrgent)
        {
            var faultList = new ObservableCollection<Fault>();

            foreach (var fault in faults) {
                if (fault.IsUrgent == isUrgent) {
                    faultList.Add(fault);
                }
            }

            ListOfFaults = faultList;
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
