using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;

namespace ClientToServerCOM
{

    /// <summary>
    /// Obiectul remotable Client to server
    /// </summary>
    public class RemotableObject : MarshalByRefObject
    {

        /// <summary>
        /// Apel functie sign in, trimitere catre wrapper
        /// </summary>
        /// <param name="username"> Username</param>
        /// <param name="password"> Parola</param>
        /// <param name="channelURL"> Canalul catre Server</param>
        /// <returns></returns>
        public int SignIn(string username, string password, string channelURL)
        {
           return Wrapper.GetInstance().SignIn(username, password,channelURL);
        }

        /// <summary>
        /// Apel functie sign out, trimitere catre wrapper
        /// </summary>
        /// <param name="username">Usernam</param>
        /// <returns></returns>
        public int SignOut(string username)
        {
            return Wrapper.GetInstance().SignOut(username);
        }

        /// <summary>
        /// Apel functie Register, trimitere catre wrapper
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Parola</param>
        /// <param name="email">Emailul</param>
        /// <param name="nume">Nume</param>
        /// <returns></returns>
        public int Register(string username, string password, string email, string nume)
        {
            return Wrapper.GetInstance().Register(username, password,  email,  nume);
        }


        /// <summary>
        /// Schimbare status user , trimitere catre wrapper
        /// </summary>
        /// <param name="username"> Username</param>
        /// <param name="status"> Status</param>
        public void ChangeStatus(string username, String status)
        {
            Wrapper.GetInstance().ChangeStatus(username, status);

        }

        /// <summary>
        /// Adaugare user , trimitere catre wrapper
        /// </summary>
        /// <param name="username"> Username</param>
        /// <param name="friend"> Username al prietenului adaugat</param>
        public void AddFriend(string username,string friend)
        {
            Wrapper.GetInstance().AddFriend(username,friend);
        }

        /// <summary>
        /// Functie de returnare a prietenilor userului dat ca parametru
        /// </summary>
        /// <param name="username"> Username </param>
        /// <returns></returns>
        public string[] GetFriends(string username)
        {
            return Wrapper.GetInstance().GetFriends(username);
        }

        /// <summary>
        /// Returneaza toti userii din baza de date care nu sunt prieteni cu userul dat ca parametru si incep cu stringul query
        /// </summary>
        /// <param name="query"> Stringul cautat</param>
        /// <param name="username">Userul pentru care se cauta</param>
        /// <returns></returns>
        public List<string> SearchUsers(string query, string username)
        {
            return Wrapper.GetInstance().SearchUsers(query, username);
        }

        /// <summary>
        /// Functie de trimitere a mesajului
        /// </summary>
        /// <param name="fromUsername">Userului catre care se trimite</param>
        /// <param name="toUsername">Userul care primeste</param>
        /// <param name="message">Mesajul trimis</param>
        public void SendMessage(string fromUserName, string toUsername, string message)
        {
            Wrapper.GetInstance().SendMessage(fromUserName, toUsername, message);
        }

        /// <summary>
        /// Functie ce returneaza toate mesajele dintre 2 useri
        /// </summary>
        /// <param name="username">Primul user</param>
        /// <param name="receiver">All doilea user</param>
        /// <returns></returns>
        public string[] GetMessages(string username,string receiver)
        {
            return Wrapper.GetInstance().GetMessages(username, receiver);
        }
    }
}
