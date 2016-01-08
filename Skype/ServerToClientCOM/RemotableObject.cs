using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerToClientCOM
{
    public class RemotableObject : MarshalByRefObject
    {
        /// <summary>
        /// Metoda ce updateaza lista de prieteni
        /// </summary>
        /// <param name="friendsList">Lista de prieteni trimisa</param>
        public void UpdateFriendList(List<string> friendsList)
        {
            Wrapper.GetInstance().UpdateFriendList(friendsList);
        }

        /// <summary>
        /// Metoda ce returneaza lista de useri cautati
        /// </summary>
        /// <param name="searchByList"></param>
        public void SearchUserList(List<string> searchByList)
        {
            Wrapper.GetInstance().SearchUserList(searchByList);
        }

        /// <summary>
        /// Metodate ce returneaza mesajul primit catre client
        /// </summary>
        /// <param name="message">Mesaj</param>
        /// <param name="username">Username</param>
        public void MessageReceived(string message, string username)
        {
            Wrapper.GetInstance().MessageReceived(message, username);
        }


        /// <summary>
        /// Returneaza statusul unui user catre prieteni
        /// </summary>
        /// <param name="userName"> Userul ce si-a schimbat statusul</param>
        /// <param name="status">Statusul nou</param>
        public void SetUserStatus(string userName, string status)
        {
            Wrapper.GetInstance().SetUserStatus(userName, status);
        }
    }
}
