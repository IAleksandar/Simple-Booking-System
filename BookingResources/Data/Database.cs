using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Database
    {
        static SQLiteConnection connection = ConnectionClass.CreateConnection();
        public static void CreateTableResource()
        {
            SQLiteCommand sqlite_cmd;
            string Createsql =
                "CREATE TABLE IF NOT EXISTS Resource " +
                "(" +
                    "ID INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "Name NVARCHAR(250), " +
                    "Quantity INTEGER" +
                ")";
            sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = Createsql;
            sqlite_cmd.ExecuteNonQuery();
        }

        public static void CreateTableBooking()
        {
            SQLiteCommand sqlite_cmd;
            string Createsql =
                "CREATE TABLE IF NOT EXISTS Booking " +
                "(" +
                    "ID INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "DateFrom DATEIME, " +
                    "DateTo DATEIME, " +
                    "BookedQuantity INTEGER, " +
                    "ResourceID INTEGER, " +
                    "FOREIGN KEY (ResourceID) REFERENCES Resource(ID)" +
                ")";
            sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = Createsql;
            sqlite_cmd.ExecuteNonQuery();
        }
    }
}
