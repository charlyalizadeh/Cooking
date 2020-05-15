using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cooking.UI;
using Cooking.Format;


namespace Cooking.App
{
    class ClientUI 
    {
        #region 
        Client m_client;
        #endregion

        #region Constructeur
        public ClientUI()
        {
            m_client = new Client();
        }
        #endregion

        #region Méthdodes

        #region MainMenu
        public bool MainMenu()
        {
            string[] menuChoice = new string[] { "MENU CLIENT", "Connexion", "Inscription", "Quitter" };
            Menu menu = new Menu(menuChoice);
            int choice = menu.Start() - 1;
            switch(choice)
            {
                case 0:
                    string pseudo = Connexion();
                    if (pseudo != "")
                        while (SecondMenu(pseudo)) ;
                    else
                    {
                        Console.Clear();
                        MessageBox.DisplayMessage("Pseudo ou mot de passe invalide",true);
                    }
                    break;
                case 1:
                    CreationCompte();
                    break;
            }

            return choice != 2;
        }
        public void Start()
        {
            while (MainMenu()) ;
        }
        #endregion

        #region Creation Compte
        public void CreationCompte()
        {
            string pseudo = CreatePseudo();
            string mdp = CreateMdp();
            LineInput input = new LineInput("Nom", DataFormat.IsNotVoid);
            string nom = input.Start(25);
            input = new LineInput("Prénom", DataFormat.IsNotVoid);
            string prenom = input.Start(25);
            input = new LineInput("Numéro de téléphone", DataFormat.IsNotVoid);
            string num_tel = input.Start(10);
            m_client.CreateAccount(pseudo, mdp, nom, prenom, num_tel, 0);
        }
        public string CreatePseudo()
        {
            string pseudo = "";
            bool isAlreadyClient = false;
            do
            {
                LineInput input = new LineInput("Pseudo", DataFormat.IsNotVoid);
                pseudo = input.Start(15);
                isAlreadyClient = m_client.IsClient(pseudo);
                if (isAlreadyClient)
                {
                    Console.Clear();
                    MessageBox.DisplayMessage("Ce pseudo est déjà pris", true);
                }
            } while (isAlreadyClient);
            return pseudo;
        }
        public string CreateMdp()
        {
            string mdp = "";
            bool isValid = false;
            do
            {
                LineInput input = new LineInput("Mot de passe", DataFormat.IsNotVoid);
                mdp = input.Start(20, true);
                input = new LineInput("Confirmer mot de passe", DataFormat.IsNotVoid);
                string cMdp = input.Start(20, true);
                isValid = mdp == cMdp;
                if (!isValid)
                {
                    Console.Clear();
                    MessageBox.DisplayMessage("La confirmation n'est pas similaire", true);
                }
            } while (!isValid);
            return mdp;
        }
        #endregion

        #region Connexion Compte
        public string Connexion()
        {
            string pseudo = "";
            LineInput input = new LineInput("Pseudo", DataFormat.IsNotVoid);
            pseudo = input.Start(15);
            string mdp = "";
            input = new LineInput("Mot de passe", DataFormat.IsNotVoid);
            mdp = input.Start(20,true);
            if (!m_client.IsValidMdp(pseudo, mdp))
                pseudo = "";
            return pseudo;
        }
        #endregion

        #region SecondMenu
        public bool SecondMenu(string pseudo)
        {
            string[] menuChoices = new string[] { "MENU CLIENT", "Gestion du compte", "Commander un plat", "Créer une recette","Quitter" };
            Menu menu = new Menu(menuChoices);
            int choice = menu.Start() - 1;
            switch(choice)
            {
                case 0:
                    while (MenuInfos(pseudo)) ;
                    break;
                case 1:
                    if (m_client.HasRecette())
                        CommanderRecette(pseudo);
                    break;
                case 2:
                    CreationRecette(pseudo);
                    break;
            }
            return choice != 3;
        }
        #endregion

        #region Gestion Compte
        public bool MenuInfos(string pseudo)
        {
            List<string> menuChoice = new List<string>();
            menuChoice.Add("INFORMATIONS");
            menuChoice.AddRange(m_client.GetClientInfos(pseudo));
            menuChoice.Add("Quitter");
            Menu menu = new Menu(menuChoice.ToArray(), 2);
            int choice = menu.Start() ;
            switch(choice)
            {
                case 2:
                    string nom = ChangeValue("Nouveau nom", 25);
                    m_client.UpdateInfos(pseudo, "nom", nom,true);
                    break;
                case 3:
                    string prenom = ChangeValue("Nouveau prénom", 25);
                    m_client.UpdateInfos(pseudo, "prenom", prenom, true);
                    break;
                case 4:
                    string num_tel = ChangeValue("Nouveau numéro", 10);
                    m_client.UpdateInfos(pseudo, "num_tel", num_tel, true);
                    break;
                case 5:
                    float solde = ChangeSolde();
                    m_client.AddSolde(pseudo, solde);
                    break;
            }
            if (menuChoice.Count == 8 && choice == 6)
                RecetteMenu(pseudo);
            return choice != menuChoice.Count - 1;
        }
        public string ChangeValue(string text, int size)
        {
            LineInput input = new LineInput(text, DataFormat.IsNotVoid);
            return input.Start(size);
        }
        public float ChangeSolde()
        {
            LineInput input = new LineInput("Somme à ajouter", DataFormat.IsValidFloatCS);
            return float.Parse(input.Start());
        }
        public void RecetteMenu(string pseudo)
        {
            List<string> menuChoices = new List<string>();
            menuChoices.Add(m_client.GetCDRRecette(pseudo));
            menuChoices.Add("Retour");
            Menu menu = new Menu(menuChoices.ToArray()); ;
            menu.Start(false,false);
        }
        #endregion

