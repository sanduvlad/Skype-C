using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Interogare_BD
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1 - Log in, 2 - Register");
            string s = Console.ReadLine();

            if(s == "1")
            {
                int count = 0;
                Console.WriteLine("Dati username ");
                string username = Console.ReadLine();
                Console.WriteLine("Dati parola");
                string parola = Console.ReadLine();

                XElement root = XElement.Load("DB.xml");
                IEnumerable<XElement> address =
                    from el in root.Elements("user")
                    where (string)el.Attribute("username") == username & (string)el.Element("parola") == parola
                    select el;
                foreach (XElement el in address)
                    count++;
                if(count == 0)
                {
                    Console.WriteLine("Username-ul sau parola sunt gresite");
                }
                else
                {
                    Console.WriteLine("Autentificare reusita!");
                }
            }
            else
            {
                int count = 0;
                Console.WriteLine("Dati username(unic): ");
                string username = Console.ReadLine();

                XElement root = XElement.Load("DB.xml");
                IEnumerable<XElement> address =
                    from el in root.Elements("user")
                    where (string)el.Attribute("username") == username
                    select el;

                foreach (XElement el in address)
                    count++;

                if (count == 0)
                {
                    Console.WriteLine("parola: ");
                    string b = Console.ReadLine();
                    Console.WriteLine("nume: ");
                    string c = Console.ReadLine();
                    Console.WriteLine("prenume: ");
                    string d = Console.ReadLine();
                    Console.WriteLine("varsta: ");
                    string e = Console.ReadLine();
                    var xDoc = XElement.Load("DB.xml");
                    if (xDoc == null)
                        return;

                    var myNewElement = new XElement("user",

                       new XAttribute("username", username),
                       new XElement("parola", b),
                       new XElement("nume", c),
                       new XElement("prenume", d),
                       new XElement("varsta", e)
                    //And so on ...
                    );
                    xDoc.Add(myNewElement);
                    xDoc.Save("DB.xml");
                }
                else
                {
                    Console.WriteLine("Username deja folosit");
                }
            }
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
