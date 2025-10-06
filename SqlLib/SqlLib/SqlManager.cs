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

        protected List<string> SQLQueryPerformList(string query)
        {
            var list = new List<string>();

            using (var conn = new SqlConnection(_ConnectionString))
            using (var command = new SqlCommand(query, conn))
            {
                conn.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var values = new object[reader.FieldCount]; //Creating an array of objects
                        reader.GetValues(values); //Fills the values array with all the data of the current row.
                        list.Add(string.Join(", ", values.Select(v => v.ToString())));//add an entry to the list and form a string
                    }
                }
            }
            return list;
        }
    }
}
