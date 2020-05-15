using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cooking.App;
using Cooking.UI;
using Cooking.Format;

namespace Cooking
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            MainApp app = new MainApp();
            app.Start();
        }
    }
}
