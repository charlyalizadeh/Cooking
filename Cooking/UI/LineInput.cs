using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cooking.Format;

namespace Cooking.UI
{
    class LineInput
    {
        #region Champs
        string m_text;
        DataFormat.IsValidDelegate m_validMethod;
        #endregion

        #region Constructeur
        public LineInput(string text, DataFormat.IsValidDelegate validMethod)
        {
            m_text = text;
            m_validMethod = validMethod;
        }
        #endregion

        #region Méthode
        public void DisplayText(string str)
        {
            Console.WriteLine('┌' + new String('─', m_text.Length) + '┐');
            Console.WriteLine('│' + m_text + '│' + " : " + str);
            Console.WriteLine('└' + new String('─', m_text.Length) + '┘');

        }
        public string Start(int max = 35,bool hide = false)
        {
            string str = "";
            Console.CursorVisible = false;
            do
            {
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                if (!hide)
                    DisplayText(str);
                else
                {
                    DisplayText(new String('*', str.Length));
                }
                Console.SetCursorPosition(m_text.Length + 5 + str.Length,1);
                var key = Console.ReadKey(hide);
                if ((Char.IsLetterOrDigit(key.KeyChar) || key.KeyChar == ' ' || Char.IsPunctuation(key.KeyChar)) && str.Length<max)
                {
                    if (key.KeyChar != '\'')
                        str += key.KeyChar;
                }
                if (str != "" && key.Key == ConsoleKey.Backspace)
                    str = str.Substring(0, str.Length - 1);
                if (key.Key == ConsoleKey.Enter && m_validMethod(str))
                    break;
            } while (true);
            return str;
        }
        #endregion
    }
}
