using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cooking.UI;

namespace Cooking.XML
{
    public class ListeCommandes
    {
        #region Champs
        private List<LigneCommande> m_commandes;
        #endregion

        #region Propriétés
        public List<LigneCommande> Commandes { get => m_commandes;  set => m_commandes = value; } 
        #endregion

        #region Constructeurs
        public ListeCommandes(List<LigneCommande> commandes)
        {
            m_commandes = commandes;
        }
        public ListeCommandes() :this(new List<LigneCommande>()) { }
        #endregion

        #region Méthodes
        public override string ToString()
        {
            string[] columnNames = new string[] { "Produit", "Quantité", "Reference Fournisseur" };
            List<string[]> tuples = new List<string[]>();
            m_commandes.Sort(CompareFournisseur);
            m_commandes.Sort(CompareProduit);
            foreach(var line in m_commandes)
                tuples.Add(new string[] { line.Produit, line.Quantite.ToString(), line.Reference });
            Grid grid = new Grid(tuples, columnNames);
            return grid.ToString(); 
        }
        int CompareFournisseur(LigneCommande l1, LigneCommande l2)
        {
            return l1.Reference.CompareTo(l2.Reference);
        }
        int CompareProduit(LigneCommande l1, LigneCommande l2)
        {
            return l1.Produit.CompareTo(l2.Produit);
        }
        public void UpdateProduit(string produit,float quantite)
        {
            for(int i = 0;i<m_commandes.Count;i++)
            {
                if(m_commandes[i].Produit == produit)
                {
                    m_commandes[i].Quantite = quantite;
                    break;
                }
            }
        }
        public void Add(LigneCommande line)
        {
            m_commandes.Add(line);
        }
        public float GetQuantite(string produit)
        {
            float quantite = 0;
            for (int i = 0; i < m_commandes.Count; i++)
            {
                if (m_commandes[i].Produit == produit)
                {
                    quantite = m_commandes[i].Quantite;
                    break;
                }
            }
            return quantite;
        }
        #endregion
    }
}
