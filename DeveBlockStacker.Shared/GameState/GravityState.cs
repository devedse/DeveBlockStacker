using DeveBlockStacker.Shared.Data;
using DeveBlockStacker.Shared.Drawwers;
using DeveBlockStacker.Shared.State;
using Microsoft.Xna.Framework.Graphics;

namespace DeveBlockStacker.Shared.GameState
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
                    if (gameData.CurrentRow == gameData.GridHeight - 1)
                    {
                        if (IsThereABlockOnLastRow())
                        {
                            //You win
                            return new YouWin(gameData);
                        }
                        else
                        {
                            //You loose
                            return new GameOver(gameData);
                        }
                    }
                    else
                    {
                        gameData.CurrentRow++;
                        return new DelayState(gameData, new PlayingState(gameData), 30);
                    }
                }

                frameTimer = 0;
            }

            return this;
        }

        public bool IsThereABlockOnLastRow()
        {
            for (int x = 0; x < gameData.GridWidth; x++)
            {
                if (gameData.Gridje[x, gameData.GridHeight - 1])
                {
                    return true;
                }
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch, ContentDistributionThing contentDistributionThing)
        {
            NormalGridDrawwer.DrawGrid(spriteBatch, contentDistributionThing, gameData);
        }
    }
}
