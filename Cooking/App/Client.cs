using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Cooking.SQL;
using Cooking.XML;
using Cooking.UI;

namespace Cooking.App
{
    class Client
    {
        #region Champs
        ClientSQL m_clientSQL;
        #endregion

        #region Constructeur
        public Client()
        {
            m_clientSQL = new ClientSQL();
        }
        #endregion

        #region Méthodes

        #region Compte
        public bool IsClient(string pseudo)
        {
            return m_clientSQL.IsClient("'" + pseudo + "'");
        }
        public bool IsValidMdp(string pseudo,string mdp)
        {
            string validMdp = m_clientSQL.GetOneValue("client", "mdp", "'" + pseudo + "'", "pseudo");
            return mdp == validMdp;
        }
        public void CreateAccount(string pseudo,string mdp,string nom,string prenom,string num_tel,float solde)
        {
            string s = solde.ToString().Replace(',', '.');
            m_clientSQL.AddRow("client",new string[] { "'"+pseudo+"'", "'"+mdp+"'", "'"+nom+"'", "'"+prenom+"'", "'"+num_tel+"'", s });
        }
        #endregion

        #region Gestion Compte
        public string[] GetClientInfos(string pseudo)
        {
            List<string> infos = new List<string>();
            infos.AddRange(m_clientSQL.GetOneValues("client", new string[] { "pseudo", "nom", "prenom", "num_tel", "solde" }, "'" + pseudo + "'", "pseudo"));
            infos[0] = "Pseudo : " + infos[0];
            infos[1] = "Nom : " + infos[1];
            infos[2] = "Prénom : " + infos[2];
            infos[3] = "Numéro de téléphone : " + infos[3];
            infos[4] = "Solde : " + infos[4];
            if (m_clientSQL.IsCDR("'" + pseudo + "'"))
                infos.Add("Afficher mes recettes");
            return infos.ToArray();
        }
        public void UpdateInfos(string pseudo,string column,string newVal,bool isStr)
        {
            if (isStr)
                newVal = "'" + newVal + "'";
            else
                newVal = newVal.Replace(',', '.');
            m_clientSQL.UpdateValue("client", column, newVal, "'"+pseudo+"'", "pseudo");
        }
        public void AddSolde(string pseudo,float solde)
        {

            m_clientSQL.AddSolde("'" + pseudo + "'", solde);
        }
        public string GetCDRRecette(string pseudo)
        {
            List<string[]> recette = m_clientSQL.GetCDRRecette("'" + pseudo + "'");
            Grid grid = new Grid(recette, new string[] { "Nom", "Quantité vendue" });
            return grid.ToString();
        }
        #endregion

        #region Creation recette
        public string[] GetAllValidProduitNom(string recette)
        {
            return m_clientSQL.GetAllValidProduit("'" + recette + "'");
        }
        public void AddRecette(string nom,string type,string descriptif,float prix,float remuneration,string pseudo)
        {
            string strPrix = prix.ToString().Replace(',', '.');
            string strRem = remuneration.ToString().Replace(',', '.');
            m_clientSQL.AddRow("recette", new string[] { "'" + nom + "'", "'" + type + "'", "'" + descriptif + "'", strPrix, strRem, "'" + pseudo + "'" });
        }
        public void AddListeProduit(string nom_produit,string nom_recette,float quantite)
        {
            string strQ = quantite.ToString().Replace(',', '.');
            m_clientSQL.AddRow("listeProduit", new string[] { "'"+nom_produit + "'", "'"+nom_recette+"'", strQ });
        }
        public void ChangeCommande(string nom_produit,float quantite)
        {
            ListeCommandes listeCommandes = XMLSerialisation.DeserialiserCommandes("commandes.xml");
            listeCommandes.UpdateProduit(nom_produit, quantite);
            XMLSerialisation.SerialiserCommandes("commandes.xml",listeCommandes);
        }
        public float GetProduitCommande(string nom)
        {
            return m_clientSQL.GetQuantiteCommande("'" + nom + "'");
        }
        public void ChangeStockProduit(string produit, float quantite)
        {
            m_clientSQL.ChangeStockProduit("'" + produit + "'", quantite);
        }
        public bool IsRecette(string nom)
        {
            return m_clientSQL.IsRecette("'"+nom +"'");
        }
        public bool IsCDR(string nom)
        {
            string count = m_clientSQL.GetOneValue("recette", "count(pseudo)", "'" + nom + "'", "pseudo");
            return count != "0";
        }
        public bool IsValidRecette(string recette)
        {
            return m_clientSQL.IsValidRecette("'" + recette + "'");
        }
        #endregion

        #region Commandes recette
        public string[] GetAllRecetteNom()
        {
            return m_clientSQL.GetAllValues("recette", "nom");
        }
        public void AddCommandeRecette(string recette,string client,int quantite)
        {
            m_clientSQL.AddRow("commandeRecette", new string[] { "NOW()", "'" + recette + "'", "'" + client + "'", quantite.ToString() });
        }
        public bool HasEnoughCook(string pseudo,float cook)
        {
            return m_clientSQL.HasEngoughCook("'" + pseudo + "'", cook);
        }
        public int GetMaxCommand(string recette)
        {
            return m_clientSQL.GetMaxCommande("'" + recette + "'");
        }
        public void SubstractCook(string pseudo, float cook)
        {
            m_clientSQL.SubstractCook("'" + pseudo + "'", cook);
        }
        public float GetPrix(string nom)
        {
            string prixStr = m_clientSQL.GetOneValue("recette", "prix","'" + nom + "'", "nom");
            prixStr = prixStr.Replace('.', ',');
            float prix = float.Parse(prixStr);
            return prix;
        }
        public void PayCDR(string recette,int quantite)
        {
            m_clientSQL.PayCDR("'" + recette + "'", quantite);
        }
        public int NombreCommandes(string recette)
        {
            string nbCommande = m_clientSQL.GetOneValue("commandeRecette", "count(nom_recetteC)", "'" + recette + "'", "nom_recetteC");
            return int.Parse(nbCommande);
        }
        public void IncreaseRecettePrix(string recette,float prix)
        {
            m_clientSQL.IncreaseRecettePrix("'" + recette + "'", prix);
        }
        public void SetRecetteRemnuneration(string recette, float remuneration)
        {
            string strRemuneration = remuneration.ToString().Replace(',', '.');
            m_clientSQL.UpdateValue("recette", "remuneration", strRemuneration, "'" + recette + "'", "nom");
        }
        public bool HasRecette()
        {
            return m_clientSQL.HasRecette();
        }
        public void DecreaseRecetteStock(string recette, int quantite)
        {
            m_clientSQL.DecreaseRecetteStock("'" + recette + "'", quantite);
        }
        #endregion


        #endregion
    }
}
