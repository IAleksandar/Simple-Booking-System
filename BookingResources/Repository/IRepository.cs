using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Repository
{
    public interface IRepository
    {
        void InsertData(SQLiteConnection connection);
        List<Resource> ReadDataResources(SQLiteConnection connection);
        List<Booking> ReadDataBookings(SQLiteConnection connection, int ResourceId);
        void InsertDataBooking(SQLiteConnection connection, DateTime from, DateTime to, int quatity, int resource);
        void DeleteData(SQLiteConnection connection);
    }
}
