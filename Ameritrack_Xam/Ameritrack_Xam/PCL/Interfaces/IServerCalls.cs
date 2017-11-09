using Ameritrack_Xam.PCL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ameritrack_Xam.PCL.Interfaces
{
    interface IServerCalls
    {
        Task<Employee> GetEmployee(int empCredentials);

        Task<List<Fault>> GetFaults();
    }
}
