using Ameritrack_Xam.PCL.Helpers;
using Ameritrack_Xam.PCL.Interfaces;
using Ameritrack_Xam.PCL.Models;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Ameritrack_Xam.Pages.ViewModels
{
    public class MapPageVM
    {
        IDatabaseServices DatabaseService = DependencyService.Get<IDatabaseServices>();
        IServerDatabase ServerDatabaseService = DependencyService.Get<IServerDatabase>();

        public MapPageVM() { }

        /// <summary>
        /// Inserts a fault's coordinates into the database
        /// </summary>
        /// <param name="pin"></param>
        /// <returns></returns>
        public async Task InsertFault(Pin pinContainingFault)
        {
            var fault = new Fault()
            {
                Latitude = pinContainingFault.Position.Latitude,
                Longitude = pinContainingFault.Position.Longitude,
            };

            await DatabaseService.InsertFault(fault);
        }

        /// <summary>
        /// Find a single fault when user clicks respective pin
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        public async Task<Fault> FindFault(double latitude, double longitude)
        {
            return await DatabaseService.GetFaultByCoordinates(latitude, longitude);
        }

        public async Task<List<Fault>> GetAllFaults()
        {
            return await DatabaseService.GetAllFaults();
        }

        public async Task<List<Fault>> GetAllFaultsByEmployee()
        {
            return await DatabaseService.GetAllFaultsByEmployee(UserDataCache.CurrentEmployeeData.EmployeeCredentials);
        }

        public async Task<Dictionary<bool, List<Fault>>> GetAllFaultsByArea()
        {
            // dictionaries index will be set to true if the fault data was retrieved from remote server....false otherwise
            Dictionary<bool, List<Fault>> faultsByAreaList = new Dictionary<bool, List<Fault>>();

            // check network connectivity
            if (CrossConnectivity.IsSupported && CrossConnectivity.Current.IsConnected)
            {
                // if connected to network, retrive faults from remote server
                var faults = await ServerDatabaseService.GetAllFaultsByAreaFromServer(InspectionDataCache.CurrentReportData.Address);
                faultsByAreaList.Add(true, faults);

                // update the local database with the newly retrieved faults
                await DatabaseService.InsertListFaults(faults);
            }
            else if (CrossConnectivity.IsSupported && !CrossConnectivity.Current.IsConnected && faultsByAreaList.Count == 0)
            {
                // get the faults that are stored locally on the device
                var faults = await DatabaseService.GetAllFaultsByArea(InspectionDataCache.CurrentReportData.Address);
                faultsByAreaList.Add(false, faults);
            }
            return faultsByAreaList;
        }

        public async Task<List<Fault>> GetAllFaultsByReport()
        {
            var faultsByReportList = await DatabaseService.GetAllFaultsByReport(InspectionDataCache.CurrentReportData.ReportId);
            return new List<Fault>(faultsByReportList);
        }

        public async Task<List<Pin>> ConstructPinsFromFaults(Dictionary<bool, List<Fault>> faults)
        {
            List<Pin> pinsList = new List<Pin>();

            return await Task.Run(() =>
            {
                foreach (var poppedFault in faults.Values)
                {
                    foreach (var fault in poppedFault)
                    {
                        var pin = new Pin()
                        {
                            Label = "Placeholder",
                            Position = new Position(fault.Latitude, fault.Longitude),
                            Type = PinType.Place
                        };

                        pinsList.Add(pin);
                    }
                }
                return pinsList;
            });
        }
    }
}
