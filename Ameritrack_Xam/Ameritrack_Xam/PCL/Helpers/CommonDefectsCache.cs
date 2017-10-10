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

        public static void GetDefectsFromServer()
        {
            // TODO: call the server for updated defects list
            //UpdatedDefectsList = new List<CommonDefects>();

            TempDefectCreator();

            // cache the defects
            //await CacheCommonDefects();
        }

        /// <summary>
        /// This will be removed once the actual server db is created
        /// </summary>
        private static void TempDefectCreator()
        {
            UpdatedDefectsList = new List<CommonDefects>()
            {
                new CommonDefects
                {
                    DefectName = "Bad Ties"
                },
                new CommonDefects
                {
                    DefectName = "Temp defect 2"
                },
                new CommonDefects
                {
                    DefectName = "Temp defect 3"
                }
            };
        }

        private static async Task CacheCommonDefects(CommonDefects defects)
        {
            DatabaseService db = new DatabaseService(false);
            await db.InsertCommonDefects(defects);
        }
    }
}
