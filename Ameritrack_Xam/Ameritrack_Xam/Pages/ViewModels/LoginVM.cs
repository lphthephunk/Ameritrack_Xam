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
        IServerDatabase ServerDatabaseService = DependencyService.Get<IServerDatabase>();

        private Employee employee;

		public LoginVM()
		{
            InitDatabase();
		}

        public async Task InitDatabase()
        {
            await DatabaseService.InitDatabase();
            CommonDefectsCache.GetDefectsFromServer();
        }

        public async Task<bool> IsValidID(string _userEntryPassword)
        {
            // make call to server
            employee = await ServerDatabaseService.GetEmployeeFromServer(_userEntryPassword);

            if (employee != null)
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

        public void SetUserData()
        {
            UserDataCache.CurrentEmployeeData = employee;
        }
    }
}
