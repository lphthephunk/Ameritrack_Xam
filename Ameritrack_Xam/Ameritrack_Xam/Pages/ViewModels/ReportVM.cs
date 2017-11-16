using System;
using System.Collections.ObjectModel;
using Ameritrack_Xam.PCL.Interfaces;
using Ameritrack_Xam.PCL.Models;
using Xamarin.Forms;

namespace Ameritrack_Xam.Pages.ViewModels
{
    public class ReportVM
    {
        IDatabaseServices DatabaseService = DependencyService.Get<IDatabaseServices>();
        public ObservableCollection<Fault> FaultList { get; set; }

        public ReportVM()
        {
        }
    }
}
