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

        public async Task<CustomPin> FindCustomPin(Pin tappedPin)
        {
            var pin = await DatabaseService.FindCustomPin(tappedPin);
            return pin;
        }

        /// <summary>
        /// Inserts the placed pin into the local SQLite database
        /// </summary>
        /// <param name="placedPin"></param>
        /// <returns></returns>
        public async Task InsertPin(CustomPin placedPin)
        {
            await DatabaseService.InsertCustomPin(placedPin);
        }

        public async Task<List<CustomPin>> GetAllPins()
        {
            return await DatabaseService.GetAllCustomPins();
        }
    }
}
