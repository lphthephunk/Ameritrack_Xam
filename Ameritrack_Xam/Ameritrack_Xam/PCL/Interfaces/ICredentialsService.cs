using System;
namespace Ameritrack_Xam.PCL.Services
{
	public interface ICredentialsService
	{
		string EmployeeId { get; }

		void SaveCredentials(string EmployeeId);

		void DeleteCredentials();

		bool DoCredentialsExist();
	}
}
