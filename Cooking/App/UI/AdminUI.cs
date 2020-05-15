using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cooking.UI;
using Cooking.Format;

namespace Cooking.App
{
    public class AdminUI
    {
        #region Champs
        Admin m_admin;
        #endregion

        #region Constructeur
        public AdminUI()
        {
            m_admin = new Admin("commandes.xml");
        }
        #endregion

        #region Méthodes

        #region MainMenu
        public bool MainMenu()
        {
            string[] menuChoice = new string[] { "MENU ADMINISTRATEUR", "Tableau de bord", "Gestion commandes", "Gestion des recettes", "Gestion des cuisiniers", "Gestion des fournisseurs", "Gestion des produit", "Quitter" };
            Menu menu = new Menu(menuChoice);
            int choice = menu.Start() - 1;
            switch(choice)
            {
                case 0:
                    DisplayTableau();
                    break;
                case 1:
                    DisplayCommandes();
                    break;
                case 2:
                    while (MenuRecette()) ;
                    break;
                case 3:
                    while (MenuCDR()) ;
                    break;
                case 4:
                    while (MenuFournisseur()) ;
                    break;
                case 5:
                    while (MenuProduit()) ;
                    break;
            }
            return choice != 6;
        }
        public void Start()
        {
            while (MainMenu()) ;
        }
        #endregion

        #region Menu produit
        public bool MenuProduit()
        {
            string[] menuChoice = new string[] { "MENU PRODUIT", "Ajouter un produit", "Liste des produits","Quitter" };
            Menu menu = new Menu(menuChoice);
            int choice = menu.Start() - 1;
            switch(choice)
            {
                case 0:
                    if (m_admin.HasFournisseur())
                        AjouterProduit();
                    break;
                case 1:
                    MenuListeProduit();
                    break;
            }
            return choice != 2;
        }
        public void AjouterProduit()
        {
            LineInput input = new LineInput("Nom produit",DataFormat.IsNotVoid);
            string nom = input.Start();
            if (m_admin.IsProduit(nom))
                MessageBox.DisplayMessage("Ce produit existe déjà", true);
            else
            {
                string categorie = MenuCategorie();
                input = new LineInput("Unité", DataFormat.IsNotVoid);
                string unite = input.Start();
                string fournisseur = MenuRefFournisseur();
                m_admin.AddProduit(nom, categorie, unite, 0, 0, 0, fournisseur);
            }
        }
        public string MenuCategorie()
        {
            string[] menuChoices = new string[] { "Catégorie", "Boisson", "Sucre", "Produit laitier", "Légume/Fruit", "Viande/Poisson/Oeuf", "Céréale/Féculent", "Corps gras" };
            Menu menu = new Menu(menuChoices);
            return menuChoices[menu.Start()];
        }
        public string MenuRefFournisseur()
        {

            List<string> menuChoices = new List<string>();
            menuChoices.Add("Fournisseur");
            List<string[]> fournisseur = m_admin.GetAllFournisseur();
            foreach(var f in fournisseur)
                menuChoices.Add(String.Join(" ", f));
            Menu menu = new Menu(menuChoices.ToArray());
            return fournisseur[menu.Start() - 1][0];
        }
        public void MenuListeProduit()
        {
            List<string> menuChoice = new List<string>();
            menuChoice.AddRange(m_admin.GetProductGrid());
            menuChoice.Add("Retour");
            Menu menu = new Menu(menuChoice.ToArray(), menuChoice.Count - 1);
            menu.Start(false, false);
        }
        #endregion

        #region Menu recette
        public bool MenuRecette()
        {
            string[] recettes = m_admin.GetAllRecetteNom();
            List<string> menuChoice = new List<string>();
            menuChoice.AddRange(m_admin.GetRecetteMenu());
            menuChoice.Add("Quitter");
            Menu menu = new Menu(menuChoice.ToArray(),3,menuChoice.Count,new int[] { menuChoice.Count - 2 });
            int choice = menu.Start(false,false);
            if (choice < menuChoice.Count - 2)
            {
                string recetteNom = "";
                foreach(string r in recettes)
                {
                    if(menuChoice[choice].Contains(r))
                    {
                        recetteNom = r;
                        break;
                    }
                }
                while (MenuOneRecette(recetteNom)) ;
            }
            return choice != menuChoice.Count - 1;
        }
        public bool MenuOneRecette(string nom)
        {
            List<string> menuChoice = new List<string>();
            menuChoice.Add(m_admin.GetRecetteTuple(nom));
            menuChoice.AddRange(new string[] { "Supprimer", "Retour" });
            Menu menu = new Menu(menuChoice.ToArray());
            int choice = menu.Start() - 1;
            if (choice == 0)
                m_admin.DeleteRecette(nom);
            return false;
        }
        #endregion

