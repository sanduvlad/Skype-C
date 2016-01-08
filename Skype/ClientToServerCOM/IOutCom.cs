using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientToServerCOM
{
    /// <summary>
    /// Status-uri disponibile user
    /// </summary>
    public enum AvailableState
    {
        Available,
        Busy,
        Gone,
        Invisible
    };

    /// <summary>
    /// Descrierea interfetei Client to Server 
    /// </summary>

    public interface IOutCom
    {
        int SignIn(string username, string password, string channelURL);
        int SignOut(string username);
        int Register(string username, string password, string email,string nume);
        void ChangeStatus(string username, String status);
        List<string> SearchUsers(string query, string username);
        string[] GetFriends(string username);
        void AddFriend(string username,string friend);
        string[] GetMessages(string username, string receiver);
        void SendMessage(string fromUsername, string toUsername, string message);
    }
}
