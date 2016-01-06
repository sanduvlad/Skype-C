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
        bool SignIn(string userName, string password, string channelURL);
        bool SignOut(string userName, string channelURL);
        void SearchFriends(string searchBy);
        void AddFriend(string userName);
        void SetAvailableState(AvailableState state);
    }
}
