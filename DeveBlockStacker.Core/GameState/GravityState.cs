using DeveBlockStacker.Core.State;
using DeveBlockStacker.Core.Data;
using DeveBlockStacker.Core.Drawwers;
using Microsoft.Xna.Framework.Graphics;

namespace DeveBlockStacker.Core.GameState
{
    public class GravityState : IGameState
    {
        private readonly int framesPerStep;
        private readonly GameData gameData;
        private int frameTimer;

        public GravityState(GameData gameData)
        {
            this.gameData = gameData;

            framesPerStep = 5;
        }

        public IGameState Update(InputStatifier inputStatifier)
        {
            frameTimer++;

            if (frameTimer >= framesPerStep)
            {
                bool didSomething = false;

                for (int y = 1; y < gameData.GridHeight; y++)
                {
                    for (int x = 0; x < gameData.GridWidth; x++)
                    {
                        if (gameData.Gridje[x, y] && !gameData.Gridje[x, y - 1])
                        {
                            gameData.Gridje[x, y - 1] = true;
                            gameData.Gridje[x, y] = false;
                            didSomething = true;
                        }
                    }
                }

                if (!didSomething)
                {
                    var blocksOnLastRow = BlocksOnLastRow();
                    if (blocksOnLastRow == 0)
                    {
                        //You loose
                        return new GameOver(gameData);
                    }
                    else if (gameData.CurrentRow == gameData.GridHeight - 1)
                    {
                        //You win
                        return new YouWin(gameData);
                    }
                    else
                    {
                        gameData.CurrentRow++;
                        return new DelayState(gameData, new PlayingState(gameData, blocksOnLastRow), 30);
                    }
                }

                frameTimer = 0;
            }

            return this;
        }

        public int BlocksOnLastRow()
        {
            int count = 0;
            for (int x = 0; x < gameData.GridWidth; x++)
            {
                if (gameData.Gridje[x, gameData.CurrentRow])
                {
                    count++;
                }
            }
            return count;
        }

        public void Draw(SpriteBatch spriteBatch, ContentDistributionThing contentDistributionThing)
        {
            NormalGridDrawwer.DrawGrid(spriteBatch, contentDistributionThing, gameData);
        }
    }
}
