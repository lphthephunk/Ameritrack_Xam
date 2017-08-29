using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ameritrack_Xam.PCL.Models
{
    public class Fault : INotifyPropertyChanged
    {
        private int _faultId { get; set; }

        private string _contactName { get; set; }

        private int _contactNumber { get; set; }

        private string _faultComments { get; set; }

        private string _faultType { get; set; }

        private byte[] _faultPicture { get; set; }

        public int FaultId
        {
            get { return _faultId; }
            set
            {
                if (value != _faultId)
                {
                    _faultId = value;
                    OnPropertyChanged(nameof(FaultId));
                }
            }
        }

        public string ContactName
        {
            get { return _contactName; }
            set
            {
                if (value != _contactName)
                {
                    _contactName = value;
                    OnPropertyChanged(nameof(ContactName));
                }
            }
        }

        public int ContactNumber
        {
            get { return _contactNumber; }
            set
            {
                if (value != _contactNumber)
                {
                    _contactNumber = value;
                    OnPropertyChanged(nameof(ContactNumber));
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
