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
	public class Employee : INotifyPropertyChanged
	{
		[PrimaryKey, AutoIncrement]
        public int? EmployeeId { get; set; }

		private string _employeeFirstName { get; set; }

		private string _employeeLastName { get; set; }

		private string _employeeCredentials { get; set; } // Ameritrack-given employee 

		public string EmployeeFirstName
		{
			get { return _employeeFirstName; }
			set
			{
				if (value != _employeeFirstName)
				{
					_employeeFirstName = value;
					OnPropertyChanged(nameof(EmployeeFirstName));
				}
			}
		}

		public string EmployeeLastName
		{
			get { return _employeeLastName; }
			set
			{
				if (value != _employeeLastName)
				{
					_employeeLastName = value;
					OnPropertyChanged(nameof(EmployeeLastName));
				}
			}
		}

		public string EmployeeCredentials
		{
			get { return _employeeCredentials; }
			set
			{
				if (value != _employeeCredentials)
				{
					_employeeCredentials = value;
					OnPropertyChanged(nameof(EmployeeCredentials));
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