using DeveBlockStacker.Shared.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeveBlockStacker.Shared.Drawwers
{
    public static class NormalGridDrawwer
    {
        public static void DrawGrid(SpriteBatch spriteBatch, ContentDistributionThing contentDistributionThing, GameData gameData)
        {
            for (int y = gameData.GridHeight - 1; y >= 0; y--)
            {
                for (int x = gameData.GridWidth - 1; x >= 0; x--)
                {
                    float widthOfBlockje = contentDistributionThing.ScreenWidth / gameData.GridWidth;
                    float heightOfBlockje = contentDistributionThing.ScreenHeight / gameData.GridHeight;

                    float yyy = contentDistributionThing.ScreenHeight - ((1 + y) * heightOfBlockje);
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
