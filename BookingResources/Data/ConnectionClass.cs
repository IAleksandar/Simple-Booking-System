using System.Data.SQLite;

namespace Data
{
    public static class ConnectionClass
    {
        public static SQLiteConnection CreateConnection()
        {

            SQLiteConnection sQLiteConnection;
            sQLiteConnection = new SQLiteConnection("Data Source=bookingResources.db; Version = 3; New = True; Compress = True; ");
            try
            {
                sQLiteConnection.Open();
            }
            catch (Exception ex)
            {

            }
            return sQLiteConnection;
        }
    }
}