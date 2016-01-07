using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace Client
{
    class ClientToServerHandle
    {
        private string ipAddresss = string.Empty;
        private ClientToServerCOM.RemotableObject remoteServerOBJ;

        public String GetServerAddress()
        {
            return "localhost";
        }

        public void InitConnectionToServer(string serverIP)
        {
            //TCP - Initialize object reference to server proxy
            //************
            TcpChannel channel = new TcpChannel();
            try
            {
               channel  = new TcpChannel(8081);
               ChannelServices.RegisterChannel(channel, false);
            }
            catch { }
            

            remoteServerOBJ = (ClientToServerCOM.RemotableObject)Activator.GetObject(typeof(ClientToServerCOM.RemotableObject), "tcp://" + serverIP + ":8080" + "/ClientToServer");
            //************
        }

        public void AddFriend(string userName,string friend)
        {
            remoteServerOBJ.AddFriend(userName,friend);
        }

        public string[] GetFriends(string username)
        {
            return remoteServerOBJ.GetFriends(username);
        }

        public void SendMessage(string fromUserName, string toUserName, string message)
        {
            remoteServerOBJ.SendMessage(fromUserName, toUserName, message);
        }

        public int SignIn(string userName, string password)
        {
            //TCP - Initialize client object proxy
            //************
            RemotingConfiguration.RegisterWellKnownServiceType
                (
                typeof(ServerToClientCOM.RemotableObject),
                userName, WellKnownObjectMode.Singleton
                );
            //ServerToClientCOM.Wrapper.GetInstance().Attach(this);
            //************
            string IP_address = string.Empty;
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    IP_address = ip.ToString();
                }
            }
            ipAddresss = IP_address;

            string clientChannelURL = "tcp://" + IP_address + ":8081" + "/" + userName;

            return remoteServerOBJ.SignIn(userName, password, clientChannelURL);
        }

        public int SignOut(string userName)
        {
            return remoteServerOBJ.SignOut(userName);

        }

        public void ChangeStatus(string userName,String status)
        {
            remoteServerOBJ.ChangeStatus(userName, status);

        }

        public int Register(string userName, string password, string email, string nume)
        {
            return remoteServerOBJ.Register(userName, password, email,nume);

        }

        public List<string> SearchUsers(string query,string username)
        {
            return remoteServerOBJ.SearchUsers(query, username);
        }
    }
}
