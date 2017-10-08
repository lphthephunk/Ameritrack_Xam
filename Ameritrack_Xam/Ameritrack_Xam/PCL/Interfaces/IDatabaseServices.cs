using Ameritrack_Xam.PCL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		bool InsertFault(Fault _fault);

		bool UpdateFault(Fault _fault);

		bool DeleteFault(Fault _fault);

		List<Fault> GetAllFaults();
	}
}
