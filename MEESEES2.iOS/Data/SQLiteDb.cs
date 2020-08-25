using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using MEESEES2.Commons;
using MEESEES2.iOS.Data;
using Xamarin.Forms;
using SQLite;
using System.IO;

[assembly: Dependency(typeof(SQLiteDb))]
namespace MEESEES2.iOS.Data
{
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, "meesees2.db3");
            return new SQLiteAsyncConnection(path);
        }
    }
}