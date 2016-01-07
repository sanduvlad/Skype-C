using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Interogare
{
    public class XmlDataBase
    {
        public int LogIn(string username, string parola)
        {
            int count = 0;

            XElement root = XElement.Load("DB.xml");
            IEnumerable<XElement> address =
                from el in root.Elements("users").Elements("user")
                where (string)el.Attribute("username") == username & (string)el.Element("parola") == parola
                select el;
            foreach (XElement el in address)
                count++;

            if (count == 0)
            {
                //Console.WriteLine("Username-ul sau parola sunt gresite");
                return 0;
            }
            else
            {
                //Console.WriteLine("Autentificare reusita!");
                return 1;
            }
        }

        public void Delete(string username)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("DB.xml");
            XmlNodeList nodes = doc.SelectNodes("//user[@username='" + username + "']");
            for (int i = nodes.Count - 1; i >= 0; i--)
            {
                nodes[i].ParentNode.RemoveChild(nodes[i]);
            }
            doc.Save("DB.xml");
        }

        public int Register(string username, string parola, string reParola, string nume, string prenume, string varsta)
        {
            if (!parola.Equals(reParola))
            {
                return 0;
            }

            int count = 0;

            XElement root = XElement.Load("DB.xml");
            IEnumerable<XElement> address =
                from el in root.Elements("user")
                where (string)el.Attribute("username") == username
                select el;

            foreach (XElement el in address)
                count++;

            if (count == 0)
            {
                var xDoc = XElement.Load("DB.xml");
                if (xDoc == null)
                {
                    Console.WriteLine("S-a produs o eroare la incarcarea xml-ului");
                    return 0;
                }

                var myNewElement = new XElement("user",

                   new XAttribute("username", username),
                   new XElement("parola", parola),
                   new XElement("nume", nume),
                   new XElement("prenume", prenume),
                   new XElement("varsta", varsta),
                   new XElement("prieteni", null)
                    //And so on ...
                );
                xDoc.Add(myNewElement);
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
    }
}
