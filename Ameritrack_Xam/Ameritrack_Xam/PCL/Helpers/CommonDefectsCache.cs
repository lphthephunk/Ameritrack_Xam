using Ameritrack_Xam.PCL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ameritrack_Xam.PCL.Helpers
{
    /// <summary>
    /// Retrieves the common defects from the main server on app startup
    /// </summary>
    public static class CommonDefectsCache
    {
        public static List<CommonDefects> UpdatedDefectsList { get; set; }

        public static Task GetDefectsFromServer()
        {
            // TODO: call the server for updated defects list
            //UpdatedDefectsList = new List<CommonDefects>();
            throw new NotImplementedException("There is no server-side database setup yet");
        }
    }
}
