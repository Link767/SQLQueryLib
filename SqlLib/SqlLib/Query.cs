using Microsoft.Data.SqlClient;

namespace SqlLib
{
    public class Query : SqlManager
    {
        private string ConnectionString { get; set; }
        public Query(string ConnectionString) 
        {
            this.ConnectionString = ConnectionString;   
        }  
        public ResultQueryStatus Insert(string table, string columns, string data)
        {
            //string query = "INSERT INTO [User] (Login, Pass) VALUES ('Tom', 36)";
            string query = $"INSERT INTO {table} ({columns}) VALUES ({data})";
            ResultQueryStatus status;
            return status = SQLQueryPerform(query);
        }
        public ResultQueryStatus Update(string table, string columns, string values)
        {
            string query = $"UPDATE {table} SET {columns} = {values}";
            ResultQueryStatus status = SQLQueryPerform(query);
            return status = SQLQueryPerform(query);
        }
        public ResultQueryStatus Delete(string table, string where)
        {
            string query = $"DELETE {table} WHERE {where}";
            ResultQueryStatus status = SQLQueryPerform(query);
            return status = SQLQueryPerform(query);
        }
        private ResultQueryStatus SQLQueryPerform(string query)
        {
            ResultQueryStatus status;
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
                    command.ExecuteNonQuery();

                    transaction.Commit();
                    status = 0;
                }
                catch(Exception ex)
                {
                    transaction.Rollback(); //Откат
                    Console.WriteLine(ex.ToString());   
                    status = (ResultQueryStatus)1;
                }
            }
            return status;
        }
    }
}
