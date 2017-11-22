using Ameritrack_Xam.PCL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ameritrack_Xam.PCL.Interfaces
{
    interface IServerDatabase
    {
        Task<Employee> GetEmployeeFromServer(string _empId);
        Task<List<Employee>> GetAllEmployeesFromServer();

        Task<List<CommonDefects>> GetAllCommonDefectsFromServer();

        Task<List<Fault>> GetAllFaultsFromServer();
        Task<Fault> GetFaultByCoordinatesFromServer(double _latitude, double _longitude);
        Task InsertFaultToServer(Fault _fault);
        Task<bool> InsertFaultListToServer(List<Fault> _faultList);
        Task InsertFaultPictureToServer(FaultPicture faultPicture);
        Task UpdateFaultAtServer(Fault _fault);
        Task<List<FaultPicture>> GetFaultPicturesFromServer(int? faultId);
        Task<List<Fault>> GetAllFaultsByAreaFromServer(string _area);
        Task<List<Fault>> GetAllFaultsByEmployeeFromServer(string _employeeId);
        Task<List<Fault>> GetAllFaultsByReportFromServer(int? _reportId);
        Task DeleteFaultFromServer(Fault _fault);

        Task<bool> InsertReportDataToServer(Report report);
        Task<List<Report>> GetReportsByEmployee(Employee employee);
    }
}
