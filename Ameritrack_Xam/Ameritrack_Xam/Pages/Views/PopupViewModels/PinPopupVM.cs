using Ameritrack_Xam.PCL.Helpers;
using Ameritrack_Xam.PCL.Interfaces;
using Ameritrack_Xam.PCL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Ameritrack_Xam.Pages.Views.PopupViewModels
{
    public class PinPopupVM
    {
        IDatabaseServices DatabaseService = DependencyService.Get<IDatabaseServices>();
        public ObservableCollection<CommonDefects> ListOfDefects { get; set; }
        public ObservableCollection<Fault> FaultData { get; set; }

        public PinPopupVM(CustomPin TappedPin)
        {
            if (CommonDefectsCache.UpdatedDefectsList != null)
            {
                ListOfDefects = new ObservableCollection<CommonDefects>(CommonDefectsCache.UpdatedDefectsList);
            }

            Task.Run(async () => { await PopulatePopup(TappedPin.Latitude, TappedPin.Longitude); });
        }

        public async Task PopulatePopup(double lat, double lng)
        {
            try
            {
                var theseFaults = await DatabaseService.GetFault(lat, lng);
                FaultData = new ObservableCollection<Fault>();
                FaultData.Add(theseFaults);
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Couldn't retrieve faults " + ex.Message);
            }
        }

        /// <summary>
        /// Submits a fault model to the database
        /// </summary>
        /// <param name="trackName"></param>
        /// <param name="faultComments"></param>
        /// <param name="faultType"></param>
        /// <param name="isUrgent"></param>
        /// <returns></returns>
        public async Task SubmitFaultToDb(int? associatedPin, string trackName, string faultComments, string faultType, bool isUrgent)
        {
            // TODO: add pictures Dictionary<string, byte[]> faultPics
            Fault fault = new Fault()
            {
                TrackName = trackName,
                FaultComments = faultComments,
                FaultType = faultType,
                Urgent = isUrgent,
                CustomPinId = associatedPin,
            };

            await DatabaseService.InsertFault(fault);
        }
    }
}
