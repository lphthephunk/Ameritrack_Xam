using SQLite.Net;
using SQLite.Net.Async;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ameritrack_Xam.PCL.Interfaces
{
    public interface ISQLite
    {
        SQLiteAsyncConnection GetConnectionAsync();
    }
}
