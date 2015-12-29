using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace Server
{
    public partial class Form1 : Form, ClientToServerCOM.I_Out_COM
    {
        //private ServerToClientCOM.RemotableObject remoteClientObj;
        private Dictionary<string, string> clients;

        public Form1()
        {

            InitializeComponent();
            
            //TCP - Initilize server object proxy
            //**********************
            TcpChannel channel = new TcpChannel(8080);
            ChannelServices.RegisterChannel(channel, false);
            RemotingConfiguration.RegisterWellKnownServiceType
                (
                typeof(ClientToServerCOM.RemotableObject),
                "ClientToServer", WellKnownObjectMode.Singleton
                );
            ClientToServerCOM.Wrapper.GetInstance().Attach(this);
            //**********************

            //TCP - Initialize object reference to client proxy
            //this should be dynamic
            //foreach client, a new remote object should be stored
            //and it should be called when that client request something
            //**********************
            //remoteClientObj = (ServerToClientCOM.RemotableObject)Activator.GetObject(typeof(ServerToClientCOM.RemotableObject), "tcp://localhost:8081/ServerToClient");
            //**********************
            clients = new Dictionary<string, string>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //send a message To all clients
            foreach (KeyValuePair<
                string, string> pair in clients)
            {
                ServerToClientCOM.RemotableObject remote = (ServerToClientCOM.RemotableObject)Activator.GetObject(typeof(ServerToClientCOM.RemotableObject), pair.Key);
                remote.MessageReceived(pair.Key, pair.Value);
            }
        }

        //Functions that a normal User can acces from it's local 
        //server proxy object
        public void AddFriend(string userName)
        {
            MessageBox.Show(userName);
            //remoteClientObj.MessageReceived("this is a message form server",
                //"from this username");

        }

        public void SearchFriends(string searchBy)
        {
            MessageBox.Show(searchBy);
        }

        public void SetAvailableState(ClientToServerCOM.AvailableState state)
        {
            MessageBox.Show(state.ToString());
        }

        public void SignIn(string userName, string password, string channelURL)
        {
            clients.Add(channelURL, userName);
        }

        public void SignOut(string userName)
        {
            MessageBox.Show(userName);
        }
    }
}
