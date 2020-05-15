using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Cooking.Format
{
    static class DataFormat
    {
        public delegate bool IsValidDelegate(string str);
        public static bool IsValidFloatCS(string f)
        {
            Regex rx = new Regex(@"^[0-9]*(\,[0-9]{1,2})?$");
            bool isValid = rx.IsMatch(f) && IsNotVoid(f);
            return isValid;
        }
        public static bool IsNotVoid(string text)
        {
            return text != "";
        }
        public static bool IsIntegerNotNull(string text)
        {
            int test = 0;
            bool isValid = int.TryParse(text, out test) && test != 0;
            return isValid;
        }

        //Use in Table.Update()

    }
}
