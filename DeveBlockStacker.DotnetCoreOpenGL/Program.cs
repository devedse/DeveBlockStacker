using DeveBlockStacker.Shared;
using System;

namespace DeveBlockStacker.DotnetCoreOpenGL
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            using (var game = new TheGame())
            {
                game.Run();
            }
        }
    }
}
