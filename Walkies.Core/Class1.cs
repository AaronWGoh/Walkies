using System;
using SQLite;
using SQLitePCL;
namespace Walkies.Cross
{
    public static class DataBaseGrabber
    {
       
        public static bool LoginTry(string UniqueId, string passcode)
        {
            var db = new SQLiteConnection(null);
            var stock = db.Table<DogProfile>();
            return true;
        }


    }
}
