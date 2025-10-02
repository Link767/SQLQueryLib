using Microsoft.Data.SqlClient;

namespace SqlLib
{
    public class Query : SqlManager
    {
        public Query(string ConnectionString) : base(ConnectionString) { }
        
        public string Insert(string tableName, string columns, string data)
        {
            string query = $"INSERT INTO {tableName} ({columns}) VALUES ({data})";
            return SQLQueryPerform(query);
        }
        public string Update(string tableName, string columns, string values)
        {
            string query = $"UPDATE {tableName} SET {columns} = {values}";
            return SQLQueryPerform(query);
        }
        public string Delete(string tableName, string where)
        {
            string query = $"DELETE {tableName} WHERE {where}";
            return SQLQueryPerform(query);
        }
        //New 02.10.25
        public string Select(string columns, string tableName)
        {
            string query = $"SELECT {columns} FROM {tableName}";
            return SQLQueryPerform(query);
        }
        public string SelectFullTable(string tableName)
        {
            string query = $"SELECT * FROM {tableName}";
            return SQLQueryPerform(query);
        }
        // You can write big requests here.
        public string MyQuery(string query)
        {
            return SQLQueryPerform(query);
        }
    }
}
