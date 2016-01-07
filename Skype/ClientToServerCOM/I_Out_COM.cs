using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientToServerCOM
{
    public enum AvailableState
    {
        Available,
        Busy,
        Gone,
        Invisible
    };

    public interface I_Out_COM
    {
        int SignIn(string userName, string password, string channelURL);
        int SignOut(string userName);
        int Register(string userName, string password, string email,string nume);
        void ChangeStatus(string userName, String status);
        List<string> SearchUsers(string query, string username);
        void SearchFriends(string searchBy);
        void AddFriend(string userName);
        void SetAvailableState(AvailableState state);
    }
}
