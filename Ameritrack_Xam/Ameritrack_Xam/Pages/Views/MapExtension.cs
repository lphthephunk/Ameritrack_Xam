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
        public event EventHandler<MapLongTouchEventArgs> Tap;
        public event EventHandler<PinTapEventArgs> PinTap;

        public List<Pin> ListOfPins = new List<Pin>();

        public MapExtension() { }

        public MapExtension(MapSpan region) : base(region) { }

        public void OnMapLongTouch(Position coordinate)
        {
            OnMapLongTouch(new MapLongTouchEventArgs { Position = coordinate });
        }

        protected virtual void OnMapLongTouch(MapLongTouchEventArgs e)
        {
            Tap?.Invoke(this, e);
        }

        public void OnPinTap(Pin pin)
        {
            OnPinTap(new PinTapEventArgs { CurrentPin = pin });
        }

        private void OnPinTap(PinTapEventArgs e)
        {
            PinTap?.Invoke(this, e);
        }
    }

    public class MapLongTouchEventArgs : EventArgs
    {
        public Position Position { get; set; }
    }

    public class PinTapEventArgs : EventArgs
    {
        public Pin CurrentPin { get; set; }
    }
}
