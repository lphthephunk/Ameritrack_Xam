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
using Xamarin.Forms.Maps;

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

        public DatabaseService(bool GetConnection = false) { }

        public async Task CreateAllTables()
        {
            using (await locker.LockAsync())
            {
               await asyncConnection.CreateTableAsync<Employee>().ConfigureAwait(false);
               await asyncConnection.CreateTableAsync<Fault>().ConfigureAwait(false);
               await asyncConnection.CreateTableAsync<CommonDefects>().ConfigureAwait(false);
            }
        }
        #region Fault Calls
        public async Task InsertFault(Fault _fault)
        {
            using (await locker.LockAsync())
            {
                await asyncConnection.InsertAsync(_fault);
            }
        }

        public async Task<List<Fault>> GetAllFaults()
        {
            using (await locker.LockAsync())
            {
                return await asyncConnection.Table<Fault>().ToListAsync();
            }
        }

        public async Task<List<Fault>> GetAllFaultsByEmployee(string _employeeId)
        {
            using (await locker.LockAsync())
            {
                return await asyncConnection.Table<Fault>().Where(x => x.Employee == _employeeId).ToListAsync();
            }
        }


        public async Task<List<Fault>> GetAllFaultsByArea(string _area)
        {
            using (await locker.LockAsync())
            {
                return await asyncConnection.Table<Fault>().Where(x => x.AreaAddress == _area).ToListAsync();
            }
        }

        public async Task<Fault> GetFault(double lat, double lng)
        {
            using (await locker.LockAsync())
            {
                return await asyncConnection.Table<Fault>().Where(x => x.Latitude == lat && x.Longitude == lng).FirstOrDefaultAsync();
            }
        }

        public async Task UpdateFault(Fault _fault)
        {
            using (await locker.LockAsync())
            {
                await asyncConnection.UpdateAsync(_fault);
            }
        }

        public async Task DeleteFault(Fault _fault)
        {
            using (await locker.LockAsync())
            {
                await asyncConnection.DeleteAsync(_fault);
            }
        }
        #endregion

        #region Employee Calls
        public List<Employee> GetAllEmployees()
        {
            throw new NotImplementedException();
        }

        public async Task<Employee> GetEmployee(string empId)
        {
            using (await locker.LockAsync())
            {
                return await asyncConnection.Table<Employee>().Where(x => x.EmployeeCredentials == empId).FirstOrDefaultAsync();
            }
        }

        public async Task InsertEmployee(Employee _employee)
        {
            using (await locker.LockAsync())
            {
                await asyncConnection.InsertAsync(_employee);
            }
        }

        public Task UpdateEmployee(Employee _employee)
        {
            throw new NotImplementedException();
        }
        #endregion

        public async Task InsertCommonDefects(CommonDefects defects)
        {
            using (await locker.LockAsync())
            {
                await asyncConnection.InsertOrReplaceAsync(defects);
            }
        }
    }
}
