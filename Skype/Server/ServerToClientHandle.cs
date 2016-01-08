using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class ServerToClientHandle
    {
        static ServerToClientCOM.RemotableObject ClientRemoteObject;

        /// <summary>
        /// Trimite un mesaj primit dela un user spre celalalt user
        /// </summary>
        /// <param name="message"></param>
        /// <param name="fromUserName"></param>
        /// <param name="toUserNameChannelURL"></param>
        public static void SendMessage(string message, string fromUserName, string toUserNameChannelURL)
        {
            if (!toUserNameChannelURL.Equals("0"))
            {
                ClientRemoteObject =
                  (ServerToClientCOM.RemotableObject)Activator.GetObject(typeof(ServerToClientCOM.RemotableObject),
                  toUserNameChannelURL);
                ClientRemoteObject.MessageReceived(message, fromUserName);
            }
        }

        public void MessageReceived(string message, string userName)
        {
            throw new NotImplementedException();
        }

        public void SearchUserList(List<string> searchByList)
        {
            throw new NotImplementedException();
        }

        public static void SetUserStatus(string clientUrl, string userName, string status)
        {
            ClientRemoteObject =
                  (ServerToClientCOM.RemotableObject)Activator.GetObject(typeof(ServerToClientCOM.RemotableObject),
                  clientUrl);
            ClientRemoteObject.SetUserStatus(userName, status);
        }

        public void UpdateFriendList(List<string> friendsList)
        {
            throw new NotImplementedException();
        }
    }
}
