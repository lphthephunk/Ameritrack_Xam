using Ameritrack_Xam.PCL.Interfaces;
using Ameritrack_Xam.UWP.Services.DatabaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite.Net;
using System.IO.IsolatedStorage;
using System.IO;
using Windows.Storage;
using SQLite.Net.Async;

[assembly: Dependency(typeof(SqliteService))]

namespace Ameritrack_Xam.UWP.Services.DatabaseService
{
    public class SqliteService : ISQLite
    {
        public SqliteService() { }

        public SQLiteAsyncConnection GetConnection()
        {
            var dbFileName = "RailServeDb.db3";

            string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, dbFileName);

            var platform = new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT();

            var param = new SQLiteConnectionString(path, false);

            // return db connection
            var connection = new SQLiteAsyncConnection(() => new SQLiteConnectionWithLock(platform, param));
            return connection;
        }
    }
}
