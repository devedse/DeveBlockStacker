using DeveBlockStacker.Core;
using DeveBlockStacker.Core.HelperObjects;
using System;

namespace DeveBlockStacker.WindowsDX
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            using (var game = new TheGame(new(720 / 2, 1280 / 2), Platform.Desktop))
            {
                game.Run();
            }
        }
    }
}
