using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Interogare
{
    /// <summary>
    /// Clasa ce contine functii ce lucreaza direct cu baza de date
    /// </summary>
    public class XmlDataBase
    {
        /// <summary>
        /// Functie ce verifica daca username-ul si o parola exista in baza de date
        /// </summary>
        /// <param name="username"></param>
        /// <param name="parola"></param>
        /// <returns></returns>
        public int VerifyUser(string username, string parola)
        {
            int count = 0;
            XElement xDoc = XElement.Load("DB.xml");
            IEnumerable<XElement> address =
                from el in xDoc.Elements("users").Elements("user")
                where (string)el.Attribute("username") == username & (string)el.Element("parola") == parola
                select el;
            foreach (XElement el in address)
                count++;
            return count;
        }

        /// <summary>
        /// Functie ce verifica daca un username exista in baza de date
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public int UserExists(string username)
        {
            int count = 0;
            XElement xDoc = XElement.Load("DB.xml");
            IEnumerable<XElement> address =
                from el in xDoc.Elements("users").Elements("user")
                where (string)el.Attribute("username") == username
                select el;
            foreach (XElement el in address)
                count++;
            return count;
        }

        /// <summary>
        /// Functie ce realizeaza sau nu conexiunea la server
        /// </summary>
        /// <param name="username"></param>
        /// <param name="parola"></param>
        /// <returns></returns>
        public int LogIn(string username, string parola)
        {
            if (VerifyUser(username, parola) == 1)
            {
                //Console.WriteLine("Autentificare reusita!");
                ChangeStatus(username, "online");
                return 1;
            }
            else
            {
                //Console.WriteLine("Username-ul sau parola sunt gresite");
                return 0;
            }
        }

        /// <summary>
        /// Functie ce delogheaza un user si schimba starea acestuia in oflline
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public int LogOut(string username)
        {
            if (UserExists(username) == 1)
            {
                //Console.WriteLine("Delogare reusita!");
                ChangeStatus(username, "offline");
                return 1;
            }
            else
            {
                //Console.WriteLine("Username-ul sau parola sunt gresite");
                return 0;
            }
        }

        /// <summary>
        /// Functie ce sterge un cont
        /// </summary>
        /// <param name="username"></param>
        /// <param name="parola"></param>
        /// <returns></returns>
        public int Delete(string username, string parola)
        {
            if (VerifyUser(username, parola) == 0)
            {
                //Console.WriteLine("Nu ai dreptul sa stergi acest cont");
                return 0;
            }
            else
            {

                XmlDocument xDoc = new XmlDocument();
                xDoc.Load("DB.xml");
                XmlNodeList nodes = xDoc.SelectNodes("//user[@username='" + username + "']");
                for (int i = nodes.Count - 1; i >= 0; i--)
                {
                    nodes[i].ParentNode.RemoveChild(nodes[i]);
                }
                xDoc.Save("DB.xml");

                //Console.WriteLine("Cont sters!");
                return 1;
            }
        }

        /// <summary>
        /// Functie ce realizeaza crearea unui nou cont pentru server, verificand daca username-ul este unic
        /// </summary>
        /// <param name="username"></param>
        /// <param name="parola"></param>
        /// <param name="reParola"></param>
        /// <param name="nume"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public int Register(string username, string parola, string reParola, string nume, string email)
        {
            if (!parola.Equals(reParola))
            {
                return 0;
            }

            int count = 0;

            XElement xDoc = XElement.Load("DB.xml");

            if (xDoc == null)
            {
                //Console.WriteLine("S-a produs o eroare la incarcarea xml-ului");
                return 0;
            }

            IEnumerable<XElement> address =
                from el in xDoc.Elements("users").Elements("user")
                where (string)el.Attribute("username") == username
                select el;

            foreach (XElement el in address)
                count++;

            if (count == 0)
            {
                var myNewElement = new XElement("user",
                   new XAttribute("username", username),
                   new XAttribute("status", "offline"),
                   new XElement("parola", parola),
                   new XElement("nume", nume),
                   new XElement("email", email)
                //And so on ...
                );

                var myNewElement2 = new XElement(username);

                xDoc.Element("users").Add(myNewElement);
                xDoc.Element("friends").Add(myNewElement2);
                xDoc.Save("DB.xml");
                //Console.WriteLine("Inregistrare reusita");
                return 1;
            }
            else
            {
                //Console.WriteLine("Username deja folosit");
                return 0;
            }
        }

        /// <summary>
        /// Functie ce adauga un prieten unui user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="friend"></param>
        /// <returns></returns>
        public int AddFriend(string username, string friend)
        {
            var myNewElement = new XElement("friend", friend);
            int count = 0;
            XElement xDoc = XElement.Load("DB.xml");
            IEnumerable<XElement> address =
               from el in xDoc.Elements("friends").Elements(username)
               select el;
            foreach (XElement el in address)
                count++;

            if (xDoc == null)
                //Console.WriteLine("S-a produs o eroare la incarcarea xml-ului");
                return 0;
            if (count == 0)
            {
                xDoc.Element("friends").Add(new XElement(username, null));
            }

            xDoc.Element("friends").Element(username).Add(myNewElement);
            xDoc.Save("DB.xml");
            return 1;
        }

        /// <summary>
        /// Functie ce schimba statusul unui user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int ChangeStatus(string username, string status)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("DB.xml");
            XmlElement formData = (XmlElement)xDoc.SelectSingleNode("//users/user[@username='" + username + "']");
            if (formData != null)
            {
                formData.SetAttribute("status", status);
            }
            xDoc.Save("DB.xml");
            return 1;
        }

        /// <summary>
        /// Functie ce returneaza toti prietenii unui user impreuna cu statusul acestora
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public string[] AllFriends(string username)
        {
            List<string> friends = new List<string>();

            XElement xDoc = XElement.Load("DB.xml");

            IEnumerable<XElement> address =
                from el in xDoc.Elements("friends").Elements(username).Elements("friend")
                select el;

            foreach (XElement el in address)
            {

                friends.Add((string)el);
            }
            return AllFriendsDetails(friends);
        }

        /// <summary>
        /// Functie ce returneaza toti prietenii unui user 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<string> GetFriends(string username)
        {
            List<string> friends = new List<string>();

            XElement xDoc = XElement.Load("DB.xml");
            IEnumerable<XElement> address =
                from el in xDoc.Elements("friends").Elements(username).Elements("friend")
                select el;

            foreach (XElement el in address)
            {

                friends.Add((string)el);
            }
            return friends;
        }

        /// <summary>
        /// Functie ce cauta toti userii existenti dupa un anumit query introdus
        /// </summary>
        /// <param name="query"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<string> FindUsers(string query, string username)
        {
            List<string> users = new List<string>();
            List<string> user = new List<string>();
            int f = 0;
            XElement xDoc = XElement.Load("DB.xml");
            List<string> friends = GetFriends(username);

            IEnumerable<XElement> address =
                from el in xDoc.Elements("users").Elements("user")
                where (string)el.Attribute("username") != username
                select el;
            foreach (XElement el in address)
            {
                user.Add((string)el.Attribute("username"));
            }

            foreach (string el in user)
            {

                if (el.Contains(query))
                {
                    f = 0;
                    foreach (string u in friends)
                    {
                        if (u == el)
                        {
                            f = 1;
                            break;
                        }
                    }
                    if (f != 1)
                    {
                        users.Add(el);

                    }
                }
            }
            return users;
        }

        /// <summary>
        /// Functie ce extrage din baza de date toate detaliile unor useri dati ca parametru
        /// </summary>
        /// <param name="friends"></param>
        /// <returns></returns>
        public string[] AllFriendsDetails(List<string> friends)
        {
            string[] details = new string[friends.Count];
            for (int j = 0; j < friends.Count; j++)
            {
                details[j] = string.Empty;
            }
            //username status
            //username2 status2
            int i = 0;
            List<string> user = new List<String>();
            XElement xDoc = XElement.Load("DB.xml");

            foreach (string el in friends)
            {
                user.Clear();
                user.Add(el);

                IEnumerable<XElement> address =
                    from elm in xDoc.Elements("users").Elements("user")
                    where (string)elm.Attribute("username") == el
                    select elm;

                foreach (XElement elm in address)
                {
                    //user.Add((string)elm.Attribute("username"));
                    user.Add((string)elm.Attribute("status"));
                }
                details[i] = (user.ElementAt(0) + " " + user.ElementAt(1)).ToString();
                i++;
            }
            return details;
        }

        /// <summary>
        /// Adauga un mesaj in baza de date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="receiver"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public int AddMessage(string sender, string receiver, string message)
        {
            int count = 0;
            XElement xDoc = XElement.Load("DB.xml");

            if (xDoc == null)
                //Console.WriteLine("S-a produs o eroare la incarcarea xml-ului");
                return 0;

            IEnumerable<XElement> address =
                from el in xDoc.Elements("messages").Elements("message")
                where (string)el.Attribute("sender") == sender & (string)el.Attribute("receiver") == receiver
                select el;
            foreach (XElement el in address)
                count++;
            if (count == 0)
            {
                DateTime thisDay = DateTime.Now;
                var myNewElement = new XElement("message",
                    new XAttribute("sender", sender),
                    new XAttribute("receiver", receiver),
                    new XElement("text",
                    new XAttribute("created", thisDay.ToString("dd/MM/yy H:mm:ss")), message)
                    );

                xDoc.Element("messages").Add(myNewElement);
                xDoc.Save("DB.xml");
            }
            else
            {
                DateTime thisDay = DateTime.Now;
                var myNewElement = new XElement("text",
                    new XAttribute("created", thisDay.ToString("dd/MM/yy H:mm:ss")), message);

                foreach (XElement el in address)
                {
                    el.Add(myNewElement);
                    xDoc.Save("DB.xml");
                }
            }
            return 1;
        }

        /// <summary>
        /// Functie ce returneaza toate mesajele trimise intre 2 users
        /// </summary>
        /// <param name="user1"></param>
        /// <param name="user2"></param>
        /// <returns></returns>
        public string[] AllMessages(string user1, string user2)
        {
            List<string> message = new List<String>();
            IEnumerable<XElement> textMessages = null;
            XElement xDoc = XElement.Load("DB.xml");

            IEnumerable<XElement> address =
                from el in xDoc.Elements("messages").Elements("message")
                where ((string)el.Attribute("sender") == user1 & (string)el.Attribute("receiver") == user2) ||
                        ((string)el.Attribute("sender") == user2 & (string)el.Attribute("receiver") == user1)
                select el;


            int count = 0, i = 0;

            foreach (XElement elm in address)
            {
                textMessages =
                from mes in elm.Elements("text")
                select mes;
                foreach (XElement ms in textMessages)
                {
                    count++;
                }
            }

            string[] messages = new string[count];
            for (int j = 0; j < count; j++)
            {
                messages[j] = string.Empty;
            }

            foreach (XElement el in address)
            {
                textMessages =
                from mes in el.Elements("text")
                select mes;
                foreach (XElement ms in textMessages)
                {
                    message.Clear();
                    message.Add((string)ms.Attribute("created"));
                    message.Add((string)el.Attribute("sender"));
                    message.Add((string)el.Attribute("receiver"));
                    message.Add((string)ms);
                    messages[i] = (message.ElementAt(0) + " " + message.ElementAt(1) + " " + message.ElementAt(2) + " " + message.ElementAt(3)).ToString();
                    i++;
                }
            }
            Array.Sort(messages);
            return messages;
        }

<<<<<<< HEAD
        /// <summary>
        /// Functie ce returneaza statusul unui user
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public string GetStatus(string username)
        {
            XElement xDoc = XElement.Load("DB.xml");
            IEnumerable<XElement> address =
                from el in xDoc.Elements("users").Elements("user")
                where (string)el.Attribute("username") == username
                select el;

            string status = null;

            foreach (XElement elm in address)
            {
                status = (string)elm.Attribute("status");
            }

            return status;
        }
=======
>>>>>>> origin/master
    }
}
