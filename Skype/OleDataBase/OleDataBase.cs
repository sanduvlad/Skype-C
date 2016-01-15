using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

//https://msdn.microsoft.com/en-us/library/dw70f090(v=vs.110).aspx

namespace OleDB
{
    public class OleDataBase
    {
        public int Register(string username, string name, string password, string reParola, string email)
        {
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source = DB.mdb;";
            string queryString =
                        "INSERT INTO Users " +
                        "VALUES ('" + username + "', '" + name + "', '" + password + "', '" + email + "', 'offline' ,'');";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(queryString, connection);

                try
                {
                    connection.Open();
                    command.ExecuteReader();
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


        /// <summary>
        /// Functie ce delogheaza un user si schimba starea acestuia in oflline
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public int LogOut(string username)
        {
            //Console.WriteLine("Delogare reusita!");
            ChangeStatus(username, "offline");
            return 1;
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
                "SELECT username from Users " +
                "WHERE username = '" + username + "' AND parola = '" + parola + "';";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(queryString, connection);

                try
                {
                    connection.Open();
                    OleDbDataReader reader = command.ExecuteReader();
                    reader.Read();
                    if (reader.HasRows)
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

        public int AddMessage(string sender, string receiver, string message)
        {
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source = DB.mdb;";
            string queryString =
                        "INSERT INTO Messages(sender, receiver, message) " +
                        "VALUES ('" + sender + "', '" + receiver + "', '" + message + "');";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(queryString, connection);

                try
                {
                    connection.Open();
                    command.ExecuteReader();
                    connection.Close();
                    //Console.WriteLine("Mesaj adaugat!");
                    return 1;
                }
                catch (Exception ex)
                {
                    //Console.WriteLine("Operatiune nereusita!");
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        public string[] AllMessages(string sender, string receiver)
        {
            string[] messages = null;
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source = DB.mdb;";
            string queryString =
                    "SELECT time, sender, receiver, message from Messages " +
                    "WHERE sender = '" + sender + "' AND receiver = '" + receiver + "'" +
                    "ORDER BY time ASC;";
            string queryStringCount =
                    "SELECT COUNT(*) from Messages " +
                    "WHERE sender = '" + sender + "' AND receiver = '" + receiver + "';";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(queryStringCount, connection);

                try
                {
                    connection.Open();
                    OleDbDataReader reader = command.ExecuteReader();
                    reader.Read();
                    int count = (int)reader[0];
                    messages = new string[count];
                    for (int i = 0; i < count; i++)
                    {
                        messages[i] = string.Empty;
                    }
                    reader.Close();

                    command = new OleDbCommand(queryString, connection);
                    reader = command.ExecuteReader();

                    int j = 0;
                    while (reader.Read())
                    {
                        messages[j] = reader[0] + " " + reader[1] + " " + reader[2] + " " + reader[3];
                        j++;
                    }
                    reader.Close();
                    connection.Close();
                    return messages;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return messages;
                }
            }
        }

        public int AddFriend(string username, string friendName)
        {
            string friends = null;

            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source = DB.mdb;";

            string queryStringFriends =
                        "SELECT friends from Users " +
                        "WHERE username = '" + username + "';";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(queryStringFriends, connection);

                try
                {
                    connection.Open();
                    OleDbDataReader reader = command.ExecuteReader();
                    reader.Read();
                    friends = (string)reader[0];

                    if (friends.IndexOf(friendName) == -1)
                    {
                        friends += " " + friendName;
                        reader.Close();
                        string queryStringUpdate =
                                    "UPDATE Users SET friends = '" + friends + "' " +
                                    "WHERE username = '" + username + "';";
                        command = new OleDbCommand(queryStringUpdate, connection);
                        reader = command.ExecuteReader();
                        reader.Close();
                    }
                    connection.Close();
                    return 1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //Console.WriteLine("Add friend nereusita");
                    return 0;
                }
            }
        }

        public string[] AllFriends(string username)
        {


            string friends_aux = "";
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source = DB.mdb;";

            string queryStringFriends =
                        "SELECT friends from Users " +
                        "WHERE username = '" + username + "';";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(queryStringFriends, connection);

                try
                {
                    connection.Open();
                    OleDbDataReader reader = command.ExecuteReader();
                    reader.Read();
                    friends_aux = reader[0].ToString();
                    int count = 0;
                    if(friends_aux.Length>0)
                    {
                        count = friends_aux.Split(' ').Length;
                    }
                    string[] friends = new string[count];

                    for (int i = 0; i < count; i++)
                    {

                        string fusername = reader[0].ToString().Split(' ')[i];
                        friends[i] = fusername + " " + GetStatus(fusername);
                    }

                    reader.Close();
                    connection.Close();

                    return friends;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //Console.WriteLine("Add friend nereusita");
                    //return friends_aux;
                }
            }
            return (new string[] { "0" });
        }


        public List<string> FindUsers(string query, string username)
        {
            List<string> users = new List<string>();
            List<string> friends = new List<string>();
            int f = 0;
            string friends_aux = "";
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source = DB.mdb;";

            string queryStringFriends =
                        "SELECT friends from Users " +
                        "WHERE username = '" + username + "';";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(queryStringFriends, connection);

                try
                {
                    connection.Open();
                    OleDbDataReader reader = command.ExecuteReader();
                    reader.Read();
                    friends_aux = reader[0].ToString();
                    int count = 0;
                    if (friends_aux.Length > 0)
                    {
                        count = friends_aux.Split(' ').Length;
                    }

                    for (int i = 0; i < count; i++)
                    {
                        friends.Add(reader[0].ToString().Split(' ')[i]);
                    }


                    string queryString = "SELECT username from Users " +
                        "WHERE 1;";
                    command = new OleDbCommand(queryString, connection);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string el = reader[0].ToString();
                        if (el.Contains(query))
                        {
                            f = 0;
                            foreach (string u in friends)
                            {
                                if (u == el)
                                {
                                    f = 1;
                                    break;
                                }
                            }
                            if (f != 1)
                            {
                                users.Add(el);

                            }
                        }
                    }

                    
                    reader.Close();
                    connection.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //Console.WriteLine("Add friend nereusita");
                    //return friends_aux;
                }
            }
            return users;


        }

        public string GetStatus(string username)
        {
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source = DB.mdb;";

            string queryString =
                    "SELECT status from Users " +
                    "WHERE username = '" + username + "';";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(queryString, connection);

                try
                {
                    connection.Open();
                    OleDbDataReader reader = command.ExecuteReader();
                    reader.Read();
                    //Console.WriteLine("Schimbare de status reusita!");
                    string status = "offline";
                 
                    if (reader.HasRows)
                    {
                        status = reader[0].ToString();
                    
                    }
                    connection.Close();
                    return status;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //Console.WriteLine("Schimbare de status nereusita!");
                    return ex.ToString();
                }
            }
        }

    }
}