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

        public PinPopupVM()
        {
            if (CommonDefectsCache.UpdatedDefectsList != null)
            {
                ListOfDefects = new ObservableCollection<CommonDefects>(CommonDefectsCache.UpdatedDefectsList);
            }
        }

        public async Task<Fault> PopulatePopup(double lat, double lng)
        {
            var fault = await DatabaseService.GetFault(lat, lng);
            return fault;
        }

        /// <summary>
        /// Submits a fault model to the database
        /// </summary>
        /// <param name="trackName"></param>
        /// <param name="faultComments"></param>
        /// <param name="faultType"></param>
        /// <param name="isUrgent"></param>
        /// <returns></returns>
        public async Task SubmitFaultToDb(int currentPinId, string trackName, string faultComments, string faultType, bool isUrgent)
        {
            // TODO: add pictures Dictionary<string, byte[]> faultPics
            Fault fault = new Fault()
            {
                TrackName = trackName,
                FaultComments = faultComments,
                FaultType = faultType,
                Urgent = isUrgent,
                CustomPinId = currentPinId
            };

            await DatabaseService.InsertFault(fault);
        }
    }
}
