using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Server
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IClientToServerHandle" in both code and config file together.
    [ServiceContract]
    public interface IClientToServerHandle
    {
        [OperationContract]
        int SignIn(string username, string password, string channelURL);

        [OperationContract]
        int SignOut(string username);

        [OperationContract]
        int Register(string username, string password, string email, string nume);

        [OperationContract]
        void ChangeStatus(string username, String status);

        [OperationContract]
        List<string> SearchUsers(string query, string username);

        [OperationContract]
        string[] GetFriends(string username);

        [OperationContract]
        void AddFriend(string username, string friend);

        [OperationContract]
        string[] GetMessages(string username, string receiver);

        [OperationContract]
        void SendMessage(string fromUsername, string toUsername, string message);
    }
}
