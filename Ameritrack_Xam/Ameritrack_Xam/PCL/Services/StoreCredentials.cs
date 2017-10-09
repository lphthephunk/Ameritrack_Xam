using Ameritrack_Xam.PCL.Interfaces;
using System;
using System.Linq;
using Xamarin.Auth;
using Xamarin.Forms;

[assembly: Dependency(typeof(Ameritrack_Xam.PCL.Services.StoreCredentials))]

namespace Ameritrack_Xam.PCL.Services
{
	public class StoreCredentials : ICredentialsService
	{
		//public string EmployeeId
		//{
		//	get
		//	{
		//		var employeeInfo = AccountStore.Create().FindAccountsForService("StoreUserInfo").FirstOrDefault();
		//		return (employeeInfo != null) ? employeeInfo.Username : null;
		//	}
		//}

		public void SaveCredentials(string _employeeId)
		{
			if (!string.IsNullOrWhiteSpace(_employeeId))
			{
				Account employeeInfoId = new Account
				{
					Username = _employeeId
				};
				// currently set up to where password is employee ID
				employeeInfoId.Properties.Add("EmployeeID", _employeeId);
				AccountStore.Create().Save(employeeInfoId, "StoreUserInfo");
			}
		}

		public void DeleteCredentials()
		{
			var account = AccountStore.Create().FindAccountsForService("StoreUserInfo").FirstOrDefault();
			if (account != null)
			{
				AccountStore.Create().Delete(account, "StoreUserInfo");
			}
		}

		public bool DoCredentialsExist()
		{
			return AccountStore.Create().FindAccountsForService("StoreUserInfo").Any() ? true : false;
		}

        public string GetCredentials()
        {
            return AccountStore.Create().FindAccountsForService("StoreUserInfo").FirstOrDefault().Username;
        }
	}
}

