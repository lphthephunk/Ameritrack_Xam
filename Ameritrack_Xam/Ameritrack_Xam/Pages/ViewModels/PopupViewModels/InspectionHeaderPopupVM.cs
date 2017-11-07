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

        public async Task InsertReportData(string clientContact, string clientAddress, string clientContactName)
        {
            var report = new Report()
            {
                Address = clientAddress,
                ClientName = clientContact,
                ClientContact = clientContactName,
                InspectorFirstName = UserDataCache.CurrentEmployeeData.EmployeeFirstName,
                InspectorLastName = UserDataCache.CurrentEmployeeData.EmployeeLastName,
                Date = DateTime.Today.Date,
                Time = DateTime.Now
            };

            InspectionDataCache.CurrentReportData = report;

            await DatabaseService.InsertReportData(report);
        }
    }
}
