using Ameritrack_Xam.PCL.DAL;
using Ameritrack_Xam.PCL.Models;
using Ameritrack_Xam.PCL.Services;
using Ameritrack_Xam.Pages.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Ameritrack_Xam.PCL.Interfaces;

namespace Ameritrack_Xam.Pages.ViewModels
{
	public class LoginVM
	{
        //ICredentialsService storeService;
        IDatabaseServices DatabaseService = DependencyService.Get<IDatabaseServices>();
		bool doCredentialsExist;

		public LoginVM()
		{
            InitDatabase();
		}

        public async Task InitDatabase()
        {
            await DatabaseService.InitDatabase();
        }

		public async Task InsertMockEmployee()
		{
			Employee emp = new Employee
			{
				EmployeeFirstName = "Test",
				EmployeeLastName = "User",
				EmployeeCredentials = "1234",
			};

            await DatabaseService.InsertEmployee(emp);
		}

		public async Task<Employee> PullEmployeeInfo()
		{
			var emp = await DatabaseService.GetEmployee("1234");

			return emp;
		}

        public async Task<bool> IsValidID(string _userEntryPassword)
        {
            var emp = await DatabaseService.GetEmployee(_userEntryPassword);

            if (emp.EmployeeCredentials != null)
            {
                //doCredentialsExist = storeService.DoCredentialsExist();

                //if (!doCredentialsExist)

                //{
                //    storeService.SaveCredentials(emp.EmployeeCredentials);
                //}

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
