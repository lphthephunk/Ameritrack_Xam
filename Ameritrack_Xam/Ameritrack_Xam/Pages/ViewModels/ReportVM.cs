using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Ameritrack_Xam.PCL.Interfaces;
using Ameritrack_Xam.PCL.Models;
using Xamarin.Forms;

namespace Ameritrack_Xam.Pages.ViewModels
{

    public class FaultList : ObservableCollection<Fault>
    {
        public string Heading { get; set; }
        public ObservableCollection<Fault> Faults => this;
    }

    public class ReportVM : INotifyPropertyChanged
    {
        IDatabaseServices DatabaseService = DependencyService.Get<IDatabaseServices>();
        private ObservableCollection<FaultList> _listOfFaults { get; set; }
        private Dictionary<String, ObservableCollection<Fault>> urgentDictionary = new Dictionary<String, ObservableCollection<Fault>>();
        private Dictionary<String, ObservableCollection<Fault>> nonUrgentDictionary = new Dictionary<String, ObservableCollection<Fault>>();


        public ObservableCollection<FaultList> ListOfFaults 
        { 
            get { return _listOfFaults; }
            set
            {
                _listOfFaults = value;
                OnPropertyChanged(nameof(ListOfFaults));
            }
        }

        private Report ReportContext;

        public ReportVM(Report report)
        {
            ListOfFaults = new ObservableCollection<FaultList>();

            ReportContext = report;
        }

        private async Task<List<Fault>> GetFaultList(Report report)
        {
            List<Fault> list = new List<Fault>();
            try
            {
                list = await DatabaseService.GetAllFaultsByReport(report.ReportId);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Couldn't retrieve any faults for report" + ex.Message);
            }

            return list;
        }

        public async Task GetFaults()
        {
            List<Fault> faultList = await GetFaultList(ReportContext);

            var urgentTrackList = new FaultList();
            var nonUrgentTrackList = new FaultList();

            foreach (var fault in faultList)
            {
                String trackName = fault.TrackName;

                if (fault.IsUrgent)
                {
                    if (urgentDictionary.ContainsKey(trackName))
                    {
                        urgentDictionary[trackName].Add(fault);
                    }
                    else
                    {
                        urgentDictionary.Add(trackName, new ObservableCollection<Fault>());
                        urgentDictionary[trackName].Add(fault);
                        urgentTrackList.Add(fault);
                    }
                }
                else
                {
                    if (nonUrgentDictionary.ContainsKey(trackName))
                    {
                        // add to list 
                        nonUrgentDictionary[trackName].Add(fault);
                    }
                    else
                    {
                        // create list
                        nonUrgentDictionary.Add(trackName, new ObservableCollection<Fault>());
                        nonUrgentDictionary[trackName].Add(fault);
                        nonUrgentTrackList.Add(fault);
                    }
                }
            }

            urgentTrackList.Heading = "Urgent";
            nonUrgentTrackList.Heading = "Non-Urgent";

            ListOfFaults = new ObservableCollection<FaultList>()
            {
                urgentTrackList,
                nonUrgentTrackList
            };
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
