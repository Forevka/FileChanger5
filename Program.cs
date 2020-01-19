using System;

namespace FileChanger3
{
    class Program
    {
        static void Main(string[] args)
        {
            DBConfig dbconfig = new DBConfig();
            DBWorker db = new DBWorker(DBConfig.Host, DBConfig.User, DBConfig.DBname, DBConfig.Port, DBConfig.Password);
            db.connect();
            Console.WriteLine("LLLLL");
        }
    }
}
