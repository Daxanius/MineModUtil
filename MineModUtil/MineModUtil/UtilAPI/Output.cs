using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineModUtil.UtilAPI
{
    public class Output
    {
        public static void FatalError(Exception error, string task = "")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Fatal error, failed to " + task + ": " + error);
            Console.ForegroundColor = ConsoleColor.White;

            DialogResult res = MessageBox.Show("Something went wrong: " + error + " Please contact Daxanius.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (res == DialogResult.OK)
            {
                return;
            }
        }

        public static void Notify(string message, string title = "Info")
        {
            DialogResult res = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (res == DialogResult.OK)
            {
                return;
            }
        }

        public static void Error(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Warning(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Success(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
