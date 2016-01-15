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
using System.ServiceModel;
using System.Threading;

namespace Client
{
    class ClientToServerHandle
    {
        //private string ipAddresss = string.Empty;
        //private ClientToServerCOM.RemotableObject remoteServerOBJ;
        private ServiceReference1.ClientToServerHandleClient serverHost = new ServiceReference1.ClientToServerHandleClient();


        /// <summary>
        /// Functie ce returneaza adresa serverului
        /// </summary>
        /// <returns></returns>
        public String GetServerAddress()
        {
            return "localhost";
        }


        /// <summary>
        /// functie de initializare conexiune
        /// </summary>
        /// <param name="serverIP"></param>
        public void InitConnectionToServer(string serverIP)
        {
            //TCP - Initialize object reference to server proxy
            //************
            TcpChannel channel = new TcpChannel();
            try
            {
                channel = new TcpChannel(8081);
                ChannelServices.RegisterChannel(channel, false);
            }
            catch { }

            serverHost.Open();
            

            //remoteServerOBJ = (ClientToServerCOM.RemotableObject)Activator.GetObject(typeof(ClientToServerCOM.RemotableObject), "tcp://" + "192.168.205.1" + ":8080" + "/ClientToServer");
            //************
            
        }

        /// <summary>
        /// Adaugare user , trimitere catre server
        /// </summary>
        /// <param name="username"> Username</param>
        /// <param name="friend"> Username al prietenului adaugat</param>

        public void AddFriend(string userName, string friend)
        {
            //remoteServerOBJ.AddFriend(userName,friend);
                    serverHost.AddFriend(userName, friend);
        }


        /// <summary>
        /// Functie de returnare a prietenilor userului dat ca parametru
        /// </summary>
        /// <param name="username"> Username </param>
        /// <returns></returns>
        public string[] GetFriends(string username)
        {
            return //remoteServerOBJ.GetFriends(username);
                serverHost.GetFriends(username).ToArray();
        }


        /// <summary>
        /// Functie de trimitere a mesajului
        /// </summary>
        /// <param name="fromUsername">Userului catre care se trimite</param>
        /// <param name="toUsername">Userul care primeste</param>
        /// <param name="message">Mesajul trimis</param>
        public void SendMessage(string fromUserName, string toUserName, string message)
        {
            //remoteServerOBJ.SendMessage(fromUserName, toUserName, message);
                serverHost.SendMessage(fromUserName, toUserName, message);
        }

        /// <summary>
        /// Functie ce returneaza toate mesajele dintre 2 useri
        /// </summary>
        /// <param name="username">Primul user</param>
        /// <param name="receiver">All doilea user</param>
        /// <returns></returns>
        public string[] GetMessages(string username, string receiver)
        {
            return //remoteServerOBJ.GetMessages(username, receiver);
                serverHost.GetMessages(username, receiver).ToArray();
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
                    break;
                }
            }
            ///ipAddresss = IP_address;

            string clientChannelURL = "tcp://" + IP_address + ":8081" + "/" + userName;

            return //remoteServerOBJ.SignIn(userName, password, clientChannelURL);
                serverHost.SignIn(userName, password, clientChannelURL);
        }


        /// <summary>
        /// Apel functie sign out, returneaza rezultatul de la server
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns></returns>
        public int SignOut(string userName)
        {
            return //remoteServerOBJ.SignOut(userName);
                serverHost.SignOut(userName);

        }

        /// <summary>
        /// Schimbare status user , returneaza rezultatul de la server
        /// </summary>
        /// <param name="username"> Username</param>
        /// <param name="status"> Status</param>
        public void ChangeStatus(string userName,String status)
        {
            //remoteServerOBJ.ChangeStatus(userName, status);
                serverHost.ChangeStatus(userName, status);
        }

        /// <summary>
        /// Apel functie Register,returneaza rezultatul de la server
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Parola</param>
        /// <param name="email">Emailul</param>
        /// <param name="nume">Nume</param>
        /// <returns></returns>
        public int Register(string userName, string password, string email, string nume)
        {
            return //remoteServerOBJ.Register(userName, password, email,nume);
                serverHost.Register(userName, password, email, nume);
        }


        // <summary>
        /// Returneaza toti userii din baza de date care nu sunt prieteni cu userul dat ca parametru si incep cu stringul query
        /// </summary>
        /// <param name="query"> Stringul cautat</param>
        /// <param name="username">Userul pentru care se cauta</param>
        /// <returns></returns>
        public List<string> SearchUsers(string query,string username)
        {
            return //remoteServerOBJ.SearchUsers(query, username);
                serverHost.SearchUsers(query, username);
        }
    }
}
