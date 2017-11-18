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
                await asyncConnection.CreateTableAsync<Report>().ConfigureAwait(false);
                await asyncConnection.CreateTableAsync<FaultPicture>().ConfigureAwait(false);
            }
        }

        public async Task InsertCommonDefects(CommonDefects defects)
        {
            using (await locker.LockAsync())
            {
                await asyncConnection.InsertOrReplaceAsync(defects);
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

        public async Task InsertListFaults(List<Fault> faults)
        {
            using (await locker.LockAsync())
            {
                await asyncConnection.InsertOrReplaceAllAsync(faults);
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

        public async Task<Fault> GetFaultByCoordinates(double lat, double lng)
        {
            using (await locker.LockAsync())
            {
                return await asyncConnection.Table<Fault>().Where(x => x.Latitude == lat && x.Longitude == lng).FirstOrDefaultAsync();
            }
        }

        public async Task<List<Fault>> GetAllFaultsByReport(int? _reportId)
        {
            using (await locker.LockAsync())
            {
                return await asyncConnection.Table<Fault>().Where(x => x.ReportId == _reportId).ToListAsync();
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

        #region Report Calls

        public async Task InsertReportData(Report report)
        {
            using (await locker.LockAsync())
            {
                await asyncConnection.InsertAsync(report);
            }
        }

        public async Task<Report> GetReportData(Report report)
        {
            using (await locker.LockAsync())
            {
                return await asyncConnection.Table<Report>().Where(x => x.DateTime == report.DateTime).FirstOrDefaultAsync();
            }
        }

        public async Task<List<Report>> GetReportsByEmployee(Employee employee)
        {
            using (await locker.LockAsync())
            {
                return await asyncConnection.Table<Report>().Where(x => x.InspectorFirstName == employee.EmployeeFirstName && x.InspectorLastName == employee.EmployeeLastName).ToListAsync();
            }
        }

        #endregion

        #region FaultPictures

        public async Task InsertFaultPicture(FaultPicture faultPicture)
        {
            using (await locker.LockAsync())
            {
                await asyncConnection.InsertAsync(faultPicture);
            }
        }

        public async Task<List<FaultPicture>> GetFaultPictures(int? faultId)
        {
            using (await locker.LockAsync())
            {
                return await asyncConnection.Table<FaultPicture>().Where(x => x.FaultId == faultId).ToListAsync();
            }
        }

        public Task<List<CommonDefects>> GetAllCommonDefects()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