        #region Menu CDR
        public bool MenuCDR()
        {
            string[] cdrNom = m_admin.GetCDRNom();
            List<string> menuChoice = new List<string>();
            menuChoice.AddRange(m_admin.GetCDRMenu ());
            menuChoice.Add("Quitter");
            Menu menu = new Menu(menuChoice.ToArray(),3,menuChoice.Count,new int[] { menuChoice.Count - 2});
            int choice = menu.Start(false,false) - 1;
            if (choice != menuChoice.Count - 2)
            {
                Menu menu2 = new Menu(new string[] { "Voulez vous supprimer ce CDR ?", "Oui", "Non" });
                int choice2 = menu2.Start() - 1;
                if (choice2 == 0) 
                {
                    string cdrPseudo = "";
                    foreach (string r in cdrNom)
                    {
                        if (menuChoice[choice+1].Contains(r))
                        {
                            cdrPseudo = r;
                            break;
                        }
                    }
                    DeleteCDRRecettes(cdrPseudo);
                }
            }
            return choice != menuChoice.Count - 2;
        }
        public bool MenuOneCDR(string pseudo)
        {
            List<string> menuChoice = new List<string>();
            menuChoice.Add(m_admin.GetCDRTuple(pseudo));
            menuChoice.Add("Supprimer les recettes");
            menuChoice.Add("Quitter");
            Menu menu = new Menu(menuChoice.ToArray());
            int choice = menu.Start(true) - 1;
            if(choice == 0)
            {
                DeleteCDRRecettes(pseudo);
            }
            return choice != 1;
        }
        public void DeleteCDRRecettes(string pseudo)
        {
            m_admin.DeleteCDRRecette(pseudo);
        }
        #endregion

        #region Tableau de bord
        public void DisplayTableau()
        {
            Menu menu = new Menu(new string[] { "CDR D'OR\n" + m_admin.GetOrCDR() ,"\n\nCDR DE LA SEMAINE\n" + m_admin.GetCDRWeek(), "\n\nTOP 5 RECETTES DE LA SEMAINE\n" + m_admin.GetTop5RecettesWeek(), "Quitter" },3);
            menu.Start(true);
        }
        #endregion

        #region Fournisseur
        public bool MenuFournisseur()
        {
            string[] menuChoice = new string[] { "MENU FOURNISSEUR", "Ajouter un fournisseur", "Liste des fournisseur", "Quitter" };
            Menu menu = new Menu(menuChoice);
            int choice = menu.Start() - 1;
            switch (choice)
            {
                case 0:
                    AjouterFournisseur();
                    break;
                case 1:
                    MenuListeFournisseur();
                    break;
            }
            return choice != 2;
        }
        public void AjouterFournisseur()
        {
            LineInput input = new LineInput("Nom fournisseur", DataFormat.IsNotVoid);
            m_admin.AddFournisseur(input.Start());
        }
        public void MenuListeFournisseur()
        {
            List<string> menuChoice = new List<string>();
            menuChoice.AddRange(m_admin.GetFrounisseurMenu());
            menuChoice.Add("Retour");
            Menu menu = new Menu(menuChoice.ToArray(), menuChoice.Count - 1);
            menu.Start(false,false);
        }
        public bool MenuOneFournisseur(string reference)
        {
            List<string> menuChoice = new List<string>();
            menuChoice.Add(String.Join("  ",m_admin.GetFournisseurtTuple(reference)));
            menuChoice.Add("Retour");
            Menu menu = new Menu(menuChoice.ToArray());
            menu.Start();
            return false;
        }
        #endregion

        #region Commandes
        public void DisplayCommandes()
        {
            Menu menu = new Menu(new string[] { m_admin.GetDescriptionCommandes(), "Retour" });
            menu.Start(false);
        }
        #endregion

        #endregion


    }
}
