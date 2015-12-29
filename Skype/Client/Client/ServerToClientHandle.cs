using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    class ServerToClientHandle : ServerToClientCOM.I_In_COM
    {
        public void MessageReceived(string message, string userName)
        {
            MessageBox.Show(message + "/r/n" + userName);
        }

        public void SearchUserList(List<string> searchByList)
        {
            throw new NotImplementedException();
        }

        public void UpdateFriendList(List<string> friendsList)
        {
            throw new NotImplementedException();
        }
    }
}
