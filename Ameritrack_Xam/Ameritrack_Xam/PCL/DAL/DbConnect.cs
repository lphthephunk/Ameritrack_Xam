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
            // this is getting our database connection asynchronously from our SQLite interface
            // this SQLite interface has been implemented explicitly on each platform (ie: andriod, IOS, UWP)
            asyncConnection = DependencyService.Get<ISQLite>().GetConnection();
            // below, all three of our tables are being created asynchronously
            // this will happen at startup unless the tables already exist
            asyncConnection.CreateTableAsync<Employee>();
            asyncConnection.CreateTableAsync<Fault>();
            asyncConnection.CreateTableAsync<CustomPin>();
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
            // emp is a dynamically typed variable that can be explicitly typed as a List<> object
            // using var is just quick for short-hand
            var emp = await asyncConnection.QueryAsync<Employee>("Select * From [Employee]");
            // our query within the paramter of QueryAsync<Employee> is selecting all data from the employee table
            // we specify which table we want to query within the angle brackets (ie: <Employee> is the table in this method that is being queried)

            // for testing purposes only, I have decided to return the first entry within this List<>
            // I did this because I know for a fact that there is only one entry within the Employee table
            return emp.FirstOrDefault();
        }

        public async Task InsertEmployee(Employee _employee)
        {
            // as opposed to the above method, where we have explicitly typed the SQL query, there are built in methods to do
            // more abstract operations

            // this extension method InsertAsync() can depict which table you want to insert into by referencing the data-type of the variable
            // being passed in

            // we await this operation because it is asynchronous; this keeps our UI from hanging
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
