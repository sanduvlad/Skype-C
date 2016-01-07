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
    public partial class Form1 : Form
    {
        ClientToServerHandle cliToSvr;
        //ServerToClientHandle svrToCli;

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

            cliToSvr = new ClientToServerHandle();
            //svrToCli = new ServerToClientHandle();
            ClientToServerCOM.Wrapper.GetInstance().Attach(cliToSvr);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //svrToCli.SendMessage("message", "user2", cliToSvr.getClientURL("user1"));
            //svrToCli.SendMessage("message", "user2", cliToSvr.getClientURL("user2"));
            
        }
    }
}
