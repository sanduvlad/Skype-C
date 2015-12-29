using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerToClientCOM
{
    public interface I_In_COM
    {
        void UpdateFriendList(List<string> friendsList);
        void SearchUserList(List<string> searchByList);
        void MessageReceived(string message, string userName);
    }
}
