using Ameritrack_Xam.PCL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace Ameritrack_Xam.PCL.Interfaces
{
	interface IDatabaseServices
	{
        Task InitDatabase();

        Task CreateAllTables();

		Task<Employee> GetEmployee(string empId);

		List<Employee> GetAllEmployees();

		Task InsertEmployee(Employee _employee);

		bool UpdateEmployee(Employee _employee);

		Task InsertFault(Fault _fault);

		Task UpdateFault(Fault _fault);

		Task DeleteFault(Fault _fault);

        Task<Fault> GetFault(double latitude, double longitude);

		List<Fault> GetAllFaults();

        Task InsertCommonDefects(CommonDefects defects);

        Task<CustomPin> FindCustomPin(Pin pinPosition);

        Task InsertCustomPin(CustomPin placedPin);

        Task<List<CustomPin>> GetAllCustomPins();

        Task<CustomPin> GetOneCustomPin(double latitude, double longitude);
	}
}
