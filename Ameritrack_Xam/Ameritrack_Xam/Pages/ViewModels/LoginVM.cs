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
using Ameritrack_Xam.PCL.Helpers;

namespace Ameritrack_Xam.Pages.ViewModels
{
	public class LoginVM
	{
        ICredentialsService StoreService = DependencyService.Get<ICredentialsService>();
        IDatabaseServices DatabaseService = DependencyService.Get<IDatabaseServices>();

		public LoginVM()
		{
            InitDatabase();
		}

        public async Task InitDatabase()
        {
            await DatabaseService.InitDatabase();
            CommonDefectsCache.GetDefectsFromServer();
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

            if (emp != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void StoreCredentials(string credentials)
        {
            if (!StoreService.DoCredentialsExist())
            {
                StoreService.SaveCredentials(credentials);
            }
        }

        public void RemoveCredentials()
        {
            if (StoreService.DoCredentialsExist())
            {
                StoreService.DeleteCredentials();
            }
        }

        public string GetCredentials()
        {
            return StoreService.GetCredentials();
        }

        public bool IsStored()
        {
            if (StoreService.DoCredentialsExist())
            {
                return true;
            }
            else
                return false;
        }
    }
}
