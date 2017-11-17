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

    public class TrackList : ObservableCollection<String>
    {
        public string Heading { get; set; }
        public ObservableCollection<String> Tracks => this;
    }

    public class ReportVM : INotifyPropertyChanged
    {
        IDatabaseServices DatabaseService = DependencyService.Get<IDatabaseServices>();
        private ObservableCollection<TrackList> _listOfTracks { get; set; }
        public Dictionary<String, ObservableCollection<Fault>> trackDictionary = new Dictionary<String, ObservableCollection<Fault>>();

        public ObservableCollection<TrackList> ListOfTracks 
        { 
            get { return _listOfTracks; }
            set
            {
                _listOfTracks = value;
                OnPropertyChanged(nameof(ListOfTracks));
            }
        }

        Report ReportContext;

        public ReportVM(Report report)
        {
            ListOfTracks = new ObservableCollection<TrackList>();
            ReportContext = report;
        }

        private async Task<List<Fault>> GetFaultList(Report report)
        {
            var faultList = new List<Fault>();
            try
            {
                faultList = await DatabaseService.GetAllFaultsByReport(report.ReportId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Couldn't retrieve any faults for report" + ex.Message);
            }

            return faultList;
        }

        public async Task GetFaults()
        {
            var urgentTrackList = new TrackList();
            var nonUrgentTrackList = new TrackList();

            var faultList = await GetFaultList(ReportContext);
            foreach (var fault in faultList)
            {
                String trackName = fault.TrackName;

                if (fault.IsUrgent)
                {
                    if (!urgentTrackList.Tracks.Contains(trackName))
                    {
                        urgentTrackList.Tracks.Add(trackName);
                        Debug.WriteLine(urgentTrackList.Tracks.Contains(trackName));
                    }
                }
                else
                {
                    if (!nonUrgentTrackList.Tracks.Contains(trackName))
                    {
                        nonUrgentTrackList.Tracks.Add(trackName);
                    }
                }
            }

            urgentTrackList.Heading = "URGENT";
            nonUrgentTrackList.Heading = "NOT URGENT";

            ListOfTracks = new ObservableCollection<TrackList>()
            {
                urgentTrackList,
                nonUrgentTrackList
            };

            SortFaultsIntoTracks(faultList);
        }

        private void SortFaultsIntoTracks(List<Fault> faultList)
        {
            var trackList = new TrackList();

            foreach (var fault in faultList)
            {
                String trackName = fault.TrackName;
                if (trackDictionary.ContainsKey(trackName))
                {
                    trackDictionary[trackName].Add(fault);
                }
                else
                {
                    trackDictionary.Add(trackName, new ObservableCollection<Fault>());
                    trackDictionary[trackName].Add(fault);
                }
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
