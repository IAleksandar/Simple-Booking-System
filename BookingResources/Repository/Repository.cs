using Domain;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class Repository : IRepository
    {
        public void InsertData(SQLiteConnection connection)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "INSERT INTO Resource (Name, Quantity) VALUES ('Resource 1', 14)";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "INSERT INTO Resource (Name, Quantity) VALUES ('Resource 2', 10)";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "INSERT INTO Resource (Name, Quantity) VALUES ('Resource 3', 8)";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "INSERT INTO Resource (Name, Quantity) VALUES ('Resource 4', 2)";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "INSERT INTO Resource (Name, Quantity) VALUES ('Resource 5', 78)";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "INSERT INTO Resource (Name, Quantity) VALUES ('Resource 6', 11)";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "INSERT INTO Resource (Name, Quantity) VALUES ('Resource 7', 9)";
            sqlite_cmd.ExecuteNonQuery();
        }
        public List<Resource> ReadDataResources(SQLiteConnection connection)
        {
            List<Resource> resources = new List<Resource>();
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM Resource";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                resources.Add(
                    new Resource
                    {
                        Id = sqlite_datareader.GetInt16(0),
                        Name = sqlite_datareader.GetString(1),
                        Quantity = sqlite_datareader.GetInt16(2)
                    });
            }
            return resources;
        }
        public List<Booking> ReadDataBookings(SQLiteConnection connection, int ResourceId)
        {
            List<Booking> bookedResources = new List<Booking>();
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM Booking WHERE ResourceID = " + ResourceId;

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                bookedResources.Add(
                    new Booking
                    {
                        Id = sqlite_datareader.GetInt16(0),
                        DateFrom = sqlite_datareader.GetDateTime(1),
                        DateTo = sqlite_datareader.GetDateTime(2),
                        BookedQuantity = sqlite_datareader.GetInt16(3),
                        ResourceId = sqlite_datareader.GetInt16(4)
                    });
            }
            return bookedResources;
        }
        public void InsertDataBooking(SQLiteConnection connection, DateTime from, DateTime to, int quatity, int resource)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "INSERT INTO Booking (DateFrom, DateTo, BookedQuantity, ResourceID) " +
                "VALUES (" + "'" + from.ToString("yyyy-MM-dd HH:mm:ss") + "'"
                + ", " + "'" + to.ToString("yyyy-MM-dd HH:mm:ss") + "'"
                + ", " + quatity + ", " + resource + ")";
            sqlite_cmd.ExecuteNonQuery();
        }

        public void DeleteData(SQLiteConnection connection)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "DELETE FROM Booking";
            sqlite_cmd.ExecuteNonQuery();
        }
    }
}
