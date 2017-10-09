using System;
namespace Ameritrack_Xam.PCL.Interfaces
{
	public interface ICredentialsService
	{
		//string EmployeeId { get; }

		void SaveCredentials(string EmployeeId);

		void DeleteCredentials();

		bool DoCredentialsExist();

        string GetCredentials();
	}
}
