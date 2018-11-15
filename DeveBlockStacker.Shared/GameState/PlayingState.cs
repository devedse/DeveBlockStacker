﻿using DeveBlockStacker.Shared.Data;
using DeveBlockStacker.Shared.State;
using Microsoft.Xna.Framework.Input;

namespace DeveBlockStacker.Shared.GameState
{
    public class PlayingState : IGameState
    {
        private int curPos;
        private int curDir;
        private readonly int width;
        private readonly int framesPerStep;

        private int frameTimer;

        public PlayingState(GameData gameData)
        {
            if (gameData.CurrentRow >= 0)
            {
                curPos = 6;
                curDir = -1;
                width = 4;
                framesPerStep = 5;
            }
        }

        public IGameState Update(InputStatifier inputStatifier, GameData gameData)
        {
            frameTimer++;

            if (frameTimer >= framesPerStep)
            {
                if ((curPos <= 0 && curDir == -1) || (curPos + width >= gameData.GridWidth && curDir == 1))
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

            if (inputStatifier.KeyPressed(Keys.Space))
            {
                gameData.CurrentRow++;
                return new GravityState(gameData);
            }

            return this;
        }
    }
}
