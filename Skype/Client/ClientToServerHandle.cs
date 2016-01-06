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

        public void InitConnectionToServer(string serverIP)
        {
            //TCP - Initialize object reference to server proxy
            //************
            TcpChannel channel = new TcpChannel();
            try
            {
               channel  = new TcpChannel(8081);
            }
            catch { }
            ChannelServices.RegisterChannel(channel, false);

            remoteServerOBJ = (ClientToServerCOM.RemotableObject)Activator.GetObject(typeof(ClientToServerCOM.RemotableObject), "tcp://" + serverIP + ":8080" + "/ClientToServer");
            //************
        }

        public void AddFriend(string userName)
        {
            throw new NotImplementedException();
        }

        public void SearchFriends(string searchBy)
        {
            throw new NotImplementedException();
        }

        public void SetAvailableState(ClientToServerCOM.AvailableState state)
        {
            throw new NotImplementedException();
        }

        public void SignIn(string userName, string password)
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

            string clientChannelURL = "tcp://" + IP_address + ":8081" + "/" + userName;

            remoteServerOBJ.SignIn(userName, password, clientChannelURL);
        }

        public void SignOut(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
