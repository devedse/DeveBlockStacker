using System;
using System.Collections.Generic;
using System.Text;
using DeveBlockStacker.Shared.Data;
using DeveBlockStacker.Shared.Drawwers;
using DeveBlockStacker.Shared.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeveBlockStacker.Shared.GameState
{
    public class GameOver : IGameState
    {
        private readonly GameData gameData;
        private int framesDelay;

        public GameOver(GameData gameData)
        {
            this.gameData = gameData;

            framesDelay = 500;
        }

        public IGameState Update(InputStatifier inputStatifier)
        {
            framesDelay--;
            if (framesDelay <= 0)
            {
                return new NewGameState();
            }
            return this;
        }

        public void Draw(SpriteBatch spriteBatch, ContentDistributionThing contentDistributionThing)
        {
            NormalGridDrawwer.DrawGrid(spriteBatch, contentDistributionThing, gameData);
            spriteBatch.DrawString(contentDistributionThing.SegoeUI20, "You fucking suck!", new Vector2(30, 30), Color.White);
        }
    }
}
