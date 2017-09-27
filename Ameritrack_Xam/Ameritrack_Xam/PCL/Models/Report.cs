using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

using Xamarin.Forms;

namespace Ameritrack_Xam.PCL.Models
{
	public class Report : INotifyPropertyChanged
	{
		[PrimaryKey, AutoIncrement]
		private int _reportId { get; set; }
		private string _clientName { get; set; }
		private string _clientContact { get; set; }
		private string _inspectorFirstName { get; set; }
		private string _inspectorLastName { get; set; }
		private string _address { get; set; }
		private DateTime _date { get; set; }
		private DateTime _time { get; set; }

		// One Report to many CustomPins
		// CascadeOperation is set to All so that if we delete this Report...
		// ... all CustomPins associated with this Report will be deleted in the database
		[OneToMany(CascadeOperations = CascadeOperation.All)]
		public List<CustomPin> CustomPins { get; set; }

		public int ReportId
		{
			get { return _reportId; }
			set
			{
				if (value != _reportId)
				{
					_reportId = value;
					OnPropertyChanged(nameof(ReportId));
				}
			}
		}

		public string ClientName
		{
			get { return _clientName; }
			set
			{
				if (value != _clientName)
				{
					_clientName = value;
					OnPropertyChanged(nameof(ClientName));
				}
			}
		}

		public string ClientContact
		{
			get { return _clientContact; }
			set
			{
				if (value != _clientContact)
				{
					_clientContact = value;
					OnPropertyChanged(nameof(ClientContact));
				}
			}
		}

		public string InspectorFirstName
		{
			get { return _inspectorFirstName; }
			set
			{
				if (value != _inspectorFirstName)
				{
					_inspectorFirstName = value;
					OnPropertyChanged(nameof(InspectorFirstName));
				}
			}
		}

		public string InspectorLastName
		{
			get { return _inspectorLastName; }
			set
			{
				if (value != _inspectorLastName)
				{
					_inspectorLastName = value;
					OnPropertyChanged(nameof(InspectorLastName));
				}
			}
		}

		public string Address
		{
			get { return _address; }
			set
			{
				if (value != _address)
				{
					_address = value;
					OnPropertyChanged(nameof(Address));
				}
			}
		}

		public DateTime Date
		{
			get { return _date; }
			set
			{
				if (value != _date)
				{
					_date = value;
					OnPropertyChanged(nameof(Date));
				}
			}
		}

		public DateTime Time
		{
			get { return _time; }
			set
			{
				if (value != _time)
				{
					_time = value;
					OnPropertyChanged(nameof(Time));
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

