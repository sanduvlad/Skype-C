using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;

namespace ClientToServerCOM
{
    public class RemotableObject : MarshalByRefObject
    {
        public bool SignIn(string userName, string password, string channelURL)
        {
           return Wrapper.GetInstance().SignIn(userName, password,channelURL);
        }

        public bool SignOut(string userName, string channelURL)
        {
            return Wrapper.GetInstance().SignOut(userName,channelURL);
        }

        public void SearchFriends(string searchBy)
        {
            Wrapper.GetInstance().SearchFriends(searchBy);
        }

        public void AddFriend(string userName)
        {
            Wrapper.GetInstance().AddFriend(userName);
        }

        public void SetAvailableState(AvailableState state)
        {
            Wrapper.GetInstance().SetAvailableState(state);
        }
    }
}
