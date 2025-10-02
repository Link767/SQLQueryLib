using Microsoft.Data.SqlClient;
namespace SqlLib
{
    public class SqlManager
    {
        protected string _ConnectionString {get; set;}
        public SqlManager() {}
        public SqlManager(string ConnectionString)
        {
            _ConnectionString = ConnectionString;
        }
        public enum ResultQueryStatus
        {
            Completed = 0,
            NotCompleted = 1
        }
        public void DBInf()
        {
            using(SqlConnection conn = new SqlConnection(_ConnectionString))
            {
                conn.Open();
                Console.WriteLine("Connection String: " + conn.ConnectionString);
                Console.WriteLine("State: " + conn.State);
                Console.WriteLine("Database: " + conn.Database);
                Console.WriteLine("DataSource: " + conn.DataSource);
                Console.WriteLine("ServerVersion: " + conn.ServerVersion + "\n");
            }
        }
        public string RenameTable(string database, string tableName, string NewTableName)
        {
            string query = $"USE {database};\n EXEC sp_rename '{tableName}', '{NewTableName}'";
            return SQLDLLComand(query);
        }
        public string DropTable(string tableName)
        {
            string query = $"DROP TABLE {tableName}";
            return SQLDLLComand(query);
        }

        protected string SQLDLLComand(string query)
        {
            ResultQueryStatus status;
            string report;
            int row;
            try
            {
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {
                    conn.Open();
                    using(SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        row = cmd.ExecuteNonQuery();
                        status = 0;
                        report = $"{status}f xnj ";
                    }
                }
            }
            catch (Exception ex)
            {
                status = (ResultQueryStatus)1;
                report = $"{status}:\n{ex}";
            }
            return report;
        }

        protected string SQLQueryPerform(string query)
        {
            ResultQueryStatus status;
            string report;
            int row;
            using (SqlConnection conn = new SqlConnection(_ConnectionString))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                SqlTransaction transaction = conn.BeginTransaction();

                command.Connection = conn;
                command.Transaction = transaction;
                try
                {
                    command.CommandText = query;
                    row = command.ExecuteNonQuery();

                    transaction.Commit();
                    status = 0;
                    report = $"{status}\nInvolved: {row}";
                }
                catch (Exception ex)
                {   //Откат 
                    status = (ResultQueryStatus)1;
                    report = $"{status}:\n{ex}";
                    transaction.Rollback();
                }
            }
            return report;
        }
    }
}
