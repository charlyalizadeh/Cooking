using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

namespace Cooking.XML
{
    static class XMLSerialisation
    {
        #region Méthodes
        public static void SerialiserCommandes(string filename,ListeCommandes commandes)
        {
            XmlSerializer xs = new XmlSerializer(typeof(ListeCommandes));
            StreamWriter wr = new StreamWriter(filename);
            xs.Serialize(wr, commandes);
            wr.Close();
        }
        public static ListeCommandes DeserialiserCommandes(string filename)
        {
            if (!File.Exists(filename))
                CreateXMLFile(filename);
            ListeCommandes commandes;
            XmlSerializer xs = new XmlSerializer(typeof(ListeCommandes));
            StreamReader rd = new StreamReader(filename);
            commandes = xs.Deserialize(rd) as ListeCommandes;
            rd.Close();
            return commandes;
        }
        public static void CreateXMLFile(string filename)
        {
            SerialiserCommandes(filename, new ListeCommandes());
        }
        #endregion
    }
}
