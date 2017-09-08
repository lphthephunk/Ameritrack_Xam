using Ameritrack_Xam.PCL.Interfaces;
using Ameritrack_Xam.PCL.Models;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ameritrack_Xam.PCL.DAL
{
    public class DbConnect
    {
        SQLiteConnection connect;
        // connect to the database and create all tables (if none exist)
        public DbConnect()
        {
            connect = DependencyService.Get<ISQLite>().GetConnection();
            connect.CreateTable<Employee>();
            connect.CreateTable<Fault>();
        }

        // this region contains all necessary queries that we would need to make to the database (ie: insert, update, delete, read)
        #region Database Query Methods

        // ex: public List<Employee> GetEmployees() { }
        // public List<Fault> GetFaults() { }

#endregion
    }
}
