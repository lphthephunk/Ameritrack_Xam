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
        private int _commonDefectId { get; set; }
        private string _defectName { get; set; }

        public int CommonDefectId
        {
            get { return _commonDefectId; }
            set
            {
                if (value != _commonDefectId)
                {
                    _commonDefectId = value;
                    OnPropertyChanged(nameof(CommonDefectId));
                }
            }
        }

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
