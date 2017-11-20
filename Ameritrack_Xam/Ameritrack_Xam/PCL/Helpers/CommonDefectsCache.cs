using Ameritrack_Xam.PCL.Interfaces;
using Ameritrack_Xam.PCL.Models;
using Ameritrack_Xam.PCL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ameritrack_Xam.PCL.Helpers
{
    /// <summary>
    /// Retrieves the common defects from the main server on app startup
    /// </summary>
    public static class CommonDefectsCache
    {
        public static List<CommonDefects> UpdatedDefectsList { get; set; }

        internal static void CacheDefects(List<CommonDefects> list)
        {
            UpdatedDefectsList = new List<CommonDefects>(list);
        }
    }
}
