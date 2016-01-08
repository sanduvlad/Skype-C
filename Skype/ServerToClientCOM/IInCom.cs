using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerToClientCOM
{
    /// <summary>
    /// Structura ce contine tipurile posibile de stari
    /// </summary>
    public enum AvailableState
    {
        Available,
        Busy,
        Gone,
        Invisible
    };

    /// <summary>
    /// Descrierea interfetei
    /// </summary>
    public interface IInCom
    {
        void UpdateFriendList(List<string> friendsList);
        void SearchUserList(List<string> searchByList);
        void MessageReceived(string message, string userName);

        void SetUserStatus(string userName, string status);
    }
}
