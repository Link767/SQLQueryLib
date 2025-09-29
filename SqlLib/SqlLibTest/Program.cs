using SqlLib;
namespace SqlLibTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connStr = "Server=(localdb)\\MSSQLLocalDB;Database=BMDB;Trusted_Connection=True;";
            SqlManager sqlManager = new SqlManager(connStr);
            string table = "[User]";
            string column = "Login, Pass, IdSettings, IdRole";
            string data = "'login', 'pass', 1, 1";
            string status =  sqlManager.Insert(table, column, data);

            Console.WriteLine(status);
        }
    }
}
