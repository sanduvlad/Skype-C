using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerToClientCOM
{
    public class Wrapper
    {
        private static I_In_COM comToClient;
        private static Wrapper instance;

        private Wrapper()
        {
            //
        }

        public static Wrapper GetInstance()
        {
            if (instance == null)
            {
                instance = new Wrapper();
            }
            return instance;
        }

        public void Attach(I_In_COM ComClient)
        {
            comToClient = ComClient;
        }

        public void UpdateFriendList(List<string> friendsList)
        {
            comToClient.UpdateFriendList(friendsList);
        }

        public void SearchUserList(List<string> searchByList)
        {
            comToClient.SearchUserList(searchByList);
        }

        public void MessageReceived(string message, string userName)
        {
            comToClient.MessageReceived(message, userName);
        }
    }
}
