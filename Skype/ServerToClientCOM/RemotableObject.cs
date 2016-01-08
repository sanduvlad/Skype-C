using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerToClientCOM
{
    public class RemotableObject : MarshalByRefObject
    {
        public void UpdateFriendList(List<string> friendsList)
        {
            Wrapper.GetInstance().UpdateFriendList(friendsList);
        }

        public void SearchUserList(List<string> searchByList)
        {
            Wrapper.GetInstance().SearchUserList(searchByList);
        }

        public void MessageReceived(string message, string userName)
        {
            Wrapper.GetInstance().MessageReceived(message, userName);
        }

        public void SetUserStatus(string userName, string status)
        {
            Wrapper.GetInstance().SetUserStatus(userName, status);
        }
    }
}
