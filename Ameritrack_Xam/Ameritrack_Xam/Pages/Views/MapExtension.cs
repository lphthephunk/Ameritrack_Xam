using Ameritrack_Xam.PCL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Ameritrack_Xam.Pages.Views
{
    public class MapExtension : Map
    {
        public event EventHandler<MapTapEventArgs> Tap;

        public List<Pin> ListOfPins = new List<Pin>();

        public MapExtension() { }

        public MapExtension(MapSpan region) : base(region)
        {

        }

        public void OnTap(Position coordinate)
        {
            OnTap(new MapTapEventArgs { Position = coordinate });
        }

        protected virtual void OnTap(MapTapEventArgs e)
        {
            var handler = Tap;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }

    public class MapTapEventArgs : EventArgs
    {
        public Position Position { get; set; }
    }
}
