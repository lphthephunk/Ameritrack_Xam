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

        public PinPopupVM(Fault fault)
        {
            if (CommonDefectsCache.UpdatedDefectsList != null)
            {
                ListOfDefects = new ObservableCollection<CommonDefects>(CommonDefectsCache.UpdatedDefectsList);
            }
        }

        public async Task PopulatePopup(double lat, double lng)
        {
            try
            {
                var thisFault = await DatabaseService.GetFault(lat, lng);
                FaultData = new ObservableCollection<Fault>();
                FaultData.Add(thisFault);
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
        public async Task SubmitFaultToDb(string trackName, string faultComments, string faultType, bool isUrgent, double lat, double lng)
        {
            // TODO: add pictures Dictionary<string, byte[]> faultPics
            Fault fault = new Fault()
            {
                TrackName = trackName,
                Employee = UserDataCache.CurrentEmployeeData.EmployeeCredentials,
                AreaAddress = InspectionDataCache.CurrentReportData.Address,
                FaultComments = faultComments,
                FaultType = faultType,
                Urgent = isUrgent,
                Latitude = lat,
                Longitude = lng
            };

            await DatabaseService.UpdateFault(fault);
        }

        public async Task DeleteFault(Fault fault)
        {
            await DatabaseService.DeleteFault(fault);
        }
    }
}
