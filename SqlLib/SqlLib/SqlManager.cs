using Microsoft.Data.SqlClient;
namespace SqlLib
{
    public class SqlManager
    {
        private string ConnectionString {get; set;}
        public enum ResultQueryStatus
        {
            Completed = 0,
            NotCompleted = 1
        }
        public void DBInf(SqlConnection conn)
        {
            //Open
            Console.WriteLine("Connection String: " + conn.ConnectionString);
            Console.WriteLine("State: " + conn.State);
            Console.WriteLine("Database: " + conn.Database);
            Console.WriteLine("DataSource: " + conn.DataSource);
            Console.WriteLine("ServerVersion: " + conn.ServerVersion + "\n");

        }
    }
}
