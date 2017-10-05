using Ameritrack_Xam.PCL.Helpers;
using Ameritrack_Xam.PCL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ameritrack_Xam.Pages.Views.PopupViewModels
{
    public class PinPopupVM
    {
        public ObservableCollection<CommonDefects> ListOfDefects { get; set; }

        public PinPopupVM()
        {
        }

        private Task GetListOfDefects()
        {
            //ListOfDefects = new ObservableCollection<CommonDefects>(CommonDefectsCache.UpdatedDefectsList);
            throw new NotImplementedException();
        } 
    }
}
