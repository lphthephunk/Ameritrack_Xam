using Ameritrack_Xam.PCL.Interfaces;
using Ameritrack_Xam.PCL.Models;
using SQLite.Net;
using SQLite.Net.Async;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ameritrack_Xam.PCL.DAL
{
    public class DbConnect : IDatabaseServices
    {
        SQLiteAsyncConnection asyncConnection;
        // connect to the database and create all tables (if none exist)
        public DbConnect()
        {
            asyncConnection = DependencyService.Get<ISQLite>().GetConnection();
            asyncConnection.CreateTableAsync<Employee>();
            asyncConnection.CreateTableAsync<Fault>();
        }

        // this region contains all necessary queries that we would need to make to the database (ie: insert, update, delete, read)
        #region Database Query Methods

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

        public async Task<Employee> GetEmployee(int empId)
        {
            var emp = await asyncConnection.QueryAsync<Employee>("Select * From [Employee]");
            return emp.FirstOrDefault();
        }

        public async Task InsertEmployee(Employee _employee)
        {
            await asyncConnection.InsertAsync(_employee);
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

        #endregion
    }
}
