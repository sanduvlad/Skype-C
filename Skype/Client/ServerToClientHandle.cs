using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Client
{
    class ServerToClientHandle : ServerToClientCOM.IInCom
    {
        private Application app;

        public ServerToClientHandle(Application ap)
        {
            this.app = ap;
        }

        /// <summary>
        /// Afisarea unui mesaj nou primit
        /// </summary>
        /// <param name="message"></param>
        /// <param name="userName"></param>
        public void MessageReceived(string message, string userName)
        {
            //Invoke((MethodInvoker)(() => lblName.Text = "Meep"));
            app.DisplayMessageOnScreen(message, userName);
        }

        /// <summary>
        /// Lista cu userii cautati
        /// </summary>
        /// <param name="searchByList"></param>
        public void SearchUserList(List<string> searchByList)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Functie ce face update la lista de prieteni
        /// </summary>
        /// <param name="friendsList"></param>
        public void UpdateFriendList(List<string> friendsList)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Seteaza unui user un anumit status primit ca parametru
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="state"></param>
        public void SetUserStatus(string userName, string state)
        {
            app.UserChangedStatus(userName, state);

        }
    }
}
