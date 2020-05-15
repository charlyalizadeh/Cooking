using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Cooking.SQL
{
    public class DemoSQL : SQLBasic
    {
 

        #region Méthodes
        public List<string[]> GetClientData()
        {
            MySqlDataReader reader = ReadCommand("SELECT C.pseudo,count(CR.nom_recetteC) FROM client C JOIN recette R ON C.pseudo = R.pseudo JOIN commandeRecette CR ON R.nom = CR.nom_recetteC GROUP BY C.pseudo;");
            List<string[]> infos = new List<string[]>();
            while(reader.Read())
                if (reader.HasRows)
                    infos.Add(new string[] { reader.GetString(0), reader.GetString(1) });
            reader.Close();
            return infos;
        }
        public string[] ProduitLowerStock()
        {
            MySqlDataReader reader = ReadCommand("SELECT P.nom FROM produit P WHERE P.stock_a <= 2 * P.stock_min;");
            List<string> produit = new List<string>();
            while (reader.Read())
                produit.Add(reader.GetString(0));
            reader.Close();
            return produit.ToArray();
        }
        public List<string[]> GetAllRecette(string produit)
        {
            MySqlDataReader reader = ReadCommand(String.Format("SELECT R.nom,LP.quantite FROM recette R JOIN listeProduit LP ON R.nom = LP.nom_recette WHERE LP.nom_produit = {0};",produit));
            List<string[]> recette = new List<string[]>();
            while(reader.Read())
            {
                List<string> temp = new List<string>();
                for (int i = 0; i < reader.FieldCount; i++)
                    temp.Add(reader.GetString(0));
                recette.Add(temp.ToArray());
            }
            reader.Close();
            return recette;
        }
        public bool IsProduit(string produit)
        {
            MySqlDataReader reader = ReadCommand(String.Format("SELECT count(nom) FROM produit WHERE nom = {0}", produit));
            reader.Read();
            int count = reader.GetInt32(0);
            reader.Close();
            return count > 0;
        }
        #endregion



    }
}
