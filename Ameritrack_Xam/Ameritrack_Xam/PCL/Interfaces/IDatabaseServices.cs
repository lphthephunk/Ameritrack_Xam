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

		Task<Employee> GetEmployee(string _empId);

		List<Employee> GetAllEmployees();

		Task InsertEmployee(Employee _employee);

		Task UpdateEmployee(Employee _employee);

		Task InsertFault(Fault _fault);

        Task InsertListFaults(List<Fault> faults);

		Task UpdateFault(Fault _fault);

		Task DeleteFault(Fault _fault);

        Task<Fault> GetFaultByCoordinates(double _latitude, double _longitude);

		Task<List<Fault>> GetAllFaults();

        Task<List<Fault>> GetAllFaultsByEmployee(string _employeeId);

        Task<List<Fault>> GetAllFaultsByArea(string _area);

        Task<List<Fault>> GetAllFaultsByReport(int? _reportId);

        Task InsertCommonDefects(CommonDefects defects);

        Task<List<CommonDefects>> GetAllCommonDefects();

        Task InsertReportData(Report report);

        Task<Report> GetReportData(Report report);

        Task<List<Report>> GetReportsByEmployee(Employee employee);

        Task InsertFaultPicture(FaultPicture faultPicture);

        Task<List<FaultPicture>> GetFaultPictures(int? faultId);
	}
}
