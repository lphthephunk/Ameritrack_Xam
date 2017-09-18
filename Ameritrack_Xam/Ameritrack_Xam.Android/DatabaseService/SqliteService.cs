using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Ameritrack_Xam.Droid.DatabaseService;
using Ameritrack_Xam.PCL.Interfaces;
using SQLite.Net;
using System.IO;
using SQLite.Net.Async;

[assembly: Dependency(typeof(SqliteService))]

namespace Ameritrack_Xam.Droid.DatabaseService
{
    public class SqliteService : ISQLite
    {
        public SqliteService() { }

        public SQLiteAsyncConnection GetConnection()
        {
            var dbFileName = "RailServeDb.db3";

            string documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            var path = Path.Combine(documentPath, dbFileName);
            if (!File.Exists(path))
            {
                File.Create(path);
            }

            var platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();

            var param = new SQLiteConnectionString(path, false);

            // return db connection
            var connection = new SQLiteAsyncConnection(() => new SQLiteConnectionWithLock(platform, param));
            return connection;
        }
    }
}