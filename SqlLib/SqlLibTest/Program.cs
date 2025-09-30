using SqlLib;
namespace SqlLibTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connStr = "Server=(localdb)\\MSSQLLocalDB;Database=BMDB;Trusted_Connection=True;";

            string table = "[User]";
            string column = "Login";
            string data = "'Insert'";

            Query query = new Query(connStr);
            var res = query.Update(table, column, data);
            Console.WriteLine(res);
        }
    }
}
