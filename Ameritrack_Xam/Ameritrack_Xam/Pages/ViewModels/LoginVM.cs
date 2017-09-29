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

namespace Ameritrack_Xam.Pages.ViewModels
{
	public class LoginVM
	{
		ICredentialsService storeService;
		private DbConnect DatabaseService = new DbConnect();
		bool doCredentialsExist;

		public LoginVM()
		{
			storeService = DependencyService.Get<ICredentialsService>();
		}

		public async void InsertMockEmployee()
		{
			Employee emp = new Employee
			{
				EmployeeFirstName = "Test",
				EmployeeLastName = "User",
				EmployeeCredentials = "1234",
			};

			if (DatabaseService.GetEmployee("1234") != null)
			{
				await DatabaseService.InsertEmployee(emp);
			}
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
				doCredentialsExist = storeService.DoCredentialsExist();

				if (!doCredentialsExist)

				{
					storeService.SaveCredentials(emp.EmployeeCredentials);
				}

				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
