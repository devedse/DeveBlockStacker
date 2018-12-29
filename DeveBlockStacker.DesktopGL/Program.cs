using DeveBlockStacker.Shared;
using System;

namespace DeveBlockStacker.DesktopGL
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            using (var game = new TheGame(720 / 2, 1280 / 2))
            {
                game.Run();
            }
        }
    }
}
