using SQLite.Net.Attributes;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Ameritrack_Xam.PCL.Models
{
    public class FaultPicture : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int? FaultPictureId { get; set; }

        private byte[] _picture { get; set; }

        public int? FaultId { get; set; }

        public byte[] Picture
        {
            get { return _picture; }

            set 
            {
                if (value != _picture)
                {
                    _picture = value;
                    OnPropertyChanged(nameof(Picture));
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
