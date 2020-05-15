using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooking.UI
{
    static class Box
    {

        #region Méthodes
        static int GetWidthBox(string text)
        {
            int dim1 = 0;
            string[] temp = text.Split('\n');
            foreach (string s in temp)
                if (dim1 < s.Length) dim1 = s.Length;

            return dim1;
        }
        public static void DisplayBox(string text)
        {
            int dim = GetWidthBox(text);
            Console.WriteLine('┌' + new String('─', dim) + '┐');
            string[] temp = text.Split('\n');
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i] != "")
                    Console.Write('│' + temp[i] + new String(' ', dim - temp[i].Length) + "│\n");
            }
            Console.WriteLine('└' + new String('─', dim) + '┘');
        }
        public static string GetStringBox(string text)
        {
            int dim = GetWidthBox(text);
            string boxStr = "";
            boxStr += '┌' + new String('─', dim) + '┐' + '\n';
            string[] temp = text.Split('\n');
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i] != "")
                    boxStr+='│' + temp[i] + new String(' ', dim - temp[i].Length) + "│\n";
            }
            boxStr += '└' + new String('─', dim) + '┘' + '\n';
            return boxStr;
        }
        #endregion
    }
}
