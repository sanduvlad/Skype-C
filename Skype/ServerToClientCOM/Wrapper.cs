using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerToClientCOM
{
    public class Wrapper
    {
        private static IInCom comToClient;
        private static Wrapper instance;

        private Wrapper()
        {
            //
        }


        /// <summary>
        /// Metoda folosita pentru a nu crea instante noi
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
        /// Ataseaza obiectul de tip ComClient
        /// </summary>
        /// <param name="ComClient"></param>

        public void Attach(IInCom ComClient)
        {
            comToClient = ComClient;
        }

        /// <summary>
        /// Metoda ce updateaza lista de prieteni
        /// </summary>
        /// <param name="friendsList">Lista de prieteni trimisa</param>
        public void UpdateFriendList(List<string> friendsList)
        {
            comToClient.UpdateFriendList(friendsList);
        }


        /// <summary>
        /// Metoda ce returneaza lista de useri cautati
        /// </summary>
        /// <param name="searchByList"></param>
        public void SearchUserList(List<string> searchByList)
        {
            comToClient.SearchUserList(searchByList);
        }

        /// <summary>
        /// Metodate ce returneaza mesajul primit catre client
        /// </summary>
        /// <param name="message">Mesaj</param>
        /// <param name="username">Username</param>
        public void MessageReceived(string message, string userName)
        {
            comToClient.MessageReceived(message, userName);
        }

        /// <summary>
        /// Returneaza statusul unui user catre prieteni
        /// </summary>
        /// <param name="userName"> Userul ce si-a schimbat statusul</param>
        /// <param name="status">Statusul nou</param>
        public void SetUserStatus(string userName, string status)
        {
            comToClient.SetUserStatus(userName, status);
        }
    }
}