        #region Creation Recette
        public void CreationRecette(string pseudo)
        {
            LineInput input = new LineInput("Nom recette", DataFormat.IsNotVoid);
            string nom = input.Start(20);
            if (m_client.IsRecette(nom))
            {
                MessageBox.DisplayMessage("Ce nom de recette existe déjà", true);
            }
            else
            {
                string type = MenuType();
                input = new LineInput("Description", DataFormat.IsNotVoid);
                string description = input.Start(50);
                m_client.AddRecette(nom, type, description, 10, 2, pseudo);
                while (CreationListeProduit(nom) || !m_client.IsValidRecette(nom));
            }
        }
        public string MenuType()
        {
            string[] menuChoice = new string[] { "TYPE", "Entrée", "Plat", "Dessert" };
            Menu menu = new Menu(menuChoice);
            return menuChoice[menu.Start()];
        }
        public bool CreationListeProduit(string recette)
        {
            List<string> menuChoices = new List<string>();
            menuChoices.Add("Produits");
            menuChoices.AddRange(m_client.GetAllValidProduitNom(recette));
            menuChoices.Add("Suivant");
            Menu menu = new Menu(menuChoices.ToArray());
            int choice = menu.Start();
            if (choice != menuChoices.Count - 1)
            {
                string nomProd = menuChoices[choice];
                LineInput input = new LineInput("Quantité", DataFormat.IsValidFloatCS);
                float quantite = float.Parse(input.Start());
                m_client.ChangeStockProduit(nomProd, quantite);
                m_client.ChangeCommande(nomProd, m_client.GetProduitCommande(nomProd));
                m_client.AddListeProduit(nomProd, recette, quantite);
            }
            return choice != menuChoices.Count - 1;
        }
        #endregion

        #region Commandes Recettes
        public void CommanderRecette(string pseudo)
        {
            string recette = MenuRecette();
            int maxCommande = m_client.GetMaxCommand(recette);
            LineInput input = new LineInput("Quantite", DataFormat.IsIntegerNotNull);
            int quantite = int.Parse(input.Start());
            if (maxCommande >= quantite)
            {
                float prixTot = quantite * m_client.GetPrix(recette);
                if (m_client.HasEnoughCook(pseudo, prixTot))
                {
                    ActionCommandeRecette(pseudo, recette, quantite, prixTot);
                }
                else
                {
                    Console.Clear();
                    MessageBox.DisplayMessage("Vous ne possédez pas assez de cook", true);
                }
            }
            else
            {
                Console.Clear();
                if (maxCommande != 0)
                    MessageBox.DisplayMessage(String.Format("Nous ne possédons pas assez de stock pour satisfaire votre commande.\nVous pouvez commander {0} {1} au maximum", maxCommande, recette), true);
                else
                    MessageBox.DisplayMessage("Cette recette est sold out.",true);
            }
        }
        public void ActionCommandeRecette(string pseudo,string recette,int quantite,float prixTot)
        {
            int oldNbCommande = m_client.NombreCommandes(recette);
            int newNbCommande = oldNbCommande + quantite;

            m_client.AddCommandeRecette(recette, pseudo, quantite);//Add the command into the database

            m_client.PayCDR(recette, quantite);//Pay the creator of the recette

            m_client.SubstractCook(pseudo, prixTot);//Withdraw the money from the client account

            m_client.DecreaseRecetteStock(recette, quantite);//Decrease the stock of all the product in the recipe's product list in function of the quantity

            //Check if the command has been ordered more than 10 or 50 times
            if (oldNbCommande < 10 && newNbCommande >= 10)
                m_client.IncreaseRecettePrix(recette, 2);
            if (oldNbCommande > 10 && oldNbCommande < 50 && newNbCommande >= 50)
            {
                m_client.IncreaseRecettePrix(recette, 5);
                m_client.SetRecetteRemnuneration(recette, 4);
            }
        }
        public string MenuRecette()
        {
            List<string> menuChoice = new List<string>();
            menuChoice.Add("MENU RECETTE");
            menuChoice.AddRange(m_client.GetAllRecetteNom());
            Menu menu = new Menu(menuChoice.ToArray());
            int choice = menu.Start();
            return menuChoice[choice];
        }
        #endregion

        #endregion

    }
}
