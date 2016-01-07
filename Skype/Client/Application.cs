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
    public partial class Application : Form
    {
        ClientToServerHandle cliToSvr;
        ServerToClientHandle svrToCli;
        String username;
        Dictionary<string, string>friendchoices = new Dictionary<string, string>();

        public Application()
        {
            InitializeComponent();
            cliToSvr = new ClientToServerHandle();
            svrToCli = new ServerToClientHandle();
            ServerToClientCOM.Wrapper.GetInstance().Attach(svrToCli);
        }


        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cliToSvr.SignOut(username)==1)
            {
                loginPanel.Visible = true;
                mainPanel.Visible = false;
            }
        }

        private void RegisterMenuItem_Click(object sender, EventArgs e)
        {
            loginPanel.Visible = false;
            registerPanel.Visible = true;
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loginPanel.Visible = true;
            registerPanel.Visible = false;
        }

        private void bLogin_Click(object sender, EventArgs e)
        {
            String serverIp = cliToSvr.GetServerAddress();
            cliToSvr.InitConnectionToServer("192.168.0.101");
            if(cliToSvr.SignIn(UsernameLoginTextBox.Text, PasswordLoginTextBox.Text)==1)
            {
                username = UsernameLoginTextBox.Text;
                loginPanel.Visible = false;
                mainPanel.Visible = true;
                loginResponseLabel.Text = "";
                UsernameApplicationTextBox.Text = username;
                StatusesComboBox.SelectedItem = "Online";
                ListFriends();
            }
            else
            {
                friendsList.Items.Clear();
                username = UsernameLoginTextBox.Text;
                loginPanel.Visible = true;
                mainPanel.Visible = false;
                loginResponseLabel.Text = "Login Failed";
            }
        }
        

       

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            String serverIp = cliToSvr.GetServerAddress();
            cliToSvr.InitConnectionToServer(serverIp);
            if(cliToSvr.Register(UsernameRegisterTextBox.Text, PasswordRegisterTextBox.Text, EmailRegisterTextBox.Text, NameRegisterTextBox.Text)==1)
            {
                ResponseRegisterLabel.Text = "Register Succeded";
            }else
            {
                ResponseRegisterLabel.Text = "Register Failed";
            }
                
        }

        private void StatusesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            cliToSvr.ChangeStatus(username,StatusesComboBox.Text.ToLower());

        }

        private void friendsList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SearchFriendsButton_Click(object sender, EventArgs e)
        {
            listSearchedUsers();
        }

        private void listSearchedUsers()
        {
            string query = searchFriendsText.Text;
            if (query.Length < 2)
            {
                addFriendsLabel.Text = "The searched term needs to be greater than 2 symbols";
                addFriendsList.Items.Clear();
            }
            else
            {
                addFriendsLabel.Text = "";
                addFriendsList.Items.Clear();
                foreach (string u in cliToSvr.SearchUsers(query, username))
                {
                    addFriendsList.Items.Add(u);
                }
                if (addFriendsList.Items.Count > 0)
                {
                    addFriendsLabel.Text = "Click the username you want to add as friend";
                }
            }
        }

        private void ListFriends()
        {
<<<<<<< HEAD
<<<<<<< HEAD
            friendsList.DataSource = null;
            friendsList.Items.Clear();
            var choices = new Dictionary<string, string>();
            string[] friends =  cliToSvr.GetFriends(username);
            foreach (string user in friends)
            {
                try
                {
                    choices[user] = user.Split(' ')[0] + "-" + user.Split(' ')[1];
                }
                catch (ArgumentOutOfRangeException)
                {
                    break;
                }
            }
            if (choices.Count == 0)
            {
                choices.Add("", "");
=======
=======
>>>>>>> origin/master
            friendchoices.Clear();
            foreach (List<string> user in cliToSvr.GetFriends(username))
            {
                friendchoices[user.ElementAt(0)] = user.ElementAt(1) + " - " + user.ElementAt(2);
            }
            if (friendchoices.Count > 0)
            {
                friendsList.DataSource = new BindingSource(friendchoices, null);
                friendsList.DisplayMember = "Value";
                friendsList.ValueMember = "Key";
                friendsList.Refresh();
<<<<<<< HEAD
>>>>>>> origin/master
=======
>>>>>>> origin/master
            }
        }

        private void addFriendsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string friendSelected = String.Copy(addFriendsList.SelectedItem.ToString());
            cliToSvr.AddFriend(username, friendSelected);
            ListFriends();
            listSearchedUsers();
<<<<<<< HEAD
        }

        private void SendMessageButton_Click(object sender, EventArgs e)
        {
            
=======
>>>>>>> origin/master
        }
    }
}
