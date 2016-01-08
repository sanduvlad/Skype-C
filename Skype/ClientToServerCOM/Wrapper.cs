using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientToServerCOM
{
    public class Wrapper
    {
        private static IOutCom comToServer;
        private static Wrapper instance;

        private Wrapper()
        {
            //
        }
        /// <summary>
        /// Clasa de tip singleton, se apeleaza pentru a nu crea instante noi
        /// </summary>
        /// <returns></returns>

        public static Wrapper GetInstance()
        {
            if (instance == null)
            {
                instance = new Wrapper();
            }
            return instance;
        }

        /// <summary>
        /// Apel functie sign in, returneaza rezultatul de la server
        /// </summary>
        /// <param name="username"> Username</param>
        /// <param name="password"> Parola</param>
        /// <param name="channelURL"> Canalul catre Server</param>
        /// <returns></returns>
        public int SignIn(string username, string password, string channelURL)
        {
            return comToServer.SignIn(username, password, channelURL);
        }

        /// <summary>
        /// Apel functie sign out, returneaza rezultatul de la server
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns></returns>
        public int SignOut(string username)
        {
           return comToServer.SignOut(username);
        }

        /// <summary>
        /// Schimbare status user , returneaza rezultatul de la server
        /// </summary>
        /// <param name="username"> Username</param>
        /// <param name="status"> Status</param>

        public void ChangeStatus(string userName, String status)
        {
            comToServer.ChangeStatus(userName, status);

        }


        /// <summary>
        /// Apel functie Register,returneaza rezultatul de la server
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Parola</param>
        /// <param name="email">Emailul</param>
        /// <param name="nume">Nume</param>
        /// <returns></returns>
        public int Register(string userName, string password, string email, string nume)
        {
            return comToServer.Register(userName, password,  email,  nume);
        }


        /// <summary>
        /// Functie de returnare a prietenilor userului dat ca parametru
        /// </summary>
        /// <param name="username"> Username </param>
        /// <returns></returns>
        public string[] GetFriends(string username)
        {
            return comToServer.GetFriends(username);
        }

        /// <summary>
        /// Adaugare user , trimitere catre wrapper
        /// </summary>
        /// <param name="username"> Username</param>
        /// <param name="friend"> Username al prietenului adaugat</param>
        public void AddFriend(string userName,string friend)
        {
            comToServer.AddFriend(userName,friend);
        }


        /// <summary>
        /// Returneaza toti userii din baza de date care nu sunt prieteni cu userul dat ca parametru si incep cu stringul query
        /// </summary>
        /// <param name="query"> Stringul cautat</param>
        /// <param name="username">Userul pentru care se cauta</param>
        /// <returns></returns>
        public List<string> SearchUsers(string query, string username)
        {
            return comToServer.SearchUsers(query, username);
        }

        /// <summary>
        /// Functie ce returneaza toate mesajele dintre 2 useri
        /// </summary>
        /// <param name="username">Primul user</param>
        /// <param name="receiver">All doilea user</param>
        /// <returns></returns>
        public string[] GetMessages(string username, string receiver)
        {
            return comToServer.GetMessages(username, receiver);
        }


        /// <summary>
        /// ataseaza obiectul de tip IOutCom pentru apelurile de metode
        /// </summary>
        /// <param name="ComServer">Obiectul de IoutCom</param>
        public void Attach(IOutCom ComServer)
        {
            comToServer = ComServer;
        }


        /// <summary>
        /// Functie de trimitere a mesajului
        /// </summary>
        /// <param name="fromUsername">Userului catre care se trimite</param>
        /// <param name="toUsername">Userul care primeste</param>
        /// <param name="message">Mesajul trimis</param>
        public void SendMessage(string fromUserName, string toUserName, string message)
        {
            comToServer.SendMessage(fromUserName, toUserName, message);
        }
    }
}
