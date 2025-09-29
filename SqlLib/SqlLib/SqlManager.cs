using Microsoft.Data.SqlClient;
namespace SqlLib
{
    public class SqlManager
    {
        private string ConnectionString {  get; set; }
        public SqlManager(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
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
        public string Insert(string table, string columns, string data)
        {
            //string query = "INSERT INTO [User] (Login, Pass) VALUES ('Tom', 36)";
            string status; 
            string query = $"INSERT INTO {table} ({columns}) VALUES ({data})";
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                SqlTransaction transaction = conn.BeginTransaction();

                command.Connection = conn;
                command.Transaction = transaction;

               try
               {
                    command.CommandText = query;
                    command.ExecuteNonQuery(); // Выполнить запрос

                    transaction.Commit();
                    status = "Commit Done";
               }
               catch (Exception ex)
               {
                    transaction.Rollback();
                    status = $"err: {ex.GetType()}";
               }

            }
            return status;
        }
    }
}
