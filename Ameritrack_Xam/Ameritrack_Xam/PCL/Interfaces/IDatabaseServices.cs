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
        Task<Employee> GetEmployee(int empId);

        List<Employee> GetAllEmployees();

        Task InsertEmployee(Employee _employee);

        bool UpdateEmployee(Employee _employee);

        bool InsertFault(Fault _fault);

        bool UpdateFault(Fault _fault);

        bool DeleteFault(Fault _fault);

        List<Fault> GetAllFaults();
    }
}
