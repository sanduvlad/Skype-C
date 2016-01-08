using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Application : Form
    {
        ClientToServerHandle cliToSvr;
        ServerToClientHandle svrToCli;
        String username;
        bool loggedIn = false;
        Dictionary<string, string> friendchoices = new Dictionary<string, string>();


        /// <summary>
        /// Construct method that starts the application
        /// </summary>
        public Application()
        {
            //StatusesComboBox.Enabled = false;
            InitializeComponent();
            this.AcceptButton = LoginButton;
            cliToSvr = new ClientToServerHandle();
            svrToCli = new ServerToClientHandle(this);
            ServerToClientCOM.Wrapper.GetInstance().Attach(svrToCli);
            StatusesComboBox.Enabled = true;
        }


        /// <summary>
        /// Logout click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cliToSvr.SignOut(username) == 1)
            {
                loginPanel.Visible = true;
                mainPanel.Visible = false;
                cliToSvr.ChangeStatus(username, "offline");
                loggedIn = false;
            }
        }
        

        /// <summary>
        /// Register click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegisterMenuItem_Click(object sender, EventArgs e)
        {
            loginPanel.Visible = false;
            registerPanel.Visible = true;
        }

        /// <summary>
        /// Login menu item click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loginPanel.Visible = true;
            registerPanel.Visible = false;
        }

        /// <summary>
        /// Login button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bLogin_Click(object sender, EventArgs e)
        {
            String serverIp = cliToSvr.GetServerAddress();
            cliToSvr.InitConnectionToServer(serverIp);
            if (cliToSvr.SignIn(UsernameLoginTextBox.Text, PasswordLoginTextBox.Text) == 1)
            {
                username = UsernameLoginTextBox.Text;
                loginPanel.Visible = false;
                mainPanel.Visible = true;
                loginResponseLabel.Text = "";
                UsernameApplicationTextBox.Text = username;
                StatusesComboBox.SelectedItem = "Online";
                ConversationTextBox.Text = string.Empty;
                ListFriends();
                this.AcceptButton = SendMessageButton;
                cliToSvr.ChangeStatus(username, "online");
                loggedIn = true;
            }
            else
            {
                friendsList.Items.Clear();
                username = UsernameLoginTextBox.Text;
                loginPanel.Visible = true;
                mainPanel.Visible = false;
                loginResponseLabel.Text = "Login Failed";
                loggedIn = false;
            }
        }




        /// <summary>
        /// Register Button Click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegisterButton_Click(object sender, EventArgs e)
        {
            String serverIp = cliToSvr.GetServerAddress();
            cliToSvr.InitConnectionToServer(serverIp);
            if (cliToSvr.Register(UsernameRegisterTextBox.Text, PasswordRegisterTextBox.Text, EmailRegisterTextBox.Text, NameRegisterTextBox.Text) == 1)
            {
                ResponseRegisterLabel.Text = "Register Succeded";
            }
            else
            {
                ResponseRegisterLabel.Text = "Register Failed";
            }

        }

        /// <summary>
        /// Status change event handle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StatusesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            cliToSvr.ChangeStatus(username, StatusesComboBox.Text.ToLower());

        }

        /// <summary>
        /// Afiseaza covnersatia dintre userul curent si userul primit ca parametru
        /// </summary>
        /// <param name="receiver">Reveiver</param>
        private void DisplayConversation(string receiver)
        {
            string[] allmessages = cliToSvr.GetMessages(username, receiver);
            ConversationTextBox.Text = string.Empty;
            foreach (string message in allmessages)
            {
                if (message.Split(' ')[2].Equals(username))
                {
                    ConversationTextBox.Text += message.Split(' ')[0] + " " + message.Split(' ')[1] + " | Me ---> "; //+ message.Split(' ')[4] + Environment.NewLine;
                    for (int i = 4; i < message.Split(' ').Length; i++)
                    {
                        ConversationTextBox.Text += message.Split(' ')[i] + " ";
                    }
                    ConversationTextBox.Text += Environment.NewLine;
                }
                else
                {
                    ConversationTextBox.Text += message.Split(' ')[0] + " " + message.Split(' ')[1] + " | " + message.Split(' ')[2] + " ---> "; //+ message.Split(' ')[4] + Environment.NewLine;
                    for (int i = 4; i < message.Split(' ').Length; i++)
                    {
                        ConversationTextBox.Text += message.Split(' ')[i] + " ";
                    }
                    ConversationTextBox.Text += Environment.NewLine;
                }

                ConversationTextBox.SelectionStart = ConversationTextBox.Text.Length;
                ConversationTextBox.ScrollToCaret();
            }


            string friend = friendsList.Items[friendsList.FindString(receiver)].ToString();
            string exclamation = " ";
            if (friend.Length > 1)
            {
                exclamation = friend.Substring(friend.Length - 1);
            }
            if (exclamation.Equals("!"))
            {
                friendsList.Items[friendsList.FindString(receiver)] = friend.Remove(friend.Length - 1);
            }

        }

        /// <summary>
        /// Event click lista de prieteni
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void friendsList_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (friendsList.SelectedIndex > -1)
                DisplayConversation(friendsList.Items[friendsList.SelectedIndex].ToString().Split(' ')[0]);

        }

        /// <summary>
        /// Event handle pentru butonul de search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchFriendsButton_Click(object sender, EventArgs e)
        {
            listSearchedUsers();
        }

        /// <summary>
        /// Functie ce trateaza search-ul 
        /// </summary>
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


        /// <summary>
        /// Functie ce listeaza prietenii in listbox
        /// </summary>
        private void ListFriends()
        {

            friendsList.DataSource = null;
            int selectedIndex = 0;
            if (friendsList.SelectedIndex > 0)
            {
                selectedIndex = friendsList.SelectedIndex;
            }
            friendsList.Items.Clear();
            string[] friends = cliToSvr.GetFriends(username);
            foreach (string user in friends)
            {
                try
                {
                    friendsList.Items.Add(user.Split(' ')[0] + " - " + user.Split(' ')[1]);
                }
                catch (ArgumentOutOfRangeException)
                {
                    break;
                }
            }
            if (friends.Length > 0 && selectedIndex < 0)
            {
                if (friendsList.SelectedIndex < 0)
                {
                    friendsList.SelectedIndex = 0;
                }
                DisplayConversation(friendsList.Items[friendsList.SelectedIndex].ToString().Split(' ')[0]);
            }
            if (friends.Length > 0)
            {
                friendsList.SelectedIndex = selectedIndex;
            }
        }

        /// <summary>
        /// Event handle pentru click pe lista de prieteni
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addFriendsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string friendSelected = String.Copy(addFriendsList.SelectedItem.ToString());
            cliToSvr.AddFriend(username, friendSelected);
            ListFriends();
            listSearchedUsers();
        }


        /// <summary>
        /// Event-ul de click pe butonul de send message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendMessageButton_Click(object sender, EventArgs e)
        {


            if (friendsList.SelectedIndex < 0)
            {
                friendsList.SelectedIndex = 0;
            }

            cliToSvr.SendMessage(username, friendsList.Items[friendsList.SelectedIndex].ToString().Split(' ')[0], SendMessageTextBox.Text);
            //admin1 - offline
            DateTime thisDay = DateTime.Now;
            ConversationTextBox.Text += thisDay.ToString("dd/MM/yy H:mm:ss") + " | Me ---> " + SendMessageTextBox.Text + Environment.NewLine;
            SendMessageTextBox.Text = string.Empty;
            ConversationTextBox.SelectionStart = ConversationTextBox.Text.Length;
            ConversationTextBox.ScrollToCaret();
        }

        /// <summary>
        /// Functie de afisare a unui mesaj primit
        /// </summary>
        /// <param name="message"></param>
        /// <param name="who"></param>
        public void DisplayMessageOnScreen(string message, string who)
        {
            Invoke((MethodInvoker)(() =>
            {
                if (friendsList.SelectedIndex > -1)
                {
                    //Invoke((MethodInvoker)(() => ConversationTextBox.Text += message + Environment.NewLine));
                    if (who.Equals(friendsList.Items[friendsList.SelectedIndex].ToString().Split(' ')[0]))
                        DisplayConversation(friendsList.Items[friendsList.SelectedIndex].ToString().Split(' ')[0]);

                    else
                    {
                        string friend = friendsList.Items[friendsList.FindString(who)].ToString();
                        string exclamation = " ";
                        if (friend.Length > 1)
                        {
                            exclamation = friend.Substring(friend.Length - 1);
                        }
                        if (!exclamation.Equals("!"))
                        {
                            friendsList.Items[friendsList.FindString(who)] = friend + " !";
                        }

                    }
                }
            }));

        }

        /// <summary>
        /// Functie ce trateaza schimbarea de status
        /// </summary>
        /// <param name="userNameParam"></param>
        /// <param name="state"></param>
        public void UserChangedStatus(string userNameParam, string state)
        {
            //Thread.Sleep(2000);
            object[] parameters = new object[] { userNameParam };
            Invoke((MethodInvoker)(() =>
            {
                for (int i = 0; i < friendsList.Items.Count; i++)
                {
                    if (friendsList.Items[i].ToString().Contains((string)parameters[0]))
                    {
                        friendsList.Items[i] = friendsList.Items[i].ToString().Split(' ')[0] + " - " + state;
                        break;
                    }
                }
            }), parameters);

        }

        /// <summary>
        /// Eventul apelat la schimbarea de text in boxul de search prieteni
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchFriendsText_TextChanged(object sender, EventArgs e)
        {
            this.AcceptButton = SearchFriendsButton;
        }


        /// <summary>
        /// Eventul apelat la schimbarea de text in text boxul de adaugare mesaj
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendMessageTextBox_TextChanged(object sender, EventArgs e)
        {
            this.AcceptButton = SendMessageButton;
        }

        private void Application_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (loggedIn == true)
            {
                cliToSvr.ChangeStatus(username, "offline");
                cliToSvr.SignOut(username);
            }
        }
    }
}