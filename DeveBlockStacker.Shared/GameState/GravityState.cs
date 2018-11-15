using DeveBlockStacker.Shared.Data;
using DeveBlockStacker.Shared.State;

namespace DeveBlockStacker.Shared.GameState
{
    public class GravityState : IGameState
    {
        private readonly int framesPerStep;

        private int frameTimer;

        public GravityState(GameData gameData)
        {
            framesPerStep = 5;
        }

        public IGameState Update(InputStatifier inputStatifier, GameData gameData)
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
                    return new DelayState(new PlayingState(gameData), 200);
                }

                frameTimer = 0;
            }

            return this;
        }
    }
}
