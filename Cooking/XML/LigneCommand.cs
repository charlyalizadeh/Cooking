using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooking.XML
{
    public class LigneCommande
    {
        #region Champs
        private string produit;
        private float quantite;
        private string reference;
        #endregion

        #region Propriétés
        public string Produit 
        { 
            get { return produit; } 
            set { produit = value; } 
        }
        public float Quantite 
        { 
            get { return quantite; } 
            set { quantite = value; } 
        }
        public string Reference 
        {
            get { return reference; } 
            set { reference = value; } 
        }
        #endregion

        #region Constructeur
        public LigneCommande(string produit,float quantite,string ref_fournisseur)
        {
            this.produit = produit;
            this.quantite = quantite;
            reference = ref_fournisseur;
        }
        public LigneCommande() : this("",0,"") { }
        #endregion



    }
}
