using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace DeveBlockStacker.Shared.Data
{
    public class GameData
    {
        public int CurrentRow { get; set; }
        public bool[,] Gridje { get; }
        public int GridHeight => 11;
        public int GridWidth => 7;

        public Stopwatch Stopwatch { get; }

        public GameData()
        {
            Gridje = new bool[GridWidth, GridHeight];
            Stopwatch = Stopwatch.StartNew();
        }
    }
}
