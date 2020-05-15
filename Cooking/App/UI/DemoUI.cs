using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cooking.UI;
using Cooking.Format;

namespace Cooking.App
{
    class DemoUI
    {
        #region 
        Demo m_demo;
        #endregion

        #region Constructeur
        public DemoUI()
        {
            m_demo = new Demo();
        }
        #endregion

        #region Méthodes
        public void Start()
        {
            while (MainMenu()) ;
        }
        public bool MainMenu()
        {
            List<string> menuChoice = new List<string>();
            menuChoice.Add(m_demo.ToString());
            menuChoice.Add("Chercher toutes les recettes d'un produit");
            menuChoice.Add("Quitter");
            Menu menu = new Menu(menuChoice.ToArray());
            int choice = menu.Start();
            if (choice == menuChoice.Count - 2)
                SearchProduit();
            return choice != 2;
        }
        public void SearchProduit()
        {
            LineInput input = new LineInput("Entrez le nom du produit", DataFormat.IsNotVoid);
            string produit = input.Start();
            if (m_demo.IsProduit(produit))
                DisplayAllRecette(produit);
            else
                MessageBox.DisplayMessage("Ce produit n'existe pas", true);
        }
        public void DisplayAllRecette(string produit)
        {
            List<string> menuChoice = new List<string>();
            menuChoice.Add(m_demo.GetAllRecette(produit));
            menuChoice.Add("Retour");
            Menu menu = new Menu(menuChoice.ToArray());
            menu.Start();
        }
        #endregion


    }
}
