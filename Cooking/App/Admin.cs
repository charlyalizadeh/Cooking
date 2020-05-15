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
    class Admin
    {
        #region Champs
        AdminSQL m_adminSQL;
        string m_commandeFilename;
        ListeCommandes m_Listecommandes;
        #endregion

        #region Constructeur
        public Admin(string comandefilename)
        {
            m_adminSQL = new AdminSQL();
            m_commandeFilename = comandefilename;
            UpdateProduit();
            UpdateCommande();
            if(!File.Exists(comandefilename))
            {
                m_Listecommandes = new ListeCommandes();
                SaveCommandes();
            }
            m_Listecommandes = XMLSerialisation.DeserialiserCommandes(m_commandeFilename);
        }
        #endregion

        #region Méthodes

        #region Tableau de bord
        public string GetCDRWeek()
        {
            string[] cdr = m_adminSQL.GetBestCDRWeek();
            string str = "";
            if (cdr.Length != 0)
            {
                str = "Pseudo : " + cdr[0] + "\n";
                str += "Nombre de recettes vendues : " + cdr[1];
            }
            return str;
        }
        public string GetTop5RecettesWeek()
        {
            List<string[]> recettes = m_adminSQL.GetBestRecetteWeek();
            Grid grid = new Grid(new string[,] { }, new string[] { });
            if (recettes.Count!=0)
                grid = new Grid(recettes, new string[] { "Nom", "CDR", "Type", "Quantité vendue" });
            return grid.ToString();
        }
        public string GetOrCDR()
        {
            string[] infos = m_adminSQL.GetOrCDR();
            string str = "";
            if (infos.Length != 0)
            {
                List<string[]> recettes = m_adminSQL.GetBestRecettesCDR("'" + infos[0] + "'");
                Grid grid0 = new Grid(recettes, new string[] { "Nom", "Quantité vendue" });
                str = "Pseudo : " + infos[0] + '\n';
                str += "Nombre de recettes vendues : " + infos[1] + '\n';
                str += grid0.ToString();
            }
            return str;
        }
        #endregion

        #region Produit
        public void AddProduit(string nom, string categorie, string unite, float stock, float stockMin, float stockMax, string reference)
        {
            string s = stock.ToString().Replace(',', '.');
            string sMin = stockMin.ToString().Replace(',', '.');
            string sMax = stockMax.ToString().Replace(',', '.');
            m_adminSQL.AddRow("produit", new string[] { "'" + nom + "'", "'" + categorie + "'", "'" + unite + "'", s, sMin, sMax, "'" + reference + "'" });
            m_Listecommandes.Add(new LigneCommande(nom, 0, reference));
            SaveCommandes();
        }
        public string[] GetAllProduitNom()
        {
            return m_adminSQL.GetAllValues("produit", "nom");
        }
        public List<string[]> GetAllFournisseur()
        {
            return m_adminSQL.GetAllTuples("fournisseur");
        }
        public bool HasFournisseur()
        {
            return m_adminSQL.GetCount("fournisseur", "ref_fournisseur")>0;
        }
        public bool IsProduit(string nom)
        {
            return m_adminSQL.IsProduit("'" + nom + "'");
        }
        public void Restock(string produit)
        {
            float quantite = m_Listecommandes.GetQuantite(produit);
            m_adminSQL.Restock("'" + produit + "'", quantite);
        }
        public void UpdateProduit()
        {
            m_adminSQL.UpdateProduitNotUsed();
            if (DateTime.Today.DayOfWeek == DayOfWeek.Monday && DateTime.Today.Hour == 0 && DateTime.Today.Minute == 0 && DateTime.Today.Second == 0 && DateTime.Today.Millisecond == 0)
            {
                string[] produitNom = GetAllProduitNom();
                foreach (string p in produitNom)
                    Restock(p);
            }
        }
        public string[] GetProductGrid()
        {
            List<string[]> produit = m_adminSQL.GetAllTupleValues("produit", new string[] { "nom", "categorie", "unite", "stock_a", "ref_fournisseur" });
            Grid grid = new Grid(produit, new string[] { "Nom", "Categorie", "Unite", "Stock", "Reference Fournisseur" });
            return grid.ToStringMenu();
        }
        #endregion

        #region Fournisseur
        public void AddFournisseur(string nom)
        {
            string reference = m_adminSQL.GetNewReference();
            m_adminSQL.AddRow("fournisseur", new string[] { "'" + reference + "'", "'" + nom + "'" });
        }
        public string[] GetFournisseurtTuple(string reference)
        {
            return m_adminSQL.GetOneTuple("fournisseur", "'" + reference + "'", "ref_fournisseur");
        }
        public string[] GetFrounisseurMenu()
        {
            List<string[]> produit = m_adminSQL.GetAllTuples("fournisseur");
            Grid grid = new Grid(produit, new string[] { "Reference", "Nom" });
            return grid.ToStringMenu();
        }
        #endregion

        #region Recette
        public string[] GetAllRecetteNom()
        {
            return m_adminSQL.GetAllValues("recette", "nom");
        }
        public string GetRecetteTuple(string nom)
        {
            string[] infos = m_adminSQL.GetOneValues("recette", new string[] { "nom", "descriptif" },"'"+nom+"'","nom");
            string description = "Nom : " + infos[0] + '\n';
            description += "Description : " + infos[1] + '\n';
            return description;
        }
        public string[] GetRecetteMenu()
        {
            List<string[]> lines = m_adminSQL.GetAllTupleValues("recette", new string[] { "nom", "type", "prix", "pseudo" });
            Grid grid = new Grid(lines, new string[] { "Nom", "Type", "Prix", "Cuisisnier" });
            return grid.ToStringMenu();
        }
        public void DeleteRecette(string nom)
        {
            m_adminSQL.DeleteRow("recette", "nom", "'" + nom + "'");
        }
        #endregion

        #region Commandes
        public string GetDescriptionCommandes()
        {
            return m_Listecommandes.ToString();
        }
        public void SaveCommandes()
        {
            XMLSerialisation.SerialiserCommandes(m_commandeFilename, m_Listecommandes);
        }
        public void UpdateCommande()
        {
            File.Delete(m_commandeFilename);
            List<string[]> infos = m_adminSQL.GetProduitStock();
            m_Listecommandes = new ListeCommandes();
            foreach (var i in infos) 
            {
                string nom = i[0];
                float quantite = float.Parse(i[2].Replace('.', ',')) - float.Parse(i[1].Replace('.', ','));
                string reference = i[3];
                m_Listecommandes.Add(new LigneCommande(nom, quantite, reference));
            }
            XMLSerialisation.SerialiserCommandes(m_commandeFilename, m_Listecommandes);
        }
        #endregion

        #region CDR
        public string[] GetCDRNom()
        {
            return m_adminSQL.GetCDRNom();
        }
        public string GetCDRTuple(string pseudo)
        {
            string[] infos = m_adminSQL.GetCDRTuple(pseudo);
            string description = "Pseudo  : " + infos[0] + '\n';
            description += "Nom : " + infos[1] + '\n';
            description += "Prénom : " + infos[2] + '\n';
            description += "Numéro de téléphone : " + infos[3] + '\n';
            description += "Solde : " + infos[4] + '\n';
            return description;
        }
        public void DeleteCDRRecette(string pseudo)
        {
            m_adminSQL.DeleteCDRRecettes(pseudo);
        }
        public string[] GetCDRMenu()
        {
            List<string[]> lines = m_adminSQL.GetAllCDR();
            Grid grid = new Grid(lines, new string[] { "Pseudo", "Nom", "Prenom", "Nombre de recette" });
            return grid.ToStringMenu();
        }
        #endregion

        #endregion
    }
}
