using Ameritrack_Xam.PCL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ameritrack_Xam.PCL.Helpers
{
    public static class InspectionDataCache
    {
        public static Report CurrentReportData { get; set; }

        public static bool IsReportStarted { get; set; }

        public static bool IsReportListFetched { get; set; }

        public static List<Report> CachedReportsList { get; set; }
    }
}
