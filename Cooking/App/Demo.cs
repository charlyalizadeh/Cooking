using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cooking.UI;
using Cooking.SQL; 

namespace Cooking.App
{
    class Demo
    {
        #region 
        DemoSQL m_demoSQL;
        #endregion

        #region Constructeur
        public Demo()
        {
            m_demoSQL = new DemoSQL();
        }
        #endregion

        #region Méthodes
        public int NumberClient()
        {
            return m_demoSQL.GetCount("client", "pseudo");
        }
        public string ClientResume()
        {
            List<string[]> infos = m_demoSQL.GetClientData();
            Grid grid = new Grid(infos, new string[] { "CDR", "Nombre de recette vendues" });
            return grid.ToString();
        }
        public int NumberRecette()
        {
            return m_demoSQL.GetCount("recette","nom");
        }
        public string ProduitLowerStock()
        {
            string[] produit = m_demoSQL.ProduitLowerStock();
            string[,] produitForGrid = new string[produit.Length, 1];
            for (int i = 0; i < produit.Length; i++)
                produitForGrid[i, 0] = produit[i];
            Grid grid = new Grid(produitForGrid, new string[] { "Produit dont les stock sont insuffisants" });
            return grid.ToString();
        }
        public string GetAllRecette(string produit)
        {
            List<string[]> recette = m_demoSQL.GetAllRecette("'" + produit + "'");
            Grid grid = new Grid(recette, new string[] { "Recette", "Quantité" });
            return grid.ToString();
        }
        public override string ToString()
        {
            string display = "";
            display += Box.GetStringBox("Nombre de client : " + NumberClient()) + '\n';
            display += ClientResume() + '\n';
            display += Box.GetStringBox("Nombre de recette : " + NumberRecette()) + '\n';
            display += ProduitLowerStock() + '\n';
            return display;
        }
        public bool IsProduit(string produit)
        {
            return m_demoSQL.IsProduit("'" + produit + "'");
        }
        #endregion
    }
}
