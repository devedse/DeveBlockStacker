using DeveBlockStacker.Core.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeveBlockStacker.Core.Drawers
{
    public static class NormalGridDrawer
    {
        public static void DrawGrid(SpriteBatch spriteBatch, ContentDistributionThing contentDistributionThing, GameData gameData)
        {
            var gameSize = contentDistributionThing.ScreenSizeGame;

            for (int y = gameData.GridHeight - 1; y >= 0; y--)
            {
                for (int x = gameData.GridWidth - 1; x >= 0; x--)
                {
                    float widthOfBlockje = gameSize.Width / gameData.GridWidth;
                    float heightOfBlockje = gameSize.Height / gameData.GridHeight;

                    float yyy = gameSize.Height - ((1 + y) * heightOfBlockje);
                    var blocketje = new Blocketje(contentDistributionThing.SquareImage, x * widthOfBlockje, yyy, widthOfBlockje, heightOfBlockje, 3);

                    if (gameData.Gridje[x, y] == true)
                    {
                        if (y > 7)
                        {
                            blocketje.Draw(spriteBatch, Color.Red);
                        }
                        else
                        {
                            blocketje.Draw(spriteBatch, Color.Blue);
                        }
                    }
                }
            }
        }
    }
}
