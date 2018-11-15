﻿using DeveBlockStacker.Shared.Data;
using DeveBlockStacker.Shared.Drawwers;
using DeveBlockStacker.Shared.State;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DeveBlockStacker.Shared.GameState
{
    public class PlayingState : IGameState
    {
        private readonly GameData gameData;

        private int curPos;
        private int curDir;
        private readonly int width;
        private readonly int framesPerStep;
        private int frameTimer;

        public PlayingState(GameData gameData)
        {
            this.gameData = gameData;

            if (gameData.CurrentRow >= 0)
            {
                curPos = 6;
                curDir = -1;
                width = 4;
                framesPerStep = 5;
            }
        }

        public IGameState Update(InputStatifier inputStatifier)
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

            if (inputStatifier.KeyPressed(Keys.Space) || inputStatifier.IsTouchTapped())
            {
                return new GravityState(gameData);
            }

            return this;
        }

        public void Draw(SpriteBatch spriteBatch, ContentDistributionThing contentDistributionThing)
        {
            NormalGridDrawwer.DrawGrid(spriteBatch, contentDistributionThing, gameData);
        }
    }
}
