using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
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
        // for the sake of variable security, we hide the privately bound variable from the end-user
        // that is why we have the public variables with get and set properties
        [PrimaryKey, AutoIncrement]
        private int _faultId { get; set; }

        private string _contactName { get; set; }

        private int _contactNumber { get; set; }

        private string _faultComments { get; set; }

        private string _faultType { get; set; }

        private byte[] _faultPicture { get; set; }

        // foreign key to associate this fault with a CustomPin (ie: a fault location)
        [ForeignKey(typeof(CustomPin))]
        public int CustomPinId { get; set; }

        // Many faults to one custom pin
        // CascadeRead allows us to only read the CustomPin associated with this fault
        // this is useful because we can delete a fault without deleting the entire CustomPin associated with the fault
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public CustomPin CustomPin { get; set; }

        // FaultId is the public variable associated with _faultId
        // we return _faultId with the the get method from FaultId
        // this is so the private variable cannot be directly modified
        public int FaultId
        {
            get { return _faultId; }
            set
            {
                // this is the body of our set method
                // this is where the value that the user is entering on the front-end gets bound to the private variable _faultId
                // that user-defined value is what is represented by the "value" attribute
                if (value != _faultId)
                {
                    _faultId = value;
                    // OnPropertyChanged is used for automatic updating of the front-end
                    // ie: if the user were to update the front end after entering a new value for FaultId, it would automatically be
                       //  the new value for _faultId would automatically be bubbled up to the front-end
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
