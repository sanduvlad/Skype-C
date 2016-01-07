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
        int SignOut(string userName, string channelURL);
        int Register(string userName, string password, string Email,string Nume, string channelURL);
        void SearchFriends(string searchBy);
        void AddFriend(string userName);
        void SetAvailableState(AvailableState state);
    }
}
