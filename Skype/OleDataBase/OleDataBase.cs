using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

//https://msdn.microsoft.com/en-us/library/dw70f090(v=vs.110).aspx

namespace OleDataBase
{
    public class OleDataBase
    {
        public int Register(string username, string name, string password, string email)
        {
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source = DB.mdb;";
            string queryString =
                            "INSERT INTO Users " +
                            "VALUES ('" + username + "', '" + name + "', '" + password + "', '" + email + "', 'offline');";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(queryString, connection);

                try
                {
                    connection.Open();
                    command.ExecuteReader();
                    command = new OleDbCommand(queryString, connection);
                    connection.Close();
                    //Console.WriteLine("Inregistrare reusita");
                    return 1;
                }
                catch (Exception ex)
                {
                    //Console.WriteLine("Inregistrare nereusita");
                    Console.WriteLine(ex.Message); 
                    return 0;
                }
            }
        }

        public int ChangeStatus(string username, string status)
        {
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source = DB.mdb;";

            string queryString =
                    "UPDATE Users SET status = '" + status + "' " +
                    "WHERE username = '" + username + "';";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(queryString, connection);

                try
                {
                    connection.Open();
                    command.ExecuteReader();
                    command = new OleDbCommand(queryString, connection);
                    connection.Close();
                    //Console.WriteLine("Schimbare de status reusita!");
                    return 1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //Console.WriteLine("Schimbare de status nereusita!");
                    return 0;
                }

            }
        }

        public int LogIn(string username, string parola)
        {
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source = DB.mdb;";

            string queryString =
                "SELECT COUNT(username) from Users " +
                "WHERE username = '" + username + "' AND parola = '" + parola + "';";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(queryString, connection);

                try
                {
                    connection.Open();
                    OleDbDataReader reader = command.ExecuteReader();
                    reader.Read();
                    if (reader[0].ToString() == "1")
                    {
                        //Console.WriteLine("Autentificare reusita!");
                        ChangeStatus(username, "online");
                        reader.Close();
                        connection.Close();
                        return 1;
                    }
                    else
                    {
                        //Console.WriteLine("Username or Password wrong!");
                        reader.Close();
                        connection.Close();
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //Console.WriteLine("Username or Password wrong!");
                    return 0;
                }
            }
        }

    }
}