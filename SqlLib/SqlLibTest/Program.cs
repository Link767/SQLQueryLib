using SqlLib;
namespace SqlLibTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connStr = "Server=(localdb)\\MSSQLLocalDB;Database=BMDB;Trusted_Connection=True;";

            string dataBase = "BMDB";
            string table = "Loger";
            string column = "Loger";
            string data = "'Update4'";

            SqlManager sqlManager = new SqlManager(connStr);
            var test = sqlManager.RenameTable(dataBase, table, "Loger77");
            Console.WriteLine(test);

            //Query query = new Query(connStr);
            //var test = query.Update(table, column, data);
            //Console.WriteLine(test);
        }
    }
}
