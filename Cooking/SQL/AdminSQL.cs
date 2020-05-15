using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Cooking.SQL
{
    public class AdminSQL : SQLBasic
    {
        #region Méthodes

        #region Tableau de bord
        /// <summary>
        /// Get the best CDR of the week
        /// </summary>
        /// <returns>A string array which contains infos on the best CDR of the week</returns>
        public string[] GetBestCDRWeek()
        {
            MySqlDataReader reader = ReadCommand("SELECT C.pseudo, sum(Q) FROM client C JOIN recette R ON C.pseudo = R.pseudo JOIN (SELECT nom_recetteC, sum(quantite) Q FROM commandeRecette WHERE TIMESTAMPDIFF(WEEK, date_commande, NOW()) <= 1 GROUP BY nom_recetteC) CR ON CR.nom_recetteC = R.nom GROUP BY C.pseudo ORDER BY CR.Q DESC LIMIT 1;");
            reader.Read();
            string[] result = new string[] { };
            if (reader.HasRows)
                result = new string[] { reader.GetString(0), reader.GetString(1) };
            reader.Close();
            return result;
        }
        /// <summary>
        /// Get the top 5 best recette of the week
        /// </summary>
        /// <returns>A list of string array, each element of the list is one recette</returns>
        public List<string[]> GetBestRecetteWeek()
        {
            MySqlDataReader reader = ReadCommand("SELECT R.nom,C.pseudo,R.type,Q FROM client C JOIN recette R ON C.pseudo = R.pseudo JOIN (SELECT nom_recetteC, sum(quantite) Q FROM commandeRecette WHERE TIMESTAMPDIFF(WEEK, date_commande, NOW()) <= 1 GROUP BY nom_recetteC) CR ON CR.nom_recetteC = R.nom ORDER BY CR.Q DESC LIMIT 5;");
            List<string[]> bestRecette = new List<string[]>();
            while(reader.Read())
                bestRecette.Add(new string[] { reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3) });
            reader.Close();
            return bestRecette;
        }
        /// <summary>
        /// Get the gold CDR
        /// </summary>
        /// <returns>A string array which contains infos about the gold CDR </returns>
        public string[] GetOrCDR()
        { 
            MySqlDataReader reader = ReadCommand("SELECT C.pseudo,sum(Q) FROM client C JOIN recette R ON C.pseudo = R.pseudo JOIN (SELECT nom_recetteC, sum(quantite) Q FROM commandeRecette GROUP BY nom_recetteC) CR ON CR.nom_recetteC = R.nom GROUP BY C.pseudo ORDER BY CR.Q DESC LIMIT 1;");
            reader.Read();
            string[] result = new string[] { };
            if (reader.HasRows)
                result = new string[] { reader.GetString(0), reader.GetString(1) };
            reader.Close();
            return result;
        }
        /// <summary>
        /// Get the top 5 best recette of a CDR
        /// </summary>
        /// <param name="pseudo">pseudo of the CDR</param>
        /// <returns>A list of string array, each element of the list is one recette</returns>
        public List<string[]> GetBestRecettesCDR(string pseudo)
        {
            MySqlDataReader reader = ReadCommand(String.Format("SELECT nom_recetteC,sum(quantite) Q FROM commandeRecette WHERE nom_recetteC IN (SELECT R.nom FROM recette R JOIN client C ON R.pseudo = C.pseudo WHERE C.pseudo = {0}) GROUP BY nom_recetteC ORDER BY Q DESC LIMIT 5;",pseudo));
            List<string[]> bestRecettes = new List<string[]>();
            while(reader.Read())
            {
                bestRecettes.Add(new string[] { reader.GetString(0), reader.GetString(1) });
            }
            reader.Close();
            return bestRecettes;
        }
        #endregion

        #region Gestion cuisinier
        public List<string[]> GetAllCDR()
        {
            MySqlDataReader reader = ReadCommand("SELECT C.pseudo,C.nom,C.prenom,count(R.pseudo) FROM client C JOIN recette R WHERE R.pseudo = C.pseudo GROUP BY C.pseudo;");
            List<string[]> pseudos = new List<string[]>();
            while (reader.Read())
            {
                List<string> temp = new List<string>();
                for(int i = 0;i<reader.FieldCount;i++)
                    temp.Add(reader.GetString(i));
                pseudos.Add(temp.ToArray());
            }
            reader.Close();
            return pseudos;
        }
        public string[] GetCDRNom()
        {
            MySqlDataReader reader = ReadCommand("SELECT C.pseudo FROM client C JOIN recette R WHERE R.pseudo = C.pseudo GROUP BY C.pseudo;");
            List<string> pseudos = new List<string>();
            while (reader.Read())
                pseudos.Add(reader.GetString(0));
            reader.Close();
            return pseudos.ToArray();
        }
        public string[] GetCDRTuple(string pseudo)
        {
            return GetOneValues("client", new string[] { "pseudo", "nom", "prenom", "num_tel", "solde" }, "'" + pseudo + "'", "pseudo");
        }
        public void DeleteCDRRecettes(string pseudo)
        {
            ExecuteCommand(String.Format("DELETE FROM recette WHERE pseudo = '{0}'", pseudo));
        }
        #endregion

        #region Gestion Fournisseur
        public string GetNewReference()
        {
            MySqlDataReader reader = ReadCommand("SELECT count(ref_fournisseur) FROM fournisseur");
            reader.Read();
            string temp = reader.GetInt32(0).ToString();
            string newRef = "F" + new String('0', 3 - temp.Length) + temp;
            reader.Close();
            return newRef;
        }
        #endregion

        #region Gestion Produit
        public bool IsProduit(string nom)
        {
            MySqlDataReader reader = ReadCommand(String.Format("SELECT count(nom) FROM produit WHERE nom = {0}", nom));
            reader.Read();
            int count = reader.GetInt16(0);
            reader.Close();
            return count != 0;
        }
        public void Restock(string produit,float quantite)
        {
            ExecuteCommand(String.Format("UPDATE produit SET quantite = quantite + {0} WHERE nom = {1}", quantite, produit));
        }
        public void UpdateProduitNotUsed()
        {
            ExecuteCommand("UPDATE produit P SET P.stock_a = P.stock_a/2 WHERE P.nom IN (SELECT LP.nom_produit FROM commandeRecette CR JOIN listeProduit LP  ON  CR.nom_recetteC = LP.nom_recette  GROUP BY LP.nom_produit HAVING TIMESTAMPDIFF(DAY,MAX(date_commande),NOW())>30);");
        }
        #endregion

        #region Gestion Commande
        public List<string[]> GetProduitStock()
        {
            MySqlDataReader reader = ReadCommand("SELECT nom,stock_a,stock_max,ref_fournisseur FROM produit");
            List<string[]> infos = new List<string[]>();
            while(reader.Read())
            {
                infos.Add(new string[] { reader.GetString(0), reader.GetString(1), reader.GetString(2),reader.GetString(3) });
            }
            reader.Close();
            return infos;
        }
        #endregion

        #endregion
    }
}
