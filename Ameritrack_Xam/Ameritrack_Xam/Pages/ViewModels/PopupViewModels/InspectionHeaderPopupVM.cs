using Ameritrack_Xam.PCL.Helpers;
using Ameritrack_Xam.PCL.Interfaces;
using Ameritrack_Xam.PCL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ameritrack_Xam.Pages.Views.PopupViewModels
{
    public class InspectionHeaderPopupVM
    {
        IDatabaseServices DatabaseService = DependencyService.Get<IDatabaseServices>();

        public InspectionHeaderPopupVM() { }

        public async Task InsertReportData(string customerName, string customerAddress, string customerContactName)
        {
            var report = new Report()
            {
                Address = customerAddress,
                ClientName = customerName,
                InspectorFirstName = UserDataCache.CurrentEmployeeData.EmployeeFirstName,
                InspectorLastName = UserDataCache.CurrentEmployeeData.EmployeeLastName,
                Date = DateTime.Today.Date,
                Time = DateTime.Now
            };

            await DatabaseService.InsertReportData(report);

            InspectionDataCache.CurrentReportData = await DatabaseService.GetReportData(report);
        }
    }
}
