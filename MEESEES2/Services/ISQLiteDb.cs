using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MEESEES2.Commons
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}
