using Ameritrack_Xam.PCL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ameritrack_Xam.PCL.Models;
using SQLite.Net.Async;
using Xamarin.Forms;
using Ameritrack_Xam.PCL.Helpers;
using Ameritrack_Xam.PCL.Services;

[assembly: Dependency(typeof(DatabaseService))]
namespace Ameritrack_Xam.PCL.Services
{
    public class DatabaseService : IDatabaseServices
    {
        ISQLite Database
        {
            get { return DependencyService.Get<ISQLite>(); }
        }

        readonly SQLiteAsyncConnection asyncConnection;
        private readonly AsyncLock locker = new AsyncLock();

        public async Task InitDatabase()
        {
            await CreateAllTables();
        }

        public DatabaseService()
        {
            asyncConnection = Database.GetConnectionAsync();
        }

        public async Task CreateAllTables()
        {
            using (await locker.LockAsync())
            {
               asyncConnection.CreateTableAsync<Employee>();
               asyncConnection.CreateTableAsync<Fault>();
               asyncConnection.CreateTableAsync<CustomPin>();
               asyncConnection.CreateTableAsync<CommonDefects>();
            }
        }

        public bool DeleteFault(Fault _fault)
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetAllEmployees()
        {
            throw new NotImplementedException();
        }

        public List<Fault> GetAllFaults()
        {
            throw new NotImplementedException();
        }

        public async Task<Employee> GetEmployee(string empId)
        {
            using (await locker.LockAsync())
            {
                var emp = asyncConnection.Table<Employee>().Where(x => x.EmployeeCredentials == empId).FirstOrDefaultAsync().Result;
                return emp;
            }
        }

        public async Task InsertEmployee(Employee _employee)
        {
            using (await locker.LockAsync())
            {
                await asyncConnection.InsertAsync(_employee);
            }
        }

        public bool InsertFault(Fault _fault)
        {
            throw new NotImplementedException();
        }

        public bool UpdateEmployee(Employee _employee)
        {
            throw new NotImplementedException();
        }

        public bool UpdateFault(Fault _fault)
        {
            throw new NotImplementedException();
        }
    }
}
