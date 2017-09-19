using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace Ameritrack_Xam.PCL.Models
{
    public class CustomPin : INotifyPropertyChanged
    {
        private double _latitude { get; set; }
        private double _longitude { get; set; }
        private string _locationName { get; set; }

        public double Latitude
        {
            get { return _latitude; }
            set
            {
                if (value != _latitude)
                {
                    _latitude = value;
                    OnPropertyChanged(nameof(Latitude));
                }
            }
        }

        public double Longitude
        {
            get { return _longitude; }
            set
            {
                if (value != _longitude)
                {
                    _longitude = value;
                    OnPropertyChanged(nameof(Longitude));
                }
            }
        }

        public string LocationName
        {
            get { return _locationName; }
            set
            {
                if (value != _locationName)
                {
                    _locationName = value;
                    OnPropertyChanged(nameof(LocationName));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
