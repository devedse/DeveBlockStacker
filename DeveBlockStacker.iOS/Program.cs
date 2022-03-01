using System;
using Foundation;
using UIKit;
using DeveBlockStacker.Core;
using DeveBlockStacker.Core.HelperObjects;

namespace DeveBlockStacker.iOS
{
 [Register("AppDelegate")]
    class Program : UIApplicationDelegate
    {
        private static TheGame game;

        internal static void RunGame()
        {
            game = new TheGame(Platform.Android);
            game.Run();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            UIApplication.Main(args, null, "AppDelegate");
        }

        public override void FinishedLaunching(UIApplication app)
        {
            RunGame();
        }
    }
}
