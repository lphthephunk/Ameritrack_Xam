using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace Ameritrack_Xam.PCL.Models
{
    public class Fault : INotifyPropertyChanged
    {
        // for the sake of variable security, we hide the privately bound variable from the end-user
        // that is why we have the public variables with get and set properties
        [PrimaryKey, AutoIncrement]
        public int? FaultId { get; set; }
        private string _employee { get; set; }
        private string _trackName { get; set; }
        private string _areaAddress { get; set; }
        private string _faultComments { get; set; }
        private string _faultType { get; set; }
        private byte[] _faultPicture { get; set; }
        private bool _isUrgent { get; set; }
		private double _latitude { get; set; }
		private double _longitude { get; set; }

        public int? ReportId { get; set; }

        //// foreign key to associate this fault with a CustomPin (ie: a fault location)
        //[ForeignKey(typeof(CustomPin))]
        //public int? CustomPinId { get; set; }

        //// Many faults to one custom pin
        //// CascadeRead allows us to only read the CustomPin associated with this fault
        //// this is useful because we can delete a fault without deleting the entire CustomPin associated with the fault
        //[ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        //public CustomPin CustomPin { get; set; }

        public string TrackName
        {
            get { return _trackName; }
            set
            {
                if (value != _trackName)
                {
                    _trackName = value;
                    OnPropertyChanged(nameof(TrackName));
                }
            }
        }

        public string Employee
        {
            get { return _employee; }
            set
            {
                if (value != _employee)
                {
                    _employee = value;
                    OnPropertyChanged(nameof(Employee));
                }
            }
        }

        public string AreaAddress
        {
            get { return _areaAddress; }
            set
            {
                if (value != _areaAddress)
                {
                    _areaAddress = value;
                    OnPropertyChanged(nameof(AreaAddress));
                }
            }
        }

        public string FaultComments
        {
            get { return _faultComments; }
            set
            {
                if (value != _faultComments)
                {
                    _faultComments = value;
                    OnPropertyChanged(nameof(FaultComments));
                }
            }
        }

        public string FaultType
        {
            get { return _faultType; }
            set
            {
                if (value != _faultType)
                {
                    _faultType = value;
                    OnPropertyChanged(nameof(FaultType));
                }
            }
        }

        public byte[] FaultPicture
        {
            get { return _faultPicture; }
            set
            {
                if (value != _faultPicture)
                {
                    _faultPicture = value;
                    OnPropertyChanged(nameof(FaultPicture));
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

		public bool IsUrgent
		{
			get { return _isUrgent; }
			set
			{
				if (value != _isUrgent)
				{
					_isUrgent = value;
					OnPropertyChanged(nameof(IsUrgent));
				}
			}
		}

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Detects which attribute of Fault is being updated by the user on the front-end
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // This is simply stating: "Have I been called? If I have, then I'll identify the sender (this) and update the 
            // attribute defined as propertyName
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
