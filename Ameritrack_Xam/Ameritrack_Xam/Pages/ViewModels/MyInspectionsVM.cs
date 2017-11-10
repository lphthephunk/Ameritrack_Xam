using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ameritrack_Xam.PCL.Interfaces;
using Ameritrack_Xam.PCL.Models;
using Xamarin.Forms;

namespace Ameritrack_Xam.Pages.ViewModels
{
    public class MyInspectionsVM
    {
        IDatabaseServices DatabaseService = DependencyService.Get<IDatabaseServices>();

        public MyInspectionsVM()
        {
        }

        public async Task<List<Report>> GetReportsByEmployee(Employee employee)
        {
            return await DatabaseService.GetReportsByEmployee(employee);
        }
    }
}
