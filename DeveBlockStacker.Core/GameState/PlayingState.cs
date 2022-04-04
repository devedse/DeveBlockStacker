using DeveBlockStacker.Core.Data;
using DeveBlockStacker.Core.Drawers;
using DeveBlockStacker.Core.State;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DeveBlockStacker.Core.GameState
{
    public class PlayingState : IGameState
    {
        private readonly GameData gameData;

        private int curPos;
        private int curDir;
        private readonly int width;
        private readonly int framesPerStep;
        private int frameTimer;

        public PlayingState(GameData gameData, int maxWidth = int.MaxValue)
        {
            this.gameData = gameData;

            var random = new Random();

            if (gameData.CurrentRow < 3)
            {
                width = 4;
            }
            else if (gameData.CurrentRow < 6)
            {
                width = 3;
            }
            else if (gameData.CurrentRow < 9)
            {
                width = 2;
            }
            else
            {
                width = 1;
            }

            width = Math.Min(maxWidth, width);
            curPos = random.Next(-width + 1, gameData.GridWidth - 1);
            framesPerStep = 5;
            curDir = random.Next(0, 2) == 0 ? -1 : 1;
        }

        public IGameState Update(InputStatifier inputStatifier)
        {
            frameTimer++;

            if (frameTimer >= framesPerStep)
            {
                if ((curPos <= -width + 1 && curDir == -1) || (curPos + 1 >= gameData.GridWidth && curDir == 1))
                {
                    curDir *= -1;
                }
                curPos += curDir;

                frameTimer = 0;
            }

            for (int x = 0; x < gameData.GridWidth; x++)
            {
                gameData.Gridje[x, gameData.CurrentRow] = x >= curPos && x < (curPos + width);
            }

            if (inputStatifier.UserDoesAction())
            {
                return new GravityState(gameData);
            }

            return this;
        }

        public void Draw(SpriteBatch spriteBatch, ContentDistributionThing contentDistributionThing)
        {
            NormalGridDrawer.DrawGrid(spriteBatch, contentDistributionThing, gameData);
        }
    }
}
