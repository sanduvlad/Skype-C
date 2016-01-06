using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    class ClientToServerHandle : ClientToServerCOM.I_Out_COM
    {
        private Dictionary<string, string> Clients = new Dictionary<string, string>();
                        // userName, channelURL //

        public string getClientURL(string userName)
        {
            return Clients[userName];
        }

        public void AddFriend(string userName)
        {
            throw new NotImplementedException();
        }

        public void SearchFriends(string searchBy)
        {
            throw new NotImplementedException();
        }

        public void SetAvailableState(ClientToServerCOM.AvailableState state)
        {
            throw new NotImplementedException();
        }

        public void SignIn(string userName, string password, string channelURL)
        {
            Clients.Add(userName, channelURL);
        }

        public void SignOut(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
