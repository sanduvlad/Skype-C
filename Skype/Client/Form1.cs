using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        ClientToServerHandle cliToSvr;
        ServerToClientHandle svrToCli;

        public Form1()
        {
            InitializeComponent();
            cliToSvr = new ClientToServerHandle();
            svrToCli = new ServerToClientHandle();
            ServerToClientCOM.Wrapper.GetInstance().Attach(svrToCli);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cliToSvr.InitConnectionToServer(textBox1.Text);
            cliToSvr.SignIn(textBox2.Text, textBox3.Text);
        }
    }
}
