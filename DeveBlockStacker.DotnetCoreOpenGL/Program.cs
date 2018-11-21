using DeveBlockStacker.Shared;
using System;

namespace DeveBlockStacker.DotnetCoreOpenGL
{
    public static class Program
    {
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
