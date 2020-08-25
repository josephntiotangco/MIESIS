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

using MEESEES2.Droid.Data;
using MEESEES2.Droid;
using MEESEES2.Commons;
using SQLite;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteDb))]
namespace MEESEES2.Droid.Data
{
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, "meesees2.db3");

            return new SQLiteAsyncConnection(path);
        }
    }
}