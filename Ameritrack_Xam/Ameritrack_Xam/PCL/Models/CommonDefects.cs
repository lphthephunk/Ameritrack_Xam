using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ameritrack_Xam.PCL.Models
{
    public class CommonDefects : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int? CommonDefectId { get; set; }
        private string _defectName { get; set; }

        public string DefectName
        {
            get { return _defectName; }
            set
            {
                if (value != _defectName)
                {
                    _defectName = value;
                    OnPropertyChanged(nameof(DefectName));
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
