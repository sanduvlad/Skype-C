using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientToServerCOM
{
    public class Wrapper
    {
        private static I_Out_COM comToServer;
        private static Wrapper instance;

        private Wrapper()
        {
            //
        }

        public static Wrapper GetInstance()
        {
            if (instance == null)
            {
                instance = new Wrapper();
            }
            return instance;
        }

        public int SignIn(string userName, string password, string channelURL)
        {
            return comToServer.SignIn(userName, password, channelURL);
        }

        public int SignOut(string userName)
        {
           return comToServer.SignOut(userName);
        }

        public void ChangeStatus(string userName, String status)
        {
            comToServer.ChangeStatus(userName, status);

        }

        public int Register(string userName, string password, string email, string nume)
        {
            return comToServer.Register(userName, password,  email,  nume);
        }

        public List<List<string>> GetFriends(string username)
        {
            return comToServer.GetFriends(username);
        }

        public void AddFriend(string userName,string friend)
        {
            comToServer.AddFriend(userName,friend);
        }
        

        public List<string> SearchUsers(string query, string username)
        {
            return comToServer.SearchUsers(query, username);
        }


        public void Attach(I_Out_COM ComServer)
        {
            comToServer = ComServer;
        }


    }
}
