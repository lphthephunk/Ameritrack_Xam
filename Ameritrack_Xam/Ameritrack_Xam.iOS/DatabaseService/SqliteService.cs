using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;
using Ameritrack_Xam.iOS.DatabaseService;
using Ameritrack_Xam.PCL.Interfaces;
using SQLite.Net;
using System.IO;

[assembly: Dependency(typeof(SqliteService))]

namespace Ameritrack_Xam.iOS.DatabaseService
{
    public class SqliteService : ISQLite
    {
        public SqliteService() { }

        public SQLiteConnection GetConnection()
        {
            var dbFileName = "RailServeDb.db3";
            string documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            string libraryPath = Path.Combine(documentPath, "..", "Library");
            var path = Path.Combine(libraryPath, dbFileName);

            if (!File.Exists(path))
            {
                File.Create(path);
            }

            var platform = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();

            // return db connection
            return new SQLite.Net.SQLiteConnection(platform, path);
        }
    }
}