using Ameritrack_Xam.PCL.DAL;
using Ameritrack_Xam.PCL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ameritrack_Xam.Pages.ViewModels
{
    public class LoginVM
    {
        private DbConnect DatabaseService = new DbConnect();

        public async void InsertMockEmployee()
        {
            Employee emp = new Employee
            {
                EmployeeFirstName = "Test",
                EmployeeLastName = "User",
                EmployeeCredentials = 1234,
            };

            if (DatabaseService.GetEmployee(1234) != null)
            {
                await DatabaseService.InsertEmployee(emp);
            }
        }

        public async Task<Employee> PullEmployeeInfo()
        {
            var emp = await DatabaseService.GetEmployee(1234);

            return emp;
        }
    }
}
