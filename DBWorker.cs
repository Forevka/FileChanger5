using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace FileChanger3
{
    class DBWorker
    {
        string Host;
        string User;
        string DBname;
        int Port;
        string Password;
        NpgsqlConnection conn;

        public DBWorker(string Host, string User, string DBname, int Port, string Password){
            this.Host = Host;
            this.User = User;
            this.DBname = DBname;
            this.Port = Port;
            this.Password = Password;
            NpgsqlConnection сonn;
        }
        public void connect()
        {
            string connString =
                String.Format(
                    "Server={0};Username={1};Database={2};Port={3};Password={4};SSLMode=Prefer",
                    this.Host,
                    this.User,
                    this.DBname,
                    this.Port,
                    this.Password);
            this.conn = new NpgsqlConnection(connString);
            this.conn.Open();

        }
        public void AddFile(string path) {

            
            string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            using (var command = new NpgsqlCommand(String.Format("INSERT INTO files(path, token) VALUES('{0}', '{1}')", path, token), conn))
            {
                command.ExecuteNonQuery();
                Console.Out.WriteLine("Finished dropping table (if existed)");

            }
           
        }

    }
}
