using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cooking.UI;

namespace Cooking.App
{
    class MainApp
    {
        #region Constructeur
        public MainApp() 
        {
            Console.SetBufferSize(150, 200);
            Console.SetWindowSize(150, 40);
            Admin admin = new Admin("commandes.xml");
        }
        #endregion

        #region Méthodes
        public bool MainMenu()
        {
            Menu menu = new Menu(new string[] { "MENU PRINCIPAL", "Client", "Administrateur","Demo","Quitter" });
            int choice = menu.Start();
            switch(choice)
            {
                case 1:
                    ClientUI client = new ClientUI();
                    client.Start();
                    break;
                case 2:
                    AdminUI admin = new AdminUI();
                    admin.Start();
                    break;
                case 3:
                    DemoUI demo = new DemoUI();
                    demo.Start();
                    break;
            }
            return choice != 4;
        }
        public void Start()
        {
            while (MainMenu()) ;
        }
        #endregion
    }
}
