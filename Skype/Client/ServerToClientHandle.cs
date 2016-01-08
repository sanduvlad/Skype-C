using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Client
{
    class ServerToClientHandle : ServerToClientCOM.I_In_COM
    {
        private Application app;

        public ServerToClientHandle(Application ap)
        {
            this.app = ap;
        }

        public void MessageReceived(string message, string userName)
        {
            //Invoke((MethodInvoker)(() => lblName.Text = "Meep"));
            app.DisplayMessageOnScreen(message, userName);
        }

        public void SearchUserList(List<string> searchByList)
        {
            throw new NotImplementedException();
        }

        public void UpdateFriendList(List<string> friendsList)
        {
            throw new NotImplementedException();
        }


        public void SetUserStatus(string userName, string state)
        {
            app.UserChangedStatus(userName, state);

        }
    }
}
