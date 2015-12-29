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

namespace Client
{
    public partial class Form1 : Form, ServerToClientCOM.I_In_COM
    {
        private ClientToServerCOM.RemotableObject remoteServerOBJ;
        private int id;
        
        public Form1()
        {
            InitializeComponent();
             
            //TCP - Initialize object reference to server proxy
            //************
            TcpChannel channel = new TcpChannel(8081);
            ChannelServices.RegisterChannel(channel, false);

            remoteServerOBJ = (ClientToServerCOM.RemotableObject)Activator.GetObject(typeof(ClientToServerCOM.RemotableObject), "tcp://192.168.202.1:8080/ClientToServer");
            //************

            //TCP - Initialize client object proxy
            //************
            id = (new Random()).Next();
            RemotingConfiguration.RegisterWellKnownServiceType
                (
                typeof(ServerToClientCOM.RemotableObject),
                id.ToString(), WellKnownObjectMode.Singleton
                );
            ServerToClientCOM.Wrapper.GetInstance().Attach(this);
            //************
        }

        private void button1_Click(object sender, EventArgs e)
        {
            remoteServerOBJ.SignIn("user", "pass", "tcp://192.168.202.1:8081/" + id.ToString());
        }


        //Functions that a normal Server can access from it's local
        //client proxy object
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
