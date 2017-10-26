using SQLite.Net.Attributes;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Ameritrack_Xam.PCL.Models
{
    public class FaultPicture : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int? FaultPictureId { get; set; }

        private byte[] _faultPicture { get; set; }

        public int? FaultId { get; set; }

        public byte[] FaultPictures
        {
            get { return _faultPicture; }

            set 
            {
                if (value != _faultPicture)
                {
                    _faultPicture = value;
                    OnPropertyChanged(nameof(FaultPictures));
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
