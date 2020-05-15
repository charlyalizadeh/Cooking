using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace Cooking.SQL
{
    public class ClientSQL : SQLBasic
    {
        #region Méthodes

        #region Creation Compte
        public bool IsClient(string pseudo)
        {
            MySqlDataReader reader = ReadCommand(String.Format("SELECT count(pseudo) FROM client WHERE pseudo = {0}", pseudo));
            reader.Read();
            int count = reader.GetInt32(0);
            reader.Close();
            return count > 0;
        }
        #endregion

        #region Gestion Compte
        public void AddSolde(string pseudo,float solde)
        {
            string s = solde.ToString().Replace(',', '.');
            ExecuteCommand(String.Format("UPDATE client SET solde  = solde + {0} WHERE pseudo = {1}", s, pseudo));
        }
        public List<string[]> GetCDRRecette(string pseudo)
        {
            MySqlDataReader reader = ReadCommand(String.Format("SELECT R.nom,count(CR.nom_recetteC) FROM recette R LEFT JOIN commandeRecette CR ON R.nom = CR.nom_recetteC WHERE R.pseudo = {0} GROUP BY R.nom;", pseudo));
            List<string[]> infos = new List<string[]>();
            while(reader.Read())
            {
                List<string> temp = new List<string>();
                for (int i = 0; i < reader.FieldCount; i++)
                    temp.Add(reader.GetString(i));
                infos.Add(temp.ToArray());
            }
            reader.Close();
            return infos;
        }
        public bool IsCDR(string pseudo)
        {
            MySqlDataReader reader = ReadCommand(String.Format("SELECT count(R.pseudo) FROM client C JOIN recette R WHERE R.pseudo = C.pseudo AND C.pseudo = {0};", pseudo));
            reader.Read();
            int count = reader.GetInt32(0);
            reader.Close();
            return count > 0;
        }
        #endregion

        #region Creation Recette
        public void ChangeStockProduit(string produit,float quantite)
        {
            string strQ = quantite.ToString().Replace(',', '.');
            ExecuteCommand(String.Format("UPDATE produit SET stock_min = stock_min/2 + 2 * {0}, stock_max =  stock_max + 3 * {0} WHERE nom = {1}", strQ,produit));
        }
        public float GetQuantiteCommande(string produit)
        {
            string strStockA = GetOneValue("produit", "stock_a", produit, "nom");
            string strStockMax = GetOneValue("produit", "stock_max",produit, "nom");
            float stockA = float.Parse(strStockA.Replace('.', ','));
            float stockMax = float.Parse(strStockMax.Replace('.', ','));
            return stockMax - stockA;
        }
        public bool IsRecette(string nom)
        {
            MySqlDataReader reader = ReadCommand(String.Format("SELECT count(nom) FROM recette WHERE nom = {0}", nom));
            reader.Read();
            int count = reader.GetInt16(0);
            reader.Close();
            return count != 0;
        }
        public bool IsValidRecette(string recette)
        {
            MySqlDataReader reader = ReadCommand(String.Format("SELECT count(nom_recette) FROM listeProduit WHERE nom_recette = {0}", recette));
            reader.Read();
            int count = reader.GetInt32(0);
            reader.Close();
            return count > 0;
        }
        public string[] GetAllValidProduit(string recette)
        {
            MySqlDataReader reader = ReadCommand(String.Format("SELECT P.nom FROM produit P WHERE P.nom NOT IN (SELECT LP.nom_produit FROM listeProduit LP WHERE lp.nom_recette ={0});", recette));
            List<string> produits = new List<string>();
            while (reader.Read())
                produits.Add(reader.GetString(0));
            reader.Close();
            return produits.ToArray();
        }
        #endregion

        #region Commande recette
        public bool HasEngoughCook(string pseudo,float cook)
        {
            MySqlDataReader reader = ReadCommand(String.Format("SELECT solde FROM client WHERE pseudo = {0}", pseudo));
            reader.Read();
            float solde = reader.GetFloat(0);
            reader.Close();
            return cook <= solde;
        }
        public void SubstractCook(string pseudo,float cook)
        {
            string strCook = cook.ToString().Replace(',', '.');
            ExecuteCommand(String.Format("UPDATE client SET solde = solde - {0} WHERE pseudo = {1}", strCook, pseudo));
        }
        public void PayCDR(string recette,int quantite)
        {
            ExecuteCommand(String.Format("UPDATE client C,recette R SET C.solde = C.solde + {0} * R.remuneration WHERE C.pseudo = R.pseudo AND R.nom = {1}", quantite, recette));
        }
        public void IncreaseRecettePrix(string recette,float prix)
        {
            string strPrix = prix.ToString().Replace(',', '.');
            ExecuteCommand(String.Format("UPDATE recette SET prix = prix + {0}", strPrix));
        }
        public void DecreaseRecetteStock(string recette,int quantite)
        {
            ExecuteCommand(String.Format("UPDATE produit P,listeProduit LP SET P.stock_a = P.stock_a -  {1} * (SELECT LP.quantite FROM listeProduit LP WHERE LP.nom_recette = {0} AND LP.nom_produit = P.nom) WHERE P.nom = LP.nom_produit AND LP.nom_recette = {0};", recette,quantite));
        }
        public bool HasRecette()
        {
            MySqlDataReader reader = ReadCommand("SELECT count(nom) FROM recette");
            reader.Read();
            int count = reader.GetInt32(0);
            reader.Close();
            return count > 0;
        }
        public int GetMaxCommande(string recette)
        {
            MySqlDataReader reader = ReadCommand(String.Format("SELECT FLOOR(MIN(P.stock_a/LP.quantite)) FROM produit P JOIN listeProduit LP ON P.nom = LP.nom_produit AND LP.nom_recette = {0}",recette));
            reader.Read();
            int max = reader.GetInt32(0);
            reader.Close();
            return max;
        }
        #endregion

        #endregion
    }
}
