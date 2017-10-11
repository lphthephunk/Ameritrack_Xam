using Ameritrack_Xam.PCL.Helpers;
using Ameritrack_Xam.PCL.Interfaces;
using Ameritrack_Xam.PCL.Models;
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
            return await DatabaseService.GetFault(latitude, longitude);
        }

        public async Task<List<Fault>> GetAllFaults()
        {
            return await DatabaseService.GetAllFaults();
        }

        public async Task<List<Fault>> GetAllFaultsByEmployee()
        {
            return await DatabaseService.GetAllFaultsByEmployee(UserDataCache.CurrentEmployeeData.EmployeeCredentials);
        }

        public async Task<List<Fault>> GetAllFaultsByArea()
        {
            var faultsByAreaList = await DatabaseService.GetAllFaultsByArea(InspectionDataCache.CurrentReportData.Address);
            return new List<Fault>(faultsByAreaList);
        }

        public async Task<List<Pin>> ConstructPinsFromFaults(List<Fault> faults)
        {
            List<Pin> pinsList = new List<Pin>();

            return await Task.Run(() =>
            {
                foreach (var fault in faults)
                {
                    var pin = new Pin()
                    {
                        Label = "Placeholder",
                        Position = new Position(fault.Latitude, fault.Longitude),
                        Type = PinType.Place
                    };

                    pinsList.Add(pin);
                }
                return pinsList;
            });
        }
    }
}
