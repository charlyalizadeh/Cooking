using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooking.UI
{
    static class MessageBox
    {
        public static void DisplayMessage(string message,bool isError = false)
        {
            if (isError)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Red;
            }

            Box.DisplayBox(message);
            if (isError)
                Console.ResetColor();
            Console.ReadKey();
        }
    }
}
