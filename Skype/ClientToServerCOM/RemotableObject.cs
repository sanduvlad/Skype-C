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
        public void SignIn(string userName, string password, string channelURL)
        {
            Wrapper.GetInstance().SignIn(userName, password,channelURL);
        }

        public void SignOut(string userName)
        {
            Wrapper.GetInstance().SignOut(userName);
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
