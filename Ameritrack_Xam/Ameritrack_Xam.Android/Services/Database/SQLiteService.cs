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
using Ameritrack_Xam.Droid.Services.Database;
using Xamarin.Forms;
using Ameritrack_Xam.PCL.Interfaces;
using SQLite.Net;

[assembly: Dependency(typeof(SQLiteService))]
namespace Ameritrack_Xam.Droid.Services.Database
{

    public class SQLiteService : ISQLite
    {
        public SQLiteService() { }

        public SQLiteConnection GetConnection()
        {
            var sqliteFileName = "RailServeDb.db3";
        }
    }
}