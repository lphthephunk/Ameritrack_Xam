using Ameritrack_Xam.PCL.Interfaces;
using Ameritrack_Xam.PCL.Models;
using SQLite.Net.Async;
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
        SQLiteAsyncConnection Connection;
        // connect to the database and create all tables (if none exist)
        public DbConnect()
        {
            // this is getting our database connection asynchronously from our SQLite interface
            // this SQLite interface has been implemented explicitly on each platform (ie: andriod, IOS, UWP)
            Connection = DependencyService.Get<ISQLite>().GetConnectionAsync();
            // below, all three of our tables are being created asynchronously
            // this will happen at startup unless the tables already exist
            Connection.CreateTableAsync<Employee>();
            Connection.CreateTableAsync<Fault>();
            Connection.CreateTableAsync<CustomPin>();
            Connection.CreateTableAsync<CommonDefects>();
        }
    }
}
