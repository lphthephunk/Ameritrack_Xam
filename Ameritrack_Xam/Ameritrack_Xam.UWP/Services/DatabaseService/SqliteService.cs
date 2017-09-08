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

[assembly: Dependency(typeof(SqliteService))]

namespace Ameritrack_Xam.UWP.Services.DatabaseService
{
    public class SqliteService : ISQLite
    {
        public SqliteService() { }

        public SQLiteConnection GetConnection()
        {
            var dbFileName = "RailServeDb.db3";

            string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, dbFileName);

            var platform = new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT();

            // return db connection
            return new SQLite.Net.SQLiteConnection(platform, path);
        }
    }
}
