﻿using System;
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
    public partial class Application : Form
    {
        ClientToServerHandle cliToSvr;
        ServerToClientHandle svrToCli;
        String username;

        public Application()
        {
            InitializeComponent();
            cliToSvr = new ClientToServerHandle();
            svrToCli = new ServerToClientHandle();
            ServerToClientCOM.Wrapper.GetInstance().Attach(svrToCli);
        }

        private void bLogin_Click(object sender, EventArgs e)
        {
            String serverIp = cliToSvr.GetServerAddress();
            cliToSvr.InitConnectionToServer(serverIp);
            if(cliToSvr.SignIn(iUsername.Text, iPassword.Text))
            {
                username = iUsername.Text;
                loginPanel.Visible = false;
                mainPanel.Visible = true;
            }
            else
            {
                username = iUsername.Text;
                loginPanel.Visible = true;
                mainPanel.Visible = false;
            }
        }
        

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cliToSvr.SignOut(username))
            {
                loginPanel.Visible = true;
                mainPanel.Visible = false;
            }
        }
        
    }
}