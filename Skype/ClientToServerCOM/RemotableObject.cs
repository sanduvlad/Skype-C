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
        public int SignIn(string userName, string password, string channelURL)
        {
           return Wrapper.GetInstance().SignIn(userName, password,channelURL);
        }

        public int SignOut(string userName, string channelURL)
        {
            return Wrapper.GetInstance().SignOut(userName,channelURL);
        }

        public int Register(string userName, string password, string email, string nume)
        {
            return Wrapper.GetInstance().Register(userName, password,  email,  nume);
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
