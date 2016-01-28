using Poker.Core;

namespace Poker
{
    using System;
    using System.Windows.Forms;

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Engine engine = new Engine();
            engine.InitializeComponents();
            engine.Run();
           Application.Run(engine.MainWindow);
        }
    }
}
