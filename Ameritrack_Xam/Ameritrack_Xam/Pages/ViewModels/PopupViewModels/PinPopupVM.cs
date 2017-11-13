using Ameritrack_Xam.PCL.Helpers;
using Ameritrack_Xam.PCL.Interfaces;
using Ameritrack_Xam.PCL.Models;
using Plugin.Media;
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
                var thisFault = await DatabaseService.GetFaultByCoordinates(lat, lng);
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
        public async Task SubmitFaultToDb(Fault fault)
        {
            // TODO: add pictures Dictionary<string, byte[]> faultPics
            //Fault fault = new Fault()
            //{
            //    FaultId = faultId,
            //    TrackName = trackName,
            //    Employee = UserDataCache.CurrentEmployeeData.EmployeeCredentials,
            //    AreaAddress = InspectionDataCache.CurrentReportData.Address,
            //    FaultComments = faultComments,
            //    FaultType = faultType,
            //    IsUrgent = isUrgent,
            //    Latitude = lat,
            //    Longitude = lng,
            //    ReportId = InspectionDataCache.CurrentReportData.ReportId
            //};

            if (fault.FaultId != null)
            {
                await DatabaseService.UpdateFault(fault);
            }
            else 
            {
                await DatabaseService.InsertFault(fault);
            }

        }

        public async Task DeleteFault(Fault fault)
        {
            await DatabaseService.DeleteFault(fault);
        }
    }
}
