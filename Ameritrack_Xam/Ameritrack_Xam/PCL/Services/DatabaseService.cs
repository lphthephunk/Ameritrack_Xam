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
               asyncConnection.CreateTableAsync<Employee>();
               asyncConnection.CreateTableAsync<Fault>();
               asyncConnection.CreateTableAsync<CustomPin>();
               asyncConnection.CreateTableAsync<CommonDefects>();
            }
        }

        public async Task InsertFault(Fault _fault)
        {
            using (await locker.LockAsync())
            {
                await asyncConnection.InsertAsync(_fault);
            }
        }

        public Task UpdateFault(Fault _fault)
        {
            throw new NotImplementedException();
        }

        public Task DeleteFault(Fault _fault)
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

        public async Task<Fault> GetFault(double lat, double lng)
        {
            using (await locker.LockAsync())
            {
                var faults = await asyncConnection.Table<Fault>().ToListAsync();
                var pin = await GetOneCustomPin(lat, lng);
                return faults.Where(x => x.CustomPinId == pin.PinId).FirstOrDefault();
            }
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

        public bool UpdateEmployee(Employee _employee)
        {
            throw new NotImplementedException();
        }

        public async Task InsertCommonDefects(CommonDefects defects)
        {
            using (await locker.LockAsync())
            {
                await asyncConnection.InsertOrReplaceAsync(defects);
            }
        }

        public async Task<CustomPin> FindCustomPin(Pin tappedPin)
        {
            using (await locker.LockAsync())
            {
                var pin = await asyncConnection.Table<CustomPin>().Where(x => x.Latitude == tappedPin.Position.Latitude && x.Longitude == tappedPin.Position.Longitude).ToListAsync();
                return pin.FirstOrDefault();
            }
        }

        public async Task InsertCustomPin(CustomPin placedPin)
        {
            using (await locker.LockAsync())
            {
                await asyncConnection.InsertAsync(placedPin);
            }
        }

        public async Task<List<CustomPin>> GetAllCustomPins()
        {
            using (await locker.LockAsync())
            {
                return await asyncConnection.Table<CustomPin>().ToListAsync();
            }
        }

        public async Task<CustomPin> GetOneCustomPin(double lat, double lng)
        {
            using (await locker.LockAsync())
            {
                return await asyncConnection.Table<CustomPin>().Where(x => x.Latitude == lat && x.Longitude == lng).FirstOrDefaultAsync();
            }
        }
    }
}
