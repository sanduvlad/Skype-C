using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Interogare;
namespace Server
{
    class ClientToServerHandle : ClientToServerCOM.IOutCom
    {
        private Dictionary<string, string> Clients = new Dictionary<string, string>();
        // userName, channelURL //
        private XmlDataBase db = new XmlDataBase();

        /// <summary>
        /// Returneaza URL-ul unui anumit client dat ca parametru
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string getClientURL(string userName)
        {
            if (Clients.ContainsKey(userName))
                return Clients[userName];
            else
                return "0";
        }

        /// <summary>
        /// Apeleaza functia de adaugare a unui prieten
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="friend"></param>
        public void AddFriend(string userName,string friend)
        {
            db.AddFriend(userName, friend);
        }

        /// <summary>
        /// Preia lista cu toti prietenii unui useri
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public string[] GetFriends(string username)
        {
            return db.AllFriends(username);
        }

        /// <summary>
        /// Realizeaza inregistrarea
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="nume"></param>
        /// <returns></returns>
        public int Register(string userName, string password, string email, string nume)
        {
            if(db.Register(userName,password,password,nume,email)==1)
            {
                return 1;
            }else
            {
                return 0;
            }
        }

        /// <summary>
        /// Apeleaza funtia de logare din dll si executa sau nu logarea
        /// </`ummary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="channelURL"></param>
        /// <returns></returns>
        public int SignIn(string userName, string password, string channelURL)
        {
            if (Clients.ContainsKey(userName))
                if (db.LogIn(userName, password) == 1)
                {
                   // Clients.Add(userName, channelURL);
                    return 1;
                }
                else
                    return 0;
            else
            {
                if (db.LogIn(userName, password) == 1)
                {
                    Clients.Add(userName, channelURL);
                    return 1;
                }
                else
                    return 0;
               
            }
            
        }

        /// <summary>
        /// Functie ce preia lista cu useri cautati
        /// </summary>
        /// <param name="query"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<string> SearchUsers(string query, string username)
        {
            return db.FindUsers(query, username);
        }

        /// <summary>
        /// Schimba statusul unui user intr-unul ales din lista
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="status"></param>
        public void ChangeStatus(string userName, String status)
        {
            db.ChangeStatus(userName, status);

            //ServerToClientHandle.SetUserStatus(getClientURL(userName), userName, status);
            foreach (KeyValuePair<string, string> pair in Clients)
            {
                if (pair.Key != userName)
                {
                    ServerToClientHandle.SetUserStatus(pair.Value, userName, status);
                }
            }
        }

        /// <summary>
        /// Delogare
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public int SignOut(string userName)
        {
            db.LogOut(userName);
            Clients.Remove(userName);
            return 1;
        }

        /// <summary>
        /// Preia lista cu toate mesajele trimise intre doi useri
        /// </summary>
        /// <param name="username"></param>
        /// <param name="receiver"></param>
        /// <returns></returns>
        public string[] GetMessages(string username, string receiver)
        {
            return db.AllMessages(username, receiver);
        }

        /// <summary>
        /// Apeleaza functia de stocare a mesajului in baza de date si o trimite si serverului
        /// </summary>
        /// <param name="fromUserName"></param>
        /// <param name="toUserName"></param>
        /// <param name="message"></param>
        public void SendMessage(string fromUserName, string toUserName, string message)
        {
            //server
            db.AddMessage(fromUserName, toUserName, message);
            ServerToClientHandle.SendMessage(message, fromUserName, getClientURL(toUserName));
        }
    }
}
