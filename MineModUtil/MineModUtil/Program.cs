using MineModUtil.UtilAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineModUtil
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Setting up workspace...");

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Output.Success("Workspace ready!");
            } catch (Exception error) { Output.FatalError(error, "set up workspace"); }

            Console.WriteLine("Starting user interface...");

            try
            {
                Application.Run(new Form());
            } catch (Exception error) { Output.FatalError(error, "start user interface"); }
        }
    }
}
