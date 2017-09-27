using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Ameritrack_Xam.PCL.Models
{    
    public class CustomPin : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        private int _pinId { get; set; }
        private double _latitude { get; set; }
        private double _longitude { get; set; }
        private string _locationName { get; set; }

        public Pin Pin { get; set; }

        // One CustomPin to many faults
        // CascadeOperation is set to All so that if we delete this CustomPin (ie: the location of the faults)..
        // ... all faults associated with this CustomPin will be deleted in the database
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Fault> Faults { get; set; }

		// Many CustomPins to one Report
		// CascadeRead allows us to only read the Report associated with this CustomPin
		// this is useful because we can delete a CustomPin without deleting the entire Report associated with the CustomPin
		[ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
		public Report Report { get; set; }

        public int PinId
        {
            get { return _pinId; }
            set
            {
                if (value != _pinId)
                {
                    _pinId = value;
                    OnPropertyChanged(nameof(PinId));
                }
            }
        }

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
