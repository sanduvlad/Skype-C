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

        public int SignOut(string userName)
        {
            return Wrapper.GetInstance().SignOut(userName);
        }

        public int Register(string userName, string password, string email, string nume)
        {
            return Wrapper.GetInstance().Register(userName, password,  email,  nume);
        }

        public void ChangeStatus(string userName, String status)
        {
            Wrapper.GetInstance().ChangeStatus(userName, status);

        }

        public void AddFriend(string userName,string friend)
        {
            Wrapper.GetInstance().AddFriend(userName,friend);
        }

        public string[] GetFriends(string username)
        {
            return Wrapper.GetInstance().GetFriends(username);
        }

        public List<string> SearchUsers(string query, string username)
        {
            return Wrapper.GetInstance().SearchUsers(query, username);
        }

        public void SendMessage(string fromUserName, string toUserName, string message)
        {
            Wrapper.GetInstance().SendMessage(fromUserName, toUserName, message);
        }

        public string[] GetMessages(string username,string receiver)
        {
            return Wrapper.GetInstance().GetMessages(username, receiver);
        }
    }
}
